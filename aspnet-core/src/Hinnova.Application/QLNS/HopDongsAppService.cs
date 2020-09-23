

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLNSExporting;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Hinnova.Configuration;
using Microsoft.Extensions.Configuration;
using Abp.Organizations;
using Hinnova.QLNS.Dtos;
using Hinnova.Organizations;

namespace Hinnova.QLNS
{
    //[AbpAuthorize(AppPermissions.Pages_HopDongs)]
    public class HopDongsAppService : HinnovaAppServiceBase, IHopDongsAppService
    {
        private readonly IRepository<HopDong> _hopDongRepository;
        private readonly IRepository<UngVien> _ungVienRepository;
        private readonly IHopDongsExcelExporter _hopDongsExcelExporter;
        private readonly IRepository<Template> _templateRepository;
        private readonly IRepository<LichSuUpload> _lichSuUploadRepository;
        private readonly IRepository<TruongGiaoDich> _truongGiaoDichRepository;
        private readonly string connectionString;
        private readonly IOrganizationUnitAppService _organizationUnitAppService;
        private readonly IWebHostEnvironment _env;
        IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly ILichSuUploadsAppService _lichSuUploadsAppService;

        public HopDongsAppService(IRepository<LichSuUpload> lichSuUploadRepository,  IOrganizationUnitAppService organizationUnitAppService, ILichSuUploadsAppService lichSuUploadsAppService ,IRepository<Template> templateRepository,  IWebHostEnvironment env, IRepository<OrganizationUnit, long> organizationUnitRepository, IRepository<UngVien> ungVienRepository, IWebHostEnvironment hostingEnvironment, IRepository<TruongGiaoDich> truongGiaoDichRepository, IRepository<HopDong> hopDongRepository, IHopDongsExcelExporter hopDongsExcelExporter)
        {
            _hopDongRepository = hopDongRepository;
            _env = hostingEnvironment;
            _templateRepository = templateRepository;
            _lichSuUploadRepository = lichSuUploadRepository;
            _organizationUnitRepository = organizationUnitRepository;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");
            _ungVienRepository = ungVienRepository;
            _hopDongsExcelExporter = hopDongsExcelExporter;
            _organizationUnitAppService = organizationUnitAppService;
            _truongGiaoDichRepository = truongGiaoDichRepository;
        _lichSuUploadsAppService = lichSuUploadsAppService;
           
        }

