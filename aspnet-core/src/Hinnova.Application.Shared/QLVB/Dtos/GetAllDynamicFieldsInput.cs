using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllDynamicFieldsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxModuleIdFilter { get; set; }
		public int? MinModuleIdFilter { get; set; }

		public string TableNameFilter { get; set; }

		public string NameFilter { get; set; }

		public int? MaxTypeFieldFilter { get; set; }
		public int? MinTypeFieldFilter { get; set; }

		public int? MaxWidthFilter { get; set; }
		public int? MinWidthFilter { get; set; }

		public string NameDescriptionFilter { get; set; }

		public int? MaxDepartmentIdFilter { get; set; }
		public int? MinDepartmentIdFilter { get; set; }

		public int IsActiveFilter { get; set; }

		public int? MaxOrderFilter { get; set; }
		public int? MinOrderFilter { get; set; }

		public int? MaxWidthDescriptionFilter { get; set; }
		public int? MinWidthDescriptionFilter { get; set; }

		public string ClassAttachFilter { get; set; }



    }
}