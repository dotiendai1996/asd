using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllVanbansForExcelInput
    {
		public string Filter { get; set; }

		public string TenCongViecFilter { get; set; }

		public DateTime? MaxNgayGiaoViecFilter { get; set; }
		public DateTime? MinNgayGiaoViecFilter { get; set; }

		public DateTime? MaxHanKetThucFilter { get; set; }
		public DateTime? MinHanKetThucFilter { get; set; }

		public string NguoiXuLyFilter { get; set; }

		public int? MaxTienDoChungFilter { get; set; }
		public int? MinTienDoChungFilter { get; set; }

		public string TinhTrangFilter { get; set; }

		public string NoiDungFilter { get; set; }



    }
}