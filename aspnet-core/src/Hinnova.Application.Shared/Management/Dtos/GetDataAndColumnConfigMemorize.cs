using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.Management.Dtos
{
    public  class GetDataAndColumnConfigMemorize
    {
		public GetDataAndColumnConfigMemorize(List<object> data, List<SqlConfigDetailDto> columnConfig)
		{
			this.ListMemorize = data;

			this.ListColumnConfigMemorize = columnConfig;
		}

		public List<object> ListMemorize { get; set; }


		public List<SqlConfigDetailDto> ListColumnConfigMemorize { get; set; }
	}
}
