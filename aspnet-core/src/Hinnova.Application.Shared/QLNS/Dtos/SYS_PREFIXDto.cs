
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLNSDtos
{
    public class SYS_PREFIXDto : EntityDto
    {
		public string Code { get; set; }

		public string Prefix { get; set; }

		public string Description { get; set; }



    }
}