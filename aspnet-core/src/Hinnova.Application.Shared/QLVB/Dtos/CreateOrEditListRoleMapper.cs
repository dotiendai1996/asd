using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.QLVB.Dtos
{
    public class CreateOrEditListRoleMapper : Entity<int>
    {
		public int RoleId { get; set; }
		public string Name { get; set; }


		public int[] LabelId { get; set; }


		public int MenuId { get; set; }


		public bool IsActive { get; set; }
	}
}
