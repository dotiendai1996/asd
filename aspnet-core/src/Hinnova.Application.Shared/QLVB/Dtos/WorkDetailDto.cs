
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class WorkDetailDto : EntityDto
    {
		public int WorkAssignId { get; set; }

		public int DonePersentage { get; set; }

		public DateTime Date { get; set; }

		public string NameID { get; set; }

		public string Description { get; set; }

		public string Repply { get; set; }

		public string Attachment { get; set; }



    }
}