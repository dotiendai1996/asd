using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("CA_MemorizeKeywords")]
    public class MemorizeKeyword : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }

		public virtual string KeyWord { get; set; }

		public virtual bool IsActive { get; set; } = true;
    }
}