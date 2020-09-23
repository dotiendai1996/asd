using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllHistoryUploadsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string FileFilter { get; set; }

		public int? MaxVersionFilter { get; set; }
		public int? MinVersionFilter { get; set; }

		public int? MaxdocumentIDFilter { get; set; }
		public int? MindocumentIDFilter { get; set; }



    }
}