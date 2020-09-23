using System.Collections.Generic;
using Hinnova.QLNSDtos;
using Hinnova.Dto;

namespace Hinnova.QLNSExporting
{
    public interface IHopDongsExcelExporter
    {
        FileDto ExportToFile(List<GetHopDongForViewDto> hopDongs);
    }
}