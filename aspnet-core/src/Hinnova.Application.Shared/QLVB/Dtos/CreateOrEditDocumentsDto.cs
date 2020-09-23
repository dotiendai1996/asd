
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditDocumentsDto : EntityDto<int?>
    {

		public int Number { get; set; }
		
		
		public int DocumentTypeId { get; set; }
		
		
		public string DateIssue { get; set; }
		
		
		public string PlaceReceive { get; set; }
		
		
		public string SaveTo { get; set; }
		
		
		public string Summary { get; set; }
		
		
		public string ApprovedBy { get; set; }
		
		
		public string Attachment { get; set; }
		
		
		public string TypeReceive { get; set; }
		
		
		public DateTime StartDate { get; set; }
		
		
		public DateTime EndDate { get; set; }
		
		
		public string Status { get; set; }
		
		
		public string Note { get; set; }

		public string MoreInformation { get; set; }

		public bool IsActive { get; set; }

		public int Order { get; set; } // Thứ tự

		public string GroupAuthor { get; set; } // nhóm ban hành

		public string Range { get; set; } // Lĩnh vực / phạm vi

		public string Position { get; set; } // chức vụ

		public string LinkedDocument { get; set; }  // văn bản liên kết

	}
}