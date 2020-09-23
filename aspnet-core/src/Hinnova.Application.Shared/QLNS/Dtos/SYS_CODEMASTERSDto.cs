
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLNSDtos
{
    public class SYS_CODEMASTERSDto : EntityDto
    {
		public string Prefix { get; set; }

		public decimal? CurValue { get; set; }

		public string Description { get; set; }

		public string Active { get; set; }



    }
}