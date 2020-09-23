using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllTextBooksForExcelInput
    {
		public string Filter { get; set; }

		public int? MaxSoDenFilter { get; set; }
		public int? MinSoDenFilter { get; set; }

		public DateTime? MaxNgayDenFilter { get; set; }
		public DateTime? MinNgayDenFilter { get; set; }

		public string SoHieuGocFilter { get; set; }

		public string CoQuanBanHanhFilter { get; set; }

		public string TrichYeuFilter { get; set; }

		public string NguoiChiDaoFilter { get; set; }

		public string Nguoi_DonViFilter { get; set; }

		public string FileDinhKemFilter { get; set; }



    }
}