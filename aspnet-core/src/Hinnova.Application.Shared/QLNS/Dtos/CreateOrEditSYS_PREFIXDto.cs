
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNSDtos
{
    public class CreateOrEditSYS_PREFIXDto : EntityDto<int?>
    {

		public string Code { get; set; }
		
		
		public string Prefix { get; set; }
		
		
		public string Description { get; set; }
		
		

    }
}