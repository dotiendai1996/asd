using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("CA_Promulgateds")]
    public class Promulgated : FullAuditedEntity , IMayHaveTenant
    {
	    public int? TenantId { get; set; }
			

		public virtual string Name { get; set; }
		
		public virtual string DisplayName { get; set; }
		
		public virtual string Representative { get; set; }
		
		public virtual string Leader { get; set; }

		public virtual string Position { get; set; }

		public virtual bool IsActive { get; set; } = true;

    }
}