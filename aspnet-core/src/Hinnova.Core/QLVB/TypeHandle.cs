using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("CA_TypeHandles")]
    public class TypeHandle : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string Name { get; set; }

		public virtual string Description { get; set; }
		
		public virtual bool IsActive { get; set; }

		public virtual int Order { get; set; }
    }
}