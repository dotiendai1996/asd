using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNSDtos
{
    public class GetAllHopDongsForExcelInput
    {
		public string Filter { get; set; }

		public string HoTenNhanVienFilter { get; set; }

		public string ViTriCongViecCodeFilter { get; set; }

		public DateTime? MaxNgayKyFilter { get; set; }
		public DateTime? MinNgayKyFilter { get; set; }

		public int? MaxDonViCongTacIDFilter { get; set; }
		public int? MinDonViCongTacIDFilter { get; set; }

		public string TenHopDongFilter { get; set; }

		public string LoaiHopDongCodeFilter { get; set; }

		public string HinhThucLamViecCodeFilter { get; set; }

		public DateTime? MaxNgayCoHieuLucFilter { get; set; }
		public DateTime? MinNgayCoHieuLucFilter { get; set; }

		public DateTime? MaxNgayHetHanFilter { get; set; }
		public DateTime? MinNgayHetHanFilter { get; set; }

		public double? MaxLuongCoBanFilter { get; set; }
		public double? MinLuongCoBanFilter { get; set; }

		public double? MaxLuongDongBaoHiemFilter { get; set; }
		public double? MinLuongDongBaoHiemFilter { get; set; }

		public string ChucDanhFilter { get; set; }

		public string TrichYeuFilter { get; set; }

		public string RECORD_STATUSFilter { get; set; }

		public int? MaxMARKER_IDFilter { get; set; }
		public int? MinMARKER_IDFilter { get; set; }

		public string AUTH_STATUSFilter { get; set; }

		public int? MaxCHECKER_IDFilter { get; set; }
		public int? MinCHECKER_IDFilter { get; set; }

		public DateTime? MaxAPPROVE_DTFilter { get; set; }
		public DateTime? MinAPPROVE_DTFilter { get; set; }

		public string ThoiHanHopDongFilter { get; set; }



    }
}