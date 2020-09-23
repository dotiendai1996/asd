using Abp.Dependency;
using Hinnova.QLNSDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.QLNS.Importing
{
    public interface IUngVienListExcelDataReader : ITransientDependency
    {
        List<UngVienImportDto> GetUngVienFromExcel(byte[] fileBytes);
    }
}
