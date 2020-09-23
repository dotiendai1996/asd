using Hinnova.Management.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.QLVB.Dtos
{
     public class GetDataAndColumnConfig_HistoryUploads
    {
		public GetDataAndColumnConfig_HistoryUploads(List<object> data, List<SqlConfigDetailDto> columnConfig)
		{

			this.ListHistoryUploads = data;
			this.ListColumnConfigHistoryUploads = columnConfig;
		}


		public List<object> ListHistoryUploads { get; set; }

		public List<SqlConfigDetailDto> ListColumnConfigHistoryUploads { get; set; }
	}
}
