using Abp.Application.Services;
using Hinnova.Dto;
using Hinnova.Logging.Dto;

namespace Hinnova.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
