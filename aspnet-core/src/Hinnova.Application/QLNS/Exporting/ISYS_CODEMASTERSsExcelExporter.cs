using System.Collections.Generic;
using Hinnova.QLNSDtos;
using Hinnova.Dto;

namespace Hinnova.QLNSExporting
{
    public interface ISYS_CODEMASTERSsExcelExporter
    {
        FileDto ExportToFile(List<GetSYS_CODEMASTERSForViewDto> syS_CODEMASTERSs);
    }
}