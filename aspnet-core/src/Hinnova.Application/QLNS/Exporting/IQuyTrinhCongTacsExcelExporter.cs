using System.Collections.Generic;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLNS.Exporting
{
    public interface IQuyTrinhCongTacsExcelExporter
    {
        FileDto ExportToFile(List<GetQuyTrinhCongTacForViewDto> quyTrinhCongTacs);
    }
}