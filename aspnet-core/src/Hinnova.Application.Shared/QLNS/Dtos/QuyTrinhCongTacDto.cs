
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLNS.Dtos
{
    public class QuyTrinhCongTacDto : EntityDto
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