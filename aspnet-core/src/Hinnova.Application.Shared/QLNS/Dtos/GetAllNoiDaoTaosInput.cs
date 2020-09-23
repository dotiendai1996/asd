using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNSDtos
{
    public class GetAllNoiDaoTaosInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string TenNoiDaoTaoFilter { get; set; }

		public string MaNoiDaoTaoFilter { get; set; }

		public string DiaChiFilter { get; set; }

		public string KhuVucFilter { get; set; }



    }
}