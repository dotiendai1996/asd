
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class DynamicFieldDto : EntityDto
    {
        public int? TenantId { get; set; }
		public int ModuleId { get; set; }

		public string TableName { get; set; }

		public string Name { get; set; }

		public int TypeField { get; set; }

		public int? Width { get; set; }

		public string NameDescription { get; set; }

		public int? DepartmentId { get; set; }

		public bool IsActive { get; set; }

		public int? Order { get; set; }

		public int? WidthDescription { get; set; }

		public string ClassAttach { get; set; }



    }
}