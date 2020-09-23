using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllDocumentHandlingDetailsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string GroupFilter { get; set; }

		public string PersonFilter { get; set; }

		public string TypeFilter { get; set; }

		public string SuperiosFilter { get; set; }

		public string PersonalCommentFilter { get; set; }

		public DateTime? MaxStartDateFilter { get; set; }
		public DateTime? MinStartDateFilter { get; set; }

		public DateTime? MaxEndDateFilter { get; set; }
		public DateTime? MinEndDateFilter { get; set; }

		public int? MaxDocumentHandlingIdFilter { get; set; }
		public int? MinDocumentHandlingIdFilter { get; set; }

		public int MainHandlingFilter { get; set; }

		public int CoHandlingFilter { get; set; }

		public int ToKnowFilter { get; set; }

		public int UserIdFilter { get; set; }

		public bool IsHandledFilter { get; set; }

	}
}