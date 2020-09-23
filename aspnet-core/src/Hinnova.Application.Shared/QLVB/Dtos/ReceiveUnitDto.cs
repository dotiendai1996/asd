
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class ReceiveUnitDto : EntityDto
    {
		public string Name { get; set; }

		public string Position { get; set; }

		public bool IsActive { get; set; }



    }
}