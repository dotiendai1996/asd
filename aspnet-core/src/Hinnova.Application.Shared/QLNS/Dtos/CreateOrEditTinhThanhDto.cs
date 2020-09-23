
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNSDtos
{
    public class CreateOrEditTinhThanhDto : EntityDto<int?>
    {

		public string TenTinhThanh { get; set; }
		
		
		public string MaTinhThanh { get; set; }
		
		

    }
}