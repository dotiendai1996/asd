using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.Management.Dtos;
using Hinnova.Organizations.Dto;

namespace Hinnova.Organizations
{
    public interface IOrganizationUnitAppService : IApplicationService
    {
        Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnits();

        Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input);

        Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input);

        Task<OrganizationUnitDto> UpdateOrganizationUnit(UpdateOrganizationUnitInput input);

        Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input);

        Task DeleteOrganizationUnit(EntityDto<long> input);
        //Task<GetOrganizationUnitForEditOutput> GetOrganizationUnitForEdit(int id);

        Task RemoveUserFromOrganizationUnit(UserToOrganizationUnitInput input);

        Task RemoveRoleFromOrganizationUnit(RoleToOrganizationUnitInput input);

        Task AddUsersToOrganizationUnit(UsersToOrganizationUnitInput input);

        Task AddRolesToOrganizationUnit(RolesToOrganizationUnitInput input);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindOrganizationUnitUsersInput input);

        Task<PagedResultDto<NameValueDto>> FindRoles(FindOrganizationUnitRolesInput input);
    }
}
