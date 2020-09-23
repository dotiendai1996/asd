using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.Management.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.Management
{
    public interface ISqlStoreParamsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetSqlStoreParamForViewDto>> GetAll(GetAllSqlStoreParamsInput input);

        Task<GetSqlStoreParamForViewDto> GetSqlStoreParamForView(int id);

		Task<GetSqlStoreParamForEditOutput> GetSqlStoreParamForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditSqlStoreParamDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetSqlStoreParamsToExcel(GetAllSqlStoreParamsForExcelInput input);

        List<SqlConfigDto> GetAllStore();
    }
}