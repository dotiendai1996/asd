using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllReceiveUnitsForExcelInput
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }

		public string PositionFilter { get; set; }

		public int IsActiveFilter { get; set; }



    }
}