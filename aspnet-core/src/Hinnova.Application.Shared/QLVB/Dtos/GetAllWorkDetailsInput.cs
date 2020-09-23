using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllWorkDetailsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxWorkAssignIdFilter { get; set; }
		public int? MinWorkAssignIdFilter { get; set; }

		public int? MaxDonePersentageFilter { get; set; }
		public int? MinDonePersentageFilter { get; set; }

		public DateTime? MaxDateFilter { get; set; }
		public DateTime? MinDateFilter { get; set; }

		public string NameIDFilter { get; set; }

		public string DescriptionFilter { get; set; }

		public string RepplyFilter { get; set; }

		public string AttachmentFilter { get; set; }



    }
}