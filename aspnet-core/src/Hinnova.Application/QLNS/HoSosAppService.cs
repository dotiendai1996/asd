

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
using Microsoft.AspNetCore.Hosting;
using Hinnova.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Abp.UI;
using Abp.Organizations;
using GemBox.Spreadsheet;
using System.IO;
using Hinnova.Organizations.Dto;
using Hinnova.Authorization.Users.Importing;

namespace Hinnova.QLNS
{
    //[AbpAuthorize(AppPermissions.Pages_HoSos)]
    public class HoSosAppService : HinnovaAppServiceBase, IHoSosAppService
    {
        private readonly IRepository<HoSo> _hoSoRepository;

        private readonly IWebHostEnvironment _env;
        private readonly IRepository<Template> _templateRepository;
        private readonly IRepository<LichSuUpload> _lichSuUploadRepository;
        private readonly IRepository<DangKyKCB> _dangKyKCBRepository;
        private readonly IHoSosExcelExporter _hoSosExcelExporter;
        private readonly IRepository<TruongGiaoDich> _truongGiaoDichRepository;
        private readonly IRepository<TinhThanh> _tinhThanhRepository;
        private readonly IRepository<HopDong> _hopDongRepository;
        private readonly IRepository<NoiDaoTao> _noiDaoTaoRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IHoSoListExcelDataReader _hoSoListExcelDataReader;
        private IQuyTrinhCongTacsAppService _quyTrinhCongTacsAppService;

        private ILichSuUploadsAppService _lichSuUploadsAppService;

        private readonly string connectionString;
        private readonly string mes;

        public HoSosAppService(IRepository<Template> templateRepository,
            IRepository<LichSuUpload> lichSuUploadRepository,
            ILichSuUploadsAppService lichSuUploadsAppService,
            IRepository<DangKyKCB> dangKyKCBRepository,
            IWebHostEnvironment env,
            IWebHostEnvironment hostingEnvironment,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<HopDong> hopDongRepository,
            IRepository<NoiDaoTao> noiDaoTaoRepository,
            IRepository<TinhThanh> tinhThanhRepository,
            IRepository<TruongGiaoDich> truongGiaoDichRepository,
            IRepository<HoSo> hoSoRepository,
            IHoSosExcelExporter hoSosExcelExporter,
            IHoSoListExcelDataReader hoSoListExcelDataReader,
            IQuyTrinhCongTacsAppService quyTrinhCongTacsAppService)
        {
            _hoSoRepository = hoSoRepository;
            _env = hostingEnvironment;
            _templateRepository = templateRepository;
            _dangKyKCBRepository = dangKyKCBRepository;
            _hopDongRepository = hopDongRepository;
            _lichSuUploadRepository = lichSuUploadRepository;
            _hoSosExcelExporter = hoSosExcelExporter;
            _organizationUnitRepository = organizationUnitRepository;
            _truongGiaoDichRepository = truongGiaoDichRepository;
            _noiDaoTaoRepository = noiDaoTaoRepository;
            _hoSoListExcelDataReader = hoSoListExcelDataReader;

            _tinhThanhRepository = tinhThanhRepository;
            _lichSuUploadsAppService = lichSuUploadsAppService;
            _quyTrinhCongTacsAppService = quyTrinhCongTacsAppService;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");
        }

        public async Task<List<HoSoDto>> GetHoSoForViewNghiPhep(string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }

