using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllCommandDatasourcesForExcelInput
    {
		public string Filter { get; set; }

		public string CommandFilter { get; set; }

		public string KeyFilter { get; set; }

		public string ValueFilter { get; set; }

		public int? MaxDynamicDatasourceIdFilter { get; set; }
		public int? MinDynamicDatasourceIdFilter { get; set; }

		public int? MaxOrderFilter { get; set; }
		public int? MinOrderFilter { get; set; }

		public int IsActiveFilter { get; set; }



    }
}