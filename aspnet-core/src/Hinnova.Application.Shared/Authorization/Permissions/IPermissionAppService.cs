using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.Authorization.Permissions.Dto;

namespace Hinnova.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
