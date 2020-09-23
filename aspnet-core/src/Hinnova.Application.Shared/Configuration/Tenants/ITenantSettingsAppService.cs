using System.Threading.Tasks;
using Abp.Application.Services;
using Hinnova.Configuration.Tenants.Dto;

namespace Hinnova.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
