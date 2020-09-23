using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllPromulgatedsForExcelInput
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }

		public string DisplayNameFilter { get; set; }

		public string RepresentativeFilter { get; set; }

		public string LeaderFilter { get; set; }

		public string PositionFilter { get; set; }

    }
}