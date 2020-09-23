using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("CA_DocumentHandling")]
    public class DocumentHandling : FullAuditedEntity , IMayHaveTenant
    {
		public int? TenantId { get; set; }
			
		public virtual int DocumentId { get; set; }
		
		public virtual string Handler { get; set; }

		public virtual int DocumentDetailId { get; set; }
		
		public virtual int? HandlingDetailId { get; set; }

		public virtual DateTime? EndDate { get; set; }
		
		public virtual string PlaceReceive { get; set; }
		
		public virtual string Content { get; set; }
		
		public virtual string Status { get; set; }
		
		public virtual string Comment { get; set; }

		public virtual bool IsActive { get; set; } = true;
		
		public virtual int Order { get; set; }
    }
}