using System.Collections.Generic;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB.Exporting
{
    public interface IWordProcessingsExcelExporter
    {
        FileDto ExportToFile(List<GetWordProcessingForViewDto> wordProcessings);
    }
}