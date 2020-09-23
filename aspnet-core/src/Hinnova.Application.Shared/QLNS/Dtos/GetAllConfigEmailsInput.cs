using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNS.Dtos
{
    public class GetAllConfigEmailsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string DiaChiEmailFilter { get; set; }

		public string TenHienThiFilter { get; set; }

		public string DiaChiIPFilter { get; set; }

		public int? MaxCongSMTPFilter { get; set; }
		public int? MinCongSMTPFilter { get; set; }

		public int CheckSSLFilter { get; set; }

		public int CheckThongTinFilter { get; set; }

		public string TenMienFilter { get; set; }

		public string TenTruyCapFilter { get; set; }

		public string MatKhauFilter { get; set; }

		public string TenCTYFilter { get; set; }



    }
}