using System.Collections.Generic;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using System.Data;
using System;

namespace Hinnova.QLNS.Exporting
{
    public interface IDataChamCongsExcelExporter
    {
        FileDto ExportToFile(DataSet dataChamCongs, DateTime dateTime);
        FileDto ExportToFile(DataSet dataChamCongs);
    }
}