using System.Collections.Generic;
using Hinnova.QLNSDtos;
using Hinnova.Dto;

namespace Hinnova.QLNSExporting
{
    public interface ISYS_PREFIXsExcelExporter
    {
        FileDto ExportToFile(List<GetSYS_PREFIXForViewDto> syS_PREFIXs);
    }
}