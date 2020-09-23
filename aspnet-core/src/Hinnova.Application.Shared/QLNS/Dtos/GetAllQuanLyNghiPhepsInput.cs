using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNS.Dtos
{
    public class GetAllQuanLyNghiPhepsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string TenNhanVienFilter { get; set; }

        public string MaNVFilter { get; set; }

		public int NghiPhepFilter { get; set; }

		public int CongTacFilter { get; set; }

        public int TangCaFilter { get; set; }

		public DateTime? MaxNgayBatDauFilter { get; set; }
		public DateTime? MinNgayBatDauFilter { get; set; }

		public DateTime? MaxNgayKetThucFilter { get; set; }
		public DateTime? MinNgayKetThucFilter { get; set; }

		public string LyDoFilter { get; set; }


		public string TepDinhKemFilter { get; set; }

		public string TenCTYFilter { get; set; }

		public int? MaxNguoiDuyetIDFilter { get; set; }
		public int? MinNguoiDuyetIDFilter { get; set; }

		public DateTime? MaxNgayDuyetFilter { get; set; }
		public DateTime? MinNgayDuyetFilter { get; set; }

        public int? TrangThaiIDFilter{ get; set; }

        public int? DonViCongTacIDFilter { get; set; }

        public int? QuanLyTrucTiepIDFilter { get; set; }

        public bool isCheckTimeFilter { get; set; }
		public DateTime? CreateTimeFilter { get; set; }
		public DateTime? LastModificationTimeFilter { get; set; }
		public DateTime? StartDateFilter { get; set; }
		public DateTime? EndDateFilter { get; set; }

        public int? RoleIDFilter { get; set; }


	}
}
