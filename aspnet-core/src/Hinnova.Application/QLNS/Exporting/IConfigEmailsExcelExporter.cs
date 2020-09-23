using System.Collections.Generic;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLNS.Exporting
{
    public interface IConfigEmailsExcelExporter
    {
        FileDto ExportToFile(List<GetConfigEmailForViewDto> configEmails);
    }
}