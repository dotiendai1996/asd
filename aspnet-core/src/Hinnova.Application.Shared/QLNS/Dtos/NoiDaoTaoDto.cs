
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLNSDtos
{
    public class NoiDaoTaoDto : EntityDto
    {
		public string TenNoiDaoTao { get; set; }

		public string MaNoiDaoTao { get; set; }

		public string DiaChi { get; set; }

		public string KhuVuc { get; set; }



    }
}