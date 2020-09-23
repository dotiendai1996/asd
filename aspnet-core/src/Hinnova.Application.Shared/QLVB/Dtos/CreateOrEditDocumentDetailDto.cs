
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditDocumentDetailDto : EntityDto<int?>
    {

		public int DocumentId { get; set; }

		public int IncomingNumber { get; set; }
		
		
		public int Pages { get; set; }
		
		
		public DateTime DateHandle { get; set; }
		
		
		public string TypeHandle { get; set; }
		
		
		public string Description { get; set; }
		
		
		public string Status { get; set; }
		
		
		public string IsStared { get; set; }
		
		
		public string Priority { get; set; }
		
		

    }
}