using System.Collections.Generic;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB.Exporting
{
    public interface IMemorize_KeywordsesExcelExporter
    {
        FileDto ExportToFile(List<GetMemorize_KeywordsForViewDto> memorize_Keywordses);
    }
}