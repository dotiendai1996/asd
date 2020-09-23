using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNS.Dtos
{
    public class GetAllLichSuUploadsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string TenFileFilter { get; set; }

		public string DungLuongFilter { get; set; }

		public string TieuDeFilter { get; set; }

		public string TypeFilter { get; set; }

		public string TypeIDFilter { get; set; }



    }
}