
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.Management.Dtos
{
    public class CreateOrEditSqlStoreParamDto : EntityDto<int?>
    {

		public int? SqlConfigId { get; set; }
		
		
		public string Code { get; set; }
		
		
		public string Format { get; set; }
		
		
		public string Name { get; set; }
		
		
		public bool IsActive { get; set; }
		
		
		public string ValueString { get; set; }
		
		
		public int? ValueInt { get; set; }
		
		

    }
}