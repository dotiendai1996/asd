
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLNS.Dtos
{
    public class QuanLyTrucTiepPNPDto : EntityDto
    {
		public int PhieuNghiPhepID { get; set; }

		public string QLTT { get; set; }

		public string GhiChu { get; set; }

		public string MaHoSo { get; set; }



    }
}