
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.Management.Dtos
{
    public class SettingConfigDto : EntityDto
    {
		public string Code { get; set; }

		public string ValueString { get; set; }

		public int? ValueInt { get; set; }

		public string ValueHtml { get; set; }

		public string Image { get; set; }



    }
}