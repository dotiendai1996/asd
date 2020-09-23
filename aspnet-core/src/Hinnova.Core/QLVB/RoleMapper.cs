using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("RoleMapper")]
    public class RoleMapper : Entity , IMayHaveTenant
    {
		public int? TenantId { get; set; }
		public virtual string Name { get; set; }

		public virtual int RoleId { get; set; }
		
		public virtual int LabelId { get; set; }
		
		public virtual int MenuId { get; set; }
		
		public virtual bool IsActive { get; set; }
		

    }
}