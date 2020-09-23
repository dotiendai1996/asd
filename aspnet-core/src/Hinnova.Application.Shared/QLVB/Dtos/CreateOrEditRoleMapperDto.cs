
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditRoleMapperDto : EntityDto<int?>
    {

		public int RoleId { get; set; }
		public string Name { get; set; }
		

		public int LabelId { get; set; }
		
		
		public int MenuId { get; set; }
		
		
		public bool IsActive { get; set; }
		
		

    }
}