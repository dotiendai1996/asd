
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.Management.Dtos
{
    public class CreateOrEditMenuDto : EntityDto<int?>
    {

		[Required]
		public string Name { get; set; }
		
		
		[Required]
		public string Title { get; set; }
		
		
		public string Icon { get; set; }
		
		
		public string Description { get; set; }
		
		
		public int? Parent { get; set; }
		
		
		[Required]
		//public bool IsParent { get; set; }
		
		
		public string Link { get; set; }
		
		
		//[Required]
		//public string Type { get; set; }
		
		
		[Required]
		public DateTime CreationTime { get; set; }
		
		
		public DateTime? LastModificationTime { get; set; }
		
		
		[Required]
		public bool IsDeleted { get; set; }
		
		
		public DateTime? DeletionTime { get; set; }
		
		
		[Required]
		public int Index { get; set; }
		
		
		//[Required]
		//public bool IsDelimiter { get; set; }
		
		
		public string RequiredPermissionName { get; set; }
		
		

    }
}