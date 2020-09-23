
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNS.Dtos
{
    public class CreateOrEditViTriCongViecDto : EntityDto<int?>
    {

		public string TenCty { get; set; }
		
		
		public string HopDongHienTai { get; set; }
		
		
		public string SoHD { get; set; }
		
		
		public string DonViCongTacName { get; set; }
		
		
		public string ChoNgoi { get; set; }
		
		
		public int NoiDaoTaoID { get; set; }
		
		
		public int LoaiHopDongID { get; set; }
		
		
		public string MaSoNoiKCB { get; set; }
		
		
		public int NoiDangKyKCBID { get; set; }
		
		
		public DateTime NgayHetHanBHYT { get; set; }
		
		
		public string SoTheBHYT { get; set; }
		
		
		public string MaTinhCap { get; set; }
		
		
		public string MaSoBHXH { get; set; }
		
		
		public string SoSoBHXH { get; set; }
		
		
		public double TyLeDongBH { get; set; }
		
		
		public DateTime NgayThamGiaBH { get; set; }
		
		
		public bool ThamGiaCongDoan { get; set; }
		
		
		public string NganHangCode { get; set; }
		
		
		public string TkNganHang { get; set; }
		
		
		public string DonViSoCongChuanCode { get; set; }
		
		
		public string SoCongChuan { get; set; }
		
		
		public string LuongDongBH { get; set; }
		
		
		public string LuongCoBan { get; set; }
		
		
		public string BacLuongCode { get; set; }
		
		
		public double SoNgayPhep { get; set; }
		
		
		public DateTime NgayChinhThuc { get; set; }
		
		
		public DateTime NgayThuViec { get; set; }
		
		
		public DateTime NgayTapSu { get; set; }
		
		
		public string SoSoQLLaoDong { get; set; }
		
		
		public string DiaDiemLamViecCode { get; set; }
		
		
		public string QuanLyGianTiep { get; set; }
		
		
		public string QuanLyTrucTiep { get; set; }
		
		
		public string TrangThaiLamViecCode { get; set; }
		
		
		public string Bac { get; set; }
		
		
		public string Cap { get; set; }
		
		
		public string ChucDanh { get; set; }
		
		
		public string MaChamCong { get; set; }
		
		
		public string DiaChiLHKC { get; set; }
		
		
		public string EmailLHKC { get; set; }
		
		
		public string DtDiDongLHKC { get; set; }
		
		
		public string DtNhaRiengLHKC { get; set; }
		
		
		public string QuanHeLHKC { get; set; }
		
		
		public string HoVaTenLHKC { get; set; }
		
		
		public string DiaChiHN { get; set; }
		
		
		public int? TinhThanhIDHN { get; set; }
		
		
		public string QuocGiaHN { get; set; }
		
		
		public bool LaChuHo { get; set; }
		
		
		public string MaSoHoGiaDinh { get; set; }
		
		
		public string SoSoHoKhau { get; set; }
		
		
		public string DiaChiHKTT { get; set; }
		
		
		public int? TinhThanhIDHKTT { get; set; }
		
		
		public string QuocGiaHKTT { get; set; }
		
		
		public string Facebook { get; set; }
		
		
		public string Skype { get; set; }
		
		
		public string NoiSinh { get; set; }
		
		
		public int? TinhThanhID { get; set; }
		
		
		public string NguyenQuan { get; set; }
		
		
		public string EmailKhac { get; set; }
		
		
		public string EmailCoQuan { get; set; }
		
		
		public string EmailCaNhan { get; set; }
		
		
		public string DtKhac { get; set; }
		
		
		public string DtNhaRieng { get; set; }
		
		
		public string DtCoQuan { get; set; }
		
		
		public string DtDiDong { get; set; }
		
		
		public string TepDinhKem { get; set; }
		
		
		public string TinhTrangHonNhanCode { get; set; }
		
		
		public string XepLoaiCode { get; set; }
		
		
		public int NamTotNghiep { get; set; }
		
		
		public string ChuyenNganh { get; set; }
		
		
		public string Khoa { get; set; }
		
		
		public string TrinhDoDaoTaoCode { get; set; }
		
		
		public string TrinhDoVanHoa { get; set; }
		
		
		public DateTime NgayHetHan { get; set; }
		
		
		public string NoiCap { get; set; }
		
		
		public DateTime NgayCap { get; set; }
		
		
		public string SoCMND { get; set; }
		
		
		public string QuocTich { get; set; }
		
		
		public string TonGiao { get; set; }
		
		
		public string DanToc { get; set; }
		
		
		public string ViTriCongViecCode { get; set; }
		
		
		public int? DonViCongTacID { get; set; }
		
		
		public string MSTCaNhan { get; set; }
		
		
		public DateTime NgaySinh { get; set; }
		
		
		public string GioiTinhCode { get; set; }
		
		
		public string AnhDaiDien { get; set; }
		
		
		public string HoVaTen { get; set; }
		
		
		public string MaNhanVien { get; set; }
		
		
		public string ChiNhanh { get; set; }
		
		
		public string DVT { get; set; }
		
		
		public DateTime NgayKyHDKTH { get; set; }
		
		
		public DateTime NgayKyHD36TH { get; set; }
		
		
		public DateTime NgayKyHD12TH { get; set; }
		
		
		public DateTime NgayKyHDTV { get; set; }
		
		
		public DateTime NgayKYHDCTV { get; set; }
		
		
		public DateTime NgayKyHDKV { get; set; }
		
		
		public DateTime NgayKYHDTT { get; set; }
		
		
		public DateTime NgayKyHD { get; set; }
		
		

    }
}