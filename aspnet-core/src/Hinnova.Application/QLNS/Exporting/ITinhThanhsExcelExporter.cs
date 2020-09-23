using System.Collections.Generic;
using Hinnova.QLNSDtos;
using Hinnova.Dto;

namespace Hinnova.QLNSExporting
{
    public interface ITinhThanhsExcelExporter
    {
        FileDto ExportToFile(List<GetTinhThanhForViewDto> tinhThanhs);
    }
}