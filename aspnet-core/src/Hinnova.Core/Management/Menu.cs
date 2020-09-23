using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.Management
{
	[Table("CA_Menus")]
    public class Menu : FullAuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		[Required]
		public virtual string Name { get; set; }
		
		[Required]
		public virtual string Title { get; set; }
		
		public virtual string Icon { get; set; }
		
		public virtual string Description { get; set; }
		
		public virtual int? Parent { get; set; }
		
		//[Required]
		//public virtual bool IsParent { get; set; }
		
		public virtual string Link { get; set; }
		
		//[Required]
		//public virtual string Type { get; set; }
		
		[Required]
		public virtual int Index { get; set; }
		
		public virtual string RequiredPermissionName { get; set; }
    }
}