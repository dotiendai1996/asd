
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditTextBookDto : EntityDto<int?>
    {

		public int SoDen { get; set; }
		
		
		public DateTime NgayDen { get; set; }
		
		
		public string SoHieuGoc { get; set; }
		
		
		public string CoQuanBanHanh { get; set; }
		
		
		public string TrichYeu { get; set; }
		
		
		public string NguoiChiDao { get; set; }
		
		
		public string Nguoi_DonVi { get; set; }
		
		
		public string FileDinhKem { get; set; }
		
		

    }
}