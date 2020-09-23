using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("SYS_PREFIX")]
    public class SYS_PREFIX : Entity 
    {

		public virtual string Code { get; set; }
		
		public virtual string Prefix { get; set; }
		
		public virtual string Description { get; set; }
		

    }
}