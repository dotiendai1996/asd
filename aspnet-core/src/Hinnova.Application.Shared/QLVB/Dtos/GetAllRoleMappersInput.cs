using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllRoleMappersInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxRoleIdFilter { get; set; }
		public int? MinRoleIdFilter { get; set; }

		public int? MaxLabelIdFilter { get; set; }
		public int? MinLabelIdFilter { get; set; }

		public int? MaxMenuIdFilter { get; set; }
		public int? MinMenuIdFilter { get; set; }

		public int IsActiveFilter { get; set; }

		//public string RoleNameFilter { get; set; }

		//public string LabelNameFilter { get; set; }

		//public string 

    }
}