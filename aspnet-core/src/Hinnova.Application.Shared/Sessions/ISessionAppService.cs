using System.Threading.Tasks;
using Abp.Application.Services;
using Hinnova.Sessions.Dto;

namespace Hinnova.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
