using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNSDtos
{
    public class GetAllUngViensForExcelInput
    {
		public string Filter { get; set; }

		public string MaUngVienFilter { get; set; }

		public string HoVaTenFilter { get; set; }

		public string ViTriUngTuyenCodeFilter { get; set; }

		public string KenhTuyenDungCodeFilter { get; set; }

		public string GioiTinhCodeFilter { get; set; }

		public string TrangThaiCodeFilter { get; set; }

		public DateTime? MaxNgaySinhFilter { get; set; }
		public DateTime? MinNgaySinhFilter { get; set; }

		public string SoCMNDFilter { get; set; }

		public string TrinhDoVanHoaFilter { get; set; }

		public int? MaxNamTotNghiepFilter { get; set; }
		public int? MinNamTotNghiepFilter { get; set; }

		public string TienDoTuyenDungCodeFilter { get; set; }

		public string RECORD_STATUSFilter { get; set; }

		public int? MaxMARKER_IDFilter { get; set; }
		public int? MinMARKER_IDFilter { get; set; }

		public string AUTH_STATUSFilter { get; set; }

		public string DienThoaiFilter { get; set; }

		public string EmailFilter { get; set; }

		public string DiaChiFilter { get; set; }
	
		public DateTime? MaxDay1Filter { get; set; }
		public DateTime? MinDay1Filter { get; set; }

		public DateTime? MaxDay2Filter { get; set; }
		public DateTime? MinDay2Filter { get; set; }

		public DateTime? MaxDay3Filter { get; set; }
		public DateTime? MinDay3Filter { get; set; }

		public string Time1Filter { get; set; }

		public string Time2Filter { get; set; }


		public string Time3Filter { get; set; }


		public string NoteFilter { get; set; }


	}
}