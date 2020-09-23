
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditDocumentHandlingDetailDto : EntityDto<int?>
    {

		public string Group { get; set; }
		
		
		public string Person { get; set; }
		
		
		public string Type { get; set; }
		
		
		public string Superios { get; set; }
		
		
		public string PersonalComment { get; set; }
		
		
		public DateTime StartDate { get; set; }
		
		
		public DateTime EndDate { get; set; }
		
		
		public int DocumentHandlingId { get; set; }
		
		
		public bool MainHandling { get; set; }
		
		
		public bool CoHandling { get; set; }
		
		
		public bool ToKnow { get; set; }
		
		public int UserId { get; set; }

		public bool IsHandled { get; set; }

	}
}