using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllWordProcessingsForExcelInput
    {
		public string Filter { get; set; }

		public string ReceivePlaceFilter { get; set; }

		public string NameFilter { get; set; }

		public string ContentFilter { get; set; }

		public string StatusFilter { get; set; }

		public string CommentFilter { get; set; }

		public int? MaxKeyWordIdFilter { get; set; }
		public int? MinKeyWordIdFilter { get; set; }



    }
}