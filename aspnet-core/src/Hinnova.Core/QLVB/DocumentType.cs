using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("CA_DocumentTypes")]
    public class DocumentType : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string TypeName { get; set; }

		public virtual string Signal { get; set; }
		
		public virtual bool IsActive { get; set; }
		
    }
}