using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface IRoleMappersAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRoleMapperForViewDto>> GetAll(GetAllRoleMappersInput input);

        Task<GetRoleMapperForViewDto> GetRoleMapperForView(int id);

		Task<GetRoleMapperForEditOutput> GetRoleMapperForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditRoleMapperDto input);

		Task Delete(EntityDto input);

		
    }
}