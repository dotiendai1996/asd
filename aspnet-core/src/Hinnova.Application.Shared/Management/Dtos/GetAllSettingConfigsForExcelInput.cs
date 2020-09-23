using Abp.Application.Services.Dto;
using System;

namespace Hinnova.Management.Dtos
{
    public class GetAllSettingConfigsForExcelInput
    {
		public string Filter { get; set; }

		public string CodeFilter { get; set; }

		public string ValueStringFilter { get; set; }

		public int? MaxValueIntFilter { get; set; }
		public int? MinValueIntFilter { get; set; }

		public string ValueHtmlFilter { get; set; }

		public string ImageFilter { get; set; }



    }
}