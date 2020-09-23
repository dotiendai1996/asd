
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class DocumentDetailDto : EntityDto
    {
		public int DocumentId { get; set; }

		public DateTime Datehandle { get; set; }

		public int Typehandle { get; set; }

		public string Description { get; set; }

		public string Status { get; set; }

		public bool IsStared { get; set; }

		public string Priority { get; set; }



    }
}