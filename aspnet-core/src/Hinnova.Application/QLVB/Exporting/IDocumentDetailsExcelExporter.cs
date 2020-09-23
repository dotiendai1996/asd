using System.Collections.Generic;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB.Exporting
{
    public interface IDocumentDetailsExcelExporter
    {
        FileDto ExportToFile(List<GetDocumentDetailForViewDto> documentDetails);
    }
}