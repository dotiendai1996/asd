
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class VanbanDto : EntityDto
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