using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("CA_DocumentHandlingDetail")]
    public class DocumentHandlingDetail : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string Group { get; set; }
		
		public virtual string Person { get; set; }
		
		public virtual string Type { get; set; }
		
		public virtual string Superios { get; set; }
		
		public virtual string PersonalComment { get; set; }
		
		public virtual DateTime StartDate { get; set; }
		
		public virtual DateTime EndDate { get; set; }
		
		public virtual int DocumentHandlingId { get; set; }
		
		public virtual bool MainHandling { get; set; }
		
		public virtual bool CoHandling { get; set; }
		
		public virtual bool ToKnow { get; set; }
		
		public virtual int UserId { get; set; }

		public virtual bool IsHandled { get; set; } = false;
    }
}