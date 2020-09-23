using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.Management.Dtos;
using Hinnova.Dto;

namespace Hinnova.Management
{
    public interface ISqlConfigsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetSqlConfigForViewDto>> GetAll(GetAllSqlConfigsInput input);

        Task<GetSqlConfigForViewDto> GetSqlConfigForView(int id);

		Task<GetSqlConfigForEditOutput> GetSqlConfigForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditSqlConfigDto input);

		Task Delete(EntityDto input);

        Task<SqlConfigDto> GetSqlConfigByCodeAsync(string code);
    }
}