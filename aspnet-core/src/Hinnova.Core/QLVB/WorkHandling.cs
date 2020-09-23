using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("CA_WorkHandlings")]
    public class WorkHandling : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string ReceivePlace { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual string Content { get; set; }
		
		public virtual string Status { get; set; }
		
		public virtual string Comment { get; set; }
		
		public virtual int KeyWordId { get; set; }

		public virtual bool IsActive { get; set; }

		public virtual int Order { get; set; }
	}
}