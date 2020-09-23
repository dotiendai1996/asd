
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNS.Dtos
{
    public class CreateOrEditQuyTrinhCongTacDto : EntityDto<int?>
    {

		public string TenCty { get; set; }
		
		
		public DateTime DateTo { get; set; }
		
		
		public DateTime DateFrom { get; set; }
		
		
		public string ViTriCongViecCode { get; set; }
		
		
		public int? DonViCongTacID { get; set; }
		
		
		public string QuanLyTrucTiep { get; set; }
		
		
		public string TrangThaiCode { get; set; }
		
		
		public string GhiChu { get; set; }

		public  int? MaHoSo { get; set; }

	}
}