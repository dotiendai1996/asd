using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNS.Dtos
{
    public class GetAllTemplatesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string MaTemplateFilter { get; set; }

		public string TenTemplateFilter { get; set; }

		public string LinkTemplateFilter { get; set; }

		public string GhiChuFilter { get; set; }



    }
}