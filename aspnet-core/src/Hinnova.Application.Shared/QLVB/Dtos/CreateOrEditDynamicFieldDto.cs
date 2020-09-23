
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditDynamicFieldDto : EntityDto<int?>
    {

		public int ModuleId { get; set; }
		
		
		public string TableName { get; set; }
		
		
		public string Name { get; set; }
		
		
		public int TypeField { get; set; }
		
		
		public int? Width { get; set; }
		
		
		public string NameDescription { get; set; }
		
		
		public int? DepartmentId { get; set; }
		
		
		public bool IsActive { get; set; }
		
		
		public int? Order { get; set; }
		
		
		public int? WidthDescription { get; set; }
		
		
		public string ClassAttach { get; set; }
		
		

    }
}