using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNSDtos
{
    public class GetAllSYS_PREFIXsForExcelInput
    {
		public string Filter { get; set; }

		public string CodeFilter { get; set; }

		public string PrefixFilter { get; set; }

		public string DescriptionFilter { get; set; }



    }
}