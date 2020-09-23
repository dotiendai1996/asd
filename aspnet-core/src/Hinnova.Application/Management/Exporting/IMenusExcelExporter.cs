using System.Collections.Generic;
using Hinnova.Management.Dtos;
using Hinnova.Dto;

namespace Hinnova.Management.Exporting
{
    public interface IMenusExcelExporter
    {
        FileDto ExportToFile(List<GetMenuForViewDto> menus);
    }
}