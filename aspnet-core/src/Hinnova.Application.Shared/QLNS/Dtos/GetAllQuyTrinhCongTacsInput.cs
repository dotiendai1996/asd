using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNS.Dtos
{
    public class GetAllQuyTrinhCongTacsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string TenCtyFilter { get; set; }

		public DateTime? MaxDateToFilter { get; set; }
		public DateTime? MinDateToFilter { get; set; }

		public DateTime? MaxDateFromFilter { get; set; }
		public DateTime? MinDateFromFilter { get; set; }

		public string ViTriCongViecCodeFilter { get; set; }

		public int? MaxDonViCongTacIDFilter { get; set; }
		public int? MinDonViCongTacIDFilter { get; set; }

		public string QuanLyTrucTiepFilter { get; set; }

		public string TrangThaiCodeFilter { get; set; }

		public string GhiChuFilter { get; set; }



    }
}