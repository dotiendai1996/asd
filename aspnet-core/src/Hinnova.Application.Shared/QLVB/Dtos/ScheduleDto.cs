
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class ScheduleDto : EntityDto
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