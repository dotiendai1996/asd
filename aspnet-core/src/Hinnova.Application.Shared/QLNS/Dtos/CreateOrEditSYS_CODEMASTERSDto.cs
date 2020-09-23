
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNSDtos
{
    public class CreateOrEditSYS_CODEMASTERSDto : EntityDto<int?>
    {

		[Required]
		public string Prefix { get; set; }
		
		
		public decimal? CurValue { get; set; }
		
		
		public string Description { get; set; }
		
		
		public string Active { get; set; }
		
		

    }
}