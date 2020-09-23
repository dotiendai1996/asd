using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.Management
{
	[Table("SettingConfig")]
    public class SettingConfig : Entity 
    {

		public virtual string Code { get; set; }
		
		public virtual string ValueString { get; set; }
		
		public virtual int? ValueInt { get; set; }
		
		public virtual string ValueHtml { get; set; }
		
		public virtual string Image { get; set; }
		

    }
}