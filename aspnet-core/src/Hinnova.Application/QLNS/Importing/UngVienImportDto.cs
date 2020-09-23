using Hinnova.QLNSDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.QLNS.Importing
{
    public class UngVienImportDto : CreateOrEditUngVienDto
    {
        public string TinhThanhName { get; set; }
        public string NoiDaoTaoName { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}
