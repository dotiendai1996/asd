
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.Management.Dtos
{
    public class LabelDto : EntityDto
    {
		public string Name { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string Icon { get; set; }

		public string Link { get; set; }

		public int? Parent { get; set; }

		public int Index { get; set; }

		public string RequiredPermissionName { get; set; }

		public LabelDto[] ChildLabels { get; set; }

		public int? Number { get; set; }

		public string SqlString { get; set; }

		public bool IsActive { get; set; }
	}
}