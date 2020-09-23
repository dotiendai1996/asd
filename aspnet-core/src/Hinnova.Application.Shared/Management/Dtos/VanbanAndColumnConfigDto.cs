using Hinnova.QLVB.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.Management.Dtos
{
	public class VanbanAndColumnConfigDto 
	{
		public VanbanAndColumnConfigDto(List<VanbanDto> vanBan, List<SqlConfigDetailDto> columnConfig)
		{
			this.ListVanBan = vanBan;
			this.ListColumnConfig = columnConfig;
		}

		public List<VanbanDto> ListVanBan { get; set; }

		public List<SqlConfigDetailDto> ListColumnConfig { get; set; }

	}
}
