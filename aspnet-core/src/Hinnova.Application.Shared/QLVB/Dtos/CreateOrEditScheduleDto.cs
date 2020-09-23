
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditScheduleDto : EntityDto<int?>
    {

		public int ScheduleTypeID { get; set; }
		
		
		public DateTime DateCreated { get; set; }
		
		
		public DateTime DateOccur { get; set; }
		
		
		public string FromTime { get; set; }
		
		
		public string ToTime { get; set; }
		
		
		public string Content { get; set; }
		
		
		public string Notes { get; set; }
		
		

    }
}