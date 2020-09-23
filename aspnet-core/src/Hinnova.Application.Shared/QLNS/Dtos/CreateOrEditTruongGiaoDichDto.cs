
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNSDtos
{
    public class CreateOrEditTruongGiaoDichDto : EntityDto<int?>
    {

		public string Code { get; set; }
		
		
		public string CDName { get; set; }
		
		
		public string Value { get; set; }
		
		
		public string GhiChu { get; set; }


		public bool SetDefault { get; set; }
		public int? DisplayOrder { get; set; }
	}
}