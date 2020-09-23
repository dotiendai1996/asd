using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.QLVB.Dtos
{
    public class DataGridData
    {
        public string Key { get; set; }

        public List<HandlingUser> Items { get; set; }

        public DataGridData()
        {
            Items = new List<HandlingUser>();
        }
    }
}
