using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllKeywordDetailsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxKeywordIdFilter { get; set; }
		public int? MinKeywordIdFilter { get; set; }

		public int IsLeaderFilter { get; set; }

		public string FullNameFilter { get; set; }

		public int MainHandlingFilter { get; set; }

		public int CoHandlingFilter { get; set; }

		public int ToKnowFilter { get; set; }

		public int IsActiveFilter { get; set; }

		public int? MaxOrderFilter { get; set; }
		public int? MinOrderFilter { get; set; }

		public int? UserIdFilter { get; set; }

	}
}