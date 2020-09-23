

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
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

namespace Hinnova.QLNS
{
	//[AbpAuthorize(AppPermissions.Pages_ViTriCongViecs)]
    public class ViTriCongViecsAppService : HinnovaAppServiceBase, IViTriCongViecsAppService
    {
		 private readonly IRepository<ViTriCongViec> _viTriCongViecRepository;
		 private readonly IViTriCongViecsExcelExporter _viTriCongViecsExcelExporter;
		 

		  public ViTriCongViecsAppService(IRepository<ViTriCongViec> viTriCongViecRepository, IViTriCongViecsExcelExporter viTriCongViecsExcelExporter ) 
		  {
			_viTriCongViecRepository = viTriCongViecRepository;
			_viTriCongViecsExcelExporter = viTriCongViecsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetViTriCongViecForViewDto>> GetAll(GetAllViTriCongViecsInput input)
         {
			
			var filteredViTriCongViecs = _viTriCongViecRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.TenCty.Contains(input.Filter) || e.HopDongHienTai.Contains(input.Filter) || e.SoHD.Contains(input.Filter) || e.DonViCongTacName.Contains(input.Filter) || e.ChoNgoi.Contains(input.Filter) || e.MaSoNoiKCB.Contains(input.Filter) || e.SoTheBHYT.Contains(input.Filter) || e.MaTinhCap.Contains(input.Filter) || e.MaSoBHXH.Contains(input.Filter) || e.SoSoBHXH.Contains(input.Filter) || e.NganHangCode.Contains(input.Filter) || e.TkNganHang.Contains(input.Filter) || e.DonViSoCongChuanCode.Contains(input.Filter) || e.SoCongChuan.Contains(input.Filter) || e.LuongDongBH.Contains(input.Filter) || e.LuongCoBan.Contains(input.Filter) || e.BacLuongCode.Contains(input.Filter) || e.SoSoQLLaoDong.Contains(input.Filter) || e.DiaDiemLamViecCode.Contains(input.Filter) || e.QuanLyGianTiep.Contains(input.Filter) || e.QuanLyTrucTiep.Contains(input.Filter) || e.TrangThaiLamViecCode.Contains(input.Filter) || e.Bac.Contains(input.Filter) || e.Cap.Contains(input.Filter) || e.ChucDanh.Contains(input.Filter) || e.MaChamCong.Contains(input.Filter) || e.DiaChiLHKC.Contains(input.Filter) || e.EmailLHKC.Contains(input.Filter) || e.DtDiDongLHKC.Contains(input.Filter) || e.DtNhaRiengLHKC.Contains(input.Filter) || e.QuanHeLHKC.Contains(input.Filter) || e.HoVaTenLHKC.Contains(input.Filter) || e.DiaChiHN.Contains(input.Filter) || e.QuocGiaHN.Contains(input.Filter) || e.MaSoHoGiaDinh.Contains(input.Filter) || e.SoSoHoKhau.Contains(input.Filter) || e.DiaChiHKTT.Contains(input.Filter) || e.QuocGiaHKTT.Contains(input.Filter) || e.Facebook.Contains(input.Filter) || e.Skype.Contains(input.Filter) || e.NoiSinh.Contains(input.Filter) || e.NguyenQuan.Contains(input.Filter) || e.EmailKhac.Contains(input.Filter) || e.EmailCoQuan.Contains(input.Filter) || e.EmailCaNhan.Contains(input.Filter) || e.DtKhac.Contains(input.Filter) || e.DtNhaRieng.Contains(input.Filter) || e.DtCoQuan.Contains(input.Filter) || e.DtDiDong.Contains(input.Filter) || e.TepDinhKem.Contains(input.Filter) || e.TinhTrangHonNhanCode.Contains(input.Filter) || e.XepLoaiCode.Contains(input.Filter) || e.ChuyenNganh.Contains(input.Filter) || e.Khoa.Contains(input.Filter) || e.TrinhDoDaoTaoCode.Contains(input.Filter) || e.TrinhDoVanHoa.Contains(input.Filter) || e.NoiCap.Contains(input.Filter) || e.SoCMND.Contains(input.Filter) || e.QuocTich.Contains(input.Filter) || e.TonGiao.Contains(input.Filter) || e.DanToc.Contains(input.Filter) || e.ViTriCongViecCode.Contains(input.Filter) || e.MSTCaNhan.Contains(input.Filter) || e.GioiTinhCode.Contains(input.Filter) || e.AnhDaiDien.Contains(input.Filter) || e.HoVaTen.Contains(input.Filter) || e.MaNhanVien.Contains(input.Filter) || e.ChiNhanh.Contains(input.Filter) || e.DVT.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.TenCtyFilter),  e => e.TenCty == input.TenCtyFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.HopDongHienTaiFilter),  e => e.HopDongHienTai == input.HopDongHienTaiFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoHDFilter),  e => e.SoHD == input.SoHDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DonViCongTacNameFilter),  e => e.DonViCongTacName == input.DonViCongTacNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ChoNgoiFilter),  e => e.ChoNgoi == input.ChoNgoiFilter)
						.WhereIf(input.MinNoiDaoTaoIDFilter != null, e => e.NoiDaoTaoID >= input.MinNoiDaoTaoIDFilter)
						.WhereIf(input.MaxNoiDaoTaoIDFilter != null, e => e.NoiDaoTaoID <= input.MaxNoiDaoTaoIDFilter)
						.WhereIf(input.MinLoaiHopDongIDFilter != null, e => e.LoaiHopDongID >= input.MinLoaiHopDongIDFilter)
						.WhereIf(input.MaxLoaiHopDongIDFilter != null, e => e.LoaiHopDongID <= input.MaxLoaiHopDongIDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaSoNoiKCBFilter),  e => e.MaSoNoiKCB == input.MaSoNoiKCBFilter)
						.WhereIf(input.MinNoiDangKyKCBIDFilter != null, e => e.NoiDangKyKCBID >= input.MinNoiDangKyKCBIDFilter)
						.WhereIf(input.MaxNoiDangKyKCBIDFilter != null, e => e.NoiDangKyKCBID <= input.MaxNoiDangKyKCBIDFilter)
						.WhereIf(input.MinNgayHetHanBHYTFilter != null, e => e.NgayHetHanBHYT >= input.MinNgayHetHanBHYTFilter)
						.WhereIf(input.MaxNgayHetHanBHYTFilter != null, e => e.NgayHetHanBHYT <= input.MaxNgayHetHanBHYTFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoTheBHYTFilter),  e => e.SoTheBHYT == input.SoTheBHYTFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaTinhCapFilter),  e => e.MaTinhCap == input.MaTinhCapFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaSoBHXHFilter),  e => e.MaSoBHXH == input.MaSoBHXHFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoSoBHXHFilter),  e => e.SoSoBHXH == input.SoSoBHXHFilter)
						.WhereIf(input.MinTyLeDongBHFilter != null, e => e.TyLeDongBH >= input.MinTyLeDongBHFilter)
						.WhereIf(input.MaxTyLeDongBHFilter != null, e => e.TyLeDongBH <= input.MaxTyLeDongBHFilter)
						.WhereIf(input.MinNgayThamGiaBHFilter != null, e => e.NgayThamGiaBH >= input.MinNgayThamGiaBHFilter)
						.WhereIf(input.MaxNgayThamGiaBHFilter != null, e => e.NgayThamGiaBH <= input.MaxNgayThamGiaBHFilter)
						.WhereIf(input.ThamGiaCongDoanFilter > -1,  e => (input.ThamGiaCongDoanFilter == 1 && e.ThamGiaCongDoan) || (input.ThamGiaCongDoanFilter == 0 && !e.ThamGiaCongDoan) )
						.WhereIf(!string.IsNullOrWhiteSpace(input.NganHangCodeFilter),  e => e.NganHangCode == input.NganHangCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TkNganHangFilter),  e => e.TkNganHang == input.TkNganHangFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DonViSoCongChuanCodeFilter),  e => e.DonViSoCongChuanCode == input.DonViSoCongChuanCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoCongChuanFilter),  e => e.SoCongChuan == input.SoCongChuanFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.LuongDongBHFilter),  e => e.LuongDongBH == input.LuongDongBHFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.LuongCoBanFilter),  e => e.LuongCoBan == input.LuongCoBanFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.BacLuongCodeFilter),  e => e.BacLuongCode == input.BacLuongCodeFilter)
						.WhereIf(input.MinSoNgayPhepFilter != null, e => e.SoNgayPhep >= input.MinSoNgayPhepFilter)
						.WhereIf(input.MaxSoNgayPhepFilter != null, e => e.SoNgayPhep <= input.MaxSoNgayPhepFilter)
						.WhereIf(input.MinNgayChinhThucFilter != null, e => e.NgayChinhThuc >= input.MinNgayChinhThucFilter)
						.WhereIf(input.MaxNgayChinhThucFilter != null, e => e.NgayChinhThuc <= input.MaxNgayChinhThucFilter)
						.WhereIf(input.MinNgayThuViecFilter != null, e => e.NgayThuViec >= input.MinNgayThuViecFilter)
						.WhereIf(input.MaxNgayThuViecFilter != null, e => e.NgayThuViec <= input.MaxNgayThuViecFilter)
						.WhereIf(input.MinNgayTapSuFilter != null, e => e.NgayTapSu >= input.MinNgayTapSuFilter)
						.WhereIf(input.MaxNgayTapSuFilter != null, e => e.NgayTapSu <= input.MaxNgayTapSuFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoSoQLLaoDongFilter),  e => e.SoSoQLLaoDong == input.SoSoQLLaoDongFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaDiemLamViecCodeFilter),  e => e.DiaDiemLamViecCode == input.DiaDiemLamViecCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuanLyGianTiepFilter),  e => e.QuanLyGianTiep == input.QuanLyGianTiepFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuanLyTrucTiepFilter),  e => e.QuanLyTrucTiep == input.QuanLyTrucTiepFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TrangThaiLamViecCodeFilter),  e => e.TrangThaiLamViecCode == input.TrangThaiLamViecCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.BacFilter),  e => e.Bac == input.BacFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.CapFilter),  e => e.Cap == input.CapFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ChucDanhFilter),  e => e.ChucDanh == input.ChucDanhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaChamCongFilter),  e => e.MaChamCong == input.MaChamCongFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiLHKCFilter),  e => e.DiaChiLHKC == input.DiaChiLHKCFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailLHKCFilter),  e => e.EmailLHKC == input.EmailLHKCFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DtDiDongLHKCFilter),  e => e.DtDiDongLHKC == input.DtDiDongLHKCFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DtNhaRiengLHKCFilter),  e => e.DtNhaRiengLHKC == input.DtNhaRiengLHKCFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuanHeLHKCFilter),  e => e.QuanHeLHKC == input.QuanHeLHKCFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.HoVaTenLHKCFilter),  e => e.HoVaTenLHKC == input.HoVaTenLHKCFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiHNFilter),  e => e.DiaChiHN == input.DiaChiHNFilter)
						.WhereIf(input.MinTinhThanhIDHNFilter != null, e => e.TinhThanhIDHN >= input.MinTinhThanhIDHNFilter)
						.WhereIf(input.MaxTinhThanhIDHNFilter != null, e => e.TinhThanhIDHN <= input.MaxTinhThanhIDHNFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuocGiaHNFilter),  e => e.QuocGiaHN == input.QuocGiaHNFilter)
						.WhereIf(input.LaChuHoFilter > -1,  e => (input.LaChuHoFilter == 1 && e.LaChuHo) || (input.LaChuHoFilter == 0 && !e.LaChuHo) )
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaSoHoGiaDinhFilter),  e => e.MaSoHoGiaDinh == input.MaSoHoGiaDinhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoSoHoKhauFilter),  e => e.SoSoHoKhau == input.SoSoHoKhauFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiHKTTFilter),  e => e.DiaChiHKTT == input.DiaChiHKTTFilter)
						.WhereIf(input.MinTinhThanhIDHKTTFilter != null, e => e.TinhThanhIDHKTT >= input.MinTinhThanhIDHKTTFilter)
						.WhereIf(input.MaxTinhThanhIDHKTTFilter != null, e => e.TinhThanhIDHKTT <= input.MaxTinhThanhIDHKTTFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuocGiaHKTTFilter),  e => e.QuocGiaHKTT == input.QuocGiaHKTTFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.FacebookFilter),  e => e.Facebook == input.FacebookFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SkypeFilter),  e => e.Skype == input.SkypeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NoiSinhFilter),  e => e.NoiSinh == input.NoiSinhFilter)
						.WhereIf(input.MinTinhThanhIDFilter != null, e => e.TinhThanhID >= input.MinTinhThanhIDFilter)
						.WhereIf(input.MaxTinhThanhIDFilter != null, e => e.TinhThanhID <= input.MaxTinhThanhIDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NguyenQuanFilter),  e => e.NguyenQuan == input.NguyenQuanFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailKhacFilter),  e => e.EmailKhac == input.EmailKhacFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailCoQuanFilter),  e => e.EmailCoQuan == input.EmailCoQuanFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailCaNhanFilter),  e => e.EmailCaNhan == input.EmailCaNhanFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DtKhacFilter),  e => e.DtKhac == input.DtKhacFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DtNhaRiengFilter),  e => e.DtNhaRieng == input.DtNhaRiengFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DtCoQuanFilter),  e => e.DtCoQuan == input.DtCoQuanFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DtDiDongFilter),  e => e.DtDiDong == input.DtDiDongFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TepDinhKemFilter),  e => e.TepDinhKem == input.TepDinhKemFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TinhTrangHonNhanCodeFilter),  e => e.TinhTrangHonNhanCode == input.TinhTrangHonNhanCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.XepLoaiCodeFilter),  e => e.XepLoaiCode == input.XepLoaiCodeFilter)
						.WhereIf(input.MinNamTotNghiepFilter != null, e => e.NamTotNghiep >= input.MinNamTotNghiepFilter)
						.WhereIf(input.MaxNamTotNghiepFilter != null, e => e.NamTotNghiep <= input.MaxNamTotNghiepFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ChuyenNganhFilter),  e => e.ChuyenNganh == input.ChuyenNganhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.KhoaFilter),  e => e.Khoa == input.KhoaFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TrinhDoDaoTaoCodeFilter),  e => e.TrinhDoDaoTaoCode == input.TrinhDoDaoTaoCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TrinhDoVanHoaFilter),  e => e.TrinhDoVanHoa == input.TrinhDoVanHoaFilter)
						.WhereIf(input.MinNgayHetHanFilter != null, e => e.NgayHetHan >= input.MinNgayHetHanFilter)
						.WhereIf(input.MaxNgayHetHanFilter != null, e => e.NgayHetHan <= input.MaxNgayHetHanFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NoiCapFilter),  e => e.NoiCap == input.NoiCapFilter)
						.WhereIf(input.MinNgayCapFilter != null, e => e.NgayCap >= input.MinNgayCapFilter)
						.WhereIf(input.MaxNgayCapFilter != null, e => e.NgayCap <= input.MaxNgayCapFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoCMNDFilter),  e => e.SoCMND == input.SoCMNDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuocTichFilter),  e => e.QuocTich == input.QuocTichFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TonGiaoFilter),  e => e.TonGiao == input.TonGiaoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DanTocFilter),  e => e.DanToc == input.DanTocFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ViTriCongViecCodeFilter),  e => e.ViTriCongViecCode == input.ViTriCongViecCodeFilter)
						.WhereIf(input.MinDonViCongTacIDFilter != null, e => e.DonViCongTacID >= input.MinDonViCongTacIDFilter)
						.WhereIf(input.MaxDonViCongTacIDFilter != null, e => e.DonViCongTacID <= input.MaxDonViCongTacIDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MSTCaNhanFilter),  e => e.MSTCaNhan == input.MSTCaNhanFilter)
						.WhereIf(input.MinNgaySinhFilter != null, e => e.NgaySinh >= input.MinNgaySinhFilter)
						.WhereIf(input.MaxNgaySinhFilter != null, e => e.NgaySinh <= input.MaxNgaySinhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.GioiTinhCodeFilter),  e => e.GioiTinhCode == input.GioiTinhCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.AnhDaiDienFilter),  e => e.AnhDaiDien == input.AnhDaiDienFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.HoVaTenFilter),  e => e.HoVaTen == input.HoVaTenFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaNhanVienFilter),  e => e.MaNhanVien == input.MaNhanVienFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ChiNhanhFilter),  e => e.ChiNhanh == input.ChiNhanhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DVTFilter),  e => e.DVT == input.DVTFilter)
						.WhereIf(input.MinNgayKyHDKTHFilter != null, e => e.NgayKyHDKTH >= input.MinNgayKyHDKTHFilter)
						.WhereIf(input.MaxNgayKyHDKTHFilter != null, e => e.NgayKyHDKTH <= input.MaxNgayKyHDKTHFilter)
						.WhereIf(input.MinNgayKyHD36THFilter != null, e => e.NgayKyHD36TH >= input.MinNgayKyHD36THFilter)
						.WhereIf(input.MaxNgayKyHD36THFilter != null, e => e.NgayKyHD36TH <= input.MaxNgayKyHD36THFilter)
						.WhereIf(input.MinNgayKyHD12THFilter != null, e => e.NgayKyHD12TH >= input.MinNgayKyHD12THFilter)
						.WhereIf(input.MaxNgayKyHD12THFilter != null, e => e.NgayKyHD12TH <= input.MaxNgayKyHD12THFilter)
						.WhereIf(input.MinNgayKyHDTVFilter != null, e => e.NgayKyHDTV >= input.MinNgayKyHDTVFilter)
						.WhereIf(input.MaxNgayKyHDTVFilter != null, e => e.NgayKyHDTV <= input.MaxNgayKyHDTVFilter)
						.WhereIf(input.MinNgayKYHDCTVFilter != null, e => e.NgayKYHDCTV >= input.MinNgayKYHDCTVFilter)
						.WhereIf(input.MaxNgayKYHDCTVFilter != null, e => e.NgayKYHDCTV <= input.MaxNgayKYHDCTVFilter)
						.WhereIf(input.MinNgayKyHDKVFilter != null, e => e.NgayKyHDKV >= input.MinNgayKyHDKVFilter)
						.WhereIf(input.MaxNgayKyHDKVFilter != null, e => e.NgayKyHDKV <= input.MaxNgayKyHDKVFilter)
						.WhereIf(input.MinNgayKYHDTTFilter != null, e => e.NgayKYHDTT >= input.MinNgayKYHDTTFilter)
						.WhereIf(input.MaxNgayKYHDTTFilter != null, e => e.NgayKYHDTT <= input.MaxNgayKYHDTTFilter)
						.WhereIf(input.MinNgayKyHDFilter != null, e => e.NgayKyHD >= input.MinNgayKyHDFilter)
						.WhereIf(input.MaxNgayKyHDFilter != null, e => e.NgayKyHD <= input.MaxNgayKyHDFilter);

			var pagedAndFilteredViTriCongViecs = filteredViTriCongViecs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var viTriCongViecs = from o in pagedAndFilteredViTriCongViecs
                         select new GetViTriCongViecForViewDto() {
							ViTriCongViec = new ViTriCongViecDto
							{
                                TenCty = o.TenCty,
                                HopDongHienTai = o.HopDongHienTai,
                                SoHD = o.SoHD,
                                DonViCongTacName = o.DonViCongTacName,
                                ChoNgoi = o.ChoNgoi,
                                NoiDaoTaoID = o.NoiDaoTaoID,
                                LoaiHopDongID = o.LoaiHopDongID,
                                MaSoNoiKCB = o.MaSoNoiKCB,
                                NoiDangKyKCBID = o.NoiDangKyKCBID,
                                NgayHetHanBHYT = o.NgayHetHanBHYT,
                                SoTheBHYT = o.SoTheBHYT,
                                MaTinhCap = o.MaTinhCap,
                                MaSoBHXH = o.MaSoBHXH,
                                SoSoBHXH = o.SoSoBHXH,
                                TyLeDongBH = o.TyLeDongBH,
                                NgayThamGiaBH = o.NgayThamGiaBH,
                                ThamGiaCongDoan = o.ThamGiaCongDoan,
                                NganHangCode = o.NganHangCode,
                                TkNganHang = o.TkNganHang,
                                DonViSoCongChuanCode = o.DonViSoCongChuanCode,
                                SoCongChuan = o.SoCongChuan,
                                LuongDongBH = o.LuongDongBH,
                                LuongCoBan = o.LuongCoBan,
                                BacLuongCode = o.BacLuongCode,
                                SoNgayPhep = o.SoNgayPhep,
                                NgayChinhThuc = o.NgayChinhThuc,
                                NgayThuViec = o.NgayThuViec,
                                NgayTapSu = o.NgayTapSu,
                                SoSoQLLaoDong = o.SoSoQLLaoDong,
                                DiaDiemLamViecCode = o.DiaDiemLamViecCode,
                                QuanLyGianTiep = o.QuanLyGianTiep,
                                QuanLyTrucTiep = o.QuanLyTrucTiep,
                                TrangThaiLamViecCode = o.TrangThaiLamViecCode,
                                Bac = o.Bac,
                                Cap = o.Cap,
                                ChucDanh = o.ChucDanh,
                                MaChamCong = o.MaChamCong,
                                DiaChiLHKC = o.DiaChiLHKC,
                                EmailLHKC = o.EmailLHKC,
                                DtDiDongLHKC = o.DtDiDongLHKC,
                                DtNhaRiengLHKC = o.DtNhaRiengLHKC,
                                QuanHeLHKC = o.QuanHeLHKC,
                                HoVaTenLHKC = o.HoVaTenLHKC,
                                DiaChiHN = o.DiaChiHN,
                                TinhThanhIDHN = o.TinhThanhIDHN,
                                QuocGiaHN = o.QuocGiaHN,
                                LaChuHo = o.LaChuHo,
                                MaSoHoGiaDinh = o.MaSoHoGiaDinh,
                                SoSoHoKhau = o.SoSoHoKhau,
                                DiaChiHKTT = o.DiaChiHKTT,
                                TinhThanhIDHKTT = o.TinhThanhIDHKTT,
                                QuocGiaHKTT = o.QuocGiaHKTT,
                                Facebook = o.Facebook,
                                Skype = o.Skype,
                                NoiSinh = o.NoiSinh,
                                TinhThanhID = o.TinhThanhID,
                                NguyenQuan = o.NguyenQuan,
                                EmailKhac = o.EmailKhac,
                                EmailCoQuan = o.EmailCoQuan,
                                EmailCaNhan = o.EmailCaNhan,
                                DtKhac = o.DtKhac,
                                DtNhaRieng = o.DtNhaRieng,
                                DtCoQuan = o.DtCoQuan,
                                DtDiDong = o.DtDiDong,
                                TepDinhKem = o.TepDinhKem,
                                TinhTrangHonNhanCode = o.TinhTrangHonNhanCode,
                                XepLoaiCode = o.XepLoaiCode,
                                NamTotNghiep = o.NamTotNghiep,
                                ChuyenNganh = o.ChuyenNganh,
                                Khoa = o.Khoa,
                                TrinhDoDaoTaoCode = o.TrinhDoDaoTaoCode,
                                TrinhDoVanHoa = o.TrinhDoVanHoa,
                                NgayHetHan = o.NgayHetHan,
                                NoiCap = o.NoiCap,
                                NgayCap = o.NgayCap,
                                SoCMND = o.SoCMND,
                                QuocTich = o.QuocTich,
                                TonGiao = o.TonGiao,
                                DanToc = o.DanToc,
                                ViTriCongViecCode = o.ViTriCongViecCode,
                                DonViCongTacID = o.DonViCongTacID,
                                MSTCaNhan = o.MSTCaNhan,
                                NgaySinh = o.NgaySinh,
                                GioiTinhCode = o.GioiTinhCode,
                                AnhDaiDien = o.AnhDaiDien,
                                HoVaTen = o.HoVaTen,
                                MaNhanVien = o.MaNhanVien,
                                ChiNhanh = o.ChiNhanh,
                                DVT = o.DVT,
                                NgayKyHDKTH = o.NgayKyHDKTH,
                                NgayKyHD36TH = o.NgayKyHD36TH,
                                NgayKyHD12TH = o.NgayKyHD12TH,
                                NgayKyHDTV = o.NgayKyHDTV,
                                NgayKYHDCTV = o.NgayKYHDCTV,
                                NgayKyHDKV = o.NgayKyHDKV,
                                NgayKYHDTT = o.NgayKYHDTT,
                                NgayKyHD = o.NgayKyHD,
                                Id = o.Id
							}
						};

            var totalCount = await filteredViTriCongViecs.CountAsync();

            return new PagedResultDto<GetViTriCongViecForViewDto>(
                totalCount,
                await viTriCongViecs.ToListAsync()
            );
         }
		 
		 public async Task<GetViTriCongViecForViewDto> GetViTriCongViecForView(int id)
         {
            var viTriCongViec = await _viTriCongViecRepository.GetAsync(id);

            var output = new GetViTriCongViecForViewDto { ViTriCongViec = ObjectMapper.Map<ViTriCongViecDto>(viTriCongViec) };
			
            return output;
         }
		 
		 //[AbpAuthorize(AppPermissions.Pages_ViTriCongViecs_Edit)]
		 public async Task<GetViTriCongViecForEditOutput> GetViTriCongViecForEdit(EntityDto input)
         {
            var viTriCongViec = await _viTriCongViecRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetViTriCongViecForEditOutput {ViTriCongViec = ObjectMapper.Map<CreateOrEditViTriCongViecDto>(viTriCongViec)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditViTriCongViecDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 //[AbpAuthorize(AppPermissions.Pages_ViTriCongViecs_Create)]
		 protected virtual async Task Create(CreateOrEditViTriCongViecDto input)
         {
            var viTriCongViec = ObjectMapper.Map<ViTriCongViec>(input);

			

            await _viTriCongViecRepository.InsertAsync(viTriCongViec);
         }

		 //[AbpAuthorize(AppPermissions.Pages_ViTriCongViecs_Edit)]
		 protected virtual async Task Update(CreateOrEditViTriCongViecDto input)
         {
            var viTriCongViec = await _viTriCongViecRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, viTriCongViec);
         }

		 //[AbpAuthorize(AppPermissions.Pages_ViTriCongViecs_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _viTriCongViecRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetViTriCongViecsToExcel(GetAllViTriCongViecsForExcelInput input)
         {
			
			var filteredViTriCongViecs = _viTriCongViecRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.TenCty.Contains(input.Filter) || e.HopDongHienTai.Contains(input.Filter) || e.SoHD.Contains(input.Filter) || e.DonViCongTacName.Contains(input.Filter) || e.ChoNgoi.Contains(input.Filter) || e.MaSoNoiKCB.Contains(input.Filter) || e.SoTheBHYT.Contains(input.Filter) || e.MaTinhCap.Contains(input.Filter) || e.MaSoBHXH.Contains(input.Filter) || e.SoSoBHXH.Contains(input.Filter) || e.NganHangCode.Contains(input.Filter) || e.TkNganHang.Contains(input.Filter) || e.DonViSoCongChuanCode.Contains(input.Filter) || e.SoCongChuan.Contains(input.Filter) || e.LuongDongBH.Contains(input.Filter) || e.LuongCoBan.Contains(input.Filter) || e.BacLuongCode.Contains(input.Filter) || e.SoSoQLLaoDong.Contains(input.Filter) || e.DiaDiemLamViecCode.Contains(input.Filter) || e.QuanLyGianTiep.Contains(input.Filter) || e.QuanLyTrucTiep.Contains(input.Filter) || e.TrangThaiLamViecCode.Contains(input.Filter) || e.Bac.Contains(input.Filter) || e.Cap.Contains(input.Filter) || e.ChucDanh.Contains(input.Filter) || e.MaChamCong.Contains(input.Filter) || e.DiaChiLHKC.Contains(input.Filter) || e.EmailLHKC.Contains(input.Filter) || e.DtDiDongLHKC.Contains(input.Filter) || e.DtNhaRiengLHKC.Contains(input.Filter) || e.QuanHeLHKC.Contains(input.Filter) || e.HoVaTenLHKC.Contains(input.Filter) || e.DiaChiHN.Contains(input.Filter) || e.QuocGiaHN.Contains(input.Filter) || e.MaSoHoGiaDinh.Contains(input.Filter) || e.SoSoHoKhau.Contains(input.Filter) || e.DiaChiHKTT.Contains(input.Filter) || e.QuocGiaHKTT.Contains(input.Filter) || e.Facebook.Contains(input.Filter) || e.Skype.Contains(input.Filter) || e.NoiSinh.Contains(input.Filter) || e.NguyenQuan.Contains(input.Filter) || e.EmailKhac.Contains(input.Filter) || e.EmailCoQuan.Contains(input.Filter) || e.EmailCaNhan.Contains(input.Filter) || e.DtKhac.Contains(input.Filter) || e.DtNhaRieng.Contains(input.Filter) || e.DtCoQuan.Contains(input.Filter) || e.DtDiDong.Contains(input.Filter) || e.TepDinhKem.Contains(input.Filter) || e.TinhTrangHonNhanCode.Contains(input.Filter) || e.XepLoaiCode.Contains(input.Filter) || e.ChuyenNganh.Contains(input.Filter) || e.Khoa.Contains(input.Filter) || e.TrinhDoDaoTaoCode.Contains(input.Filter) || e.TrinhDoVanHoa.Contains(input.Filter) || e.NoiCap.Contains(input.Filter) || e.SoCMND.Contains(input.Filter) || e.QuocTich.Contains(input.Filter) || e.TonGiao.Contains(input.Filter) || e.DanToc.Contains(input.Filter) || e.ViTriCongViecCode.Contains(input.Filter) || e.MSTCaNhan.Contains(input.Filter) || e.GioiTinhCode.Contains(input.Filter) || e.AnhDaiDien.Contains(input.Filter) || e.HoVaTen.Contains(input.Filter) || e.MaNhanVien.Contains(input.Filter) || e.ChiNhanh.Contains(input.Filter) || e.DVT.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.TenCtyFilter),  e => e.TenCty == input.TenCtyFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.HopDongHienTaiFilter),  e => e.HopDongHienTai == input.HopDongHienTaiFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoHDFilter),  e => e.SoHD == input.SoHDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DonViCongTacNameFilter),  e => e.DonViCongTacName == input.DonViCongTacNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ChoNgoiFilter),  e => e.ChoNgoi == input.ChoNgoiFilter)
						.WhereIf(input.MinNoiDaoTaoIDFilter != null, e => e.NoiDaoTaoID >= input.MinNoiDaoTaoIDFilter)
						.WhereIf(input.MaxNoiDaoTaoIDFilter != null, e => e.NoiDaoTaoID <= input.MaxNoiDaoTaoIDFilter)
						.WhereIf(input.MinLoaiHopDongIDFilter != null, e => e.LoaiHopDongID >= input.MinLoaiHopDongIDFilter)
						.WhereIf(input.MaxLoaiHopDongIDFilter != null, e => e.LoaiHopDongID <= input.MaxLoaiHopDongIDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaSoNoiKCBFilter),  e => e.MaSoNoiKCB == input.MaSoNoiKCBFilter)
						.WhereIf(input.MinNoiDangKyKCBIDFilter != null, e => e.NoiDangKyKCBID >= input.MinNoiDangKyKCBIDFilter)
						.WhereIf(input.MaxNoiDangKyKCBIDFilter != null, e => e.NoiDangKyKCBID <= input.MaxNoiDangKyKCBIDFilter)
						.WhereIf(input.MinNgayHetHanBHYTFilter != null, e => e.NgayHetHanBHYT >= input.MinNgayHetHanBHYTFilter)
						.WhereIf(input.MaxNgayHetHanBHYTFilter != null, e => e.NgayHetHanBHYT <= input.MaxNgayHetHanBHYTFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoTheBHYTFilter),  e => e.SoTheBHYT == input.SoTheBHYTFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaTinhCapFilter),  e => e.MaTinhCap == input.MaTinhCapFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaSoBHXHFilter),  e => e.MaSoBHXH == input.MaSoBHXHFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoSoBHXHFilter),  e => e.SoSoBHXH == input.SoSoBHXHFilter)
						.WhereIf(input.MinTyLeDongBHFilter != null, e => e.TyLeDongBH >= input.MinTyLeDongBHFilter)
						.WhereIf(input.MaxTyLeDongBHFilter != null, e => e.TyLeDongBH <= input.MaxTyLeDongBHFilter)
						.WhereIf(input.MinNgayThamGiaBHFilter != null, e => e.NgayThamGiaBH >= input.MinNgayThamGiaBHFilter)
						.WhereIf(input.MaxNgayThamGiaBHFilter != null, e => e.NgayThamGiaBH <= input.MaxNgayThamGiaBHFilter)
						.WhereIf(input.ThamGiaCongDoanFilter > -1,  e => (input.ThamGiaCongDoanFilter == 1 && e.ThamGiaCongDoan) || (input.ThamGiaCongDoanFilter == 0 && !e.ThamGiaCongDoan) )
						.WhereIf(!string.IsNullOrWhiteSpace(input.NganHangCodeFilter),  e => e.NganHangCode == input.NganHangCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TkNganHangFilter),  e => e.TkNganHang == input.TkNganHangFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DonViSoCongChuanCodeFilter),  e => e.DonViSoCongChuanCode == input.DonViSoCongChuanCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoCongChuanFilter),  e => e.SoCongChuan == input.SoCongChuanFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.LuongDongBHFilter),  e => e.LuongDongBH == input.LuongDongBHFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.LuongCoBanFilter),  e => e.LuongCoBan == input.LuongCoBanFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.BacLuongCodeFilter),  e => e.BacLuongCode == input.BacLuongCodeFilter)
						.WhereIf(input.MinSoNgayPhepFilter != null, e => e.SoNgayPhep >= input.MinSoNgayPhepFilter)
						.WhereIf(input.MaxSoNgayPhepFilter != null, e => e.SoNgayPhep <= input.MaxSoNgayPhepFilter)
						.WhereIf(input.MinNgayChinhThucFilter != null, e => e.NgayChinhThuc >= input.MinNgayChinhThucFilter)
						.WhereIf(input.MaxNgayChinhThucFilter != null, e => e.NgayChinhThuc <= input.MaxNgayChinhThucFilter)
						.WhereIf(input.MinNgayThuViecFilter != null, e => e.NgayThuViec >= input.MinNgayThuViecFilter)
						.WhereIf(input.MaxNgayThuViecFilter != null, e => e.NgayThuViec <= input.MaxNgayThuViecFilter)
						.WhereIf(input.MinNgayTapSuFilter != null, e => e.NgayTapSu >= input.MinNgayTapSuFilter)
						.WhereIf(input.MaxNgayTapSuFilter != null, e => e.NgayTapSu <= input.MaxNgayTapSuFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoSoQLLaoDongFilter),  e => e.SoSoQLLaoDong == input.SoSoQLLaoDongFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaDiemLamViecCodeFilter),  e => e.DiaDiemLamViecCode == input.DiaDiemLamViecCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuanLyGianTiepFilter),  e => e.QuanLyGianTiep == input.QuanLyGianTiepFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuanLyTrucTiepFilter),  e => e.QuanLyTrucTiep == input.QuanLyTrucTiepFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TrangThaiLamViecCodeFilter),  e => e.TrangThaiLamViecCode == input.TrangThaiLamViecCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.BacFilter),  e => e.Bac == input.BacFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.CapFilter),  e => e.Cap == input.CapFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ChucDanhFilter),  e => e.ChucDanh == input.ChucDanhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaChamCongFilter),  e => e.MaChamCong == input.MaChamCongFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiLHKCFilter),  e => e.DiaChiLHKC == input.DiaChiLHKCFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailLHKCFilter),  e => e.EmailLHKC == input.EmailLHKCFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DtDiDongLHKCFilter),  e => e.DtDiDongLHKC == input.DtDiDongLHKCFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DtNhaRiengLHKCFilter),  e => e.DtNhaRiengLHKC == input.DtNhaRiengLHKCFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuanHeLHKCFilter),  e => e.QuanHeLHKC == input.QuanHeLHKCFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.HoVaTenLHKCFilter),  e => e.HoVaTenLHKC == input.HoVaTenLHKCFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiHNFilter),  e => e.DiaChiHN == input.DiaChiHNFilter)
						.WhereIf(input.MinTinhThanhIDHNFilter != null, e => e.TinhThanhIDHN >= input.MinTinhThanhIDHNFilter)
						.WhereIf(input.MaxTinhThanhIDHNFilter != null, e => e.TinhThanhIDHN <= input.MaxTinhThanhIDHNFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuocGiaHNFilter),  e => e.QuocGiaHN == input.QuocGiaHNFilter)
						.WhereIf(input.LaChuHoFilter > -1,  e => (input.LaChuHoFilter == 1 && e.LaChuHo) || (input.LaChuHoFilter == 0 && !e.LaChuHo) )
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaSoHoGiaDinhFilter),  e => e.MaSoHoGiaDinh == input.MaSoHoGiaDinhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoSoHoKhauFilter),  e => e.SoSoHoKhau == input.SoSoHoKhauFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiHKTTFilter),  e => e.DiaChiHKTT == input.DiaChiHKTTFilter)
						.WhereIf(input.MinTinhThanhIDHKTTFilter != null, e => e.TinhThanhIDHKTT >= input.MinTinhThanhIDHKTTFilter)
						.WhereIf(input.MaxTinhThanhIDHKTTFilter != null, e => e.TinhThanhIDHKTT <= input.MaxTinhThanhIDHKTTFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuocGiaHKTTFilter),  e => e.QuocGiaHKTT == input.QuocGiaHKTTFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.FacebookFilter),  e => e.Facebook == input.FacebookFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SkypeFilter),  e => e.Skype == input.SkypeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NoiSinhFilter),  e => e.NoiSinh == input.NoiSinhFilter)
						.WhereIf(input.MinTinhThanhIDFilter != null, e => e.TinhThanhID >= input.MinTinhThanhIDFilter)
						.WhereIf(input.MaxTinhThanhIDFilter != null, e => e.TinhThanhID <= input.MaxTinhThanhIDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NguyenQuanFilter),  e => e.NguyenQuan == input.NguyenQuanFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailKhacFilter),  e => e.EmailKhac == input.EmailKhacFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailCoQuanFilter),  e => e.EmailCoQuan == input.EmailCoQuanFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailCaNhanFilter),  e => e.EmailCaNhan == input.EmailCaNhanFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DtKhacFilter),  e => e.DtKhac == input.DtKhacFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DtNhaRiengFilter),  e => e.DtNhaRieng == input.DtNhaRiengFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DtCoQuanFilter),  e => e.DtCoQuan == input.DtCoQuanFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DtDiDongFilter),  e => e.DtDiDong == input.DtDiDongFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TepDinhKemFilter),  e => e.TepDinhKem == input.TepDinhKemFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TinhTrangHonNhanCodeFilter),  e => e.TinhTrangHonNhanCode == input.TinhTrangHonNhanCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.XepLoaiCodeFilter),  e => e.XepLoaiCode == input.XepLoaiCodeFilter)
						.WhereIf(input.MinNamTotNghiepFilter != null, e => e.NamTotNghiep >= input.MinNamTotNghiepFilter)
						.WhereIf(input.MaxNamTotNghiepFilter != null, e => e.NamTotNghiep <= input.MaxNamTotNghiepFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ChuyenNganhFilter),  e => e.ChuyenNganh == input.ChuyenNganhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.KhoaFilter),  e => e.Khoa == input.KhoaFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TrinhDoDaoTaoCodeFilter),  e => e.TrinhDoDaoTaoCode == input.TrinhDoDaoTaoCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TrinhDoVanHoaFilter),  e => e.TrinhDoVanHoa == input.TrinhDoVanHoaFilter)
						.WhereIf(input.MinNgayHetHanFilter != null, e => e.NgayHetHan >= input.MinNgayHetHanFilter)
						.WhereIf(input.MaxNgayHetHanFilter != null, e => e.NgayHetHan <= input.MaxNgayHetHanFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NoiCapFilter),  e => e.NoiCap == input.NoiCapFilter)
						.WhereIf(input.MinNgayCapFilter != null, e => e.NgayCap >= input.MinNgayCapFilter)
						.WhereIf(input.MaxNgayCapFilter != null, e => e.NgayCap <= input.MaxNgayCapFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoCMNDFilter),  e => e.SoCMND == input.SoCMNDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuocTichFilter),  e => e.QuocTich == input.QuocTichFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TonGiaoFilter),  e => e.TonGiao == input.TonGiaoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DanTocFilter),  e => e.DanToc == input.DanTocFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ViTriCongViecCodeFilter),  e => e.ViTriCongViecCode == input.ViTriCongViecCodeFilter)
						.WhereIf(input.MinDonViCongTacIDFilter != null, e => e.DonViCongTacID >= input.MinDonViCongTacIDFilter)
						.WhereIf(input.MaxDonViCongTacIDFilter != null, e => e.DonViCongTacID <= input.MaxDonViCongTacIDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MSTCaNhanFilter),  e => e.MSTCaNhan == input.MSTCaNhanFilter)
						.WhereIf(input.MinNgaySinhFilter != null, e => e.NgaySinh >= input.MinNgaySinhFilter)
						.WhereIf(input.MaxNgaySinhFilter != null, e => e.NgaySinh <= input.MaxNgaySinhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.GioiTinhCodeFilter),  e => e.GioiTinhCode == input.GioiTinhCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.AnhDaiDienFilter),  e => e.AnhDaiDien == input.AnhDaiDienFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.HoVaTenFilter),  e => e.HoVaTen == input.HoVaTenFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaNhanVienFilter),  e => e.MaNhanVien == input.MaNhanVienFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ChiNhanhFilter),  e => e.ChiNhanh == input.ChiNhanhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DVTFilter),  e => e.DVT == input.DVTFilter)
						.WhereIf(input.MinNgayKyHDKTHFilter != null, e => e.NgayKyHDKTH >= input.MinNgayKyHDKTHFilter)
						.WhereIf(input.MaxNgayKyHDKTHFilter != null, e => e.NgayKyHDKTH <= input.MaxNgayKyHDKTHFilter)
						.WhereIf(input.MinNgayKyHD36THFilter != null, e => e.NgayKyHD36TH >= input.MinNgayKyHD36THFilter)
						.WhereIf(input.MaxNgayKyHD36THFilter != null, e => e.NgayKyHD36TH <= input.MaxNgayKyHD36THFilter)
						.WhereIf(input.MinNgayKyHD12THFilter != null, e => e.NgayKyHD12TH >= input.MinNgayKyHD12THFilter)
						.WhereIf(input.MaxNgayKyHD12THFilter != null, e => e.NgayKyHD12TH <= input.MaxNgayKyHD12THFilter)
						.WhereIf(input.MinNgayKyHDTVFilter != null, e => e.NgayKyHDTV >= input.MinNgayKyHDTVFilter)
						.WhereIf(input.MaxNgayKyHDTVFilter != null, e => e.NgayKyHDTV <= input.MaxNgayKyHDTVFilter)
						.WhereIf(input.MinNgayKYHDCTVFilter != null, e => e.NgayKYHDCTV >= input.MinNgayKYHDCTVFilter)
						.WhereIf(input.MaxNgayKYHDCTVFilter != null, e => e.NgayKYHDCTV <= input.MaxNgayKYHDCTVFilter)
						.WhereIf(input.MinNgayKyHDKVFilter != null, e => e.NgayKyHDKV >= input.MinNgayKyHDKVFilter)
						.WhereIf(input.MaxNgayKyHDKVFilter != null, e => e.NgayKyHDKV <= input.MaxNgayKyHDKVFilter)
						.WhereIf(input.MinNgayKYHDTTFilter != null, e => e.NgayKYHDTT >= input.MinNgayKYHDTTFilter)
						.WhereIf(input.MaxNgayKYHDTTFilter != null, e => e.NgayKYHDTT <= input.MaxNgayKYHDTTFilter)
						.WhereIf(input.MinNgayKyHDFilter != null, e => e.NgayKyHD >= input.MinNgayKyHDFilter)
						.WhereIf(input.MaxNgayKyHDFilter != null, e => e.NgayKyHD <= input.MaxNgayKyHDFilter);

			var query = (from o in filteredViTriCongViecs
                         select new GetViTriCongViecForViewDto() { 
							ViTriCongViec = new ViTriCongViecDto
							{
                                TenCty = o.TenCty,
                                HopDongHienTai = o.HopDongHienTai,
                                SoHD = o.SoHD,
                                DonViCongTacName = o.DonViCongTacName,
                                ChoNgoi = o.ChoNgoi,
                                NoiDaoTaoID = o.NoiDaoTaoID,
                                LoaiHopDongID = o.LoaiHopDongID,
                                MaSoNoiKCB = o.MaSoNoiKCB,
                                NoiDangKyKCBID = o.NoiDangKyKCBID,
                                NgayHetHanBHYT = o.NgayHetHanBHYT,
                                SoTheBHYT = o.SoTheBHYT,
                                MaTinhCap = o.MaTinhCap,
                                MaSoBHXH = o.MaSoBHXH,
                                SoSoBHXH = o.SoSoBHXH,
                                TyLeDongBH = o.TyLeDongBH,
                                NgayThamGiaBH = o.NgayThamGiaBH,
                                ThamGiaCongDoan = o.ThamGiaCongDoan,
                                NganHangCode = o.NganHangCode,
                                TkNganHang = o.TkNganHang,
                                DonViSoCongChuanCode = o.DonViSoCongChuanCode,
                                SoCongChuan = o.SoCongChuan,
                                LuongDongBH = o.LuongDongBH,
                                LuongCoBan = o.LuongCoBan,
                                BacLuongCode = o.BacLuongCode,
                                SoNgayPhep = o.SoNgayPhep,
                                NgayChinhThuc = o.NgayChinhThuc,
                                NgayThuViec = o.NgayThuViec,
                                NgayTapSu = o.NgayTapSu,
                                SoSoQLLaoDong = o.SoSoQLLaoDong,
                                DiaDiemLamViecCode = o.DiaDiemLamViecCode,
                                QuanLyGianTiep = o.QuanLyGianTiep,
                                QuanLyTrucTiep = o.QuanLyTrucTiep,
                                TrangThaiLamViecCode = o.TrangThaiLamViecCode,
                                Bac = o.Bac,
                                Cap = o.Cap,
                                ChucDanh = o.ChucDanh,
                                MaChamCong = o.MaChamCong,
                                DiaChiLHKC = o.DiaChiLHKC,
                                EmailLHKC = o.EmailLHKC,
                                DtDiDongLHKC = o.DtDiDongLHKC,
                                DtNhaRiengLHKC = o.DtNhaRiengLHKC,
                                QuanHeLHKC = o.QuanHeLHKC,
                                HoVaTenLHKC = o.HoVaTenLHKC,
                                DiaChiHN = o.DiaChiHN,
                                TinhThanhIDHN = o.TinhThanhIDHN,
                                QuocGiaHN = o.QuocGiaHN,
                                LaChuHo = o.LaChuHo,
                                MaSoHoGiaDinh = o.MaSoHoGiaDinh,
                                SoSoHoKhau = o.SoSoHoKhau,
                                DiaChiHKTT = o.DiaChiHKTT,
                                TinhThanhIDHKTT = o.TinhThanhIDHKTT,
                                QuocGiaHKTT = o.QuocGiaHKTT,
                                Facebook = o.Facebook,
                                Skype = o.Skype,
                                NoiSinh = o.NoiSinh,
                                TinhThanhID = o.TinhThanhID,
                                NguyenQuan = o.NguyenQuan,
                                EmailKhac = o.EmailKhac,
                                EmailCoQuan = o.EmailCoQuan,
                                EmailCaNhan = o.EmailCaNhan,
                                DtKhac = o.DtKhac,
                                DtNhaRieng = o.DtNhaRieng,
                                DtCoQuan = o.DtCoQuan,
                                DtDiDong = o.DtDiDong,
                                TepDinhKem = o.TepDinhKem,
                                TinhTrangHonNhanCode = o.TinhTrangHonNhanCode,
                                XepLoaiCode = o.XepLoaiCode,
                                NamTotNghiep = o.NamTotNghiep,
                                ChuyenNganh = o.ChuyenNganh,
                                Khoa = o.Khoa,
                                TrinhDoDaoTaoCode = o.TrinhDoDaoTaoCode,
                                TrinhDoVanHoa = o.TrinhDoVanHoa,
                                NgayHetHan = o.NgayHetHan,
                                NoiCap = o.NoiCap,
                                NgayCap = o.NgayCap,
                                SoCMND = o.SoCMND,
                                QuocTich = o.QuocTich,
                                TonGiao = o.TonGiao,
                                DanToc = o.DanToc,
                                ViTriCongViecCode = o.ViTriCongViecCode,
                                DonViCongTacID = o.DonViCongTacID,
                                MSTCaNhan = o.MSTCaNhan,
                                NgaySinh = o.NgaySinh,
                                GioiTinhCode = o.GioiTinhCode,
                                AnhDaiDien = o.AnhDaiDien,
                                HoVaTen = o.HoVaTen,
                                MaNhanVien = o.MaNhanVien,
                                ChiNhanh = o.ChiNhanh,
                                DVT = o.DVT,
                                NgayKyHDKTH = o.NgayKyHDKTH,
                                NgayKyHD36TH = o.NgayKyHD36TH,
                                NgayKyHD12TH = o.NgayKyHD12TH,
                                NgayKyHDTV = o.NgayKyHDTV,
                                NgayKYHDCTV = o.NgayKYHDCTV,
                                NgayKyHDKV = o.NgayKyHDKV,
                                NgayKYHDTT = o.NgayKYHDTT,
                                NgayKyHD = o.NgayKyHD,
                                Id = o.Id
							}
						 });


            var viTriCongViecListDtos = await query.ToListAsync();

            return _viTriCongViecsExcelExporter.ExportToFile(viTriCongViecListDtos);
         }


    }
}