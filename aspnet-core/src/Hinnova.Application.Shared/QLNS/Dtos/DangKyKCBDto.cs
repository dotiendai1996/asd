
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLNSDtos
{
    public class DangKyKCBDto : EntityDto
    {
		public string TenNoiKCB { get; set; }

		public string MaNoiKCB { get; set; }

		public string DiaChi { get; set; }

		public int? TinhThanhID { get; set; }

		public string GhiChu { get; set; }



    }
}