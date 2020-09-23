using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNS.Dtos
{
    public class GetAllQuanLyNghiPhepsForExcelInput
    {
		public string Filter { get; set; }

		public string TenNhanVienFilter { get; set; }

		public int? MaxDonViCongTacIDFilter { get; set; }
		public int? MinDonViCongTacIDFilter { get; set; }

		public string MaNVFilter { get; set; }

		public int NghiPhepFilter { get; set; }

		public int CongTacFilter { get; set; }

		public DateTime? MaxNgayBatDauFilter { get; set; }
		public DateTime? MinNgayBatDauFilter { get; set; }

		public DateTime? MaxNgayKetThucFilter { get; set; }
		public DateTime? MinNgayKetThucFilter { get; set; }

		public string LyDoFilter { get; set; }

		public int? MaxQuanLyTrucTiepIDFilter { get; set; }
		public int? MinQuanLyTrucTiepIDFilter { get; set; }

		public int? MaxTruongBoPhanIDFilter { get; set; }
		public int? MinTruongBoPhanIDFilter { get; set; }

		public string TepDinhKemFilter { get; set; }

		public string TenCTYFilter { get; set; }

		public int? MaxNguoiDuyetIDFilter { get; set; }
		public int? MinNguoiDuyetIDFilter { get; set; }

		public DateTime? MaxNgayDuyetFilter { get; set; }
		public DateTime? MinNgayDuyetFilter { get; set; }

		public string TrangThaiFilter { get; set; }



    }
}