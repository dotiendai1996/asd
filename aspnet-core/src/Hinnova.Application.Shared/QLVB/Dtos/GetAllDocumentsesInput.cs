using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllDocumentsesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string NumberFilter { get; set; }

		public int? MaxIncommingNumberFilter { get; set; }
		public int? MinIncommingNumberFilter { get; set; }

		public int? MaxPagesFilter { get; set; }
		public int? MinPagesFilter { get; set; }

		public int? MaxDocumentTypeIdFilter { get; set; }
		public int? MinDocumentTypeIdFilter { get; set; }

		public string DateIssueFilter { get; set; }

		public string PlaceRecevieFilter { get; set; }

		public string SaveToFilter { get; set; }

		public string SummaryFilter { get; set; }

		public string ApprovedByFilter { get; set; }

		public string AttachmentFilter { get; set; }

		public string TypeReceiveFilter { get; set; }

		public DateTime? MaxStartDateFilter { get; set; }
		public DateTime? MinStartDateFilter { get; set; }

		public DateTime? MaxEndDateFilter { get; set; }
		public DateTime? MinEndDateFilter { get; set; }

		public string StatusFilter { get; set; }

		public string NoteFilter { get; set; }

		public int? PriorityFilter { get; set; }

		public DateTime IncommingDateFilter { get; set; }

		public string RangeFilter { get; set; }

		public string PositionFilter { get; set; }
		public string LinkedDocumentFilter { get; set; }
	}
}