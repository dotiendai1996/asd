using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.Authorization.Users.Dto;
using Hinnova.Dto;
using Hinnova.QLNSDtos;

namespace Hinnova.Authorization.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task<PagedResultDto<UserListDto>> GetUsers(GetUsersInput input);

        Task<FileDto> GetUsersToExcel(GetUsersToExcelInput input);

        FileDto GetListUsersToExcel();
        Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input);

        Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(EntityDto<long> input);

        Task ResetUserSpecificPermissions(EntityDto<long> input);

        Task UpdateUserPermissions(UpdateUserPermissionsInput input);

        Task CreateOrUpdateUser(CreateOrUpdateUserInput input);

        Task DeleteUser(EntityDto<long> input);

        Task UnlockUser(EntityDto<long> input);

        Task<string> GetRoleNameOfUser(long id);

        Task<int> GetRoleIdOfUser(long id);

        List<TruongGiaoDichDto> GetProfileStatus();
    }
}