using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.Management.Dtos;
using Hinnova.Dto;

namespace Hinnova.Management
{
    public interface ISettingConfigsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetSettingConfigForViewDto>> GetAll(GetAllSettingConfigsInput input);

        Task<GetSettingConfigForViewDto> GetSettingConfigForView(int id);

		Task<GetSettingConfigForEditOutput> GetSettingConfigForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditSettingConfigDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetSettingConfigsToExcel(GetAllSettingConfigsForExcelInput input);

		
    }
}