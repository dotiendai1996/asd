
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.Management.Dtos
{
    public class CreateOrEditSettingConfigDto : EntityDto<int?>
    {

		public string Code { get; set; }
		
		
		public string ValueString { get; set; }
		
		
		public int? ValueInt { get; set; }
		
		
		public string ValueHtml { get; set; }
		
		
		public string Image { get; set; }
		
		

    }
}