
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
	public class DocumentsDto
	{
		public int? Id { get; set; }
		public string Number { get; set; }

		public int DocumentTypeId { get; set; }

		public string PlaceReceive { get; set; }

		public string SaveTo { get; set; }

		public string Summary { get; set; }

		public int IncommingNumber { get; set; }

		public int? Priority { get; set; }

		public DateTime IncommingDate { get; set; }

		public int Pages { get; set; }

		public string Author { get; set; }

		public string ApprovedBy { get; set; }

		public string Attachment { get; set; }

		public string TypeReceive { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string Status { get; set; }

		public string Note { get; set; }

		public string MoreInformation { get; set; }

		public bool IsActive { get; set; }

		public int Order { get; set; }

		public string GroupAuthor { get; set; }

		public string Range { get; set; }

		public string Position { get; set; }

		public string LinkedDocument { get; set; }

		public int STT { get; set; }

		public int? Action { get; set; }
	}
}