

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.Management.Dtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Hinnova.Management
{
    [AbpAuthorize(AppPermissions.Pages_SqlConfigs)]
    public class SqlConfigsAppService : HinnovaAppServiceBase, ISqlConfigsAppService
    {
        private readonly IRepository<SqlConfig> _sqlConfigRepository;


        public SqlConfigsAppService(IRepository<SqlConfig> sqlConfigRepository)
        {
            _sqlConfigRepository = sqlConfigRepository;

        }

        public async Task<SqlConfigDto> GetSqlConfigByCodeAsync(string code)
        {
            var res = await _sqlConfigRepository.GetAll().Where(x => x.Code == code).FirstOrDefaultAsync();
            return ObjectMapper.Map<SqlConfigDto>(res);
        }

        public async Task<PagedResultDto<GetSqlConfigForViewDto>> GetAll(GetAllSqlConfigsInput input)
        {

            var filteredSqlConfigs = _sqlConfigRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter) || e.SqlContent.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter), e => e.Code.ToLower() == input.CodeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                        .WhereIf(input.IsRawQueryFilter > -1, e => Convert.ToInt32(e.IsRawQuery) == input.IsRawQueryFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SqlContentFilter), e => e.SqlContent.ToLower() == input.SqlContentFilter.ToLower().Trim())
                        .WhereIf(input.MinGroupLevelFilter != null, e => e.GroupLevel >= input.MinGroupLevelFilter)
                        .WhereIf(input.MaxGroupLevelFilter != null, e => e.GroupLevel <= input.MaxGroupLevelFilter)
                        .WhereIf(input.MinDisplayTypeFilter != null, e => e.DisplayType >= input.MinDisplayTypeFilter)
                        .WhereIf(input.MaxDisplayTypeFilter != null, e => e.DisplayType <= input.MaxDisplayTypeFilter)
                        .WhereIf(input.MinVersionFilter != null, e => e.Version >= input.MinVersionFilter)
                        .WhereIf(input.MaxVersionFilter != null, e => e.Version <= input.MaxVersionFilter)
                        .WhereIf(input.IsDynamicColumnFilter > -1, e => Convert.ToInt32(e.IsDynamicColumn) == input.IsDynamicColumnFilter)
                        .WhereIf(input.MinTypeGetColumnFilter != null, e => e.TypeGetColumn >= input.MinTypeGetColumnFilter)
                        .WhereIf(input.MaxTypeGetColumnFilter != null, e => e.TypeGetColumn <= input.MaxTypeGetColumnFilter);

            var pagedAndFilteredSqlConfigs = filteredSqlConfigs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var sqlConfigs = from o in pagedAndFilteredSqlConfigs
                             select new GetSqlConfigForViewDto()
                             {
                                 SqlConfig = new SqlConfigDto
                                 {
                                     Code = o.Code,
                                     Name = o.Name,
                                     IsRawQuery = o.IsRawQuery,
                                     SqlContent = o.SqlContent,
                                     GroupLevel = o.GroupLevel,
                                     DisplayType = o.DisplayType,
                                     Version = o.Version,
                                     IsDynamicColumn = o.IsDynamicColumn,
                                     TypeGetColumn = o.TypeGetColumn,
                                     Id = o.Id
                                 }
                             };

            var totalCount = await filteredSqlConfigs.CountAsync();

            return new PagedResultDto<GetSqlConfigForViewDto>(
                totalCount,
                await sqlConfigs.ToListAsync()
            );
        }

        public async Task<GetSqlConfigForViewDto> GetSqlConfigForView(int id)
        {
            var sqlConfig = await _sqlConfigRepository.GetAsync(id);

            var output = new GetSqlConfigForViewDto { SqlConfig = ObjectMapper.Map<SqlConfigDto>(sqlConfig) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_SqlConfigs_Edit)]
        public async Task<GetSqlConfigForEditOutput> GetSqlConfigForEdit(EntityDto input)
        {
            var sqlConfig = await _sqlConfigRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetSqlConfigForEditOutput { SqlConfig = ObjectMapper.Map<CreateOrEditSqlConfigDto>(sqlConfig) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditSqlConfigDto input)
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

        [AbpAuthorize(AppPermissions.Pages_SqlConfigs_Create)]
        protected virtual async Task Create(CreateOrEditSqlConfigDto input)
        {
            var sqlConfig = ObjectMapper.Map<SqlConfig>(input);


            if (AbpSession.TenantId != null)
            {
                sqlConfig.TenantId = (int?)AbpSession.TenantId;
            }


            await _sqlConfigRepository.InsertAsync(sqlConfig);
        }

        [AbpAuthorize(AppPermissions.Pages_SqlConfigs_Edit)]
        protected virtual async Task Update(CreateOrEditSqlConfigDto input)
        {
            var sqlConfig = await _sqlConfigRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, sqlConfig);
        }

        [AbpAuthorize(AppPermissions.Pages_SqlConfigs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _sqlConfigRepository.DeleteAsync(input.Id);
        }
    }
}