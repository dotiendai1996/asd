using Abp.Application.Services.Dto;
using System;

namespace Hinnova.Management.Dtos
{
    public class GetAllMenusInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }

		public string TitleFilter { get; set; }

		public string IconFilter { get; set; }

		public string DescriptionFilter { get; set; }

		public int? MaxParentFilter { get; set; }
		public int? MinParentFilter { get; set; }

		//public int IsParentFilter { get; set; } = -1;

		public string LinkFilter { get; set; }

		//public string TypeFilter { get; set; }

		public DateTime? MaxCreationTimeFilter { get; set; }
		public DateTime? MinCreationTimeFilter { get; set; }

		public DateTime? MaxLastModificationTimeFilter { get; set; }
		public DateTime? MinLastModificationTimeFilter { get; set; }

		public int IsDeletedFilter { get; set; } = -1;

		public DateTime? MaxDeletionTimeFilter { get; set; }
		public DateTime? MinDeletionTimeFilter { get; set; }

		public int? MaxIndexFilter { get; set; }
		public int? MinIndexFilter { get; set; }

		//public int IsDelimiterFilter { get; set; } = -1;

		public string RequiredPermissionNameFilter { get; set; }

	}
}