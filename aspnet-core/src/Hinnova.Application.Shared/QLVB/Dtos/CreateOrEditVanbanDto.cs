
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditVanbanDto : EntityDto<int?>
    {

		public string TenCongViec { get; set; }
		
		
		public DateTime NgayGiaoViec { get; set; }
		
		
		public DateTime HanKetThuc { get; set; }
		
		
		public string NguoiXuLy { get; set; }
		
		
		public int TienDoChung { get; set; }
		
		
		public string TinhTrang { get; set; }
		
		
		public string NoiDung { get; set; }
		
		

    }
}