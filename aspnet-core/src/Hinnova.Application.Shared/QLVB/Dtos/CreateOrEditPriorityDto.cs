
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditPriorityDto : EntityDto<int?>
    {

		public string Key { get; set; }
		
		
		public int Value { get; set; }
		
		

    }
}