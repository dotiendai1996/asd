

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.Management.Exporting;
using Hinnova.Management.Dtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Hinnova.Management
{
    [AbpAuthorize(AppPermissions.Pages_SqlStoreParams)]
    public class SqlStoreParamsAppService : HinnovaAppServiceBase, ISqlStoreParamsAppService
    {
        private readonly IRepository<SqlStoreParam> _sqlStoreParamRepository;
        private readonly IRepository<SqlConfig> _sqlConfigRepository;
        private readonly ISqlStoreParamsExcelExporter _sqlStoreParamsExcelExporter;


        public SqlStoreParamsAppService(IRepository<SqlStoreParam> sqlStoreParamRepository, ISqlStoreParamsExcelExporter sqlStoreParamsExcelExporter, IRepository<SqlConfig> sqlConfigRepository)
        {
            _sqlStoreParamRepository = sqlStoreParamRepository;
            _sqlStoreParamsExcelExporter = sqlStoreParamsExcelExporter;
            _sqlConfigRepository = sqlConfigRepository;
        }

        //Lấy ra danh sách store
        public List<SqlConfigDto> GetAllStore()
        {
            return ObjectMapper.Map<List<SqlConfigDto>>(_sqlConfigRepository.GetAll().Where(x => x.IsRawQuery == false).ToList());
        }

        public async Task<PagedResultDto<GetSqlStoreParamForViewDto>> GetAll(GetAllSqlStoreParamsInput input)
        {

            var filteredSqlStoreParams = _sqlStoreParamRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Code.Contains(input.Filter) || e.Format.Contains(input.Filter) || e.Name.Contains(input.Filter) || e.ValueString.Contains(input.Filter))
                        .WhereIf(input.MinSqlConfigIdFilter != null, e => e.SqlConfigId >= input.MinSqlConfigIdFilter)
                        .WhereIf(input.MaxSqlConfigIdFilter != null, e => e.SqlConfigId <= input.MaxSqlConfigIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter), e => e.Code.ToLower() == input.CodeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FormatFilter), e => e.Format.ToLower() == input.FormatFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                        .WhereIf(input.IsActiveFilter > -1, e => Convert.ToInt32(e.IsActive) == input.IsActiveFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueStringFilter), e => e.ValueString.ToLower() == input.ValueStringFilter.ToLower().Trim())
                        .WhereIf(input.MinValueIntFilter != null, e => e.ValueInt >= input.MinValueIntFilter)
                        .WhereIf(input.MaxValueIntFilter != null, e => e.ValueInt <= input.MaxValueIntFilter);

            var pagedAndFilteredSqlStoreParams = filteredSqlStoreParams
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var sqlStoreParams = from o in pagedAndFilteredSqlStoreParams
                                 select new GetSqlStoreParamForViewDto()
                                 {
                                     SqlStoreParam = new SqlStoreParamDto
                                     {
                                         SqlConfigId = o.SqlConfigId,
                                         Code = o.Code,
                                         Format = o.Format,
                                         Name = o.Name,
                                         IsActive = o.IsActive,
                                         ValueString = o.ValueString,
                                         ValueInt = o.ValueInt,
                                         Id = o.Id
                                     }
                                 };

            var totalCount = await filteredSqlStoreParams.CountAsync();

            return new PagedResultDto<GetSqlStoreParamForViewDto>(
                totalCount,
                await sqlStoreParams.ToListAsync()
            );
        }

        public async Task<GetSqlStoreParamForViewDto> GetSqlStoreParamForView(int id)
        {
            var sqlStoreParam = await _sqlStoreParamRepository.GetAsync(id);

            var output = new GetSqlStoreParamForViewDto { SqlStoreParam = ObjectMapper.Map<SqlStoreParamDto>(sqlStoreParam) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_SqlStoreParams_Edit)]
        public async Task<GetSqlStoreParamForEditOutput> GetSqlStoreParamForEdit(EntityDto input)
        {
            var sqlStoreParam = await _sqlStoreParamRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetSqlStoreParamForEditOutput { SqlStoreParam = ObjectMapper.Map<CreateOrEditSqlStoreParamDto>(sqlStoreParam) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditSqlStoreParamDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_SqlStoreParams_Create)]
        protected virtual async Task Create(CreateOrEditSqlStoreParamDto input)
        {
            var sqlStoreParam = ObjectMapper.Map<SqlStoreParam>(input);


            if (AbpSession.TenantId != null)
            {
                sqlStoreParam.TenantId = (int?)AbpSession.TenantId;
            }


            await _sqlStoreParamRepository.InsertAsync(sqlStoreParam);
        }

        [AbpAuthorize(AppPermissions.Pages_SqlStoreParams_Edit)]
        protected virtual async Task Update(CreateOrEditSqlStoreParamDto input)
        {
            var sqlStoreParam = await _sqlStoreParamRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, sqlStoreParam);
        }

        [AbpAuthorize(AppPermissions.Pages_SqlStoreParams_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _sqlStoreParamRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetSqlStoreParamsToExcel(GetAllSqlStoreParamsForExcelInput input)
        {

            var filteredSqlStoreParams = _sqlStoreParamRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Code.Contains(input.Filter) || e.Format.Contains(input.Filter) || e.Name.Contains(input.Filter) || e.ValueString.Contains(input.Filter))
                        .WhereIf(input.MinSqlConfigIdFilter != null, e => e.SqlConfigId >= input.MinSqlConfigIdFilter)
                        .WhereIf(input.MaxSqlConfigIdFilter != null, e => e.SqlConfigId <= input.MaxSqlConfigIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter), e => e.Code.ToLower() == input.CodeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FormatFilter), e => e.Format.ToLower() == input.FormatFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                        .WhereIf(input.IsActiveFilter > -1, e => Convert.ToInt32(e.IsActive) == input.IsActiveFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueStringFilter), e => e.ValueString.ToLower() == input.ValueStringFilter.ToLower().Trim())
                        .WhereIf(input.MinValueIntFilter != null, e => e.ValueInt >= input.MinValueIntFilter)
                        .WhereIf(input.MaxValueIntFilter != null, e => e.ValueInt <= input.MaxValueIntFilter);

            var query = (from o in filteredSqlStoreParams
                         select new GetSqlStoreParamForViewDto()
                         {
                             SqlStoreParam = new SqlStoreParamDto
                             {
                                 SqlConfigId = o.SqlConfigId,
                                 Code = o.Code,
                                 Format = o.Format,
                                 Name = o.Name,
                                 IsActive = o.IsActive,
                                 ValueString = o.ValueString,
                                 ValueInt = o.ValueInt,
                                 Id = o.Id
                             }
                         });


            var sqlStoreParamListDtos = await query.ToListAsync();

            return _sqlStoreParamsExcelExporter.ExportToFile(sqlStoreParamListDtos);
        }


    }
}