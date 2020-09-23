using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("HardDatasource")]
    public class HardDatasource : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string Key { get; set; }
		
		public virtual string Value { get; set; }
		
		public virtual int? DynamicDatasourceId { get; set; }
		
		public virtual int? Order { get; set; }
		
		public virtual bool IsActive { get; set; }
		

    }
}