using System.Collections.Generic;
using Hinnova.Chat.Dto;
using Hinnova.Dto;

namespace Hinnova.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(List<ChatMessageExportDto> messages);
    }
}
