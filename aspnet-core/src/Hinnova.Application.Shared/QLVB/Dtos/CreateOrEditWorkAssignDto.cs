
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditWorkAssignDto : EntityDto<int?>
    {

		public string Name { get; set; }
		
		
		public DateTime StartDate { get; set; }
		
		
		public DateTime EndDate { get; set; }
		
		
		public string Assignee { get; set; }
		
		
		[Range(WorkAssignConsts.MinProgressValue, WorkAssignConsts.MaxProgressValue)]
		public int Progress { get; set; }
		
		
		public string Status { get; set; }
		
		
		public string Description { get; set; }
		
		
		public string Action { get; set; }
		
		

    }
}