
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNS.Dtos
{
    public class CreateOrEditConfigEmailDto : EntityDto<int?>
    {

		public string DiaChiEmail { get; set; }
		
		
		public string TenHienThi { get; set; }
		
		
		public string DiaChiIP { get; set; }
		
		
		public int CongSMTP { get; set; }
		
		
		public bool CheckSSL { get; set; }
		
		
		public bool CheckThongTin { get; set; }
		
		
		public string TenMien { get; set; }
		
		
		public string TenTruyCap { get; set; }
		
		
		public string MatKhau { get; set; }
		
		
		public string TenCTY { get; set; }
		
		

    }
}