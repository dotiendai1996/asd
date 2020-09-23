using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllDocumentTypesForExcelInput
    {
		public string Filter { get; set; }

		public string TypeNameFilter { get; set; }

        public string SignalFilter { get; set; }

        public int IsActiveFilter { get; set; }



    }
}