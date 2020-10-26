

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLNS.Exporting;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Organizations;
using Abp.UI;
using Dapper;
using Hinnova.Authorization.Users;
using Hinnova.Authorization.Users.Dto;
using Hinnova.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Hinnova.QLNS
{
//	[AbpAuthorize(AppPermissions.Pages_QuyTrinhCongTacs)]
    public class QuyTrinhCongTacsAppService : HinnovaAppServiceBase, IQuyTrinhCongTacsAppService
    {
        private readonly IRepository<QuyTrinhCongTac> _quyTrinhCongTacRepository;
        private readonly IQuyTrinhCongTacsExcelExporter _quyTrinhCongTacsExcelExporter;
        private readonly IRepository<TruongGiaoDich> _truongGiaoDichRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<HoSo, int> _hoSoRepository;
        private readonly IRepository<CM_BRANCH, string> _branchRepository;
        private readonly UserManager _userManager;
        private readonly string _connectionString;
        public QuyTrinhCongTacsAppService(IRepository<QuyTrinhCongTac> quyTrinhCongTacRepository, IRepository<TruongGiaoDich> truongGiaoDichRepository, IRepository<OrganizationUnit, long> organizationUnitRepository, IQuyTrinhCongTacsExcelExporter quyTrinhCongTacsExcelExporter, IRepository<HoSo, int> hoSoRepository, IRepository<CM_BRANCH, string> branchRepository, UserManager userManager, IWebHostEnvironment env)
        {
            _quyTrinhCongTacRepository = quyTrinhCongTacRepository;
            _quyTrinhCongTacsExcelExporter = quyTrinhCongTacsExcelExporter;
            _hoSoRepository = hoSoRepository;
            _branchRepository = branchRepository;
            _userManager = userManager;
            _organizationUnitRepository = organizationUnitRepository;
            _truongGiaoDichRepository = truongGiaoDichRepository;
            _connectionString = env.GetAppConfiguration().GetConnectionString("Default");
        }

        [AbpAuthorize(AppPermissions.Pages_QuyTrinhCongTacs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _quyTrinhCongTacRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetQuyTrinhCongTacsToExcel(GetAllQuyTrinhCongTacsForExcelInput input)
        {

            var filteredQuyTrinhCongTacs = _quyTrinhCongTacRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.TenCty.Contains(input.Filter) || e.ViTriCongViecCode.Contains(input.Filter) || e.QuanLyTrucTiep.Contains(input.Filter) || e.TrangThaiCode.Contains(input.Filter) || e.GhiChu.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenCtyFilter), e => e.TenCty == input.TenCtyFilter)
                        .WhereIf(input.MinDateToFilter != null, e => e.DateTo >= input.MinDateToFilter)
                        .WhereIf(input.MaxDateToFilter != null, e => e.DateTo <= input.MaxDateToFilter)
                        .WhereIf(input.MinDateFromFilter != null, e => e.DateFrom >= input.MinDateFromFilter)
                        .WhereIf(input.MaxDateFromFilter != null, e => e.DateFrom <= input.MaxDateFromFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ViTriCongViecCodeFilter), e => e.ViTriCongViecCode == input.ViTriCongViecCodeFilter)
                        .WhereIf(input.MinDonViCongTacIDFilter != null, e => e.DonViCongTacID >= input.MinDonViCongTacIDFilter)
                        .WhereIf(input.MaxDonViCongTacIDFilter != null, e => e.DonViCongTacID <= input.MaxDonViCongTacIDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.QuanLyTrucTiepFilter), e => e.QuanLyTrucTiep == input.QuanLyTrucTiepFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TrangThaiCodeFilter), e => e.TrangThaiCode == input.TrangThaiCodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GhiChuFilter), e => e.GhiChu == input.GhiChuFilter);

            var query = (from o in filteredQuyTrinhCongTacs
                         select new GetQuyTrinhCongTacForViewDto()
                         {
                             QuyTrinhCongTac = new QuyTrinhCongTacDto
                             {
                                 TenCty = o.TenCty,
                                 DateTo = o.DateTo,
                                 DateFrom = o.DateFrom,
                                 ViTriCongViecCode = o.ViTriCongViecCode,
                                 DonViCongTacID = o.DonViCongTacID,
                                 QuanLyTrucTiep = o.QuanLyTrucTiep,
                                 TrangThaiCode = o.TrangThaiCode,
                                 GhiChu = o.GhiChu,
                                 Id = o.Id
                             }
                         });


            var quyTrinhCongTacListDtos = await query.ToListAsync();

            return _quyTrinhCongTacsExcelExporter.ExportToFile(quyTrinhCongTacListDtos);
        }

        public async Task<PagedResultDto<GetQuyTrinhCongTacForViewDto>> GetAll( string id)
         {

             var filteredQuyTrinhCongTacs = _quyTrinhCongTacRepository.GetAll()
                 .Where(x => x.MaHoSo == int.Parse(id));
						//.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.TenCty.Contains(input.Filter) || e.ViTriCongViecCode.Contains(input.Filter) || e.QuanLyTrucTiep.Contains(input.Filter) || e.TrangThaiCode.Contains(input.Filter) || e.GhiChu.Contains(input.Filter))
						//.WhereIf(!string.IsNullOrWhiteSpace(input.TenCtyFilter),  e => e.TenCty == input.TenCtyFilter)
						//.WhereIf(input.MinDateToFilter != null, e => e.DateTo >= input.MinDateToFilter)
						//.WhereIf(input.MaxDateToFilter != null, e => e.DateTo <= input.MaxDateToFilter)
						//.WhereIf(input.MinDateFromFilter != null, e => e.DateFrom >= input.MinDateFromFilter)
						//.WhereIf(input.MaxDateFromFilter != null, e => e.DateFrom <= input.MaxDateFromFilter)
						//.WhereIf(!string.IsNullOrWhiteSpace(input.ViTriCongViecCodeFilter),  e => e.ViTriCongViecCode == input.ViTriCongViecCodeFilter)
						//.WhereIf(input.MinDonViCongTacIDFilter != null, e => e.DonViCongTacID >= input.MinDonViCongTacIDFilter)
						//.WhereIf(input.MaxDonViCongTacIDFilter != null, e => e.DonViCongTacID <= input.MaxDonViCongTacIDFilter)
						//.WhereIf(!string.IsNullOrWhiteSpace(input.QuanLyTrucTiepFilter),  e => e.QuanLyTrucTiep == input.QuanLyTrucTiepFilter)
						//.WhereIf(!string.IsNullOrWhiteSpace(input.TrangThaiCodeFilter),  e => e.TrangThaiCode == input.TrangThaiCodeFilter)
						//.WhereIf(!string.IsNullOrWhiteSpace(input.GhiChuFilter),  e => e.GhiChu == input.GhiChuFilter);

                        var pagedAndFilteredQuyTrinhCongTacs = filteredQuyTrinhCongTacs;
                //.OrderBy(input.Sorting ?? "id asc")
                //.PageBy(input);

            var units = _organizationUnitRepository.GetAll();
            var tgd = _truongGiaoDichRepository.GetAll();
            var quyTrinhCongTacs = from o in pagedAndFilteredQuyTrinhCongTacs

                join unit in units on o.DonViCongTacID.Value equals unit.Id into unitjoin
                from joinedtunit in unitjoin.DefaultIfEmpty()

                join unit in units on o.ViTriCongViecCode equals unit.Code into unitjoin1
                from joinedtunit1 in unitjoin1.DefaultIfEmpty()

                join htlv in tgd.Where(x => x.Code == "TTNS") on o.TrangThaiCode equals htlv.CDName into htlvJoin
                from joinedhtlv in htlvJoin.DefaultIfEmpty()

                                   select new GetQuyTrinhCongTacForViewDto() {
							QuyTrinhCongTac = new QuyTrinhCongTacDto
							{
                                TenCty = o.TenCty,
                                DateTo = o.DateTo,
                                DateFrom = o.DateFrom,
                                ViTriCongViecCode = o.ViTriCongViecCode,
                                DonViCongTacID = o.DonViCongTacID,
                                QuanLyTrucTiep = o.QuanLyTrucTiep,
                                TrangThaiCode = o.TrangThaiCode,
                                GhiChu = o.GhiChu,
                                MaHoSo = o.MaHoSo,
                                Id = o.Id
							},

                            ViTriCongViecValue = joinedtunit1 == null ? "" : joinedtunit1.DisplayName.ToString(),
                            DonViCongTacValue = joinedtunit == null ? "" : joinedtunit.DisplayName.ToString(),
                            HinhThucLamViecValue = joinedhtlv == null ? "" : joinedhtlv.Value.ToString(),
                                   };
       

            var totalCount = await filteredQuyTrinhCongTacs.CountAsync();

            return new PagedResultDto<GetQuyTrinhCongTacForViewDto>(
                totalCount,
                await quyTrinhCongTacs.ToListAsync()
            );
         }


         public async Task<List<QuyTrinhCongTacDto>> GetListQuaTrinhCongTac( string id)
         {

             var listQTCT = await _quyTrinhCongTacRepository.GetAll().Where(x =>  x.MaHoSo == int.Parse(id)).ToListAsync();

             var listQTCTDto = ObjectMapper.Map<List<QuyTrinhCongTacDto>>(listQTCT);
             return listQTCTDto;

         }

        //public List<QuyTrinhCongTacDto> GetAllQuyTrinhCongTacList(int id)
        //{

        //    return _quyTrinhCongTacRepository.GetAll().Where(t => t.IsDeleted == false).Select(t => new QuyTrinhCongTacDto { Id=t.Id , MaHoSo = t.MaHoSo  }).ToList();
        //}
        
        public async Task CreateOrEdit(CreateOrEditQuyTrinhCongTacDto input)
        {

            try
            {
                if (input.Id == null)
                {
                    await Create(input);
                }
                else
                {
                    await Update(input);
                }
            }
            catch (Exception e)
            {
                Logger.Warn(e.Message, e);
            }
        }
        
        public async Task<int> CreateOrEditForMobile(CreateOrEditQuyTrinhCongTacDto input)
        {
            try
            {
                if (DateTime.Compare(input.DateFrom, input.DateTo) < 0)
                {
                    if (input.Id == null)
                    {
                       return await CreateForMobile(input);
                    }

                    return await EditForMobile(input);
                }
                throw new UserFriendlyException(L(name: "Ngày kết thúc lớn hơn ngày bắt đầu"));
                
            }
            catch (Exception e)
            {
                Logger.Warn(e.Message, e);
            }
            return 0;
        }
        public async Task<int> CreateForMobile(CreateOrEditQuyTrinhCongTacDto input)
        {

            try
            {
                var branchId = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == input.CreatorUserId);
                if (branchId.BRANCH_ID.Equals("1"))
                {
                    Logger.Error("Tổng Giám Đốc ko thể đăng ký Công Tác");
                    return 0;
                }
                var conn = new SqlConnection(_connectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@branchId", branchId.BRANCH_ID);
                var lstUserListDtos = await conn.QueryAsync<UserListDto>("GetUserByParentFather", parameters, null, null, CommandType.StoredProcedure);
                var userListDtos = lstUserListDtos.ToList();
                if (userListDtos.Any())
                {
                    input.QuanLyTrucTiep = userListDtos.Find(x => x.BRANCH_TYPE.Equals("TN"))?.EmployeeCode;
                    input.TruongBoPhanId = userListDtos.Find(x => x.BRANCH_TYPE.Equals("CN"))?.EmployeeCode;
                    input.GiamDocBoPhanId = userListDtos.Find(x => x.BRANCH_TYPE.Equals("KV"))?.EmployeeCode;
                    input.GiamDocId = userListDtos.Find(x => x.BRANCH_TYPE.Equals("HS"))?.EmployeeCode;
                    if (input.QuanLyTrucTiep != null)
                    {
                        input.NguoiDuyetId = input.QuanLyTrucTiep;
                    }
                    else if(input.TruongBoPhanId != null)
                    {
                        input.NguoiDuyetId = input.TruongBoPhanId;
                    }
                    else if(input.GiamDocBoPhanId != null)
                    {
                        input.NguoiDuyetId = input.GiamDocBoPhanId;
                    }
                    else
                    {
                        input.NguoiDuyetId = input.GiamDocId;
                    }
                    //input.PhongCTNSId = userListDtos.Count >=  ? userListDtos[3].EmployeeCode : null;

                }
                var quyTrinhCongTac = ObjectMapper.Map<QuyTrinhCongTac>(input);
                return await _quyTrinhCongTacRepository.InsertAndGetIdAsync(quyTrinhCongTac);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return 0;
            }
        }
        public async Task<int> EditForMobile(CreateOrEditQuyTrinhCongTacDto input)
        {
            try
            {
                var quyTrinhCongTac = await _quyTrinhCongTacRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                quyTrinhCongTac.NgayDuyet = input.NgayDuyet;
                quyTrinhCongTac.GhiChu = input.GhiChu;
                if (quyTrinhCongTac.NguoiDuyetId.Equals(quyTrinhCongTac.QuanLyTrucTiep) && !string.IsNullOrEmpty(quyTrinhCongTac.TruongBoPhanId))
                {
                    quyTrinhCongTac.NguoiDuyetId = quyTrinhCongTac.TruongBoPhanId;
                }
                else if (quyTrinhCongTac.NguoiDuyetId.Equals(quyTrinhCongTac.TruongBoPhanId) && !string.IsNullOrEmpty(quyTrinhCongTac.GiamDocBoPhanId))
                {
                    quyTrinhCongTac.NguoiDuyetId = quyTrinhCongTac.GiamDocBoPhanId;
                }
                else if (quyTrinhCongTac.NguoiDuyetId.Equals(quyTrinhCongTac.GiamDocBoPhanId) && !string.IsNullOrEmpty(quyTrinhCongTac.GiamDocId))
                {
                    quyTrinhCongTac.NguoiDuyetId = quyTrinhCongTac.GiamDocId;
                }

                //else if (quyTrinhCongTac.NguoiDuyetId.Equals(input.QuanLyTrucTiep))
                //{
                //    quyTrinhCongTac.NguoiDuyetId = input.QuanLyTrucTiep;
                //}
                //var getFather_Id = _userManager.Users
                //    .Join(_branchRepository.GetAll(), u => u.BRANCH_ID, b => b.Id, (u, b) => new {u, b})
                //    .FirstOrDefaultAsync(x => x.u.EmployeeCode == quyTrinhCongTac.NguoiDuyetId).Result.b.FATHER_ID;
                //var maNV = _userManager.Users.FirstOrDefaultAsync(x => x.BRANCH_ID == getFather_Id).Result.EmployeeCode;

                //quyTrinhCongTac.NguoiDuyetId = maNV;

                await _quyTrinhCongTacRepository.UpdateAsync(quyTrinhCongTac);
                return quyTrinhCongTac.Id;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return 0;
            }
        }
        
        //[AbpAuthorize(AppPermissions.Pages_QuyTrinhCongTacs_Create)]
        protected virtual async Task Create(CreateOrEditQuyTrinhCongTacDto input)
         {
            var quyTrinhCongTac = ObjectMapper.Map<QuyTrinhCongTac>(input);

            await _quyTrinhCongTacRepository.InsertAsync(quyTrinhCongTac);
         }

		 //[AbpAuthorize(AppPermissions.Pages_QuyTrinhCongTacs_Edit)]
		 protected virtual async Task Update(CreateOrEditQuyTrinhCongTacDto input)
         {
            var quyTrinhCongTac = await _quyTrinhCongTacRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, quyTrinhCongTac);
         }

         [AbpAuthorize(AppPermissions.Pages_QuyTrinhCongTacs_Edit)]
         public async Task<GetQuyTrinhCongTacForEditOutput> GetQuyTrinhCongTacForEdit(EntityDto input)
         {
             var quyTrinhCongTac = await _quyTrinhCongTacRepository.FirstOrDefaultAsync(input.Id);

             var output = new GetQuyTrinhCongTacForEditOutput { QuyTrinhCongTac = ObjectMapper.Map<CreateOrEditQuyTrinhCongTacDto>(quyTrinhCongTac) };

             return output;
         }

        public async Task<(GetQuyTrinhCongTacDto TruongNhomId, GetQuyTrinhCongTacDto TruongBoPhanId, GetQuyTrinhCongTacDto GiamDocBoPhanId, List<GetQuyTrinhCongTacDto> PhongCTNSId, List<GetQuyTrinhCongTacDto> GiamDocId)?> GetQuyTrinhCongTac(long id)
        {
            var branchId = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (branchId.BRANCH_ID.Equals("1"))
            {
                Logger.Error("Tổng Giám Đốc ko thể đăng ký Công Tác");
                return null;
            }
            var conn = new SqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            var parameters = new DynamicParameters();
            parameters.Add("@branchId", branchId.BRANCH_ID);
            var lstUserListDtos = await conn.QueryAsync<UserListDto>("GetUserByParentFather", parameters, null, null, CommandType.StoredProcedure);
            var userListDtos = lstUserListDtos.ToList();
            (GetQuyTrinhCongTacDto TruongNhomId, GetQuyTrinhCongTacDto TruongBoPhanId, GetQuyTrinhCongTacDto
                GiamDocBoPhanId, List<GetQuyTrinhCongTacDto> PhongCTNSId, List<GetQuyTrinhCongTacDto> GiamDocId)
                result = (new GetQuyTrinhCongTacDto(), new GetQuyTrinhCongTacDto(), new GetQuyTrinhCongTacDto(), new List<GetQuyTrinhCongTacDto>(), new List<GetQuyTrinhCongTacDto>());
            if (userListDtos.Any())
                return null;
            userListDtos.ForEach(_ =>
            {
                switch (_.BRANCH_TYPE)
                {
                    case "TN":
                        {
                            result.TruongNhomId.Id = _.Id.ToString();
                            result.TruongNhomId.MaChamCong = _.MaChamCong;
                            result.TruongNhomId.Name = _.HoVaTen;
                            break;
                        }
                    case "CN":
                        {
                            if (!_.BRANCH_CODE.Equals("TCNS"))
                            {
                                result.TruongBoPhanId.Id = _.Id.ToString();
                                result.TruongBoPhanId.MaChamCong = _.MaChamCong;
                                result.TruongBoPhanId.Name = _.HoVaTen;
                            }
                            break;
                        }
                    case "KV":
                        {
                            result.GiamDocBoPhanId.Id = _.Id.ToString();
                            result.GiamDocBoPhanId.MaChamCong = _.MaChamCong;
                            result.GiamDocBoPhanId.Name = _.HoVaTen;
                            break;
                        }
                }
            });
            _userManager.Users
                   .Join(_branchRepository.GetAll(), u => u.BRANCH_ID, b => b.Id,
                       (u, b) => new { u, b })
                   .Join(_hoSoRepository.GetAll(), ub => ub.u.EmployeeCode, h => h.MaNhanVien, (ub, h) => new { ub, h })
                   .Where(ubh =>
                       ubh.ub.b.BRANCH_CODE.Equals("TCNS") && ubh.ub.b.BRANCH_TYPE.Equals("HS") &&
                       !ubh.ub.u.IsDeleted && !ubh.h.IsDeleted)
                   .Select(_ => new UserListDto()
                   {
                       BRANCH_ID = _.ub.u.BRANCH_ID,
                       EmployeeCode = _.ub.u.EmployeeCode,
                       MaChamCong = _.h.MaChamCong,
                       Id = _.ub.u.Id,
                       HoVaTen = _.h.HoVaTen,
                       BRANCH_TYPE = _.ub.b.BRANCH_TYPE,
                       BRANCH_CODE = _.ub.b.BRANCH_CODE,
                       UserName = _.ub.u.UserName,
                       CreationTime = _.ub.u.CreationTime,
                       EmailAddress = _.ub.u.EmailAddress,
                       IsActive = _.ub.u.IsActive,
                       IsEmailConfirmed = _.ub.u.IsEmailConfirmed,
                       Name = _.ub.u.Name,
                       PhoneNumber = _.ub.u.PhoneNumber,
                       ProfilePictureId = _.ub.u.ProfilePictureId,
                       Surname = _.ub.u.Surname

                   })
                   .ToList()
                   .ForEach(_ =>
                   {
                       if (_.BRANCH_CODE == "TCNS")
                       {
                           result.PhongCTNSId.Add(new GetQuyTrinhCongTacDto
                           {
                               MaChamCong = _.MaChamCong,
                               Name = _.HoVaTen,
                               Id = _.Id.ToString()
                           });
                       }
                       else
                       {
                           result.GiamDocId.Add(new GetQuyTrinhCongTacDto
                           {
                               MaChamCong = _.MaChamCong,
                               Name = _.HoVaTen,
                               Id = _.Id.ToString()
                           });
                       }
                   });
            return result;
        }

    }
}