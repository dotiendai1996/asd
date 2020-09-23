
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class MemorizeKeywordDto : EntityDto
    {
		public string KeyWord { get; set; }

        public bool IsActive { get; set; }

    }
}