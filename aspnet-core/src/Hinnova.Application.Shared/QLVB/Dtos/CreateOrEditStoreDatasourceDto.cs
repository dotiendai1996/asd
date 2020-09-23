
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditStoreDatasourceDto : EntityDto<int?>
    {

		public string NameStore { get; set; }
		
		
		public string Key { get; set; }
		
		
		public string Value { get; set; }
		
		
		public int? DynamicDatasourceId { get; set; }
		
		
		public int? Order { get; set; }
		
		
		public bool IsActive { get; set; }
		
		

    }
}