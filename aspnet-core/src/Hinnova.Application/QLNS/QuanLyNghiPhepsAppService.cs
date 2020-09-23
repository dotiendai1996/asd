

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLNS.Exporting;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Hinnova.Management.Dtos;
using Abp.Organizations;
using Dapper;
using Hinnova.Authorization.Users;
using Hinnova.Organizations;
using Hinnova.Organizations.Dto;
using Hinnova.QLNSDtos;
using Microsoft.AspNetCore.Hosting;
using Hinnova.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Hinnova.Authorization.Users.Dto;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Abp.UI;

namespace Hinnova.QLNS
{
    [AbpAuthorize(AppPermissions.Pages_QuanLyNghiPheps)]
    public class QuanLyNghiPhepsAppService : HinnovaAppServiceBase, IQuanLyNghiPhepsAppService
    {
        private readonly IRepository<QuanLyNghiPhep> _quanLyNghiPhepRepository;
        private readonly IRepository<QuanLyTrucTiepPNP> _quanLyTrucTiepPNPRepository;
        private readonly IRepository<LichSuUpload> _lichSuUploadRepository;
        private readonly IRepository<HoSo> _hoSoRepository;
        private readonly IOrganizationUnitAppService _organizationUnitAppService;
        private readonly IRepository<TruongGiaoDich> _truongGiaoDichRepository;
        private readonly IUserAppService UserAppService;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly ILichSuUploadsAppService _lichSuUploadsAppService;

        private readonly IQuanLyTrucTiepPNPsAppService _quanLyTrucTiepPNPsAppService;
        private readonly IQuanLyNghiPhepsExcelExporter _quanLyNghiPhepsExcelExporter;
        private readonly string connectionString;


        public QuanLyNghiPhepsAppService(IQuanLyTrucTiepPNPsAppService quanLyTrucTiepPNPsAppService, IRepository<QuanLyTrucTiepPNP> quanLyTrucTiepPNPRepository, IRepository<HoSo> hoSoRepository, IWebHostEnvironment env, IRepository<User, long> userRepository, ILichSuUploadsAppService lichSuUploadsAppService, IOrganizationUnitAppService organizationUnitAppService, IRepository<QuanLyNghiPhep> quanLyNghiPhepRepository, IRepository<TruongGiaoDich> truongGiaoDichRepository, IRepository<OrganizationUnit, long> organizationUnitRepository, IRepository<LichSuUpload> lichSuUploadRepository, IQuanLyNghiPhepsExcelExporter quanLyNghiPhepsExcelExporter)
        {
            _quanLyNghiPhepRepository = quanLyNghiPhepRepository;
            _lichSuUploadRepository = lichSuUploadRepository;
            _userRepository = userRepository;
            _quanLyTrucTiepPNPRepository = quanLyTrucTiepPNPRepository;
            _hoSoRepository = hoSoRepository;
            _quanLyTrucTiepPNPsAppService = quanLyTrucTiepPNPsAppService;
            _organizationUnitRepository = organizationUnitRepository;
            _organizationUnitAppService = organizationUnitAppService;
            _lichSuUploadsAppService = lichSuUploadsAppService;
            _truongGiaoDichRepository = truongGiaoDichRepository;
            _quanLyNghiPhepsExcelExporter = quanLyNghiPhepsExcelExporter;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");

        }


        public async Task<GetOrganizationUnitForEditOutput> GetOrganizationUnitForEdit(int id)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(id);

            var output = new GetOrganizationUnitForEditOutput { OrganizationUnit = ObjectMapper.Map<OrganizationUnitDto>(organizationUnit) };

            return output;
        }

