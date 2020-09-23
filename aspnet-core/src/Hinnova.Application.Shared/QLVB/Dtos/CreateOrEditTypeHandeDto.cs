
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditTypeHandeDto : EntityDto<int?>
    {

		public string Name { get; set; }
		
		

    }
}