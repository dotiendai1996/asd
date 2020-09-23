
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class DocumentHandlingDto : EntityDto
    {
		public int DocumentId { get; set; }

		public string Handler { get; set; }

		public int? HandlingDetailId { get; set; }

		public virtual int DocumentDetailId { get; set; }

		public string PlaceReceive { get; set; }

		//public int? KeywordId { get; set; }

		public string Content { get; set; }

		public string Status { get; set; }

		public string Comment { get; set; }

		public DateTime CreationTime { get; set; }

		public DateTime? EndDate { get; set; }


	}
}