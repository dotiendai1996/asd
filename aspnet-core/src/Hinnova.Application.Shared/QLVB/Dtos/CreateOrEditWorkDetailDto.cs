
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditWorkDetailDto : EntityDto<int?>
    {

		public int WorkAssignId { get; set; }
		
		
		[Range(WorkDetailConsts.MinDonePersentageValue, WorkDetailConsts.MaxDonePersentageValue)]
		public int DonePersentage { get; set; }
		
		
		public DateTime Date { get; set; }
		
		
		public string NameID { get; set; }
		
		
		public string Description { get; set; }
		
		
		public string Repply { get; set; }
		
		
		public string Attachment { get; set; }
		
		

    }
}