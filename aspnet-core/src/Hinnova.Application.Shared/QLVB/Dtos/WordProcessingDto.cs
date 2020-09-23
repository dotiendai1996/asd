
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class WordProcessingDto : EntityDto
    {
		public string ReceivePlace { get; set; }

		public string Name { get; set; }

		public string Content { get; set; }

		public string Status { get; set; }

		public string Comment { get; set; }

		public int KeyWordId { get; set; }



    }
}