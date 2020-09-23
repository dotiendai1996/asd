
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLNSDtos
{
    public class HopDongDto : EntityDto
    {
		public string NhanVienId { get; set; }

		public string HoTenNhanVien { get; set; }

		public string ViTriCongViecCode { get; set; }

		public string SoHopDong { get; set; }

		public DateTime? NgayKy { get; set; }

		public int? DonViCongTacID { get; set; }

		public string TenHopDong { get; set; }

		public string LoaiHopDongCode { get; set; }

		public string HinhThucLamViecCode { get; set; }

		public DateTime? NgayCoHieuLuc { get; set; }

		public DateTime? NgayHetHan { get; set; }

		public string LuongCoBan { get; set; }

		public string LuongDongBaoHiem { get; set; }

		public double? TyLeHuongLuong { get; set; }

		public string NguoiDaiDienCongTy { get; set; }

		public string ChucDanh { get; set; }

		public string TrichYeu { get; set; }

		public string TepDinhKem { get; set; }

		public string GhiChu { get; set; }

		public string RECORD_STATUS { get; set; }

		public int? MARKER_ID { get; set; }

		public string AUTH_STATUS { get; set; }

		public int? CHECKER_ID { get; set; }

		public DateTime? APPROVE_DT { get; set; }

		public string ThoiHanHopDong { get; set; }


        public string DonViCongTac { get; set; }

        public  string LinkTemplate { get; set; }

        public int? LoaiHopDongID { get; set; }

        public  string TenCTY { get; set; }

	}
}