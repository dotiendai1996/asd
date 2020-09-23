using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllDynamicValuesForExcelInput
    {
		public string Filter { get; set; }

		public int? MaxObjectIdFilter { get; set; }
		public int? MinObjectIdFilter { get; set; }

		public string KeyFilter { get; set; }

		public string ValueFilter { get; set; }

		public int? MaxDynamicFieldIdFilter { get; set; }
		public int? MinDynamicFieldIdFilter { get; set; }



    }
}