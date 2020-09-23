using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("Templates")]
    public class Template : Entity 
    {

		public virtual string MaTemplate { get; set; }
		
		public virtual string TenTemplate { get; set; }
		
		public virtual string LinkTemplate { get; set; }
		
		public virtual string GhiChu { get; set; }

		public virtual string NoiDung { get; set; }


	}
}