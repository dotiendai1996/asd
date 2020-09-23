using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("CA_DocumentDetails")]
    public class DocumentDetail : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }

		public virtual int DocumentId { get; set; }

		public virtual DateTime DateHandle { get; set; }
		
		public virtual int TypeHandle { get; set; }
		
		public virtual string Description { get; set; }
		
		public virtual string Status { get; set; }
		
		public virtual bool IsStared { get; set; }
		
		public virtual string Priority { get; set; }

		public virtual bool IsActive { get; set; }
		
		public virtual int Order { get; set; }
    }
}