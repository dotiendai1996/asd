﻿
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditDynamicValueDto : EntityDto<int?>
    {

		public int? ObjectId { get; set; }
		
		
		public string Key { get; set; }
		
		
		public int? DynamicFieldId { get; set; }
		
		
		public bool IsActive { get; set; }
		
		
		public int? Order { get; set; }
		
		
		public string Value { get; set; }
		
		

    }
}