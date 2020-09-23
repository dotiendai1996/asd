
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditPromulgatedDto : EntityDto<int?>
    {

		public string Name { get; set; }
		
		
		public string DisplayName { get; set; }
		
		
		public string Representative { get; set; }
		
		
		public string Leader { get; set; }
		
		public string Position { get; set; }

    }
}