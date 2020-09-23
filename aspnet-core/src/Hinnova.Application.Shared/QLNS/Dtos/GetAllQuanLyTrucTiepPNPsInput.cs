using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNS.Dtos
{
    public class GetAllQuanLyTrucTiepPNPsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxPhieuNghiPhepIDFilter { get; set; }
		public int? MinPhieuNghiPhepIDFilter { get; set; }

		public string QLTTFilter { get; set; }

		public string GhiChuFilter { get; set; }

		public string MaHoSoFilter { get; set; }



    }
}