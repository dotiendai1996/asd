
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNSDtos
{
    public class CreateOrEditDangKyKCBDto : EntityDto<int?>
    {

		public string TenNoiKCB { get; set; }
		
		
		public string MaNoiKCB { get; set; }
		
		
		public string DiaChi { get; set; }
		
		
		public int? TinhThanhID { get; set; }
		
		
		public string GhiChu { get; set; }
		
		

    }
}