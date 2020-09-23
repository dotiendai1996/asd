using Hinnova.QLVB.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.Management.Dtos
{
	public class GetDataAndColumnConfig_ProcesHanding
	{
		public GetDataAndColumnConfig_ProcesHanding(List<object> document, List<SqlConfigDetailDto> columnConfig)
		{
			this.ListDocumentHanding = document;
			this.ListColumnConfigProcesHanding = columnConfig;
		}

		public List<object> ListDocumentHanding { get; set; }

		public List<SqlConfigDetailDto> ListColumnConfigProcesHanding { get; set; }

	}
}
