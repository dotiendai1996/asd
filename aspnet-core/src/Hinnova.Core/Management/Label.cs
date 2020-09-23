using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.Management
{
	[Table("Label")]
    public class Label : Entity 
    {

		public virtual string Name { get; set; }
		
		public virtual string Title { get; set; }
		
		public virtual string Description { get; set; }
		
		public virtual string Icon { get; set; }
		
		public virtual string Link { get; set; }

		public virtual int? Parent { get; set; } = 0;
		
		public virtual int Index { get; set; }

		public virtual string RequiredPermissionName { get; set; }
		
		public virtual int MenuId { get; set; }

		public virtual string SqlString { get; set; }

		public virtual bool IsActive { get; set; }
	}
}