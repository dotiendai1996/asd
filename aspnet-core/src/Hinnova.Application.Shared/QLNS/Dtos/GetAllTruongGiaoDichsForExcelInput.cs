using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNSDtos
{
    public class GetAllTruongGiaoDichsForExcelInput
    {
		public string Filter { get; set; }

		public string CodeFilter { get; set; }

		public string CDNameFilter { get; set; }

		public string ValueFilter { get; set; }

		public string GhiChuFilter { get; set; }



    }
}