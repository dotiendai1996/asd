
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditDynamicDatasourceDto : EntityDto<int?>
    {

		[Required]
		public int Type { get; set; }
		
		
		public int? ObjectId { get; set; }
		
		
		public int? DynamicFieldId { get; set; }
		
		
		public int? Order { get; set; }
		
		
		public bool IsActive { get; set; }
		
		

    }
}