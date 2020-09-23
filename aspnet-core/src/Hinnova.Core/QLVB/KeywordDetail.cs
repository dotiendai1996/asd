using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("KeywordDetail")]
    public class KeywordDetail : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual int KeywordId { get; set; }
		
		public virtual bool IsLeader { get; set; }

		public virtual int UserId { get; set; }
		
		public virtual string FullName { get; set; }
		
		public virtual bool MainHandling { get; set; }
		
		public virtual bool CoHandling { get; set; }
		
		public virtual bool ToKnow { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual int Order { get; set; }
		

    }
}