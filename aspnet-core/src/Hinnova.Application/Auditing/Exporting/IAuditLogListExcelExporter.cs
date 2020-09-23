using System.Collections.Generic;
using Hinnova.Auditing.Dto;
using Hinnova.Dto;

namespace Hinnova.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
