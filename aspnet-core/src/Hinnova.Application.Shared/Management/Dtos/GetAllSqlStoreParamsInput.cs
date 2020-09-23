using Abp.Application.Services.Dto;
using System;

namespace Hinnova.Management.Dtos
{
    public class GetAllSqlStoreParamsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxSqlConfigIdFilter { get; set; }
		public int? MinSqlConfigIdFilter { get; set; }

		public string CodeFilter { get; set; }

		public string FormatFilter { get; set; }

		public string NameFilter { get; set; }

		public int IsActiveFilter { get; set; }

		public string ValueStringFilter { get; set; }

		public int? MaxValueIntFilter { get; set; }
		public int? MinValueIntFilter { get; set; }



    }
}