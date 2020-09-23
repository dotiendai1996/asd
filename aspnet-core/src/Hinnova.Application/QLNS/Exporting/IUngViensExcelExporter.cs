using System.Collections.Generic;
using Hinnova.QLNSDtos;
using Hinnova.Dto;

namespace Hinnova.QLNSExporting
{
    public interface IUngViensExcelExporter
    {
        FileDto ExportToFile(List<GetUngVienForViewDto> ungViens);
    }
}