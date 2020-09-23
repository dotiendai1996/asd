using System.Collections.Generic;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB.Exporting
{
    public interface ITextBooksExcelExporter
    {
        FileDto ExportToFile(List<GetTextBookForViewDto> textBooks);
    }
}