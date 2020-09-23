using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("CA_Schedules")]
    public class Schedule : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual int ScheduleTypeID { get; set; }
		
		public virtual DateTime DateCreated { get; set; }
		
		public virtual DateTime DateOccur { get; set; }
		
		public virtual string FromTime { get; set; }
		
		public virtual string ToTime { get; set; }
		
		public virtual string Content { get; set; }
		
		public virtual string Notes { get; set; }

		public virtual bool IsActive { get; set; }
		
		public virtual int Order { get; set; }
    }
}