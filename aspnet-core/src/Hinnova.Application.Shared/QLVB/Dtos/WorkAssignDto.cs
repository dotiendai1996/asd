
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class WorkAssignDto : EntityDto
    {
		public string Name { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string Assignee { get; set; }

		public int Progress { get; set; }

		public string Status { get; set; }

		public string Description { get; set; }

		public string Action { get; set; }



    }
}