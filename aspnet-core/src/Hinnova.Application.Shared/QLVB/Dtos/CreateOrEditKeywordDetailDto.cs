
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditKeywordDetailDto : EntityDto<int?>
    {

		public int KeywordId { get; set; }
		
		
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