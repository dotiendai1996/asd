using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.Management
{
	[Table("SqlConfig")]
    public class SqlConfig : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string Code { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual bool IsRawQuery { get; set; }
		
		public virtual string SqlContent { get; set; }
		
		public virtual int? GroupLevel { get; set; }
		
		public virtual int? DisplayType { get; set; }
		
		public virtual int? Version { get; set; }
		
		public virtual bool IsDynamicColumn { get; set; }
		
		public virtual int? TypeGetColumn { get; set; }
		

    }
}