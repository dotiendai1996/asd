
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditWordProcessingDto : EntityDto<int?>
    {

		public string ReceivePlace { get; set; }
		
		
		public string Name { get; set; }
		
		
		public string Content { get; set; }
		
		
		public string Status { get; set; }
		
		
		public string Comment { get; set; }
		
		
		public int KeyWordId { get; set; }
		
		

    }
}