        public async Task<PagedResultDto<GetHopDongForViewDto>> GetAll(GetAllHopDongsInput input)
        {

            var filteredHopDongs = _hopDongRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.HoTenNhanVien.Contains(input.Filter) || e.ChucDanh.Contains(input.Filter) || e.SoHopDong.Contains(input.Filter) || e.TenHopDong.Contains(input.Filter) || e.ThoiHanHopDong.Contains(input.Filter) || e.tenCTY.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.HoTenNhanVienFilter), e => e.HoTenNhanVien.ToLower() == input.HoTenNhanVienFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ViTriCongViecCodeFilter), e => e.ViTriCongViecCode.ToLower() == input.ViTriCongViecCodeFilter.ToLower().Trim())
                        .WhereIf(input.MinNgayKyFilter != null, e => e.NgayKy >= input.MinNgayKyFilter)
                        .WhereIf(input.MaxNgayKyFilter != null, e => e.NgayKy <= input.MaxNgayKyFilter)
                        .WhereIf(input.MinDonViCongTacIDFilter != null, e => e.DonViCongTacID >= input.MinDonViCongTacIDFilter)
                        .WhereIf(input.MaxDonViCongTacIDFilter != null, e => e.DonViCongTacID <= input.MaxDonViCongTacIDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenHopDongFilter), e => e.TenHopDong.ToLower() == input.TenHopDongFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LoaiHopDongCodeFilter), e => e.LoaiHopDongCode.ToLower() == input.LoaiHopDongCodeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.HinhThucLamViecCodeFilter), e => e.HinhThucLamViecCode.ToLower() == input.HinhThucLamViecCodeFilter.ToLower().Trim())
                        .WhereIf(input.MinNgayCoHieuLucFilter != null, e => e.NgayCoHieuLuc >= input.MinNgayCoHieuLucFilter)
                        .WhereIf(input.MaxNgayCoHieuLucFilter != null, e => e.NgayCoHieuLuc <= input.MaxNgayCoHieuLucFilter)
                        .WhereIf(input.MinNgayHetHanFilter != null, e => e.NgayHetHan >= input.MinNgayHetHanFilter)
                        .WhereIf(input.MaxNgayHetHanFilter != null, e => e.NgayHetHan <= input.MaxNgayHetHanFilter)
                        .WhereIf(input.MinLuongCoBanFilter != null, e => e.LuongCoBan >= input.MinLuongCoBanFilter)
                        .WhereIf(input.MaxLuongCoBanFilter != null, e => e.LuongCoBan <= input.MaxLuongCoBanFilter)
                        .WhereIf(input.MinLuongDongBaoHiemFilter != null, e => e.LuongDongBaoHiem >= input.MinLuongDongBaoHiemFilter)
                        .WhereIf(input.MaxLuongDongBaoHiemFilter != null, e => e.LuongDongBaoHiem <= input.MaxLuongDongBaoHiemFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ChucDanhFilter), e => e.ChucDanh.ToLower() == input.ChucDanhFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TrichYeuFilter), e => e.TrichYeu.ToLower() == input.TrichYeuFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RECORD_STATUSFilter), e => e.RECORD_STATUS.ToLower() == input.RECORD_STATUSFilter.ToLower().Trim())
                        .WhereIf(input.MinMARKER_IDFilter != null, e => e.MARKER_ID >= input.MinMARKER_IDFilter)
                        .WhereIf(input.MaxMARKER_IDFilter != null, e => e.MARKER_ID <= input.MaxMARKER_IDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AUTH_STATUSFilter), e => e.AUTH_STATUS.ToLower() == input.AUTH_STATUSFilter.ToLower().Trim())
                        .WhereIf(input.MinCHECKER_IDFilter != null, e => e.CHECKER_ID >= input.MinCHECKER_IDFilter)
                        .WhereIf(input.MaxCHECKER_IDFilter != null, e => e.CHECKER_ID <= input.MaxCHECKER_IDFilter)
                        .WhereIf(input.MinAPPROVE_DTFilter != null, e => e.APPROVE_DT >= input.MinAPPROVE_DTFilter)
                        .WhereIf(input.MaxAPPROVE_DTFilter != null, e => e.APPROVE_DT <= input.MaxAPPROVE_DTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ThoiHanHopDongFilter), e => e.ThoiHanHopDong.ToLower() == input.ThoiHanHopDongFilter.ToLower().Trim());

            var pagedAndFilteredHopDongs = filteredHopDongs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);
            var units = _organizationUnitRepository.GetAll();
            var tgd = _truongGiaoDichRepository.GetAll();
            var lhds = _templateRepository.GetAll();
            var hopDongs = from o in pagedAndFilteredHopDongs


                           join htlv in tgd.Where(x => x.Code == "HTLV") on o.HinhThucLamViecCode equals htlv.CDName into htlvJoin
                           from joinedhtlv in htlvJoin.DefaultIfEmpty()

                           join thhd in tgd.Where(x => x.Code == "THHD") on o.ThoiHanHopDong equals thhd.CDName into thhdJoin
                           from joinedthhd in thhdJoin.DefaultIfEmpty()

                           join unit in units on o.DonViCongTacID.Value equals unit.Id into unitjoin
                           from joinedtunit in unitjoin.DefaultIfEmpty()

                           join unit in units on o.ViTriCongViecCode equals unit.Code into unitjoin1
                           from joinedtunit1 in unitjoin1.DefaultIfEmpty()


                           join lhd in lhds on o.LoaiHopDongID equals lhd.Id into lhdJoin
                           from joinelhd in lhdJoin.DefaultIfEmpty()



                           select new GetHopDongForViewDto()
                           {
                               HopDong = new HopDongDto
                               {
                                   NhanVienId = o.NhanVienId,
                                   HoTenNhanVien = o.HoTenNhanVien,
                                   ViTriCongViecCode = o.ViTriCongViecCode,
                                   SoHopDong = o.SoHopDong,
                                   NgayKy = o.NgayKy,
                                   DonViCongTacID = o.DonViCongTacID,
                                   TenHopDong = o.TenHopDong,
                                   LoaiHopDongCode = o.LoaiHopDongCode,
                                   HinhThucLamViecCode = o.HinhThucLamViecCode,
                                   NgayCoHieuLuc = o.NgayCoHieuLuc,
                                   NgayHetHan = o.NgayHetHan,
                                   LuongCoBan = (o.LuongCoBan ?? 0).ToString("#,###"),
                                   LuongDongBaoHiem = (o.LuongDongBaoHiem ?? 0).ToString("#,###"),
                                   TyLeHuongLuong = o.TyLeHuongLuong,
                                   NguoiDaiDienCongTy = o.NguoiDaiDienCongTy,
                                   ChucDanh = o.ChucDanh,
                                   TrichYeu = o.TrichYeu,
                                   TepDinhKem = o.TepDinhKem,
                                   GhiChu = o.GhiChu,
                                   RECORD_STATUS = o.RECORD_STATUS,
                                   MARKER_ID = o.MARKER_ID,
                                   AUTH_STATUS = o.AUTH_STATUS,
                                   CHECKER_ID = o.CHECKER_ID,
                                   APPROVE_DT = o.APPROVE_DT,
                                   ThoiHanHopDong = o.ThoiHanHopDong,
                                   Id = o.Id
                               },
                               LoaiHopDongValue = joinelhd == null ? "" : joinelhd.TenTemplate.ToString(),
                               ViTriCongViecValue = joinedtunit1 == null ? "" : joinedtunit1.DisplayName.ToString(),
                               DonViCongTacValue = joinedtunit == null ? "" : joinedtunit.DisplayName.ToString(),
                               ThoiHanhopDongTaoValue = joinedthhd == null ? "" : joinedthhd.Value.ToString(),
                              
                               HinhThucLamViecValue = joinedhtlv == null ? "" : joinedhtlv.Value.ToString(),
                           };

            var totalCount = await filteredHopDongs.CountAsync();

            return new PagedResultDto<GetHopDongForViewDto>(
                totalCount,
                await hopDongs.ToListAsync()
            );
        }
        public List<HopDongDto> GetAllHopDong()
        {
            return ObjectMapper.Map<List<HopDongDto>>(_hopDongRepository.GetAll().ToList());
        }


        public async Task<List<CreateOrEditHopDongDto>> GetHDForEdit(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<CreateOrEditHopDongDto>(sql: "SELECT * FROM  HopDong  WHERE IsDeleted = 0   and id =" + id);
                return result.ToList();
            }
        }
        public async Task<GetHopDongForEditOutput> GetListItemSearch()
        {
            var truongGiaoDichList = await _truongGiaoDichRepository.GetAll().ToListAsync();
            var orgList = await _organizationUnitAppService.GetOrganizationUnits();
            var templateList = _templateRepository.GetAll().ToList();
            var output = new GetHopDongForEditOutput();
            output.TemplateList = ObjectMapper.Map<List<TemplateDto>>(templateList.OrderBy(x => x.TenTemplate));
            output.HinhThucLamViec = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.HTLV)).OrderBy(x => x.Value));
            return output;
        }
        public async Task<List<TruongGiaoDichDto>> GetInFoLHD(string name)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<TruongGiaoDichDto>(sql: "dbo.GetInFoLHD", param: new { name }, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        public async Task<GetHopDongForViewDto> GetHopDongForView(int id)
        {
            var hopDong = await _hopDongRepository.GetAsync(id);

            var output = new GetHopDongForViewDto { HopDong = ObjectMapper.Map<HopDongDto>(hopDong) };

            return output;
        }

        //[AbpAuthorize(AppPermissions.Pages_HopDongs_Edit)]
        public async Task<GetHopDongForEditOutput> GetHopDongForEdit(EntityDto input)
        {

            var hopDong = await _hopDongRepository.FirstOrDefaultAsync(input.Id);
            var orgList = await _organizationUnitAppService.GetOrganizationUnits();

            var truongGiaoDichList = _truongGiaoDichRepository.GetAll().ToList();

            var output = new GetHopDongForEditOutput();
            if (hopDong != null)
            {
               
                if (hopDong.tenCTY != null)
                {
                    var templateList = _templateRepository.GetAll()
                        .Where(x => x.GhiChu.ToUpper().Equals(hopDong.tenCTY.ToUpper()))
                        .ToList();
                    output.TemplateList = ObjectMapper.Map<List<TemplateDto>>(templateList);
                }
                else
                {
                    output.TemplateList = null;
                  
                }
              
                output.LichSuUploadList = await _lichSuUploadsAppService.GetListLichSuUploadDto("HD", hopDong.Id.ToString());
              

            }

            output.OrganizationUnitList = orgList;
            output.HopDong = ObjectMapper.Map<CreateOrEditHopDongDto>(hopDong);
         

            //output.ViTriCongViec = ObjectMapper.Map<List<OrganizationUnit>>(orgList.Where(x => x.Code).OrderBy(x => x.DisplayName));
          
            output.TrangThai = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TRTH)).OrderBy(x => x.Value));
            
            output.Congty = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.CT)).OrderBy(x => x.Value));
            output.GioiTinh = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.GT)).OrderBy(x => x.Value));
            output.HinhThucLamViec = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.HTLV)).OrderBy(x => x.Value));
            output.ThoiHanHopDong = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.THHD)).OrderBy(x => x.Value));
            return output;
        }

        public async Task<int> CreateOrEdit(CreateOrEditHopDongInput hopDongInput)
        {
            var input = hopDongInput.HopDong;
            DateTime Ngay_Sinh;
            if (input.Id == null)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                        await conn.OpenAsync();
                    var tableName = "HopDong";
                    var tenCTY = input.TenCTY;
                    var result = await conn.QueryAsync<string>(sql: "dbo.SYS_CodeMasters_Gen ", param: new { tableName, tenCTY }, commandType: CommandType.StoredProcedure);
                    var id = await Create(input);
                    var lichSu = hopDongInput.LichSuUpLoad;
                    if (lichSu.Count > 0)
                    {
                        for (int i = 0; i < lichSu.Count; i++)

                        {
                            var lichSuUpload = new LichSuUpload();

                            lichSuUpload.TenFile = lichSu[i].TenFile;
                            lichSuUpload.TieuDe = lichSu[i].TieuDe;
                            lichSuUpload.DungLuong = lichSu[i].DungLuong;
                            lichSuUpload.Type = "HD";
                            lichSuUpload.TypeID = id.ToString();
                            await _lichSuUploadRepository.InsertAsync(lichSuUpload);
                        }
                    }

                    return id;
                }
            }
            else
            {
                await Update(input);
            }
            return 0;
        }

        //[AbpAuthorize(AppPermissions.Pages_HopDongs_Create)]
        protected virtual async Task<int> Create(CreateOrEditHopDongDto input)
        {
            var hopDong = ObjectMapper.Map<HopDong>(input);



         return     await _hopDongRepository.InsertAndGetIdAsync(hopDong);
        }

        //[AbpAuthorize(AppPermissions.Pages_HopDongs_Edit)]
        protected virtual async Task Update(CreateOrEditHopDongDto input)
        {
            var hopDong = await _hopDongRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, hopDong);
        }

        //[AbpAuthorize(AppPermissions.Pages_HopDongs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _hopDongRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetHopDongsToExcel(GetAllHopDongsForExcelInput input)
        {

            var filteredHopDongs = _hopDongRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.HoTenNhanVien.Contains(input.Filter) || e.ViTriCongViecCode.Contains(input.Filter) || e.SoHopDong.Contains(input.Filter) || e.TenHopDong.Contains(input.Filter) || e.LoaiHopDongCode.Contains(input.Filter) || e.HinhThucLamViecCode.Contains(input.Filter) || e.NguoiDaiDienCongTy.Contains(input.Filter) || e.ChucDanh.Contains(input.Filter) || e.TrichYeu.Contains(input.Filter) || e.TepDinhKem.Contains(input.Filter) || e.GhiChu.Contains(input.Filter) || e.RECORD_STATUS.Contains(input.Filter) || e.AUTH_STATUS.Contains(input.Filter) || e.ThoiHanHopDong.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.HoTenNhanVienFilter), e => e.HoTenNhanVien.ToLower() == input.HoTenNhanVienFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ViTriCongViecCodeFilter), e => e.ViTriCongViecCode.ToLower() == input.ViTriCongViecCodeFilter.ToLower().Trim())
                        .WhereIf(input.MinNgayKyFilter != null, e => e.NgayKy >= input.MinNgayKyFilter)
                        .WhereIf(input.MaxNgayKyFilter != null, e => e.NgayKy <= input.MaxNgayKyFilter)
                        .WhereIf(input.MinDonViCongTacIDFilter != null, e => e.DonViCongTacID >= input.MinDonViCongTacIDFilter)
                        .WhereIf(input.MaxDonViCongTacIDFilter != null, e => e.DonViCongTacID <= input.MaxDonViCongTacIDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenHopDongFilter), e => e.TenHopDong.ToLower() == input.TenHopDongFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LoaiHopDongCodeFilter), e => e.LoaiHopDongCode.ToLower() == input.LoaiHopDongCodeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.HinhThucLamViecCodeFilter), e => e.HinhThucLamViecCode.ToLower() == input.HinhThucLamViecCodeFilter.ToLower().Trim())
                        .WhereIf(input.MinNgayCoHieuLucFilter != null, e => e.NgayCoHieuLuc >= input.MinNgayCoHieuLucFilter)
                        .WhereIf(input.MaxNgayCoHieuLucFilter != null, e => e.NgayCoHieuLuc <= input.MaxNgayCoHieuLucFilter)
                        .WhereIf(input.MinNgayHetHanFilter != null, e => e.NgayHetHan >= input.MinNgayHetHanFilter)
                        .WhereIf(input.MaxNgayHetHanFilter != null, e => e.NgayHetHan <= input.MaxNgayHetHanFilter)
                        .WhereIf(input.MinLuongCoBanFilter != null, e => e.LuongCoBan >= input.MinLuongCoBanFilter)
                        .WhereIf(input.MaxLuongCoBanFilter != null, e => e.LuongCoBan <= input.MaxLuongCoBanFilter)
                        .WhereIf(input.MinLuongDongBaoHiemFilter != null, e => e.LuongDongBaoHiem >= input.MinLuongDongBaoHiemFilter)
                        .WhereIf(input.MaxLuongDongBaoHiemFilter != null, e => e.LuongDongBaoHiem <= input.MaxLuongDongBaoHiemFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ChucDanhFilter), e => e.ChucDanh.ToLower() == input.ChucDanhFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TrichYeuFilter), e => e.TrichYeu.ToLower() == input.TrichYeuFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RECORD_STATUSFilter), e => e.RECORD_STATUS.ToLower() == input.RECORD_STATUSFilter.ToLower().Trim())
                        .WhereIf(input.MinMARKER_IDFilter != null, e => e.MARKER_ID >= input.MinMARKER_IDFilter)
                        .WhereIf(input.MaxMARKER_IDFilter != null, e => e.MARKER_ID <= input.MaxMARKER_IDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AUTH_STATUSFilter), e => e.AUTH_STATUS.ToLower() == input.AUTH_STATUSFilter.ToLower().Trim())
                        .WhereIf(input.MinCHECKER_IDFilter != null, e => e.CHECKER_ID >= input.MinCHECKER_IDFilter)
                        .WhereIf(input.MaxCHECKER_IDFilter != null, e => e.CHECKER_ID <= input.MaxCHECKER_IDFilter)
                        .WhereIf(input.MinAPPROVE_DTFilter != null, e => e.APPROVE_DT >= input.MinAPPROVE_DTFilter)
                        .WhereIf(input.MaxAPPROVE_DTFilter != null, e => e.APPROVE_DT <= input.MaxAPPROVE_DTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ThoiHanHopDongFilter), e => e.ThoiHanHopDong.ToLower() == input.ThoiHanHopDongFilter.ToLower().Trim());
            var units = _organizationUnitRepository.GetAll();
            var tgd = _truongGiaoDichRepository.GetAll();
            

            var hopDongs = from o in filteredHopDongs
                           join vtcv in tgd.Where(x => x.Code == "VTCV")
                           on o.ViTriCongViecCode equals vtcv.CDName into vtcvJoin
                           from joinedvtcv in vtcvJoin.DefaultIfEmpty()

                           //join lhd in tgd.Where(x => x.Code == "LHD") on o.LoaiHopDongCode equals lhd.CDName into lhdJoin
                           //from joinedlhd in lhdJoin.DefaultIfEmpty()

                           join lhd in tgd.Where(x => x.Code == "LHDG") on o.LoaiHopDongCode equals lhd.CDName into lhdJoin
                           from joinedlhd in lhdJoin.DefaultIfEmpty()

                           join htlv in tgd.Where(x => x.Code == "HTLV") on o.HinhThucLamViecCode equals htlv.CDName into htlvJoin
                           from joinedhtlv in htlvJoin.DefaultIfEmpty()

                           join thhd in tgd.Where(x => x.Code == "THHD") on o.ThoiHanHopDong equals thhd.CDName into thhdJoin
                           from joinedthhd in thhdJoin.DefaultIfEmpty()

                           join unit in units on o.DonViCongTacID.Value equals unit.Id into unitjoin
                           from joinedtunit in unitjoin.DefaultIfEmpty()
                  
                         
                         select new GetHopDongForViewDto()
                         {
                             HopDong = new HopDongDto
                             {
                                 NhanVienId = o.NhanVienId,
                                 HoTenNhanVien = o.HoTenNhanVien,
                                 ViTriCongViecCode = o.ViTriCongViecCode,
                                 SoHopDong = o.SoHopDong,
                                 NgayKy = o.NgayKy,
                                 DonViCongTacID = o.DonViCongTacID,
                                 TenHopDong = o.TenHopDong,
                                 LoaiHopDongCode = o.LoaiHopDongCode,
                                 HinhThucLamViecCode = o.HinhThucLamViecCode,
                                 NgayCoHieuLuc = o.NgayCoHieuLuc,
                                 NgayHetHan = o.NgayHetHan,
                                 LuongCoBan = (o.LuongCoBan ?? 0).ToString("#,###"),
                                 LuongDongBaoHiem = (o.LuongDongBaoHiem ?? 0).ToString("#,###"),
                                 TyLeHuongLuong = o.TyLeHuongLuong,
                                 NguoiDaiDienCongTy = o.NguoiDaiDienCongTy,
                                 ChucDanh = o.ChucDanh,
                                 TrichYeu = o.TrichYeu,
                                 TepDinhKem = o.TepDinhKem,
                                 GhiChu = o.GhiChu,
                                 RECORD_STATUS = o.RECORD_STATUS,
                                 MARKER_ID = o.MARKER_ID,
                                 AUTH_STATUS = o.AUTH_STATUS,
                                 CHECKER_ID = o.CHECKER_ID,
                                 APPROVE_DT = o.APPROVE_DT,
                                 ThoiHanHopDong = o.ThoiHanHopDong,
                                 Id = o.Id
                             },
                             DonViCongTacValue = joinedtunit == null ? "" : joinedtunit.DisplayName.ToString(),
                             ThoiHanhopDongTaoValue = joinedthhd == null ? "" : joinedthhd.Value.ToString(),
                             LoaiHopDongValue = joinedlhd == null ? "" : joinedlhd.Value.ToString(),
                             HinhThucLamViecValue = joinedhtlv == null ? "" : joinedhtlv.Value.ToString(),
                         };


            var hopDongListDtos = await hopDongs.ToListAsync();

            return _hopDongsExcelExporter.ExportToFile(hopDongListDtos);
        }

    }
}