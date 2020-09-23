using System.Threading.Tasks;
using Abp.Application.Services;
using Hinnova.Editions.Dto;
using Hinnova.MultiTenancy.Dto;

namespace Hinnova.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}