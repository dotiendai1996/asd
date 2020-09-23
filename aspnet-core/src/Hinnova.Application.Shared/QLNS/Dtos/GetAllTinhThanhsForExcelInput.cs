using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNSDtos
{
    public class GetAllTinhThanhsForExcelInput
    {
		public string Filter { get; set; }

		public string TenTinhThanhFilter { get; set; }

		public string MaTinhThanhFilter { get; set; }



    }
}