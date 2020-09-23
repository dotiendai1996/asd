

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLVB.Exporting;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Hinnova.QLVB
{
    [AbpAuthorize(AppPermissions.Pages_HardDatasources)]
    public class HardDatasourcesAppService : HinnovaAppServiceBase, IHardDatasourcesAppService
    {
        private readonly IRepository<HardDatasource> _hardDatasourceRepository;
        private readonly IHardDatasourcesExcelExporter _hardDatasourcesExcelExporter;


        public HardDatasourcesAppService(IRepository<HardDatasource> hardDatasourceRepository, IHardDatasourcesExcelExporter hardDatasourcesExcelExporter)
        {
            _hardDatasourceRepository = hardDatasourceRepository;
            _hardDatasourcesExcelExporter = hardDatasourcesExcelExporter;

        }

        public async Task<PagedResultDto<GetHardDatasourceForViewDto>> GetAll(GetAllHardDatasourcesInput input)
        {

            var filteredHardDatasources = _hardDatasourceRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter) || e.Value.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter), e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter), e => e.Value.ToLower() == input.ValueFilter.ToLower().Trim())
                        .WhereIf(input.MinDynamicDatasourceIdFilter != null, e => e.DynamicDatasourceId >= input.MinDynamicDatasourceIdFilter)
                        .WhereIf(input.MaxDynamicDatasourceIdFilter != null, e => e.DynamicDatasourceId <= input.MaxDynamicDatasourceIdFilter)
                        .WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
                        .WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter)
                        .WhereIf(input.IsActiveFilter > -1, e => Convert.ToInt32(e.IsActive) == input.IsActiveFilter);

            var pagedAndFilteredHardDatasources = filteredHardDatasources
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var hardDatasources = from o in pagedAndFilteredHardDatasources
                                  select new GetHardDatasourceForViewDto()
                                  {
                                      HardDatasource = new HardDatasourceDto
                                      {
                                          Key = o.Key,
                                          Value = o.Value,
                                          DynamicDatasourceId = o.DynamicDatasourceId,
                                          Order = o.Order,
                                          IsActive = o.IsActive,
                                          Id = o.Id
                                      }
                                  };

            var totalCount = await filteredHardDatasources.CountAsync();

            return new PagedResultDto<GetHardDatasourceForViewDto>(
                totalCount,
                await hardDatasources.ToListAsync()
            );
        }

        public async Task<GetHardDatasourceForViewDto> GetHardDatasourceForView(int id)
        {
            var hardDatasource = await _hardDatasourceRepository.GetAsync(id);

            var output = new GetHardDatasourceForViewDto { HardDatasource = ObjectMapper.Map<HardDatasourceDto>(hardDatasource) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_HardDatasources_Edit)]
        public async Task<GetHardDatasourceForEditOutput> GetHardDatasourceForEdit(EntityDto input)
        {
            var hardDatasource = await _hardDatasourceRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetHardDatasourceForEditOutput { HardDatasource = ObjectMapper.Map<CreateOrEditHardDatasourceDto>(hardDatasource) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditHardDatasourceDto input)
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

        [AbpAuthorize(AppPermissions.Pages_HardDatasources_Create)]
        protected virtual async Task Create(CreateOrEditHardDatasourceDto input)
        {
            var hardDatasource = ObjectMapper.Map<HardDatasource>(input);


            if (AbpSession.TenantId != null)
            {
                hardDatasource.TenantId = (int?)AbpSession.TenantId;
            }


            await _hardDatasourceRepository.InsertAsync(hardDatasource);
        }

        [AbpAuthorize(AppPermissions.Pages_HardDatasources_Edit)]
        protected virtual async Task Update(CreateOrEditHardDatasourceDto input)
        {
            var hardDatasource = await _hardDatasourceRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, hardDatasource);
        }

        [AbpAuthorize(AppPermissions.Pages_HardDatasources_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _hardDatasourceRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetHardDatasourcesToExcel(GetAllHardDatasourcesForExcelInput input)
        {

            var filteredHardDatasources = _hardDatasourceRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter) || e.Value.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter), e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter), e => e.Value.ToLower() == input.ValueFilter.ToLower().Trim())
                        .WhereIf(input.MinDynamicDatasourceIdFilter != null, e => e.DynamicDatasourceId >= input.MinDynamicDatasourceIdFilter)
                        .WhereIf(input.MaxDynamicDatasourceIdFilter != null, e => e.DynamicDatasourceId <= input.MaxDynamicDatasourceIdFilter)
                        .WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
                        .WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter)
                        .WhereIf(input.IsActiveFilter > -1, e => Convert.ToInt32(e.IsActive) == input.IsActiveFilter);

            var query = (from o in filteredHardDatasources
                         select new GetHardDatasourceForViewDto()
                         {
                             HardDatasource = new HardDatasourceDto
                             {
                                 Key = o.Key,
                                 Value = o.Value,
                                 DynamicDatasourceId = o.DynamicDatasourceId,
                                 Order = o.Order,
                                 IsActive = o.IsActive,
                                 Id = o.Id
                             }
                         });


            var hardDatasourceListDtos = await query.ToListAsync();

            return _hardDatasourcesExcelExporter.ExportToFile(hardDatasourceListDtos);
        }


    }
}