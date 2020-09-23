
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNS.Dtos
{
    public class CreateOrEditTemplateDto : EntityDto<int?>
    {

		public string MaTemplate { get; set; }
		
		
		public string TenTemplate { get; set; }
		
		
		public string LinkTemplate { get; set; }
		
		
		public string GhiChu { get; set; }

		public string NoiDung { get; set; }

	}
}