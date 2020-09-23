
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNS.Dtos
{
    public class CreateOrEditQuanLyTrucTiepPNPDto : EntityDto<int?>
    {

		public int PhieuNghiPhepID { get; set; }
		
		
		public string QLTT { get; set; }
		
		
		public string GhiChu { get; set; }
		
		
		public string MaHoSo { get; set; }
		
		

    }
}