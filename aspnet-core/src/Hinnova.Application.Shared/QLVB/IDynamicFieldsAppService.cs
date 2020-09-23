using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.QLVB
{
    public interface IDynamicFieldsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetDynamicFieldForViewDto>> GetAll(GetAllDynamicFieldsInput input);

        Task<GetDynamicFieldForViewDto> GetDynamicFieldForView(int id);

		Task<GetDynamicFieldForEditOutput> GetDynamicFieldForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditDynamicFieldDto input);

		Task Delete(EntityDto input);

        Task<List<DynamicFieldListDto>> GetDynamicFields(GetDynamicFieldListInput input);

        Task InsertUpdateDynamicFields(List<DynamicValueDto> input);

        Task CreateDynamicFieldForModule(List<CreateOrEditDynamicFieldDto> input);

        List<DynamicFieldDto> GetDynamicFieldByModuleId(int moduleId);

        List<DynamicFieldDto> GetCbbField(int? moduleId);
    }
}