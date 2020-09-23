using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllWorkAssignsForExcelInput
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }

		public DateTime? MaxStartDateFilter { get; set; }
		public DateTime? MinStartDateFilter { get; set; }

		public DateTime? MaxEndDateFilter { get; set; }
		public DateTime? MinEndDateFilter { get; set; }

		public string AssigneeFilter { get; set; }

		public int? MaxProgressFilter { get; set; }
		public int? MinProgressFilter { get; set; }

		public string StatusFilter { get; set; }

		public string DescriptionFilter { get; set; }

		public string ActionFilter { get; set; }



    }
}