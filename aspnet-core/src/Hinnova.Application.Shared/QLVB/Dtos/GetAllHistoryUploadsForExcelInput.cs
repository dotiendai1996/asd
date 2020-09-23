using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllHistoryUploadsForExcelInput
    {
		public string Filter { get; set; }

		public string FileFilter { get; set; }

        public int VersionFilter { get; set; }

    }
}