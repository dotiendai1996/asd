using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("CA_WorkDetails")]
    public class WorkDetail : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual int WorkAssignId { get; set; }
		
		[Range(WorkDetailConsts.MinDonePersentageValue, WorkDetailConsts.MaxDonePersentageValue)]
		public virtual int DonePersentage { get; set; }
		
		public virtual DateTime Date { get; set; }
		
		public virtual string NameID { get; set; }
		
		public virtual string Description { get; set; }
		
		public virtual string Repply { get; set; }
		
		public virtual string Attachment { get; set; }
		
		public virtual bool IsActive { get; set; }

		public virtual int Order { get; set; }
    }
}