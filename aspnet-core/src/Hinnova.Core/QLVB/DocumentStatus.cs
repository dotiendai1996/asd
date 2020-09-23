using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("CA_DocumentStatus")]
    public class DocumentStatus : Entity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string Key { get; set; }
		
		public virtual string Value { get; set; }
		

    }
}