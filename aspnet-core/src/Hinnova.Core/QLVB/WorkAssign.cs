using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("CA_WorkAssigns")]
    public class WorkAssign : Entity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string Name { get; set; }
		
		public virtual DateTime StartDate { get; set; }
		
		public virtual DateTime EndDate { get; set; }
		
		public virtual string Assignee { get; set; }
		
		[Range(WorkAssignConsts.MinProgressValue, WorkAssignConsts.MaxProgressValue)]
		public virtual int Progress { get; set; }
		
		public virtual string Status { get; set; }
		
		public virtual string Description { get; set; }
		
		public virtual string Action { get; set; }
		
		public virtual int Order { get; set; }
    }
}