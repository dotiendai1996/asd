using System.Threading.Tasks;
using Abp.Application.Services;
using Hinnova.Configuration.Host.Dto;

namespace Hinnova.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
