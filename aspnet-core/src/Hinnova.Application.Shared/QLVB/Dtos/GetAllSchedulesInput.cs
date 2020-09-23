using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllSchedulesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxScheduleTypeIDFilter { get; set; }
		public int? MinScheduleTypeIDFilter { get; set; }

		public DateTime? MaxDateCreatedFilter { get; set; }
		public DateTime? MinDateCreatedFilter { get; set; }

		public DateTime? MaxDateOccurFilter { get; set; }
		public DateTime? MinDateOccurFilter { get; set; }

		public string FromTimeFilter { get; set; }

		public string ToTimeFilter { get; set; }

		public string ContentFilter { get; set; }

		public string NotesFilter { get; set; }



    }
}