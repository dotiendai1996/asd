using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.Management
{
	[Table("SqlStoreParam")]
    public class SqlStoreParam : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual int? SqlConfigId { get; set; }
		
		public virtual string Code { get; set; }
		
		public virtual string Format { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual string ValueString { get; set; }
		
		public virtual int? ValueInt { get; set; }
		

    }
}