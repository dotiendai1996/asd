
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNSDtos
{
    public class CreateOrEditNoiDaoTaoDto : EntityDto<int?>
    {

		public string TenNoiDaoTao { get; set; }
		
		
		public string MaNoiDaoTao { get; set; }
		
		
		public string DiaChi { get; set; }
		
		
		public string KhuVuc { get; set; }
		
		

    }
}