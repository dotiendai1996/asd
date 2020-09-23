using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("TruongGiaoDich")]
    public class TruongGiaoDich : FullAuditedEntity 
    {

		public virtual string Code { get; set; }
		
		public virtual string CDName { get; set; }
		
		public virtual string Value { get; set; }
		
		public virtual string GhiChu { get; set; }
		
		public virtual bool SetDefault { get; set; }

        public virtual int?  DisplayOrder { get; set; }
	}
}