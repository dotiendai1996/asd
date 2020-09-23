using Abp.Application.Services.Dto;
using System;

namespace Hinnova.Management.Dtos
{
    public class GetAllLabelsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }

		public string TitleFilter { get; set; }

		public string DescriptionFilter { get; set; }

		public string IconFilter { get; set; }

		public string LinkFilter { get; set; }

		public int? MaxParentFilter { get; set; }
		public int? MinParentFilter { get; set; }

		public int? MaxIndexFilter { get; set; }
		public int? MinIndexFilter { get; set; }

		public string RequiredPermissionNameFilter { get; set; }

		public string SqlStringFilter { get; set; }
    }
}