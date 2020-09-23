using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNSDtos
{
    public class GetAllDangKyKCBsForExcelInput
    {
		public string Filter { get; set; }

		public string TenNoiKCBFilter { get; set; }

		public string MaNoiKCBFilter { get; set; }

		public string DiaChiFilter { get; set; }

		public int? MaxTinhThanhIDFilter { get; set; }
		public int? MinTinhThanhIDFilter { get; set; }

		public string GhiChuFilter { get; set; }



    }
}