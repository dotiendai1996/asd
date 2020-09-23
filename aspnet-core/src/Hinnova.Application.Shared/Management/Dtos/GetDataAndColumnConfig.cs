using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.Management.Dtos
{
    public class GetDataAndColumnConfig
    {
		public GetDataAndColumnConfig(List<object> data, List<SqlConfigDetailDto> columnConfig)
		{
			this.ListVanBan = data;
		
			this.ListColumnConfig = columnConfig;
		}

		public List<object> ListVanBan { get; set; }
		

		public List<SqlConfigDetailDto> ListColumnConfig { get; set; }
	}
}
