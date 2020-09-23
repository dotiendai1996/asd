using System.Collections.Generic;
using Hinnova.QLNSDtos;
using Hinnova.Dto;

namespace Hinnova.QLNSExporting
{
    public interface ITruongGiaoDichsExcelExporter
    {
        FileDto ExportToFile(List<GetTruongGiaoDichForViewDto> truongGiaoDichs);
    }
}