                //    var name = GetCurrentUser();
                string sql = $"SELECT * FROM  HoSo WHERE IsDeleted = 0 and MaNhanVien = '{id}' ";
                var result = await conn.QueryAsync<HoSoDto>(sql: sql);
                return result.ToList();
            }
        }

        public bool CheckCMND(string cmnd)
        {
            if (cmnd.IsNullOrEmpty())
            {
                Logger.Warn("CMND is Null");
                return false;
            }
            var x = _hoSoRepository.GetAll().Where(t => t.IsDeleted == false).Any(k => k.SoCMND == cmnd);
            return x;
        }

        public List<HoSoDto> GetAllCMND()
        {
            return _hoSoRepository.GetAll().Where(t => t.IsDeleted == false).Select(t => new HoSoDto { Id = t.Id, SoCMND = t.SoCMND }).ToList();
        }

        public IQueryable<OrganizationUnit> GetAllCMND1()
        {
            return _organizationUnitRepository.GetAll().Where(t => t.IsDeleted == false).Select(t => new OrganizationUnit { Id = t.Id, DisplayName = t.DisplayName });
        }

        public async Task<List<string>> GetAllUnit()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<string>(sql: "SELECT DisplayName FROM dbo.AbpOrganizationUnits WHERE IsDeleted = 0");
                return result.ToList();
            }
        }

        public async Task<List<OrganizationUnitDto>> GetAllPB()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<OrganizationUnitDto>(sql: "SELECT * FROM dbo.AbpOrganizationUnits WHERE IsDeleted = 0");
                return result.ToList();
            }
        }



        public async Task<List<CreateOrEditHoSoDto>> GetListNhanVienToCty(string TenCty)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<CreateOrEditHoSoDto>(sql: "SELECT * FROM  HoSo WHERE IsDeleted = 0 and TenCty=N'" + TenCty + "'");
                return result.ToList();
            }
        }


        public async Task<List<TruongGiaoDichDto>> GetAllTHHD()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<TruongGiaoDichDto>(sql: "  select * from TruongGiaoDich tg where tg.code= N'THHD'and  tg.isDeleted = 0 ");
                return result.ToList();
            }
        }


        public async Task<List<OrganizationUnitDto>> GetAllCongViec(int parentID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }

                string sql = $"SELECT* FROM  AbpOrganizationUnits WHERE IsDeleted = 0   and ParentId = {parentID} Order By DisplayName asc";

                var result = await conn.QueryAsync<OrganizationUnitDto>(sql: sql);
                return result.ToList();
            }
        }

        public async Task<List<HoSoDto>> GetAllUserPhongBan(int phongbanID, int? phongbanGD)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }

                if (phongbanGD == null)
                {
                    string sql =
                        $"select * from hoso where IsDeleted = 0   and DonViCongTacID ={phongbanID} ";

                    var result = await conn.QueryAsync<HoSoDto>(sql: sql);
                    return result.ToList();
                }
                else
                {
                    string sql =
                        $"select * from hoso where IsDeleted = 0   and DonViCongTacID ={phongbanID}  or DonViCongTacID =221  UNION 	select* from hoso where IsDeleted = 0   and DonViCongTacID = {phongbanGD} ";

                    var result = await conn.QueryAsync<HoSoDto>(sql: sql);
                    return result.ToList();
                }

            }
        }

        public async Task<List<HoSoDto>> getHoSoNhanVien(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                string sql =
                    $"select * from HoSo join AbpUsers on HoSo.MaNhanVien = AbpUsers.EmployeeCode where HoSo.IsDeleted = 0 and AbpUsers.id ={userId}";

                var result = await conn.QueryAsync<HoSoDto>(sql: sql);
                return result.ToList();
            }

        }


        public async Task<List<HoSoDto>> GetAllHoSo()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }

                string sql = $"SELECT * FROM HoSo WHERE IsDeleted = 0";

                var result = await conn.QueryAsync<HoSoDto>(sql: sql);
                return result.ToList();
            }
        }

        public async Task<PagedResultDto<GetHoSoForViewDto>> GetAll(GetAllHoSosInput input)
        {

            var filteredHoSos = _hoSoRepository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    e => false || e.TenCty.Contains(input.Filter) || e.HopDongHienTai.Contains(input.Filter) ||
                         e.SoHD.Contains(input.Filter) || e.DonViCongTacName.Contains(input.Filter) ||
                         e.ChoNgoi.Contains(input.Filter) ||
                         e.SoTheBHYT.Contains(input.Filter) || e.TkNganHang.Contains(input.Filter) ||
                         e.QuanLyTrucTiep.Contains(input.Filter) || e.ChucDanh.Contains(input.Filter) ||
                         e.MaChamCong.Contains(input.Filter) || e.SDT.Contains(input.Filter) ||
                         e.SoCMND.Contains(input.Filter) || e.GioiTinhCode.Contains(input.Filter) ||
                         e.HoVaTen.Contains(input.Filter) || e.MaNhanVien.Contains(input.Filter))
                .WhereIf(!string.IsNullOrWhiteSpace(input.TenCtyFilter), e => e.TenCty == input.TenCtyFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.HopDongHienTaiFilter),
                    e => e.HopDongHienTai == input.HopDongHienTaiFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.SoHDFilter), e => e.SoHD == input.SoHDFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.DonViCongTacNameFilter),
                    e => e.DonViCongTacName == input.DonViCongTacNameFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.ChoNgoiFilter), e => e.ChoNgoi == input.ChoNgoiFilter)
                .WhereIf(input.MinNoiDaoTaoIDFilter != null, e => e.NoiDaoTaoID >= input.MinNoiDaoTaoIDFilter)
                .WhereIf(input.MaxNoiDaoTaoIDFilter != null, e => e.NoiDaoTaoID <= input.MaxNoiDaoTaoIDFilter)
                .WhereIf(input.MinLoaiHopDongIDFilter != null, e => e.LoaiHopDongID >= input.MinLoaiHopDongIDFilter)
                .WhereIf(input.MaxLoaiHopDongIDFilter != null, e => e.LoaiHopDongID <= input.MaxLoaiHopDongIDFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.MaSoNoiKCBFilter),
                    e => e.MaSoNoiKCB == input.MaSoNoiKCBFilter)
                .WhereIf(input.MinNoiDangKyKCBIDFilter != null, e => e.NoiDangKyKCBID >= input.MinNoiDangKyKCBIDFilter)
                .WhereIf(input.MaxNoiDangKyKCBIDFilter != null, e => e.NoiDangKyKCBID <= input.MaxNoiDangKyKCBIDFilter)
                .WhereIf(input.MinNgayHetHanBHYTFilter != null, e => e.NgayHetHanBHYT >= input.MinNgayHetHanBHYTFilter)
                .WhereIf(input.MaxNgayHetHanBHYTFilter != null, e => e.NgayHetHanBHYT <= input.MaxNgayHetHanBHYTFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.SoTheBHYTFilter), e => e.SoTheBHYT == input.SoTheBHYTFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.MaTinhCapFilter), e => e.MaTinhCap == input.MaTinhCapFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.MaSoBHXHFilter), e => e.MaSoBHXH == input.MaSoBHXHFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.SoSoBHXHFilter), e => e.SoSoBHXH == input.SoSoBHXHFilter)
                .WhereIf(input.MinTyLeDongBHFilter != null, e => e.TyLeDongBH >= input.MinTyLeDongBHFilter)
                .WhereIf(input.MaxTyLeDongBHFilter != null, e => e.TyLeDongBH <= input.MaxTyLeDongBHFilter)
                .WhereIf(input.MinNgayThamGiaBHFilter != null, e => e.NgayThamGiaBH >= input.MinNgayThamGiaBHFilter)
                .WhereIf(input.MaxNgayThamGiaBHFilter != null, e => e.NgayThamGiaBH <= input.MaxNgayThamGiaBHFilter)
                .WhereIf(input.ThamGiaCongDoanFilter > -1,
                    e => (input.ThamGiaCongDoanFilter == 1 && e.ThamGiaCongDoan) ||
                         (input.ThamGiaCongDoanFilter == 0 && !e.ThamGiaCongDoan))
                .WhereIf(!string.IsNullOrWhiteSpace(input.NganHangCodeFilter),
                    e => e.NganHangCode == input.NganHangCodeFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.TkNganHangFilter),
                    e => e.TkNganHang == input.TkNganHangFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.DonViSoCongChuanCodeFilter),
                    e => e.DonViSoCongChuanCode == input.DonViSoCongChuanCodeFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.SoCongChuanFilter),
                    e => e.SoCongChuan == input.SoCongChuanFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.LuongDongBHFilter),
                    e => e.LuongDongBH == input.LuongDongBHFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.LuongCoBanFilter),
                    e => e.LuongCoBan == input.LuongCoBanFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.BacLuongCodeFilter),
                    e => e.BacLuongCode == input.BacLuongCodeFilter)
                .WhereIf(input.MinSoNgayPhepFilter != null, e => e.SoNgayPhep >= input.MinSoNgayPhepFilter)
                .WhereIf(input.MaxSoNgayPhepFilter != null, e => e.SoNgayPhep <= input.MaxSoNgayPhepFilter)
                .WhereIf(input.MinNgayChinhThucFilter != null, e => e.NgayChinhThuc >= input.MinNgayChinhThucFilter)
                .WhereIf(input.MaxNgayChinhThucFilter != null, e => e.NgayChinhThuc <= input.MaxNgayChinhThucFilter)
                .WhereIf(input.MinNgayThuViecFilter != null, e => e.NgayThuViec >= input.MinNgayThuViecFilter)
                .WhereIf(input.MaxNgayThuViecFilter != null, e => e.NgayThuViec <= input.MaxNgayThuViecFilter)
                .WhereIf(input.MinNgayTapSuFilter != null, e => e.NgayTapSu >= input.MinNgayTapSuFilter)
                .WhereIf(input.MaxNgayTapSuFilter != null, e => e.NgayTapSu <= input.MaxNgayTapSuFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.SoSoQLLaoDongFilter),
                    e => e.SoSoQLLaoDong == input.SoSoQLLaoDongFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.DiaDiemLamViecCodeFilter),
                    e => e.DiaDiemLamViecCode == input.DiaDiemLamViecCodeFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.QuanLyGianTiepFilter),
                    e => e.QuanLyGianTiep == input.QuanLyGianTiepFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.QuanLyTrucTiepFilter),
                    e => e.QuanLyTrucTiep == input.QuanLyTrucTiepFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.TrangThaiLamViecCodeFilter),
                    e => e.TrangThaiLamViecCode == input.TrangThaiLamViecCodeFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.BacFilter), e => e.Bac == input.BacFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.CapFilter), e => e.Cap == input.CapFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.ChucDanhFilter), e => e.ChucDanh == input.ChucDanhFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.MaChamCongFilter),
                    e => e.MaChamCong == input.MaChamCongFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiLHKCFilter),
                    e => e.DiaChiLHKC == input.DiaChiLHKCFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.EmailLHKCFilter), e => e.EmailLHKC == input.EmailLHKCFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.DtDiDongLHKCFilter),
                    e => e.DtDiDongLHKC == input.DtDiDongLHKCFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.DtNhaRiengLHKCFilter),
                    e => e.DtNhaRiengLHKC == input.DtNhaRiengLHKCFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.QuanHeLHKCFilter),
                    e => e.QuanHeLHKC == input.QuanHeLHKCFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.HoVaTenLHKCFilter),
                    e => e.HoVaTenLHKC == input.HoVaTenLHKCFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiHNFilter), e => e.DiaChiHN == input.DiaChiHNFilter)
                .WhereIf(input.MinTinhThanhIDHNFilter != null, e => e.TinhThanhIDHN >= input.MinTinhThanhIDHNFilter)
                .WhereIf(input.MaxTinhThanhIDHNFilter != null, e => e.TinhThanhIDHN <= input.MaxTinhThanhIDHNFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.QuocGiaHNFilter), e => e.QuocGiaHN == input.QuocGiaHNFilter)
                .WhereIf(input.LaChuHoFilter > -1,
                    e => (input.LaChuHoFilter == 1 && e.LaChuHo) || (input.LaChuHoFilter == 0 && !e.LaChuHo))
                .WhereIf(!string.IsNullOrWhiteSpace(input.MaSoHoGiaDinhFilter),
                    e => e.MaSoHoGiaDinh == input.MaSoHoGiaDinhFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.SoSoHoKhauFilter),
                    e => e.SoSoHoKhau == input.SoSoHoKhauFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiHKTTFilter),
                    e => e.DiaChiHKTT == input.DiaChiHKTTFilter)
                .WhereIf(input.MinTinhThanhIDHKTTFilter != null,
                    e => e.TinhThanhIDHKTT >= input.MinTinhThanhIDHKTTFilter)
                .WhereIf(input.MaxTinhThanhIDHKTTFilter != null,
                    e => e.TinhThanhIDHKTT <= input.MaxTinhThanhIDHKTTFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.QuocGiaHKTTFilter),
                    e => e.QuocGiaHKTT == input.QuocGiaHKTTFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.FacebookFilter), e => e.Facebook == input.FacebookFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.SkypeFilter), e => e.Skype == input.SkypeFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.NoiSinhFilter), e => e.NoiSinh == input.NoiSinhFilter)
                .WhereIf(input.MinTinhThanhIDFilter != null, e => e.TinhThanhID >= input.MinTinhThanhIDFilter)
                .WhereIf(input.MaxTinhThanhIDFilter != null, e => e.TinhThanhID <= input.MaxTinhThanhIDFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.NguyenQuanFilter),
                    e => e.NguyenQuan == input.NguyenQuanFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.EmailKhacFilter), e => e.EmailKhac == input.EmailKhacFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.EmailCoQuanFilter),
                    e => e.EmailCoQuan == input.EmailCoQuanFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.EmailCaNhanFilter),
                    e => e.EmailCaNhan == input.EmailCaNhanFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.DtKhacFilter), e => e.DtKhac == input.DtKhacFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.DtNhaRiengFilter),
                    e => e.DtNhaRieng == input.DtNhaRiengFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.DtCoQuanFilter), e => e.DtCoQuan == input.DtCoQuanFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.DtDiDongFilter), e => e.DtDiDong == input.DtDiDongFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.TepDinhKemFilter),
                    e => e.TepDinhKem == input.TepDinhKemFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.TinhTrangHonNhanCodeFilter),
                    e => e.TinhTrangHonNhanCode == input.TinhTrangHonNhanCodeFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.XepLoaiCodeFilter),
                    e => e.XepLoaiCode == input.XepLoaiCodeFilter)
                .WhereIf(input.MinNamTotNghiepFilter != null, e => e.NamTotNghiep >= input.MinNamTotNghiepFilter)
                .WhereIf(input.MaxNamTotNghiepFilter != null, e => e.NamTotNghiep <= input.MaxNamTotNghiepFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.ChuyenNganhFilter),
                    e => e.ChuyenNganh == input.ChuyenNganhFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.KhoaFilter), e => e.Khoa == input.KhoaFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.TrinhDoDaoTaoCodeFilter),
                    e => e.TrinhDoDaoTaoCode == input.TrinhDoDaoTaoCodeFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.TrinhDoVanHoaFilter),
                    e => e.TrinhDoVanHoa == input.TrinhDoVanHoaFilter)
                .WhereIf(input.MinNgayHetHanFilter != null, e => e.NgayHetHan >= input.MinNgayHetHanFilter)
                .WhereIf(input.MaxNgayHetHanFilter != null, e => e.NgayHetHan <= input.MaxNgayHetHanFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.NoiCapFilter), e => e.NoiCap == input.NoiCapFilter)
                .WhereIf(input.MinNgayCapFilter != null, e => e.NgayCap >= input.MinNgayCapFilter)
                .WhereIf(input.MaxNgayCapFilter != null, e => e.NgayCap <= input.MaxNgayCapFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.SoCMNDFilter), e => e.SoCMND == input.SoCMNDFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.QuocTichFilter), e => e.QuocTich == input.QuocTichFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.TonGiaoFilter), e => e.TonGiao == input.TonGiaoFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.DanTocFilter), e => e.DanToc == input.DanTocFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.ViTriCongViecCodeFilter),
                    e => e.ViTriCongViecCode == input.ViTriCongViecCodeFilter)
                .WhereIf(input.MinDonViCongTacIDFilter != null, e => e.DonViCongTacID >= input.MinDonViCongTacIDFilter)
                .WhereIf(input.MaxDonViCongTacIDFilter != null, e => e.DonViCongTacID <= input.MaxDonViCongTacIDFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.MSTCaNhanFilter), e => e.MSTCaNhan == input.MSTCaNhanFilter)
                .WhereIf(input.MinNgaySinhFilter != null, e => e.NgaySinh >= input.MinNgaySinhFilter)
                .WhereIf(input.MaxNgaySinhFilter != null, e => e.NgaySinh <= input.MaxNgaySinhFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.GioiTinhCodeFilter),
                    e => e.GioiTinhCode == input.GioiTinhCodeFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.AnhDaiDienFilter),
                    e => e.AnhDaiDien == input.AnhDaiDienFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.HoVaTenFilter), e => e.HoVaTen == input.HoVaTenFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.MaNhanVienFilter),
                    e => e.MaNhanVien == input.MaNhanVienFilter);


            //.WhereIf(!string.IsNullOrWhiteSpace(input.ChiNhanhFilter), e => e.ChiNhanh == input.ChiNhanhFilter)
            //.WhereIf(!string.IsNullOrWhiteSpace(input.DVTFilter), e => e.DVT == input.DVTFilter)
            //.WhereIf(input.MinNgayKyHDKTHFilter != null, e => e.NgayKyHDKTH >= input.MinNgayKyHDKTHFilter)
            //.WhereIf(input.MaxNgayKyHDKTHFilter != null, e => e.NgayKyHDKTH <= input.MaxNgayKyHDKTHFilter)
            //.WhereIf(input.MinNgayKyHD36THFilter != null, e => e.NgayKyHD36TH >= input.MinNgayKyHD36THFilter)
            //.WhereIf(input.MaxNgayKyHD36THFilter != null, e => e.NgayKyHD36TH <= input.MaxNgayKyHD36THFilter)
            //.WhereIf(input.MinNgayKyHD12THFilter != null, e => e.NgayKyHD12TH >= input.MinNgayKyHD12THFilter)
            //.WhereIf(input.MaxNgayKyHD12THFilter != null, e => e.NgayKyHD12TH <= input.MaxNgayKyHD12THFilter)
            //.WhereIf(input.MinNgayKyHDTVFilter != null, e => e.NgayKyHDTV >= input.MinNgayKyHDTVFilter)
            //.WhereIf(input.MaxNgayKyHDTVFilter != null, e => e.NgayKyHDTV <= input.MaxNgayKyHDTVFilter)
            //.WhereIf(input.MinNgayKYHDCTVFilter != null, e => e.NgayKYHDCTV >= input.MinNgayKYHDCTVFilter)
            //.WhereIf(input.MaxNgayKYHDCTVFilter != null, e => e.NgayKYHDCTV <= input.MaxNgayKYHDCTVFilter)
            //.WhereIf(input.MinNgayKyHDKVFilter != null, e => e.NgayKyHDKV >= input.MinNgayKyHDKVFilter)
            //.WhereIf(input.MaxNgayKyHDKVFilter != null, e => e.NgayKyHDKV <= input.MaxNgayKyHDKVFilter)
            //.WhereIf(input.MinNgayKYHDTTFilter != null, e => e.NgayKYHDTT >= input.MinNgayKYHDTTFilter)
            //.WhereIf(input.MaxNgayKYHDTTFilter != null, e => e.NgayKYHDTT <= input.MaxNgayKYHDTTFilter)
            //.WhereIf(input.MinNgayKyHDFilter != null, e => e.NgayKyHD >= input.MinNgayKyHDFilter)
            //.WhereIf(input.MaxNgayKyHDFilter != null, e => e.NgayKyHD <= input.MaxNgayKyHDFilter);

            var pagedAndFilteredHoSos = filteredHoSos
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var tgd = _truongGiaoDichRepository.GetAll();

            var units = _organizationUnitRepository.GetAll();
            var lhds = _templateRepository.GetAll();

            var hoSos = from o in pagedAndFilteredHoSos



                        join htlv in tgd.Where(x => x.Code == "TTNS") on o.TrangThaiLamViecCode equals htlv.CDName into htlvJoin
                        from joinedhtlv in htlvJoin.DefaultIfEmpty()

                        join gt in tgd.Where(x => x.Code == "GT") on o.GioiTinhCode equals gt.CDName into gtJoin
                        from joinedgt in gtJoin.DefaultIfEmpty()

                        join tthn in tgd.Where(x => x.Code == "TTHN") on o.TinhTrangHonNhanCode equals tthn.CDName into tthnJoin
                        from joinedtthn in tthnJoin.DefaultIfEmpty()

                        join tddt in tgd.Where(x => x.Code == "TDDT") on o.TrinhDoDaoTaoCode equals tddt.CDName into tddtJoin
                        from joinedtddt in tddtJoin.DefaultIfEmpty()

                        join xl in tgd.Where(x => x.Code == "XLHL") on o.XepLoaiCode equals xl.CDName into xlJoin
                        from joinedxl in xlJoin.DefaultIfEmpty()

                            //join lhd in tgd.Where(x => x.Code == "LHD") on o.LoaiHopDongID equals lhd.Id into lhdJoin
                            //from joinelhd in lhdJoin.DefaultIfEmpty()

                        join lhd in lhds on o.LoaiHopDongID equals lhd.Id into lhdJoin
                        from joinelhd in lhdJoin.DefaultIfEmpty()

                        join ndt in _noiDaoTaoRepository.GetAll() on o.NoiDaoTaoID equals ndt.Id into ndtJoin
                        from joinedndt in ndtJoin.DefaultIfEmpty()

                        join unit in units on o.ViTriCongViecCode equals unit.Code into unitjoin1
                        from joinedtunit1 in unitjoin1.DefaultIfEmpty()

                        join unit in units on o.DonViCongTacID.Value equals unit.Id into unitjoin
                        from joinedtunit in unitjoin.DefaultIfEmpty()



                        join noiDangKy in _dangKyKCBRepository.GetAll() on o.NoiDangKyKCBID equals noiDangKy.Id into noiDangKyjoin
                        from joinedtnoiDangKy in noiDangKyjoin.DefaultIfEmpty()


                        select new GetHoSoForViewDto()
                        {
                            HoSo = new HoSoDto
                            {
                                MaNhanVien = o.MaNhanVien,
                                HoVaTen = o.HoVaTen,
                                AnhDaiDien = o.AnhDaiDien,
                                GioiTinhCode = o.GioiTinhCode,
                                NgaySinh = o.NgaySinh,
                                MSTCaNhan = o.MSTCaNhan,
                                DonViCongTacID = o.DonViCongTacID,
                                ViTriCongViecCode = o.ViTriCongViecCode,
                                DanToc = o.DanToc,
                                TonGiao = o.TonGiao,
                                QuocTich = o.QuocTich,
                                SoCMND = o.SoCMND,
                                NgayCap = o.NgayCap,
                                NoiCap = o.NoiCap,
                                NgayHetHan = o.NgayHetHan,
                                TrinhDoVanHoa = o.TrinhDoVanHoa,
                                TrinhDoDaoTaoCode = o.TrinhDoDaoTaoCode,
                                NoiDaoTaoID = o.NoiDaoTaoID,
                                Khoa = o.Khoa,
                                ChuyenNganh = o.ChuyenNganh,
                                NamTotNghiep = o.NamTotNghiep,
                                XepLoaiCode = o.XepLoaiCode,
                                TinhTrangHonNhanCode = o.TinhTrangHonNhanCode,
                                TepDinhKem = o.TepDinhKem,
                                DtDiDong = o.DtDiDong,
                                DtCoQuan = o.DtCoQuan,
                                DtNhaRieng = o.DtNhaRieng,
                                DtKhac = o.DtKhac,
                                EmailCaNhan = o.EmailCaNhan,
                                EmailCoQuan = o.EmailCoQuan,
                                EmailKhac = o.EmailKhac,
                                NguyenQuan = o.NguyenQuan,
                                TinhThanhID = o.TinhThanhID,
                                NoiSinh = o.NoiSinh,
                                Skype = o.Skype,
                                Facebook = o.Facebook,
                                QuocGiaHKTT = o.QuocGiaHKTT,
                                TinhThanhIDHKTT = o.TinhThanhIDHKTT,
                                DiaChiHKTT = o.DiaChiHKTT,
                                SoSoHoKhau = o.SoSoHoKhau,
                                MaSoHoGiaDinh = o.MaSoHoGiaDinh,
                                LaChuHo = o.LaChuHo,
                                QuocGiaHN = o.QuocGiaHN,
                                TinhThanhIDHN = o.TinhThanhIDHN,
                                DiaChiHN = o.DiaChiHN,
                                HoVaTenLHKC = o.HoVaTenLHKC,
                                QuanHeLHKC = o.QuanHeLHKC,
                                DtDiDongLHKC = o.DtDiDongLHKC,
                                DtNhaRiengLHKC = o.DtNhaRiengLHKC,
                                EmailLHKC = o.EmailLHKC,
                                DiaChiLHKC = o.DiaChiLHKC,
                                MaChamCong = o.MaChamCong,
                                ChucDanh = o.ChucDanh,
                                Cap = o.Cap,
                                Bac = o.Bac,
                                TrangThaiLamViecCode = o.TrangThaiLamViecCode,
                                QuanLyTrucTiep = o.QuanLyTrucTiep,
                                QuanLyGianTiep = o.QuanLyGianTiep,
                                DiaDiemLamViecCode = o.DiaDiemLamViecCode,
                                SoSoQLLaoDong = o.SoSoQLLaoDong,
                                LoaiHopDongID = o.LoaiHopDongID,
                                NgayTapSu = o.NgayTapSu,
                                NgayThuViec = o.NgayThuViec,
                                NgayChinhThuc = o.NgayChinhThuc,
                                SoNgayPhep = o.SoNgayPhep,
                                BacLuongCode = o.BacLuongCode,
                                LuongCoBan = o.LuongCoBan,
                                LuongDongBH = o.LuongDongBH,
                                SoCongChuan = o.SoCongChuan,
                                DonViSoCongChuanCode = o.DonViSoCongChuanCode,
                                TkNganHang = o.TkNganHang,
                                NganHangCode = o.NganHangCode,
                                ThamGiaCongDoan = o.ThamGiaCongDoan,
                                NgayThamGiaBH = o.NgayThamGiaBH,
                                TyLeDongBH = o.TyLeDongBH,
                                SoSoBHXH = o.SoSoBHXH,
                                MaSoBHXH = o.MaSoBHXH,
                                MaTinhCap = o.MaTinhCap,
                                SoTheBHYT = o.SoTheBHYT,
                                NgayHetHanBHYT = o.NgayHetHanBHYT,
                                NoiDangKyKCBID = o.NoiDangKyKCBID,
                                MaSoNoiKCB = o.MaSoNoiKCB,
                                SDT = o.SDT,
                                //	TrangThaiLamViecCode= o.TrangThaiLamViecCode,
                                TenCty = o.TenCty,
                                ChoNgoi = o.ChoNgoi,
                                SoHD = o.SoHD,
                                HopDongHienTai = o.HopDongHienTai,
                                //AUTH_STATUS = o.AUTH_STATUS,
                                //RECORD_STATUS = o.RECORD_STATUS,
                                //MARKER_ID = o.MARKER_ID,
                                //CHECKER_ID = o.CHECKER_ID,
                                //APPROVE_DT = o.APPROVE_DT,
                                Id = o.Id
                            },
                            TinhTrangNhanSuValue = joinedhtlv == null ? "" : joinedhtlv.Value.ToString(),
                            LoaiHopDongValue = joinelhd == null ? "" : joinelhd.TenTemplate.ToString(),
                            ViTriCongViecValue = joinedtunit1 == null ? "" : joinedtunit1.DisplayName.ToString(),
                            GioiTinhValue = joinedgt == null ? "" : joinedgt.Value.ToString(),
                            TinhTrangHonNhanValue = joinedtthn == null ? "" : joinedtthn.Value.ToString(),
                            TrinhDoDaoTaoValue = joinedtddt == null ? "" : joinedtddt.Value.ToString(),
                            XepLoaiValue = joinedxl == null ? "" : joinedxl.Value.ToString(),
                            NoiDaoTaoValue = joinedndt == null ? "" : joinedndt.TenNoiDaoTao.ToString(),
                            //TinhThanhIDHKTTValue = joinedtinhThanh1 == null ? "" : joinedtinhThanh1.TenTinhThanh.ToString(),
                            //TinhThanhValue = joinedtinhThanh2 == null ? "" : joinedtinhThanh2.TenTinhThanh.ToString(),
                            //TinhThanhIDHNValue = joinedtinhThanh3 == null ? "" : joinedtinhThanh3.TenTinhThanh.ToString(),
                            //HopDongValue = joinedthopdong == null ? "" : joinedthopdong.TenHopDong.ToString(),
                            DonViCongTacValue = joinedtunit == null ? "" : joinedtunit.DisplayName.ToString(),
                            NoiDangKyValue = joinedtnoiDangKy == null ? "" : joinedtnoiDangKy.TenNoiKCB.ToString(),
                        };

            var totalCount = await filteredHoSos.CountAsync();

            return new PagedResultDto<GetHoSoForViewDto>(
                totalCount,
                await hoSos.ToListAsync()
            );
        }


        public async Task<GetUngVienForEditOutput> GetListItemSearchHoSo()
        {
            var truongGiaoDichList = _truongGiaoDichRepository.GetAll().ToList();

            var output = new GetUngVienForEditOutput();
            output.ViTriCongViec = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.VTUT)).OrderBy(x => x.Value));
            output.TrangThai = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TTNS)).OrderBy(x => x.Value));
            //output.Congty = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.CT)).OrderBy(x => x.Value));
            //output.GioiTinh = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.GT)).OrderBy(x => x.Value));
            //output.TinhTrangHonNhan = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TTHN)).OrderBy(x => x.Value));

            return output;
        }

        public async Task<List<string>> GetNameUnit(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)

                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<string>(sql: "SELECT DisplayName FROM dbo.AbpOrganizationUnits WHERE IsDeleted = 0 and Id=" + id);
                return result.ToList();
            }
        }


        public async Task<List<ListInfoHoSo>> GetListInFoHoSo()
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<ListInfoHoSo>(sql: "dbo.GetListInfoHoSo", param: new { }, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<CreateOrEditHoSoDto>> GetListNhanVienToDataFromDate(string fromDate, string toDate, string tenCty)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<CreateOrEditHoSoDto>(sql: "dbo.GetListNhanVienToDateFromDate", param: new { fromDate, toDate, tenCty }, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        public async Task<CreateOrEditHoSoDto> GetHoSoForView(int id)
        {
            var hoso = await _hoSoRepository.GetAsync(id);
            return ObjectMapper.Map<CreateOrEditHoSoDto>(hoso);

        }

        //[AbpAuthorize(AppPermissions.Pages_HoSos_Edit)]
        public async Task<GetHoSoForEditOutput> GetHoSoForEdit(EntityDto input)
        {
            var hoSo = await _hoSoRepository.GetAsync(input.Id);
            var output = new GetHoSoForEditOutput();
            if (hoSo != null)
            {
                output.HoSo = ObjectMapper.Map<CreateOrEditHoSoDto>(hoSo);
                output.QuaTrinhCongTac = await _quyTrinhCongTacsAppService.GetAll(hoSo.MaHoSo);
                output.DanhSachCV = await GetAllCongViec(hoSo.DonViCongTacID ?? 0);
                output.LichSuUpload = await _lichSuUploadsAppService.GetListLichSuUploadDto("HS", hoSo.Id.ToString());
            }
            return output;
        }

        public async Task<int> CreateOrEdit(CreateOrEditHoSoInput hoSoInput)
        {
            var input = hoSoInput.HoSo;
            DateTime Ngay_Sinh;
            if (input.Id == null)
            {
                if (input.SoCMND.IsNullOrEmpty())
                {
                    input.SoCMND = null;
                }
                else
                {
                    if (CheckCMND(input.SoCMND))
                    {
                        throw new UserFriendlyException("Số CMND đã bị trùng");

                    }
                }
                Ngay_Sinh = Convert.ToDateTime(input.NgaySinh);
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(Ngay_Sinh.ToString("yyyyMMdd"));
                int age = (now - dob) / 10000;
                if (age < 18)
                {
                    throw new UserFriendlyException("Chưa đủ 18 tuổi ");
                }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                        await conn.OpenAsync();
                    var tableName = "HoSo";
                    var tenCTY = input.TenCty;
                    var result = await conn.QueryAsync<string>(sql: "dbo.SYS_CodeMasters_Gen_LHD ", param: new { tableName, tenCTY }, commandType: CommandType.StoredProcedure);
                    input.MaNhanVien = result.ToList().First();
                    input.MaHoSo = result.ToList().First();
                    var id = await Create(input);
                    var lichSu = hoSoInput.LichSuUpLoad;
                    if (lichSu.Count > 0)
                    {
                        for (int i = 0; i < lichSu.Count; i++)

                        {
                            var lichSuUpload = new LichSuUpload();

                            lichSuUpload.TenFile = lichSu[i].TenFile;
                            lichSuUpload.TieuDe = lichSu[i].TieuDe;
                            lichSuUpload.DungLuong = lichSu[i].DungLuong;
                            lichSuUpload.Type = "HS";
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

        //[AbpAuthorize(AppPermissions.Pages_HoSos_Create)]
        protected virtual async Task<int> Create(CreateOrEditHoSoDto input)
        {
            var hoSo = ObjectMapper.Map<HoSo>(input);



            return await _hoSoRepository.InsertAndGetIdAsync(hoSo);
        }

        //[AbpAuthorize(AppPermissions.Pages_HoSos_Edit)]
        protected virtual async Task Update(CreateOrEditHoSoDto input)
        {
            var hoSo = await _hoSoRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, hoSo);
        }

        //[AbpAuthorize(AppPermissions.Pages_HoSos_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _hoSoRepository.DeleteAsync(input.Id);
        }

        //public async Task<FileDto> GetHoSosToExcel(GetAllHoSosForExcelInput input)
        //{

        //	var filteredHoSos = _hoSoRepository.GetAll()
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MaNhanVien.Contains(input.Filter) || e.HoVaTen.Contains(input.Filter) || e.AnhDaiDien.Contains(input.Filter) || e.GioiTinhCode.Contains(input.Filter) || e.MSTCaNhan.Contains(input.Filter) || e.ViTriCongViecCode.Contains(input.Filter) || e.DanToc.Contains(input.Filter) || e.TonGiao.Contains(input.Filter) || e.QuocTich.Contains(input.Filter) || e.SoCMND.Contains(input.Filter) || e.NoiCap.Contains(input.Filter) || e.TrinhDoVanHoa.Contains(input.Filter) || e.TrinhDoDaoTaoCode.Contains(input.Filter) || e.Khoa.Contains(input.Filter) || e.ChuyenNganh.Contains(input.Filter) || e.XepLoaiCode.Contains(input.Filter) || e.TinhTrangHonNhanCode.Contains(input.Filter) || e.TepDinhKem.Contains(input.Filter) || e.DtDiDong.Contains(input.Filter) || e.DtCoQuan.Contains(input.Filter) || e.DtNhaRieng.Contains(input.Filter) || e.DtKhac.Contains(input.Filter) || e.EmailCaNhan.Contains(input.Filter) || e.EmailCoQuan.Contains(input.Filter) || e.EmailKhac.Contains(input.Filter) || e.NguyenQuan.Contains(input.Filter) || e.NoiSinh.Contains(input.Filter) || e.Skype.Contains(input.Filter) || e.Facebook.Contains(input.Filter) || e.QuocGiaHKTT.Contains(input.Filter) || e.DiaChiHKTT.Contains(input.Filter) || e.SoSoHoKhau.Contains(input.Filter) || e.MaSoHoGiaDinh.Contains(input.Filter) || e.QuocGiaHN.Contains(input.Filter) || e.DiaChiHN.Contains(input.Filter) || e.HoVaTenLHKC.Contains(input.Filter) || e.QuanHeLHKC.Contains(input.Filter) || e.DtDiDongLHKC.Contains(input.Filter) || e.DtNhaRiengLHKC.Contains(input.Filter) || e.EmailLHKC.Contains(input.Filter) || e.DiaChiLHKC.Contains(input.Filter) || e.MaChamCong.Contains(input.Filter) || e.ChucDanh.Contains(input.Filter) || e.Cap.Contains(input.Filter) || e.Bac.Contains(input.Filter) || e.TrangThaiLamViecCode.Contains(input.Filter) || e.QuanLyTrucTiep.Contains(input.Filter) || e.QuanLyGianTiep.Contains(input.Filter) || e.DiaDiemLamViecCode.Contains(input.Filter) || e.SoSoQLLaoDong.Contains(input.Filter) || e.BacLuongCode.Contains(input.Filter) || e.DonViSoCongChuanCode.Contains(input.Filter) || e.TkNganHang.Contains(input.Filter) || e.NganHangCode.Contains(input.Filter) || e.SoSoBHXH.Contains(input.Filter) || e.MaSoBHXH.Contains(input.Filter) || e.MaTinhCap.Contains(input.Filter) || e.SoTheBHYT.Contains(input.Filter) || e.MaSoNoiKCB.Contains(input.Filter) || e.AUTH_STATUS.Contains(input.Filter) || e.RECORD_STATUS.Contains(input.Filter))
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.MaNhanVienFilter), e => e.MaNhanVien.ToLower() == input.MaNhanVienFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.HoVaTenFilter), e => e.HoVaTen.ToLower() == input.HoVaTenFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.AnhDaiDienFilter), e => e.AnhDaiDien.ToLower() == input.AnhDaiDienFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.GioiTinhCodeFilter), e => e.GioiTinhCode.ToLower() == input.GioiTinhCodeFilter.ToLower().Trim())
        //				.WhereIf(input.MinNgaySinhFilter != null, e => e.NgaySinh >= input.MinNgaySinhFilter)
        //				.WhereIf(input.MaxNgaySinhFilter != null, e => e.NgaySinh <= input.MaxNgaySinhFilter)
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.MSTCaNhanFilter), e => e.MSTCaNhan.ToLower() == input.MSTCaNhanFilter.ToLower().Trim())
        //				.WhereIf(input.MinDonViCongTacIDFilter != null, e => e.DonViCongTacID >= input.MinDonViCongTacIDFilter)
        //				.WhereIf(input.MaxDonViCongTacIDFilter != null, e => e.DonViCongTacID <= input.MaxDonViCongTacIDFilter)
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.ViTriCongViecCodeFilter), e => e.ViTriCongViecCode.ToLower() == input.ViTriCongViecCodeFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.DanTocFilter), e => e.DanToc.ToLower() == input.DanTocFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.TonGiaoFilter), e => e.TonGiao.ToLower() == input.TonGiaoFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.QuocTichFilter), e => e.QuocTich.ToLower() == input.QuocTichFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.SoCMNDFilter), e => e.SoCMND.ToLower() == input.SoCMNDFilter.ToLower().Trim())
        //				.WhereIf(input.MinNgayCapFilter != null, e => e.NgayCap >= input.MinNgayCapFilter)
        //				.WhereIf(input.MaxNgayCapFilter != null, e => e.NgayCap <= input.MaxNgayCapFilter)
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.NoiCapFilter), e => e.NoiCap.ToLower() == input.NoiCapFilter.ToLower().Trim())
        //				.WhereIf(input.MinNgayHetHanFilter != null, e => e.NgayHetHan >= input.MinNgayHetHanFilter)
        //				.WhereIf(input.MaxNgayHetHanFilter != null, e => e.NgayHetHan <= input.MaxNgayHetHanFilter)
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.TrinhDoVanHoaFilter), e => e.TrinhDoVanHoa.ToLower() == input.TrinhDoVanHoaFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.TrinhDoDaoTaoCodeFilter), e => e.TrinhDoDaoTaoCode.ToLower() == input.TrinhDoDaoTaoCodeFilter.ToLower().Trim())
        //				.WhereIf(input.MinNoiDaoTaoIDFilter != null, e => e.NoiDaoTaoID >= input.MinNoiDaoTaoIDFilter)
        //				.WhereIf(input.MaxNoiDaoTaoIDFilter != null, e => e.NoiDaoTaoID <= input.MaxNoiDaoTaoIDFilter)
        //				//.WhereIf(!string.IsNullOrWhiteSpace(input.NoiDaoTaoCodeFilter),  e => e.NoiDaoTaoCode.ToLower() == input.NoiDaoTaoCodeFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.KhoaFilter), e => e.Khoa.ToLower() == input.KhoaFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.ChuyenNganhFilter), e => e.ChuyenNganh.ToLower() == input.ChuyenNganhFilter.ToLower().Trim())
        //				.WhereIf(input.MinNamTotNghiepFilter != null, e => e.NamTotNghiep >= input.MinNamTotNghiepFilter)
        //				.WhereIf(input.MaxNamTotNghiepFilter != null, e => e.NamTotNghiep <= input.MaxNamTotNghiepFilter)
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.XepLoaiCodeFilter), e => e.XepLoaiCode.ToLower() == input.XepLoaiCodeFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.TinhTrangHonNhanCodeFilter), e => e.TinhTrangHonNhanCode.ToLower() == input.TinhTrangHonNhanCodeFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.TepDinhKemFilter), e => e.TepDinhKem.ToLower() == input.TepDinhKemFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.DtDiDongFilter), e => e.DtDiDong.ToLower() == input.DtDiDongFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.DtCoQuanFilter), e => e.DtCoQuan.ToLower() == input.DtCoQuanFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.DtNhaRiengFilter), e => e.DtNhaRieng.ToLower() == input.DtNhaRiengFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.DtKhacFilter), e => e.DtKhac.ToLower() == input.DtKhacFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.EmailCaNhanFilter), e => e.EmailCaNhan.ToLower() == input.EmailCaNhanFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.EmailCoQuanFilter), e => e.EmailCoQuan.ToLower() == input.EmailCoQuanFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.EmailKhacFilter), e => e.EmailKhac.ToLower() == input.EmailKhacFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.NguyenQuanFilter), e => e.NguyenQuan.ToLower() == input.NguyenQuanFilter.ToLower().Trim())
        //				.WhereIf(input.MinTinhThanhIDFilter != null, e => e.TinhThanhID >= input.MinTinhThanhIDFilter)
        //				.WhereIf(input.MaxTinhThanhIDFilter != null, e => e.TinhThanhID <= input.MaxTinhThanhIDFilter)
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.NoiSinhFilter), e => e.NoiSinh.ToLower() == input.NoiSinhFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.SkypeFilter), e => e.Skype.ToLower() == input.SkypeFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.FacebookFilter), e => e.Facebook.ToLower() == input.FacebookFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.QuocGiaHKTTFilter), e => e.QuocGiaHKTT.ToLower() == input.QuocGiaHKTTFilter.ToLower().Trim())
        //				.WhereIf(input.MinTinhThanhIDHKTTFilter != null, e => e.TinhThanhIDHKTT >= input.MinTinhThanhIDHKTTFilter)
        //				.WhereIf(input.MaxTinhThanhIDHKTTFilter != null, e => e.TinhThanhIDHKTT <= input.MaxTinhThanhIDHKTTFilter)
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiHKTTFilter), e => e.DiaChiHKTT.ToLower() == input.DiaChiHKTTFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.SoSoHoKhauFilter), e => e.SoSoHoKhau.ToLower() == input.SoSoHoKhauFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.MaSoHoGiaDinhFilter), e => e.MaSoHoGiaDinh.ToLower() == input.MaSoHoGiaDinhFilter.ToLower().Trim())
        //				 .WhereIf(input.LaChuHoFilter > -1, e => (input.LaChuHoFilter == 1 && e.LaChuHo) || (input.LaChuHoFilter == 0 && !e.LaChuHo))
        //				//.WhereIf(input.LaChuHoFilter > -1,  e => Convert.ToInt32(e.LaChuHo) == input.LaChuHoFilter )
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.QuocGiaHNFilter), e => e.QuocGiaHN.ToLower() == input.QuocGiaHNFilter.ToLower().Trim())
        //				.WhereIf(input.MinTinhThanhIDHNFilter != null, e => e.TinhThanhIDHN >= input.MinTinhThanhIDHNFilter)
        //				.WhereIf(input.MaxTinhThanhIDHNFilter != null, e => e.TinhThanhIDHN <= input.MaxTinhThanhIDHNFilter)
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiHNFilter), e => e.DiaChiHN.ToLower() == input.DiaChiHNFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.HoVaTenLHKCFilter), e => e.HoVaTenLHKC.ToLower() == input.HoVaTenLHKCFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.QuanHeLHKCFilter), e => e.QuanHeLHKC.ToLower() == input.QuanHeLHKCFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.DtDiDongLHKCFilter), e => e.DtDiDongLHKC.ToLower() == input.DtDiDongLHKCFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.DtNhaRiengLHKCFilter), e => e.DtNhaRiengLHKC.ToLower() == input.DtNhaRiengLHKCFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.EmailLHKCFilter), e => e.EmailLHKC.ToLower() == input.EmailLHKCFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiLHKCFilter), e => e.DiaChiLHKC.ToLower() == input.DiaChiLHKCFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.MaChamCongFilter), e => e.MaChamCong.ToLower() == input.MaChamCongFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.ChucDanhFilter), e => e.ChucDanh.ToLower() == input.ChucDanhFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.CapFilter), e => e.Cap.ToLower() == input.CapFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.BacFilter), e => e.Bac.ToLower() == input.BacFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.TrangThaiLamViecCodeFilter), e => e.TrangThaiLamViecCode.ToLower() == input.TrangThaiLamViecCodeFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.QuanLyTrucTiepFilter), e => e.QuanLyTrucTiep.ToLower() == input.QuanLyTrucTiepFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.QuanLyGianTiepFilter), e => e.QuanLyGianTiep.ToLower() == input.QuanLyGianTiepFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.DiaDiemLamViecCodeFilter), e => e.DiaDiemLamViecCode.ToLower() == input.DiaDiemLamViecCodeFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.SoSoQLLaoDongFilter), e => e.SoSoQLLaoDong.ToLower() == input.SoSoQLLaoDongFilter.ToLower().Trim())
        //				.WhereIf(input.MinLoaiHopDongIDFilter != null, e => e.LoaiHopDongID >= input.MinLoaiHopDongIDFilter)
        //				.WhereIf(input.MaxLoaiHopDongIDFilter != null, e => e.LoaiHopDongID <= input.MaxLoaiHopDongIDFilter)

        //				//.WhereIf(!string.IsNullOrWhiteSpace(input.LoaiHopDongCodeFilter),  e => e.LoaiHopDongCode.ToLower() == input.LoaiHopDongCodeFilter.ToLower().Trim())
        //				.WhereIf(input.MinNgayTapSuFilter != null, e => e.NgayTapSu >= input.MinNgayTapSuFilter)
        //				.WhereIf(input.MaxNgayTapSuFilter != null, e => e.NgayTapSu <= input.MaxNgayTapSuFilter)
        //				.WhereIf(input.MinNgayThuViecFilter != null, e => e.NgayThuViec >= input.MinNgayThuViecFilter)
        //				.WhereIf(input.MaxNgayThuViecFilter != null, e => e.NgayThuViec <= input.MaxNgayThuViecFilter)
        //				.WhereIf(input.MinNgayChinhThucFilter != null, e => e.NgayChinhThuc >= input.MinNgayChinhThucFilter)
        //				.WhereIf(input.MaxNgayChinhThucFilter != null, e => e.NgayChinhThuc <= input.MaxNgayChinhThucFilter)
        //				.WhereIf(input.MinSoNgayPhepFilter != null, e => e.SoNgayPhep >= input.MinSoNgayPhepFilter)
        //				.WhereIf(input.MaxSoNgayPhepFilter != null, e => e.SoNgayPhep <= input.MaxSoNgayPhepFilter)
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.BacLuongCodeFilter), e => e.BacLuongCode.ToLower() == input.BacLuongCodeFilter.ToLower().Trim())
        //				.WhereIf(input.MinLuongCoBanFilter != null, e => e.LuongCoBan >= input.MinLuongCoBanFilter)
        //				.WhereIf(input.MaxLuongCoBanFilter != null, e => e.LuongCoBan <= input.MaxLuongCoBanFilter)
        //				.WhereIf(input.MinLuongDongBHFilter != null, e => e.LuongDongBH >= input.MinLuongDongBHFilter)
        //				.WhereIf(input.MaxLuongDongBHFilter != null, e => e.LuongDongBH <= input.MaxLuongDongBHFilter)
        //				.WhereIf(input.MinSoCongChuanFilter != null, e => e.SoCongChuan >= input.MinSoCongChuanFilter)
        //				.WhereIf(input.MaxSoCongChuanFilter != null, e => e.SoCongChuan <= input.MaxSoCongChuanFilter)
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.DonViSoCongChuanCodeFilter), e => e.DonViSoCongChuanCode.ToLower() == input.DonViSoCongChuanCodeFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.TkNganHangFilter), e => e.TkNganHang.ToLower() == input.TkNganHangFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.NganHangCodeFilter), e => e.NganHangCode.ToLower() == input.NganHangCodeFilter.ToLower().Trim())
        //			   .WhereIf(input.ThamGiaCongDoanFilter > -1, e => (input.ThamGiaCongDoanFilter == 1 && e.ThamGiaCongDoan) || (input.ThamGiaCongDoanFilter == 0 && !e.ThamGiaCongDoan))
        //				//.WhereIf(input.ThamGiaCongDoanFilter > -1,  e => Convert.ToInt32(e.ThamGiaCongDoan) == input.ThamGiaCongDoanFilter )
        //				.WhereIf(input.MinNgayThamGiaBHFilter != null, e => e.NgayThamGiaBH >= input.MinNgayThamGiaBHFilter)
        //				.WhereIf(input.MaxNgayThamGiaBHFilter != null, e => e.NgayThamGiaBH <= input.MaxNgayThamGiaBHFilter)
        //				.WhereIf(input.MinTyLeDongBHFilter != null, e => e.TyLeDongBH >= input.MinTyLeDongBHFilter)
        //				.WhereIf(input.MaxTyLeDongBHFilter != null, e => e.TyLeDongBH <= input.MaxTyLeDongBHFilter)
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.SoSoBHXHFilter), e => e.SoSoBHXH.ToLower() == input.SoSoBHXHFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.MaSoBHXHFilter), e => e.MaSoBHXH.ToLower() == input.MaSoBHXHFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.MaTinhCapFilter), e => e.MaTinhCap.ToLower() == input.MaTinhCapFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.SoTheBHYTFilter), e => e.SoTheBHYT.ToLower() == input.SoTheBHYTFilter.ToLower().Trim())
        //				.WhereIf(input.MinNgayHetHanBHYTFilter != null, e => e.NgayHetHanBHYT >= input.MinNgayHetHanBHYTFilter)
        //				.WhereIf(input.MaxNgayHetHanBHYTFilter != null, e => e.NgayHetHanBHYT <= input.MaxNgayHetHanBHYTFilter)
        //				.WhereIf(input.MinNoiDangKyKCBIDFilter != null, e => e.NoiDangKyKCBID >= input.MinNoiDangKyKCBIDFilter)
        //				.WhereIf(input.MaxNoiDangKyKCBIDFilter != null, e => e.NoiDangKyKCBID <= input.MaxNoiDangKyKCBIDFilter)
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.MaSoNoiKCBFilter), e => e.MaSoNoiKCB.ToLower() == input.MaSoNoiKCBFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.AUTH_STATUSFilter), e => e.AUTH_STATUS.ToLower() == input.AUTH_STATUSFilter.ToLower().Trim())
        //				.WhereIf(!string.IsNullOrWhiteSpace(input.RECORD_STATUSFilter), e => e.RECORD_STATUS.ToLower() == input.RECORD_STATUSFilter.ToLower().Trim())
        //				.WhereIf(input.MinMARKER_IDFilter != null, e => e.MARKER_ID >= input.MinMARKER_IDFilter)
        //				.WhereIf(input.MaxMARKER_IDFilter != null, e => e.MARKER_ID <= input.MaxMARKER_IDFilter)
        //				.WhereIf(input.MinCHECKER_IDFilter != null, e => e.CHECKER_ID >= input.MinCHECKER_IDFilter)
        //				.WhereIf(input.MaxCHECKER_IDFilter != null, e => e.CHECKER_ID <= input.MaxCHECKER_IDFilter)
        //				.WhereIf(input.MinAPPROVE_DTFilter != null, e => e.APPROVE_DT >= input.MinAPPROVE_DTFilter)
        //				.WhereIf(input.MaxAPPROVE_DTFilter != null, e => e.APPROVE_DT <= input.MaxAPPROVE_DTFilter);
        //	//var pagedAndFilteredHoSos = filteredHoSos
        //	//	.OrderBy(input.Sorting ?? "id asc")
        //	//	.PageBy(input);

        //	var tgd = _truongGiaoDichRepository.GetAll();
        //	var units = _organizationUnitRepository.GetAll();

        //	var hoSos = from o in filteredHoSos

        //				join gt in tgd.Where(x => x.Code == "GT") on o.GioiTinhCode equals gt.CDName into gtJoin
        //				from joinedgt in gtJoin.DefaultIfEmpty()

        //				join tthn in tgd.Where(x => x.Code == "TTHN") on o.TinhTrangHonNhanCode equals tthn.CDName into tthnJoin
        //				from joinedtthn in tthnJoin.DefaultIfEmpty()

        //				join tddt in tgd.Where(x => x.Code == "TDDT") on o.TrinhDoDaoTaoCode equals tddt.CDName into tddtJoin
        //				from joinedtddt in tddtJoin.DefaultIfEmpty()

        //				join lhd in tgd.Where(x => x.Code == "LHDG") on o.LoaiHopDongID equals lhd.Id into lhdJoin
        //				from joinelhd in lhdJoin.DefaultIfEmpty()

        //				join xl in tgd.Where(x => x.Code == "XLHL") on o.XepLoaiCode equals xl.CDName into xlJoin
        //				from joinedxl in xlJoin.DefaultIfEmpty()

        //				join ndt in _noiDaoTaoRepository.GetAll() on o.NoiDaoTaoID equals ndt.Id into ndtJoin
        //				from joinedndt in ndtJoin.DefaultIfEmpty()

        //				join tinhThanh1 in _tinhThanhRepository.GetAll() on o.TinhThanhIDHKTT equals tinhThanh1.Id into tinhThanh1Join
        //				from joinedtinhThanh1 in tinhThanh1Join.DefaultIfEmpty()

        //				join tinhThanh2 in _tinhThanhRepository.GetAll() on o.TinhThanhID equals tinhThanh2.Id into tinhThanh2Join
        //				from joinedtinhThanh2 in tinhThanh2Join.DefaultIfEmpty()

        //				join tinhThanh3 in _tinhThanhRepository.GetAll() on o.TinhThanhIDHN equals tinhThanh3.Id into tinhThanh3Join
        //				from joinedtinhThanh3 in tinhThanh3Join.DefaultIfEmpty()


        //				join hopdong in _hopDongRepository.GetAll() on o.LoaiHopDongID equals hopdong.Id into hopdongjoin
        //				from joinedthopdong in hopdongjoin.DefaultIfEmpty()

        //				join unit in units on o.DonViCongTacID.Value equals unit.Id into unitjoin
        //				from joinedtunit in unitjoin.DefaultIfEmpty()

        //				join noiDangKy in _dangKyKCBRepository.GetAll() on o.NoiDangKyKCBID.Value equals noiDangKy.Id into noiDangKyjoin
        //				from joinedtnoiDangKy in noiDangKyjoin.DefaultIfEmpty()

        //				select new GetHoSoForViewDto()
        //				{
        //					HoSo = new HoSoDto
        //					{
        //						MaNhanVien = o.MaNhanVien,
        //						HoVaTen = o.HoVaTen,
        //						AnhDaiDien = o.AnhDaiDien,
        //						GioiTinhValue = o.GioiTinhCode,
        //						NgaySinh = o.NgaySinh,
        //						MSTCaNhan = o.MSTCaNhan,
        //						DonViCongTacID = o.DonViCongTacID,
        //						ViTriCongViecValue = o.ViTriCongViecCode,
        //						DanToc = o.DanToc,
        //						TonGiao = o.TonGiao,
        //						QuocTich = o.QuocTich,
        //						SoCMND = o.SoCMND,
        //						NgayCap = o.NgayCap,
        //						NoiCap = o.NoiCap,
        //						NgayHetHan = o.NgayHetHan,
        //						TrinhDoVanHoa = o.TrinhDoVanHoa,
        //						TrinhDoDaoTaoCode = o.TrinhDoDaoTaoCode,
        //						NoiDaoTaoID = o.NoiDaoTaoID,
        //						Khoa = o.Khoa,
        //						ChuyenNganh = o.ChuyenNganh,
        //						NamTotNghiep = o.NamTotNghiep,
        //						XepLoaiCode = o.XepLoaiCode,
        //						TinhTrangHonNhanCode = o.TinhTrangHonNhanCode,
        //						TepDinhKem = o.TepDinhKem,
        //						DtDiDong = o.DtDiDong,
        //						DtCoQuan = o.DtCoQuan,
        //						DtNhaRieng = o.DtNhaRieng,
        //						DtKhac = o.DtKhac,
        //						EmailCaNhan = o.EmailCaNhan,
        //						EmailCoQuan = o.EmailCoQuan,
        //						EmailKhac = o.EmailKhac,
        //						NguyenQuan = o.NguyenQuan,
        //						TinhThanhID = o.TinhThanhID,
        //						NoiSinh = o.NoiSinh,
        //						Skype = o.Skype,
        //						Facebook = o.Facebook,
        //						QuocGiaHKTT = o.QuocGiaHKTT,
        //						TinhThanhIDHKTT = o.TinhThanhIDHKTT,
        //						DiaChiHKTT = o.DiaChiHKTT,
        //						SoSoHoKhau = o.SoSoHoKhau,
        //						MaSoHoGiaDinh = o.MaSoHoGiaDinh,
        //						LaChuHo = o.LaChuHo,
        //						QuocGiaHN = o.QuocGiaHN,
        //						TinhThanhIDHN = o.TinhThanhIDHN,
        //						DiaChiHN = o.DiaChiHN,
        //						HoVaTenLHKC = o.HoVaTenLHKC,
        //						QuanHeLHKC = o.QuanHeLHKC,
        //						DtDiDongLHKC = o.DtDiDongLHKC,
        //						DtNhaRiengLHKC = o.DtNhaRiengLHKC,
        //						EmailLHKC = o.EmailLHKC,
        //						DiaChiLHKC = o.DiaChiLHKC,
        //						MaChamCong = o.MaChamCong,
        //						ChucDanh = o.ChucDanh,
        //						Cap = o.Cap,
        //						Bac = o.Bac,
        //						TrangThaiLamViecCode = o.TrangThaiLamViecCode,
        //						QuanLyTrucTiep = o.QuanLyTrucTiep,
        //						QuanLyGianTiep = o.QuanLyGianTiep,
        //						DiaDiemLamViecCode = o.DiaDiemLamViecCode,
        //						SoSoQLLaoDong = o.SoSoQLLaoDong,
        //						HopDongValue = o.LoaiHopDongID,
        //						NgayTapSu = o.NgayTapSu,
        //						NgayThuViec = o.NgayThuViec,
        //						NgayChinhThuc = o.NgayChinhThuc,
        //						SoNgayPhep = o.SoNgayPhep,
        //						BacLuongCode = o.BacLuongCode,
        //						LuongCoBan = o.LuongCoBan,
        //						LuongDongBH = o.LuongDongBH,
        //						SoCongChuan = o.SoCongChuan,
        //						DonViSoCongChuanCode = o.DonViSoCongChuanCode,
        //						TkNganHang = o.TkNganHang,
        //						NganHangCode = o.NganHangCode,
        //						ThamGiaCongDoan = o.ThamGiaCongDoan,
        //						NgayThamGiaBH = o.NgayThamGiaBH,
        //						TyLeDongBH = o.TyLeDongBH,
        //						SoSoBHXH = o.SoSoBHXH,
        //						MaSoBHXH = o.MaSoBHXH,
        //						MaTinhCap = o.MaTinhCap,
        //						SoTheBHYT = o.SoTheBHYT,
        //						NgayHetHanBHYT = o.NgayHetHanBHYT,
        //						NoiDangKyKCBID = o.NoiDangKyKCBID,
        //						MaSoNoiKCB = o.MaSoNoiKCB,
        //						AUTH_STATUS = o.AUTH_STATUS,
        //						RECORD_STATUS = o.RECORD_STATUS,
        //						MARKER_ID = o.MARKER_ID,
        //						CHECKER_ID = o.CHECKER_ID,
        //						APPROVE_DT = o.APPROVE_DT,
        //						Id = o.Id
        //					},

        //					GioiTinhValue = joinedgt == null ? "" : joinedgt.Value.ToString(),
        //					TinhTrangHonNhanValue = joinedtthn == null ? "" : joinedtthn.Value.ToString(),
        //					LoaiHopDongValue = joinelhd == null ? "" : joinelhd.Value.ToString(),
        //					TrinhDoDaoTaoValue = joinedtddt == null ? "" : joinedtddt.Value.ToString(),
        //					XepLoaiValue = joinedxl == null ? "" : joinedxl.Value.ToString(),
        //					NoiDaoTaoValue = joinedndt == null ? "" : joinedndt.TenNoiDaoTao.ToString(),
        //					TinhThanhIDHKTTValue = joinedtinhThanh1 == null ? "" : joinedtinhThanh1.TenTinhThanh.ToString(),
        //					TinhThanhValue = joinedtinhThanh2 == null ? "" : joinedtinhThanh2.TenTinhThanh.ToString(),
        //					TinhThanhIDHNValue = joinedtinhThanh3 == null ? "" : joinedtinhThanh3.TenTinhThanh.ToString(),
        //					HopDongValue = joinedthopdong == null ? "" : joinedthopdong.TenHopDong.ToString(),
        //			        DonViCongTacValue = joinedtunit == null ? "" : joinedtunit.DisplayName.ToString(),
        //					NoiDangKyValue = joinedtnoiDangKy == null ? "" : joinedtnoiDangKy.TenNoiKCB.ToString(),
        //				};


        //	var hoSoListDtos = await hoSos.ToListAsync();

        //	return _hoSosExcelExporter.ExportToFile(hoSoListDtos);
        //}


        public async Task<int> GetMaTinhThanh(string tenTP)
        {

            var TP = await _tinhThanhRepository.FirstOrDefaultAsync(x => x.TenTinhThanh == tenTP);
            return TP.Id;
        }
        public async Task<int> GetMaNoiDaotao(string tenNDT)
        {
            var NDT = await _noiDaoTaoRepository.FirstOrDefaultAsync(x => x.TenNoiDaoTao.Contains(tenNDT));
            if (NDT == null)
            {
                return 926;
            }
            return NDT.Id;
        }

        public FileDto GetHoSoTemplateToExcel()
        {
            return _hoSosExcelExporter.ExportTemplateToFile();
        }

        public async Task<string> importToExcel(string currentTime, string path)
        {
            try
            {
                string date = DateTime.Today.ToString("dd-MM-yyyy");
                SpreadsheetInfo.SetLicense("ELAP-G41W-CZA2-XNNC");
                var pathExcelImport = Path.Combine(_env.WebRootPath, date, currentTime, path);
                var excelImportByteArray = await File.ReadAllBytesAsync(pathExcelImport);

                var TGD = _truongGiaoDichRepository.GetAll();
                var dvct = _organizationUnitRepository.GetAll();

                var dataImport = _hoSoListExcelDataReader.GetHoSoFromExcel(excelImportByteArray);
                foreach (var data in dataImport)
                {
                    var hoSo = ObjectMapper.Map<CreateOrEditHoSoDto>(data);

                    hoSo.ViTriCongViecCode = dvct.FirstOrDefault(x => x.DisplayName.Equals(data.ViTriCongViecCode))?.Code;
                    var donViCongTac = await dvct.FirstOrDefaultAsync(x => x.DisplayName.Equals(data.DonViCongTacName));
                    if (donViCongTac != null)
                        hoSo.DonViCongTacID = Convert.ToInt32(donViCongTac.Id);

                    var loaiHopDong = await TGD.FirstOrDefaultAsync(x => x.Code == "LHD" && x.Value.Equals(data.HopDongHienTai));
                    if (loaiHopDong != null)
                        hoSo.LoaiHopDongID = Convert.ToInt32(loaiHopDong.Id);

                    hoSo.GioiTinhCode = TGD.FirstOrDefault(x => x.Code == "GT" && x.Value.Equals(data.GioiTinhCode))?.CDName;
                    hoSo.TrinhDoDaoTaoCode = TGD.FirstOrDefault(x => x.Code == "TDDT" && x.Value.Equals(data.TrinhDoDaoTaoCode))?.CDName;
                    hoSo.NoiDaoTaoID = await GetMaNoiDaotao(data.NoiDaoTaoName);

                    hoSo.XepLoaiCode = TGD.FirstOrDefault(x => x.Code == "TDDT" && x.Value.Equals(data.XepLoaiCode))?.CDName;

                    CreateOrEditHoSoInput createOrEditHoSoInput = new CreateOrEditHoSoInput();
                    createOrEditHoSoInput.HoSo = hoSo;
                    await CreateOrEdit(createOrEditHoSoInput);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
                return "";
            }
        }
    }
}