using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNSDtos
{
    public class GetAllLichSuLamViecsForExcelInput
    {
		public string Filter { get; set; }

		public int? MaxUngVienIdFilter { get; set; }
		public int? MinUngVienIdFilter { get; set; }

		public string NoiDungFilter { get; set; }

		public string TepDinhKemFilter { get; set; }



    }
}