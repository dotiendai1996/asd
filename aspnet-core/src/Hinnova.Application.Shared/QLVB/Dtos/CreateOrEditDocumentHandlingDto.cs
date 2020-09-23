
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditDocumentHandlingDto : EntityDto<int?>
    {

		public int DocumentId { get; set; }
		
		
		public string Handler { get; set; }
		
		
		public int? HandlingDetailId { get; set; }
		
		
		public string PlaceReceive { get; set; }
		
		
		//public int? KeywordId { get; set; }
		
		
		public string Content { get; set; }
		
		
		public string Status { get; set; }
		
		
		public string Comment { get; set; }

		public DateTime? EndDate { get; set; }

	}
}