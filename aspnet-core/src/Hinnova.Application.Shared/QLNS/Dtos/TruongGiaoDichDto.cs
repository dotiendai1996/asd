
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLNSDtos
{
    public class TruongGiaoDichDto : EntityDto
    {
		public string Code { get; set; }

		public string CDName { get; set; }

		public string Value { get; set; }

		public string GhiChu { get; set; }

		public bool SetDefault { get; set; }
		public int? DisplayOrder { get; set; }

	}
}