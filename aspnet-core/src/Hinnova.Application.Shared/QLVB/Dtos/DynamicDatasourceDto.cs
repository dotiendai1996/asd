
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class DynamicDatasourceDto : EntityDto
    {
		public int Type { get; set; }

		public int? ObjectId { get; set; }

		public int? DynamicFieldId { get; set; }

		public int? Order { get; set; }

		public bool IsActive { get; set; }



    }
}