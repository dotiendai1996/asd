using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("HistoryUploads")]
    public class HistoryUpload : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string File { get; set; }
		
		public virtual int Version { get; set; }
		
		public virtual int documentID { get; set; }
		

    }
}