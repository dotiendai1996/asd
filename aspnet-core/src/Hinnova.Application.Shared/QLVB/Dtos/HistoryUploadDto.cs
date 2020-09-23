
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class HistoryUploadDto : EntityDto
    {
		public string File { get; set; }

		public int Version { get; set; }

		public int documentID { get; set; }



    }
}