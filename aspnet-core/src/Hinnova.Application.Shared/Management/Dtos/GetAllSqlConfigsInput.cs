using Abp.Application.Services.Dto;
using System;

namespace Hinnova.Management.Dtos
{
    public class GetAllSqlConfigsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string CodeFilter { get; set; }

		public string NameFilter { get; set; }

		public int IsRawQueryFilter { get; set; } = -1;

		public string SqlContentFilter { get; set; }

		public int? MaxGroupLevelFilter { get; set; }
		public int? MinGroupLevelFilter { get; set; }

		public int? MaxDisplayTypeFilter { get; set; }
		public int? MinDisplayTypeFilter { get; set; }

		public int? MaxVersionFilter { get; set; }
		public int? MinVersionFilter { get; set; }

		public int IsDynamicColumnFilter { get; set; } = -1;

		public int? MaxTypeGetColumnFilter { get; set; }
		public int? MinTypeGetColumnFilter { get; set; }



    }
}