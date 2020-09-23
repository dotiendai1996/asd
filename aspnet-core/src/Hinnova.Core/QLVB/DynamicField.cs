using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("DynamicField")]
    public class DynamicField : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual int ModuleId { get; set; }
		
		public virtual string TableName { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual int TypeField { get; set; }
		
		public virtual int? Width { get; set; }
		
		public virtual string NameDescription { get; set; }
		
		public virtual int? DepartmentId { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual int? Order { get; set; }
		
		public virtual int? WidthDescription { get; set; }
		
		public virtual string ClassAttach { get; set; }
		

    }
}