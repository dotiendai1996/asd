using Abp.Application.Services.Dto;
using System;

namespace Hinnova.Management.Dtos
{
    public class GetAllSqlConfigDetailsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxSqlConfigIdFilter { get; set; }
		public int? MinSqlConfigIdFilter { get; set; }

		public string CodeFilter { get; set; }

		public string NameFilter { get; set; }

		public string FormatFilter { get; set; }

		public string TypeFilter { get; set; }

		public string WidthFilter { get; set; }

		public int? MaxColNumFilter { get; set; }
		public int? MinColNumFilter { get; set; }

		public int? MaxGroupLevelFilter { get; set; }
		public int? MinGroupLevelFilter { get; set; }

		public int IsDisplayFilter { get; set; } = -1;

		public int? MaxOrderFilter { get; set; }
		public int? MinOrderFilter { get; set; }

		public string TextAlignFilter { get; set; }

		public int? MaxVersionFilter { get; set; }
		public int? MinVersionFilter { get; set; }

		public int IsSumFilter { get; set; } = -1;

		public int IsFreePaneFilter { get; set; } = -1;

		public int IsParentFilter { get; set; } = -1;

		public string ParentCodeFilter { get; set; }

		public string GroupSortFilter { get; set; }

		public string CellTemplateFilter { get; set; }

    }
}