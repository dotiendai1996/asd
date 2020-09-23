using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.QLVB
{
    public interface IDynamicDatasourceAppService : IApplicationService 
    {
        Task<PagedResultDto<GetDynamicDatasourceForViewDto>> GetAll(GetAllDynamicDatasourceInput input);

        Task<GetDynamicDatasourceForViewDto> GetDynamicDatasourceForView(int id);

		Task<GetDynamicDatasourceForEditOutput> GetDynamicDatasourceForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditDynamicDatasourceDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetDynamicDatasourceToExcel(GetAllDynamicDatasourceForExcelInput input);

        List<DynamicDatasourceDto> GetDynamicDatasourceByType(int type);
    }
}