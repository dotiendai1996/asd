
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditReceiveUnitDto : EntityDto<int?>
    {

		public string Name { get; set; }
		
		
		public string Position { get; set; }
		
		
		public bool IsActive { get; set; }
		
		

    }
}