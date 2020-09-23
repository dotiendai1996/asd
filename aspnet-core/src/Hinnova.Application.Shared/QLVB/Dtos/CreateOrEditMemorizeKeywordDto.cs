
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditMemorizeKeywordDto : EntityDto<int?>
    {

		public string KeyWord { get; set; }
		
		

    }
}