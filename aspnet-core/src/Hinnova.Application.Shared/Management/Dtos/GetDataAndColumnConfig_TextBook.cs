using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.Management.Dtos
{
   public class GetDataAndColumnConfig_TextBook
    {
		public GetDataAndColumnConfig_TextBook(List<object> data, List<SqlConfigDetailDto> columnConfig)
		{
		
			this.ListTextBook = data;
			this.ListColumnConfigTextBook = columnConfig;
		}

		
		public List<object> ListTextBook { get; set; }

		public List<SqlConfigDetailDto> ListColumnConfigTextBook { get; set; }

	}
}
