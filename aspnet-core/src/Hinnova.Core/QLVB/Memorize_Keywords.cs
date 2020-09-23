using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("Memorize_Keywordses")]
    public class Memorize_Keywords : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string TenGoiNho { get; set; }
		
		public virtual string XuLyChinh { get; set; }
		
		public virtual string DongXuLy { get; set; }
		
		public virtual string DeBiet { get; set; }
		
		public virtual int Head_ID { get; set; }
		
		public virtual string Full_Name { get; set; }
		
		public virtual string Prefix { get; set; }
		
		public virtual DateTime Hire_Date { get; set; }
		
		public virtual string KeyWord { get; set; }
		
		public virtual int IsActive { get; set; }
		

    }
}