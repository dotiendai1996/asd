using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNS.Dtos
{
    public class GetAllDataChamCongsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
		public DateTime? MinProcessDate { get; set; }
		public DateTime? MaxProcessDate { get; set; }
		public string TenCTy { get; set; }
		public string PhongBan { get; set; }
		public string ChucDanh { get; set; }
		public bool? DiTre { get; set; }
		public bool? VeSom { get; set; }
		public bool? TangCa { get; set; }
		public bool? NghiPhep { get; set; }
		public bool? CongTac { get; set; }
		public bool? QuenChamCong { get; set; }
		public bool IsExportExcel { get; set; } = false;
		public int TotalCount { get; set; }
	}
}