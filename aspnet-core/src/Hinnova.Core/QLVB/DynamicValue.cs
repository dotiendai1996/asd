using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("DynamicValue")]
    public class DynamicValue : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual int? ObjectId { get; set; }
		
		public virtual string Key { get; set; }
		
		public virtual int? DynamicFieldId { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual int? Order { get; set; }
		
		public virtual string Value { get; set; }
		

    }
}