
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class KeywordDetailDto : EntityDto
    {
		public int KeywordId { get; set; }

		public string Keyword { get; set; }

		public bool IsLeader { get; set; }

		public string FullName { get; set; }

		public bool MainHandling { get; set; }

		public bool CoHandling { get; set; }

		public bool ToKnow { get; set; }

		public bool IsActive { get; set; }

		public int Order { get; set; }

		public int UserId { get; set; }

    }
}