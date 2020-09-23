
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class RoleMapperDto : EntityDto
    {
		public int RoleId { get; set; }

		public string Name { get; set; }

		public int LabelId { get; set; }

		public int MenuId { get; set; }

		public bool IsActive { get; set; }



    }
}