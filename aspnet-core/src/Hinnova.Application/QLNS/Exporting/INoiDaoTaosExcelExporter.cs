using System.Collections.Generic;
using Hinnova.QLNSDtos;
using Hinnova.Dto;

namespace Hinnova.QLNSExporting
{
    public interface INoiDaoTaosExcelExporter
    {
        FileDto ExportToFile(List<GetNoiDaoTaoForViewDto> noiDaoTaos);
    }
}