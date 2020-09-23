

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
	[AbpAuthorize(AppPermissions.Pages_StoreDatasources)]
    public class StoreDatasourcesAppService : HinnovaAppServiceBase, IStoreDatasourcesAppService
    {
		 private readonly IRepository<StoreDatasource> _storeDatasourceRepository;
		 private readonly IStoreDatasourcesExcelExporter _storeDatasourcesExcelExporter;
		 

		  public StoreDatasourcesAppService(IRepository<StoreDatasource> storeDatasourceRepository, IStoreDatasourcesExcelExporter storeDatasourcesExcelExporter ) 
		  {
			_storeDatasourceRepository = storeDatasourceRepository;
			_storeDatasourcesExcelExporter = storeDatasourcesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetStoreDatasourceForViewDto>> GetAll(GetAllStoreDatasourcesInput input)
         {
			
			var filteredStoreDatasources = _storeDatasourceRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.NameStore.Contains(input.Filter) || e.Key.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameStoreFilter),  e => e.NameStore.ToLower() == input.NameStoreFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter),  e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter),  e => e.Value.ToLower() == input.ValueFilter.ToLower().Trim())
						.WhereIf(input.MinDynamicDatasourceIdFilter != null, e => e.DynamicDatasourceId >= input.MinDynamicDatasourceIdFilter)
						.WhereIf(input.MaxDynamicDatasourceIdFilter != null, e => e.DynamicDatasourceId <= input.MaxDynamicDatasourceIdFilter)
						.WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
						.WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter)
						.WhereIf(input.IsActiveFilter > -1,  e => Convert.ToInt32(e.IsActive) == input.IsActiveFilter );

			var pagedAndFilteredStoreDatasources = filteredStoreDatasources
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var storeDatasources = from o in pagedAndFilteredStoreDatasources
                         select new GetStoreDatasourceForViewDto() {
							StoreDatasource = new StoreDatasourceDto
							{
                                NameStore = o.NameStore,
                                Key = o.Key,
                                Value = o.Value,
                                DynamicDatasourceId = o.DynamicDatasourceId,
                                Order = o.Order,
                                IsActive = o.IsActive,
                                Id = o.Id
							}
						};

            var totalCount = await filteredStoreDatasources.CountAsync();

            return new PagedResultDto<GetStoreDatasourceForViewDto>(
                totalCount,
                await storeDatasources.ToListAsync()
            );
         }
		 
		 public async Task<GetStoreDatasourceForViewDto> GetStoreDatasourceForView(int id)
         {
            var storeDatasource = await _storeDatasourceRepository.GetAsync(id);

            var output = new GetStoreDatasourceForViewDto { StoreDatasource = ObjectMapper.Map<StoreDatasourceDto>(storeDatasource) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_StoreDatasources_Edit)]
		 public async Task<GetStoreDatasourceForEditOutput> GetStoreDatasourceForEdit(EntityDto input)
         {
            var storeDatasource = await _storeDatasourceRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetStoreDatasourceForEditOutput {StoreDatasource = ObjectMapper.Map<CreateOrEditStoreDatasourceDto>(storeDatasource)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditStoreDatasourceDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_StoreDatasources_Create)]
		 protected virtual async Task Create(CreateOrEditStoreDatasourceDto input)
         {
            var storeDatasource = ObjectMapper.Map<StoreDatasource>(input);

			
			if (AbpSession.TenantId != null)
			{
				storeDatasource.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _storeDatasourceRepository.InsertAsync(storeDatasource);
         }

		 [AbpAuthorize(AppPermissions.Pages_StoreDatasources_Edit)]
		 protected virtual async Task Update(CreateOrEditStoreDatasourceDto input)
         {
            var storeDatasource = await _storeDatasourceRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, storeDatasource);
         }

		 [AbpAuthorize(AppPermissions.Pages_StoreDatasources_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _storeDatasourceRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetStoreDatasourcesToExcel(GetAllStoreDatasourcesForExcelInput input)
         {
			
			var filteredStoreDatasources = _storeDatasourceRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.NameStore.Contains(input.Filter) || e.Key.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameStoreFilter),  e => e.NameStore.ToLower() == input.NameStoreFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter),  e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter),  e => e.Value.ToLower() == input.ValueFilter.ToLower().Trim())
						.WhereIf(input.MinDynamicDatasourceIdFilter != null, e => e.DynamicDatasourceId >= input.MinDynamicDatasourceIdFilter)
						.WhereIf(input.MaxDynamicDatasourceIdFilter != null, e => e.DynamicDatasourceId <= input.MaxDynamicDatasourceIdFilter)
						.WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
						.WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter)
						.WhereIf(input.IsActiveFilter > -1,  e => Convert.ToInt32(e.IsActive) == input.IsActiveFilter );

			var query = (from o in filteredStoreDatasources
                         select new GetStoreDatasourceForViewDto() { 
							StoreDatasource = new StoreDatasourceDto
							{
                                NameStore = o.NameStore,
                                Key = o.Key,
                                Value = o.Value,
                                DynamicDatasourceId = o.DynamicDatasourceId,
                                Order = o.Order,
                                IsActive = o.IsActive,
                                Id = o.Id
							}
						 });


            var storeDatasourceListDtos = await query.ToListAsync();

            return _storeDatasourcesExcelExporter.ExportToFile(storeDatasourceListDtos);
         }


    }
}