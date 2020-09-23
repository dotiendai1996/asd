
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class PriorityDto : EntityDto
    {
		public string Key { get; set; }

		public int Value { get; set; }


    }
}