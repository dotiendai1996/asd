
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditDocumentTypeDto : EntityDto<int?>
    {

		public string TypeName { get; set; }

		public string Signal { get; set; }

		public int Order { get; set; }
		
		public bool IsActive { get; set; }
		
    }
}