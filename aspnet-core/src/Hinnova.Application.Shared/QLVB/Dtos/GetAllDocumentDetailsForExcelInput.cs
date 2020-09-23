using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllDocumentDetailsForExcelInput
    {
		public string Filter { get; set; }

		public int DocumentIdFilter { get; set; }

		public DateTime? MaxDatehandleFilter { get; set; }
		public DateTime? MinDatehandleFilter { get; set; }

		public int TypehandleFilter { get; set; }

		public string DescriptionFilter { get; set; }

		public string StatusFilter { get; set; }

		public bool IsStaredFilter { get; set; }

		public string PriorityFilter { get; set; }



    }
}