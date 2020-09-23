using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllPrioritiesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string KeyFilter { get; set; }

		public int? MaxValueFilter { get; set; }
		public int? MinValueFilter { get; set; }



    }
}