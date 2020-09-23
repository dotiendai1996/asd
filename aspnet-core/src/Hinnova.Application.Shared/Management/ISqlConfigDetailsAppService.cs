using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.Management.Dtos;
using Hinnova.Dto;
using Hinnova.Managerment.Dtos;
using System.Collections.Generic;

namespace Hinnova.Management
{
    public interface ISqlConfigDetailsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetSqlConfigDetailForViewDto>> GetAll(GetAllSqlConfigDetailsInput input);

        Task<GetSqlConfigDetailForViewDto> GetSqlConfigDetailForView(int id);

		Task<GetSqlConfigDetailForEditOutput> GetSqlConfigDetailForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditSqlConfigDetailDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetSqlConfigDetailsToExcel(GetAllSqlConfigDetailsForExcelInput input);

        DataVm GetColumn(int sqlConfigId, string sqlContent);

        Task CreateConfigIfNotExistsAsync(int sqlConfigId, SqlConfigDetailDto[] listdata);

        List<SqlConfigDetailDto> GetColumnConfigBySqlId(int id);
    }
}