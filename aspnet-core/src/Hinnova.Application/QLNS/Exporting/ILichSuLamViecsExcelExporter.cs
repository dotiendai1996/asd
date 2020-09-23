using System.Collections.Generic;
using Hinnova.QLNSDtos;
using Hinnova.Dto;

namespace Hinnova.QLNSExporting
{
    public interface ILichSuLamViecsExcelExporter
    {
        FileDto ExportToFile(List<GetLichSuLamViecForViewDto> lichSuLamViecs);
    }
}