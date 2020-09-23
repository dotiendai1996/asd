using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNSDtos
{
    public class GetAllSYS_CODEMASTERSsForExcelInput
    {
		public string Filter { get; set; }

		public string PrefixFilter { get; set; }

		public decimal? MaxCurValueFilter { get; set; }
		public decimal? MinCurValueFilter { get; set; }

		public string DescriptionFilter { get; set; }

		public string ActiveFilter { get; set; }



    }
}