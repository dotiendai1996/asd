using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.Management
{
	[Table("SqlConfigDetail")]
    public class SqlConfigDetail : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual int SqlConfigId { get; set; }
		
		public virtual string Code { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual string Format { get; set; }
		
		public virtual string Type { get; set; }
		
		public virtual string Width { get; set; }
		
		public virtual int? ColNum { get; set; }
		
		public virtual int? GroupLevel { get; set; }
		
		public virtual bool IsDisplay { get; set; }
		
		public virtual int? Order { get; set; }
		
		public virtual string TextAlign { get; set; }
		
		public virtual int? Version { get; set; }
		
		public virtual bool IsSum { get; set; }
		
		public virtual bool IsFreePane { get; set; }
		
		public virtual bool IsParent { get; set; }
		
		public virtual string ParentCode { get; set; }
		
		public virtual string GroupSort { get; set; }
		
		public virtual string CellTemplate { get; set; }
	}
}