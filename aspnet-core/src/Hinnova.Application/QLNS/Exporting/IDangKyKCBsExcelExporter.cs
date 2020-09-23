using System.Collections.Generic;
using Hinnova.QLNSDtos;
using Hinnova.Dto;

namespace Hinnova.QLNSExporting
{
    public interface IDangKyKCBsExcelExporter
    {
        FileDto ExportToFile(List<GetDangKyKCBForViewDto> dangKyKCBs);
    }
}