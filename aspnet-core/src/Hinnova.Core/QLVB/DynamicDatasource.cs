using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("DynamicDatasource")]
    public class DynamicDatasource : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		[Required]
		public virtual int Type { get; set; }
		
		public virtual int? ObjectId { get; set; }
		
		public virtual int? DynamicFieldId { get; set; }
		
		public virtual int? Order { get; set; }
		
		public virtual bool IsActive { get; set; }
		

    }
}