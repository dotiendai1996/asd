using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface IDynamicValuesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetDynamicValueForViewDto>> GetAll(GetAllDynamicValuesInput input);

        Task<GetDynamicValueForViewDto> GetDynamicValueForView(int id);

		Task<GetDynamicValueForEditOutput> GetDynamicValueForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditDynamicValueDto input);

		Task Delete(EntityDto input);

		
    }
}