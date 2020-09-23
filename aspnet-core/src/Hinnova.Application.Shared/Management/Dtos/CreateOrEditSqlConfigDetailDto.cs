
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.Management.Dtos
{
    public class CreateOrEditSqlConfigDetailDto : EntityDto<int?>
    {

		public int SqlConfigId { get; set; }
		
		
		public string Code { get; set; }
		
		
		public string Name { get; set; }
		
		
		public string Format { get; set; }
		
		
		public string Type { get; set; }
		
		
		public string Width { get; set; }
		
		
		public int? ColNum { get; set; }
		
		
		public int? GroupLevel { get; set; }
		
		
		public bool IsDisplay { get; set; }
		
		
		public int? Order { get; set; }
		
		
		public string TextAlign { get; set; }
		
		
		public int? Version { get; set; }
		
		
		public bool IsSum { get; set; }
		
		
		public bool IsFreePane { get; set; }
		
		
		public bool IsParent { get; set; }
		
		
		public string ParentCode { get; set; }
		
		
		public string GroupSort { get; set; }


		public string CellTemplate { get; set; }
	}
}