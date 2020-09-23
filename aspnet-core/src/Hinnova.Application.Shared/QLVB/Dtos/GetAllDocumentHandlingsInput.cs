using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllDocumentHandlingsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxDocumentIdFilter { get; set; }
		public int? MinDocumentIdFilter { get; set; }

		public string HandlerFilter { get; set; }

		public int? MaxHandlingDetailIdFilter { get; set; }
		public int? MinHandlingDetailIdFilter { get; set; }

		public string PlaceReceiveFilter { get; set; }

		//public int? MaxKeywordIdFilter { get; set; }
		//public int? MinKeywordIdFilter { get; set; }

		public string ContentFilter { get; set; }

		public string StatusFilter { get; set; }

		public string CommentFilter { get; set; }



    }
}