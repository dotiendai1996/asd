using System.Collections.Generic;
using Hinnova.Management.Dtos;
using Hinnova.Dto;

namespace Hinnova.Management.Exporting
{
    public interface ISqlConfigDetailsExcelExporter
    {
        FileDto ExportToFile(List<GetSqlConfigDetailForViewDto> sqlConfigDetails);
    }
}