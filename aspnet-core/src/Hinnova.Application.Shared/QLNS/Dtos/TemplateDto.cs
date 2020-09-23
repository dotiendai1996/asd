
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLNS.Dtos
{
    public class TemplateDto : EntityDto
    {
		public string MaTemplate { get; set; }

		public string TenTemplate { get; set; }

		public string LinkTemplate { get; set; }

		public string GhiChu { get; set; }

		public  string NoiDung { get; set; }

	}
}