using Hinnova.QLNSDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.QLNS.Importing
{
    public class HoSoImportDto : CreateOrEditHoSoDto
    {

        public string NoiDaoTaoName { get; set; }

        /// <summary>
        /// Can be set when reading data from excel or when importing user
        /// </summary>
        public string Exception { get; set; }

        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}
