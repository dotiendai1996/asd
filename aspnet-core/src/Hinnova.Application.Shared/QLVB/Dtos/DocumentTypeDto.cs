
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class DocumentTypeDto : EntityDto
    {
		public string TypeName { get; set; }

        public string Signal { get; set; }

        public bool IsActive { get; set; }

    }
}