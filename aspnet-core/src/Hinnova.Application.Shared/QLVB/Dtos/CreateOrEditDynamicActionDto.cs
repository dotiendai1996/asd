
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditDynamicActionDto : EntityDto<int?>
    {

		public int LabelId { get; set; }
		
		public int? TenantId { get; set; }
		//public bool IsActive { get; set; }

		public int RoleId { get; set; }
		
		
		public bool HasSave { get; set; }
		
		
		public bool HasReturn { get; set; }
		
		
		public string HasTransfer { get; set; }
		
		
		public bool HasSaveAndTransfer { get; set; }
		
		
		public bool HasFinish { get; set; }
		
		
		public int Position { get; set; }
		
		
		public bool IsBack { get; set; }
		
		
		public string HasAssignWork { get; set; }
		
		
		public string Description { get; set; }
		
		
		public int Order { get; set; }

		public bool HasDelete { get; set; }

		public bool HasCreate { get; set; } 

		public bool HasReport { get; set; }

		public bool HasSaveAndCreate { get; set; }

		public string CellTemplate { get; set; }
	}
}