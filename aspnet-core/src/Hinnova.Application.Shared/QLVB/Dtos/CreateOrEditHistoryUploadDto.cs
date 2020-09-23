
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditHistoryUploadDto : EntityDto<int?>
    {

		public string File { get; set; }
		
		
		public int Version { get; set; }
		
		
		public int documentID { get; set; }
		
		

    }
}