        public async Task<List<UserListDto>> GetAllUser()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<UserListDto>(sql: "SELECT * FROM  AbpUsers WHERE IsDeleted = 0");
                return result.ToList();
            }
        }


        public async Task<List<QuanLyNghiPhepDto>> GetAllPhieuNghiPhep()
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<QuanLyNghiPhepDto>(sql: "dbo.QuanLyNghiPhep_Search", param: new { }, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<QuanLyNghiPhepDto>> GetAllPhieuNghiPhepToMaNV(string MaNVFilter)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<QuanLyNghiPhepDto>(sql: "dbo.GetAllPhieuNghiPhepFilter", param: new { MaNVFilter }, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<PagedResultDto<GetQuanLyNghiPhepForViewDto>> GetAll(GetAllQuanLyNghiPhepsInput input)
        {
            var tgd = _truongGiaoDichRepository.GetAll();

            var units = _organizationUnitRepository.GetAll();

            var qltts = _quanLyTrucTiepPNPRepository.GetAll();

            //  var user = _userRepository.GetAll();
            var hoso = _hoSoRepository.GetAll();

        
                var filteredQuanLyNghiPheps = _quanLyNghiPhepRepository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    e => false || e.TenNhanVien.Contains(input.Filter) || e.MaNV.Contains(input.Filter) ||
                         e.LyDo.Contains(input.Filter) || e.TepDinhKem.Contains(input.Filter) ||
                         e.TenCTY.Contains(input.Filter))
                .WhereIf(!string.IsNullOrWhiteSpace(input.TenNhanVienFilter),
                    e => e.TenNhanVien == input.TenNhanVienFilter)

                .WhereIf(input.NghiPhepFilter > -1,
                    e => (input.NghiPhepFilter == 1 && e.NghiPhep) || (input.NghiPhepFilter == 0 && !e.NghiPhep))
                .WhereIf(input.CongTacFilter > -1,
                    e => (input.CongTacFilter == 1 && e.CongTac) || (input.CongTacFilter == 0 && !e.CongTac))
                .WhereIf(input.TangCaFilter > -1,
                    e => (input.TangCaFilter == 1 && e.TangCa) || (input.TangCaFilter == 0 && !e.TangCa))

                .WhereIf(input.MinNgayBatDauFilter != null, e => e.NgayBatDau >= input.MinNgayBatDauFilter)
                .WhereIf(input.MaxNgayBatDauFilter != null, e => e.NgayBatDau <= input.MaxNgayBatDauFilter)
                .WhereIf(input.MinNgayKetThucFilter != null, e => e.NgayKetThuc >= input.MinNgayKetThucFilter)
                .WhereIf(input.MaxNgayKetThucFilter != null, e => e.NgayKetThuc <= input.MaxNgayKetThucFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.LyDoFilter), e => e.LyDo == input.LyDoFilter)

                .WhereIf(!string.IsNullOrWhiteSpace(input.TepDinhKemFilter),
                    e => e.TepDinhKem == input.TepDinhKemFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.TenCTYFilter), e => e.TenCTY == input.TenCTYFilter)
                .WhereIf(input.MinNguoiDuyetIDFilter != null, e => e.NguoiDuyetID >= input.MinNguoiDuyetIDFilter)
                .WhereIf(input.MaxNguoiDuyetIDFilter != null, e => e.NguoiDuyetID <= input.MaxNguoiDuyetIDFilter)
                .WhereIf(input.MinNgayDuyetFilter != null, e => e.NgayDuyet >= input.MinNgayDuyetFilter)

                .WhereIf(input.MaxNgayDuyetFilter != null, e => e.NgayDuyet <= input.MaxNgayDuyetFilter)
                .WhereIf(input.TrangThaiIDFilter != null, e => e.TrangThaiID == (input.TrangThaiIDFilter))
                .WhereIf(input.DonViCongTacIDFilter != null, e => e.DonViCongTacID == (input.DonViCongTacIDFilter))
             
                .WhereIf(input.StartDateFilter != null && input.isCheckTimeFilter == false, e => e.CreationTime.Date >= ((DateTime)input.StartDateFilter).Date)
                .WhereIf(input.EndDateFilter != null && input.isCheckTimeFilter == false, e => e.CreationTime.Date <= ((DateTime)input.EndDateFilter).Date)
                 .WhereIf(input.StartDateFilter != null && input.isCheckTimeFilter == true, e => ((DateTime)e.LastModificationTime).Date >= ((DateTime)input.StartDateFilter).Date)
                .WhereIf(input.EndDateFilter != null && input.isCheckTimeFilter == true, e => ((DateTime)e.LastModificationTime).Date <= ((DateTime)input.EndDateFilter).Date);
                if (tgd.Any(k => k.Code == "QXPNP" && k.CDName == (input.RoleIDFilter).ToString()) == true)
                {
                    var pagedAndFilteredHoSos = filteredQuanLyNghiPheps
                        .OrderBy(input.Sorting ?? "id asc")
                        .PageBy(input);

                var quanLyNghiPheps = from o in pagedAndFilteredHoSos

                                      join gt in tgd.Where(x => x.Code == "TTPD") on o.TrangThaiID.Value equals gt.Id into gtJoin
                                      from joinedgt in gtJoin.DefaultIfEmpty()

                                      join unit in units on o.DonViCongTacID.Value equals unit.Id into unitjoin
                                      from joinedtunit in unitjoin.DefaultIfEmpty()

                                      //join qltt in qltts on o.Id equals qltt.PhieuNghiPhepID into qlttjoin
                                      //from joinedqltt in qlttjoin.DefaultIfEmpty()

                                      join hoso2 in hoso on o.TruongBoPhanID equals hoso2.MaNhanVien into hoso2join
                                      from joinedthoso2 in hoso2join.DefaultIfEmpty()

                                      //where  input.QuanLyTrucTiepIDFilter == null
                                      select new GetQuanLyNghiPhepForViewDto()
                                      {
                                          QuanLyNghiPhep = new QuanLyNghiPhepDto
                                          {
                                              TenNhanVien = o.TenNhanVien,
                                              DonViCongTacID = o.DonViCongTacID,
                                              MaNV = o.MaNV,
                                              NghiPhep = o.NghiPhep,
                                              CongTac = o.CongTac,
                                              CreateTime = o.CreationTime,
                                              LastModificationTime = o.LastModificationTime,
                                              NgayBatDau = o.NgayBatDau,
                                              NgayKetThuc = o.NgayKetThuc,
                                              LyDo = o.LyDo,
                                              QuanLyTrucTiepID = o.QuanLyTrucTiepID,
                                              TruongBoPhanID = o.TruongBoPhanID,
                                              TepDinhKem = o.TepDinhKem,
                                              TenCTY = o.TenCTY,
                                              NguoiDuyetID = o.NguoiDuyetID,
                                              NgayDuyet = o.NgayDuyet,
                                              TrangThaiID = o.TrangThaiID,
                                              Id = o.Id
                                          },
                                          DonViCongTacValue = joinedtunit == null ? "" : joinedtunit.DisplayName.ToString(),
                                          //   QuanLyTrucTiepValue  = joinedthoso1  == null ? "" : joinedthoso1.HoVaTen.ToString(),
                                          TruongBoPhanValue = joinedthoso2 == null ? "" : joinedthoso2.HoVaTen.ToString(),
                                          TrangThaiValue = joinedgt == null ? "" : joinedgt.Value.ToString(),

                                      };

                var totalCount = await filteredQuanLyNghiPheps.CountAsync();
                //var quanLyNghiPhepsList = await quanLyNghiPheps.ToListAsync();

                //var quanLyNghiPhepsListGroupBy = quanLyNghiPhepsList.GroupBy(x => x.QuanLyNghiPhep.Id).Select(x => x.First()).ToList();


                //var totalCount = quanLyNghiPhepsListGroupBy.Count();

                //var pagedAndFilteredQuanLyNghiPheps = quanLyNghiPhepsListGroupBy


                //    .OrderBy(x => input.Sorting)
                //    .Skip(input.SkipCount).Take(input.MaxResultCount);
                return new PagedResultDto<GetQuanLyNghiPhepForViewDto>(
                    totalCount,
                  await quanLyNghiPheps.ToListAsync()
                 // await quanLyNghiPheps.ToList()
                );
            }
            else
            {
                
                var quanLyNghiPheps = from o in filteredQuanLyNghiPheps

                                      join gt in tgd.Where(x => x.Code == "TTPD") on o.TrangThaiID.Value equals gt.Id into gtJoin
                                      from joinedgt in gtJoin.DefaultIfEmpty()

                                      join unit in units on o.DonViCongTacID.Value equals unit.Id into unitjoin
                                      from joinedtunit in unitjoin.DefaultIfEmpty()

                                      join qltt in qltts on o.Id equals qltt.PhieuNghiPhepID into qlttjoin
                                      from joinedqltt in qlttjoin.DefaultIfEmpty()

                                      join hoso2 in hoso on o.TruongBoPhanID equals hoso2.MaNhanVien into hoso2join
                                      from joinedthoso2 in hoso2join.DefaultIfEmpty()

                                      where joinedqltt.QLTT == input.MaNVFilter 
                                       || o.MaNV == input.MaNVFilter 
                                       || o.TruongBoPhanID == input.MaNVFilter


                                      select new GetQuanLyNghiPhepForViewDto()
                                      {
                                          QuanLyNghiPhep = new QuanLyNghiPhepDto
                                          {
                                              TenNhanVien = o.TenNhanVien,
                                              DonViCongTacID = o.DonViCongTacID,
                                              MaNV = o.MaNV,
                                              NghiPhep = o.NghiPhep,
                                              CongTac = o.CongTac,
                                              CreateTime = o.CreationTime,
                                              LastModificationTime = o.LastModificationTime,
                                              NgayBatDau = o.NgayBatDau,
                                              NgayKetThuc = o.NgayKetThuc,
                                              LyDo = o.LyDo,
                                              QuanLyTrucTiepID = o.QuanLyTrucTiepID,
                                              TruongBoPhanID = o.TruongBoPhanID,
                                              TepDinhKem = o.TepDinhKem,
                                              TenCTY = o.TenCTY,
                                              NguoiDuyetID = o.NguoiDuyetID,
                                              NgayDuyet = o.NgayDuyet,
                                              TrangThaiID = o.TrangThaiID,
                                              Id = o.Id
                                          },
                                          DonViCongTacValue = joinedtunit == null ? "" : joinedtunit.DisplayName.ToString(),
                                          TruongBoPhanValue = joinedthoso2 == null ? "" : joinedthoso2.HoVaTen.ToString(),
                                          TrangThaiValue = joinedgt == null ? "" : joinedgt.Value.ToString(),
                                      };
                var quanLyNghiPhepsList = await quanLyNghiPheps.ToListAsync();

                var quanLyNghiPhepsListGroupBy = quanLyNghiPhepsList.GroupBy(x => x.QuanLyNghiPhep.Id).Select(x => x.First()).ToList();
                var totalCount = quanLyNghiPhepsListGroupBy.Count();
                var pagedAndFilteredQuanLyNghiPheps = quanLyNghiPhepsListGroupBy
                    .OrderBy(x => input.Sorting)
                    .Skip(input.SkipCount).Take(input.MaxResultCount);
                return new PagedResultDto<GetQuanLyNghiPhepForViewDto>(
                    totalCount,
                    quanLyNghiPhepsListGroupBy.ToList()
                );

            }

        }

        public async Task<GetQuanLyNghiPhepForViewDto> GetQuanLyNghiPhepForView(int id)
        {
            var quanLyNghiPhep = await _quanLyNghiPhepRepository.GetAsync(id);

            var output = new GetQuanLyNghiPhepForViewDto { QuanLyNghiPhep = ObjectMapper.Map<QuanLyNghiPhepDto>(quanLyNghiPhep) };

            return output;
        }



        // [AbpAuthorize(AppPermissions.Pages_QuanLyNghiPheps_Edit)]
        public async Task<GetQuanLyNghiPhepForEditOutput> GetQuanLyNghiPhepForEdit(EntityDto input)

        {

            var quanLyNghiPhep = await _quanLyNghiPhepRepository.FirstOrDefaultAsync(input.Id);

            //  var output = new GetQuanLyNghiPhepForEditOutput { QuanLyNghiPhep = ObjectMapper.Map<CreateOrEditQuanLyNghiPhepDto>(quanLyNghiPhep) };
            var orgList = await _organizationUnitAppService.GetOrganizationUnits();

            var truongGiaoDichList = _truongGiaoDichRepository.GetAll().ToList();

            var output = new GetQuanLyNghiPhepForEditOutput();
            if (quanLyNghiPhep != null)
            {


                output.LichSuUploadList = await _lichSuUploadsAppService.GetListLichSuUploadDto("NP", quanLyNghiPhep.Id.ToString());
                output.QuanLyTrucTiepList = await _quanLyTrucTiepPNPsAppService.GetListQuanLyTrucTiepDto(quanLyNghiPhep.Id);
            }

            output.OrganizationUnitList = orgList;
            output.QuanLyNghiPhep = ObjectMapper.Map<CreateOrEditQuanLyNghiPhepDto>(quanLyNghiPhep);


            output.ViTriCongViec = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.VTUT)).OrderBy(x => x.Value));

            // output.TrangThai = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TRTH)).OrderBy(x => x.Value));

            output.Congty = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.CT)).OrderBy(x => x.Value));
            //output.GioiTinh = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.GT)).OrderBy(x => x.Value));



            return output;


        }


        public async Task<int> CreateOrEdit(CreateOrEditNghiPhepInput inputNghiPhep)
        {
            var input = inputNghiPhep.NghiPhep;
            DateTime NgayBatDau, NgayKetThuc;
            NgayBatDau = Convert.ToDateTime(input.NgayBatDau);
            NgayKetThuc = Convert.ToDateTime(input.NgayKetThuc);
            int now = int.Parse(NgayKetThuc.ToString("yyyyMMdd"));
            int dob = int.Parse(NgayBatDau.ToString("yyyyMMdd"));
            if (now >= dob)
            {
                if (input.Id == null)
                {
                    var id = await Create(input);
                    var lichSu = inputNghiPhep.LichSuUpLoad;

                    var quanLyTrucTiep = inputNghiPhep.QuanLyTrucTiepPNP;
                    if (lichSu.Count > 0)
                    {
                        for (int i = 0; i < lichSu.Count; i++)

                        {
                            var lichSuUpload = new LichSuUpload();

                            lichSuUpload.TenFile = lichSu[i].TenFile;
                            lichSuUpload.TieuDe = lichSu[i].TieuDe;
                            lichSuUpload.DungLuong = lichSu[i].DungLuong;
                            lichSuUpload.Type = "NP";
                            lichSuUpload.TypeID = id.ToString();
                            await _lichSuUploadRepository.InsertAsync(lichSuUpload);
                        }

                    }
                    if (quanLyTrucTiep.Count > 0)
                    {
                        for (int i = 0; i < quanLyTrucTiep.Count; i++)

                        {
                            var quanLyTrucTiepPNP = new QuanLyTrucTiepPNP();

                            quanLyTrucTiepPNP.QLTT = quanLyTrucTiep[i].QLTT;
                            quanLyTrucTiepPNP.GhiChu = quanLyTrucTiep[i].GhiChu;
                            quanLyTrucTiepPNP.MaHoSo = quanLyTrucTiep[i].MaHoSo;
                            quanLyTrucTiepPNP.PhieuNghiPhepID = id;
                            await _quanLyTrucTiepPNPRepository.InsertAsync(quanLyTrucTiepPNP);
                        }

                    }
                    return id;


                }
                else
                {
                    await Update(input);
                }
            }
            else
            {
                throw new UserFriendlyException(L(name: "Ngày kết thúc lớn hơn ngày bắt đầu"));
            }

          

            return 0;
        }

        [AbpAuthorize(AppPermissions.Pages_QuanLyNghiPheps_Create)]
        protected virtual async Task<int> Create(CreateOrEditQuanLyNghiPhepDto input)
        {
            var quanLyNghiPhep = ObjectMapper.Map<QuanLyNghiPhep>(input);



            return await _quanLyNghiPhepRepository.InsertAndGetIdAsync(quanLyNghiPhep);
        }

        [AbpAuthorize(AppPermissions.Pages_QuanLyNghiPheps_Edit)]
        protected virtual async Task Update(CreateOrEditQuanLyNghiPhepDto input)
        {
            var quanLyNghiPhep = await _quanLyNghiPhepRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, quanLyNghiPhep);
        }

        [AbpAuthorize(AppPermissions.Pages_QuanLyNghiPheps_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _quanLyNghiPhepRepository.DeleteAsync(input.Id);
        }

        //public async Task<FileDto> GetQuanLyNghiPhepsToExcel(GetAllQuanLyNghiPhepsForExcelInput input)
        //      {

        //          var filteredQuanLyNghiPheps = _quanLyNghiPhepRepository.GetAll()
        //              .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
        //                  e => false || e.TenNhanVien.Contains(input.Filter) || e.MaNV.Contains(input.Filter) ||
        //                       e.LyDo.Contains(input.Filter) || e.TepDinhKem.Contains(input.Filter) ||
        //                       e.TenCTY.Contains(input.Filter) )
        //              .WhereIf(!string.IsNullOrWhiteSpace(input.TenNhanVienFilter),
        //                  e => e.TenNhanVien == input.TenNhanVienFilter)
        //              .WhereIf(input.MinDonViCongTacIDFilter != null, e => e.DonViCongTacID >= input.MinDonViCongTacIDFilter)
        //              .WhereIf(input.MaxDonViCongTacIDFilter != null, e => e.DonViCongTacID <= input.MaxDonViCongTacIDFilter)
        //              .WhereIf(!string.IsNullOrWhiteSpace(input.MaNVFilter), e => e.MaNV == input.MaNVFilter)
        //              .WhereIf(input.NghiPhepFilter > -1,
        //                  e => (input.NghiPhepFilter == 1 && e.NghiPhep) || (input.NghiPhepFilter == 0 && !e.NghiPhep))
        //              .WhereIf(input.CongTacFilter > -1,
        //                  e => (input.CongTacFilter == 1 && e.CongTac) || (input.CongTacFilter == 0 && !e.CongTac))
        //              .WhereIf(input.MinNgayBatDauFilter != null, e => e.NgayBatDau >= input.MinNgayBatDauFilter)
        //              .WhereIf(input.MaxNgayBatDauFilter != null, e => e.NgayBatDau <= input.MaxNgayBatDauFilter)
        //              .WhereIf(input.MinNgayKetThucFilter != null, e => e.NgayKetThuc >= input.MinNgayKetThucFilter)
        //              .WhereIf(input.MaxNgayKetThucFilter != null, e => e.NgayKetThuc <= input.MaxNgayKetThucFilter)
        //              .WhereIf(!string.IsNullOrWhiteSpace(input.LyDoFilter), e => e.LyDo == input.LyDoFilter)
        //              .WhereIf(input.MinQuanLyTrucTiepIDFilter != null,
        //                  e => e.QuanLyTrucTiepID >= input.MinQuanLyTrucTiepIDFilter)
        //              .WhereIf(input.MaxQuanLyTrucTiepIDFilter != null,
        //                  e => e.QuanLyTrucTiepID <= input.MaxQuanLyTrucTiepIDFilter)
        //              .WhereIf(input.MinTruongBoPhanIDFilter != null, e => e.TruongBoPhanID >= input.MinTruongBoPhanIDFilter)
        //              .WhereIf(input.MaxTruongBoPhanIDFilter != null, e => e.TruongBoPhanID <= input.MaxTruongBoPhanIDFilter)
        //              .WhereIf(!string.IsNullOrWhiteSpace(input.TepDinhKemFilter),
        //                  e => e.TepDinhKem == input.TepDinhKemFilter)
        //              .WhereIf(!string.IsNullOrWhiteSpace(input.TenCTYFilter), e => e.TenCTY == input.TenCTYFilter)
        //              .WhereIf(input.MinNguoiDuyetIDFilter != null, e => e.NguoiDuyetID >= input.MinNguoiDuyetIDFilter)
        //              .WhereIf(input.MaxNguoiDuyetIDFilter != null, e => e.NguoiDuyetID <= input.MaxNguoiDuyetIDFilter)
        //              .WhereIf(input.MinNgayDuyetFilter != null, e => e.NgayDuyet >= input.MinNgayDuyetFilter)
        //              .WhereIf(input.MaxNgayDuyetFilter != null, e => e.NgayDuyet <= input.MaxNgayDuyetFilter);
        //				//.WhereIf(!string.IsNullOrWhiteSpace(input.TrangThaiFilter),  e => e.TrangThai == input.TrangThaiFilter);

        //	var query = (from o in filteredQuanLyNghiPheps
        //                       select new GetQuanLyNghiPhepForViewDto() { 
        //					QuanLyNghiPhep = new QuanLyNghiPhepDto
        //					{
        //                              TenNhanVien = o.TenNhanVien,
        //                              DonViCongTacID = o.DonViCongTacID,
        //                              MaNV = o.MaNV,
        //                              NghiPhep = o.NghiPhep,
        //                              CongTac = o.CongTac,
        //                              NgayBatDau = o.NgayBatDau,
        //                              NgayKetThuc = o.NgayKetThuc,
        //                              LyDo = o.LyDo,
        //                              QuanLyTrucTiepID = o.QuanLyTrucTiepID,
        //                              TruongBoPhanID = o.TruongBoPhanID,
        //                              TepDinhKem = o.TepDinhKem,
        //                              TenCTY = o.TenCTY,
        //                              NguoiDuyetID = o.NguoiDuyetID,
        //                              NgayDuyet = o.NgayDuyet,
        //                              TrangThaiID = o.TrangThaiID,
        //                              Id = o.Id
        //					}
        //				 });


        //          var quanLyNghiPhepListDtos = await query.ToListAsync();

        //          return _quanLyNghiPhepsExcelExporter.ExportToFile(quanLyNghiPhepListDtos);
        //       }


    }
}
