using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface IDynamicActionsAppService : IApplicationService 
    {
        //Task<PagedResultDto<GetDynamicActionForViewDto>> GetAll(GetAllDynamicActionsInput input);

        //Task<GetDynamicActionForViewDto> GetDynamicActionForView(int id);

        Task<GetDynamicActionForViewDto> GetDynamicActionForView(int id);


        Task<GetDynamicActionForEditOutput> GetDynamicActionForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditDynamicActionDto input);

		Task Delete(EntityDto input);

        //Task<FileDto> GetDynamicActionsToExcel(GetAllDynamicActionsForExcelInput input);

        Task<DynamicActionDto> GetDynamicActionByLabelId(int labelId);

        Task<CreateOrEditDynamicActionDto> GetAllDynamicActionByLabelId(int labelId, int roleId, int? tenantId);
    }
}