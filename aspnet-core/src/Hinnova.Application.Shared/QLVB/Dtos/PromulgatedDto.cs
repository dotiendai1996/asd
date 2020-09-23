
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class PromulgatedDto : EntityDto
    {
		public string Name { get; set; }

		public string DisplayName { get; set; }

		public string Representative { get; set; }

		public string Leader { get; set; }

		public string Position { get; set; }

    }
}