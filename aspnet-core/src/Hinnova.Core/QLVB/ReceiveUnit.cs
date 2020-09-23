using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("CA_ReceiveUnits")]
    public class ReceiveUnit : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string Name { get; set; }
		
		public virtual string Position { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual int Order { get; set; }
    }
}