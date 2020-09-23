using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("ViTriCongViecs")]
    public class ViTriCongViec : AuditedEntity 
    {

		public virtual string TenCty { get; set; }
		
		public virtual string HopDongHienTai { get; set; }
		
		public virtual string SoHD { get; set; }
		
		public virtual string DonViCongTacName { get; set; }
		
		public virtual string ChoNgoi { get; set; }
		
		public virtual int NoiDaoTaoID { get; set; }
		
		public virtual int LoaiHopDongID { get; set; }
		
		public virtual string MaSoNoiKCB { get; set; }
		
		public virtual int NoiDangKyKCBID { get; set; }
		
		public virtual DateTime NgayHetHanBHYT { get; set; }
		
		public virtual string SoTheBHYT { get; set; }
		
		public virtual string MaTinhCap { get; set; }
		
		public virtual string MaSoBHXH { get; set; }
		
		public virtual string SoSoBHXH { get; set; }
		
		public virtual double TyLeDongBH { get; set; }
		
		public virtual DateTime NgayThamGiaBH { get; set; }
		
		public virtual bool ThamGiaCongDoan { get; set; }
		
		public virtual string NganHangCode { get; set; }
		
		public virtual string TkNganHang { get; set; }
		
		public virtual string DonViSoCongChuanCode { get; set; }
		
		public virtual string SoCongChuan { get; set; }
		
		public virtual string LuongDongBH { get; set; }
		
		public virtual string LuongCoBan { get; set; }
		
		public virtual string BacLuongCode { get; set; }
		
		public virtual double SoNgayPhep { get; set; }
		
		public virtual DateTime NgayChinhThuc { get; set; }
		
		public virtual DateTime NgayThuViec { get; set; }
		
		public virtual DateTime NgayTapSu { get; set; }
		
		public virtual string SoSoQLLaoDong { get; set; }
		
		public virtual string DiaDiemLamViecCode { get; set; }
		
		public virtual string QuanLyGianTiep { get; set; }
		
		public virtual string QuanLyTrucTiep { get; set; }
		
		public virtual string TrangThaiLamViecCode { get; set; }
		
		public virtual string Bac { get; set; }
		
		public virtual string Cap { get; set; }
		
		public virtual string ChucDanh { get; set; }
		
		public virtual string MaChamCong { get; set; }
		
		public virtual string DiaChiLHKC { get; set; }
		
		public virtual string EmailLHKC { get; set; }
		
		public virtual string DtDiDongLHKC { get; set; }
		
		public virtual string DtNhaRiengLHKC { get; set; }
		
		public virtual string QuanHeLHKC { get; set; }
		
		public virtual string HoVaTenLHKC { get; set; }
		
		public virtual string DiaChiHN { get; set; }
		
		public virtual int? TinhThanhIDHN { get; set; }
		
		public virtual string QuocGiaHN { get; set; }
		
		public virtual bool LaChuHo { get; set; }
		
		public virtual string MaSoHoGiaDinh { get; set; }
		
		public virtual string SoSoHoKhau { get; set; }
		
		public virtual string DiaChiHKTT { get; set; }
		
		public virtual int? TinhThanhIDHKTT { get; set; }
		
		public virtual string QuocGiaHKTT { get; set; }
		
		public virtual string Facebook { get; set; }
		
		public virtual string Skype { get; set; }
		
		public virtual string NoiSinh { get; set; }
		
		public virtual int? TinhThanhID { get; set; }
		
		public virtual string NguyenQuan { get; set; }
		
		public virtual string EmailKhac { get; set; }
		
		public virtual string EmailCoQuan { get; set; }
		
		public virtual string EmailCaNhan { get; set; }
		
		public virtual string DtKhac { get; set; }
		
		public virtual string DtNhaRieng { get; set; }
		
		public virtual string DtCoQuan { get; set; }
		
		public virtual string DtDiDong { get; set; }
		
		public virtual string TepDinhKem { get; set; }
		
		public virtual string TinhTrangHonNhanCode { get; set; }
		
		public virtual string XepLoaiCode { get; set; }
		
		public virtual int NamTotNghiep { get; set; }
		
		public virtual string ChuyenNganh { get; set; }
		
		public virtual string Khoa { get; set; }
		
		public virtual string TrinhDoDaoTaoCode { get; set; }
		
		public virtual string TrinhDoVanHoa { get; set; }
		
		public virtual DateTime NgayHetHan { get; set; }
		
		public virtual string NoiCap { get; set; }
		
		public virtual DateTime NgayCap { get; set; }
		
		public virtual string SoCMND { get; set; }
		
		public virtual string QuocTich { get; set; }
		
		public virtual string TonGiao { get; set; }
		
		public virtual string DanToc { get; set; }
		
		public virtual string ViTriCongViecCode { get; set; }
		
		public virtual int? DonViCongTacID { get; set; }
		
		public virtual string MSTCaNhan { get; set; }
		
		public virtual DateTime NgaySinh { get; set; }
		
		public virtual string GioiTinhCode { get; set; }
		
		public virtual string AnhDaiDien { get; set; }
		
		public virtual string HoVaTen { get; set; }
		
		public virtual string MaNhanVien { get; set; }
		
		public virtual string ChiNhanh { get; set; }
		
		public virtual string DVT { get; set; }
		
		public virtual DateTime NgayKyHDKTH { get; set; }
		
		public virtual DateTime NgayKyHD36TH { get; set; }
		
		public virtual DateTime NgayKyHD12TH { get; set; }
		
		public virtual DateTime NgayKyHDTV { get; set; }
		
		public virtual DateTime NgayKYHDCTV { get; set; }
		
		public virtual DateTime NgayKyHDKV { get; set; }
		
		public virtual DateTime NgayKYHDTT { get; set; }
		
		public virtual DateTime NgayKyHD { get; set; }
		

    }
}