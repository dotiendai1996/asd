using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("SYS_CODEMASTERS")]
    public class SYS_CODEMASTERS : Entity 
    {

		[Required]
		public virtual string Prefix { get; set; }
		
		public virtual decimal? CurValue { get; set; }
		
		public virtual string Description { get; set; }
		
		public virtual string Active { get; set; }
		

    }
}