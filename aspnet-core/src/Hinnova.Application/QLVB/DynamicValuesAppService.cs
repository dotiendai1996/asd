

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Hinnova.QLVB
{
	[AbpAuthorize(AppPermissions.Pages_DynamicValues)]
    public class DynamicValuesAppService : HinnovaAppServiceBase, IDynamicValuesAppService
    {
		 private readonly IRepository<DynamicValue> _dynamicValueRepository;
		 

		  public DynamicValuesAppService(IRepository<DynamicValue> dynamicValueRepository ) 
		  {
			_dynamicValueRepository = dynamicValueRepository;
			
		  }

		 public async Task<PagedResultDto<GetDynamicValueForViewDto>> GetAll(GetAllDynamicValuesInput input)
         {
			
			var filteredDynamicValues = _dynamicValueRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(input.MinObjectIdFilter != null, e => e.ObjectId >= input.MinObjectIdFilter)
						.WhereIf(input.MaxObjectIdFilter != null, e => e.ObjectId <= input.MaxObjectIdFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter),  e => e.Key == input.KeyFilter)
						.WhereIf(input.MinDynamicFieldIdFilter != null, e => e.DynamicFieldId >= input.MinDynamicFieldIdFilter)
						.WhereIf(input.MaxDynamicFieldIdFilter != null, e => e.DynamicFieldId <= input.MaxDynamicFieldIdFilter)
						.WhereIf(input.IsActiveFilter > -1,  e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive) )
						.WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
						.WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter),  e => e.Value == input.ValueFilter);

			var pagedAndFilteredDynamicValues = filteredDynamicValues
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var dynamicValues = from o in pagedAndFilteredDynamicValues
                         select new GetDynamicValueForViewDto() {
							DynamicValue = new DynamicValueDto
							{
                                ObjectId = o.ObjectId,
                                Key = o.Key,
                                DynamicFieldId = o.DynamicFieldId,
                                IsActive = o.IsActive,
                                Order = o.Order,
                                Value = o.Value,
                                Id = o.Id
							}
						};

            var totalCount = await filteredDynamicValues.CountAsync();

            return new PagedResultDto<GetDynamicValueForViewDto>(
                totalCount,
                await dynamicValues.ToListAsync()
            );
         }
		 
		 public async Task<GetDynamicValueForViewDto> GetDynamicValueForView(int id)
         {
            var dynamicValue = await _dynamicValueRepository.GetAsync(id);

            var output = new GetDynamicValueForViewDto { DynamicValue = ObjectMapper.Map<DynamicValueDto>(dynamicValue) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_DynamicValues_Edit)]
		 public async Task<GetDynamicValueForEditOutput> GetDynamicValueForEdit(EntityDto input)
         {
            var dynamicValue = await _dynamicValueRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetDynamicValueForEditOutput {DynamicValue = ObjectMapper.Map<CreateOrEditDynamicValueDto>(dynamicValue)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditDynamicValueDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_DynamicValues_Create)]
		 protected virtual async Task Create(CreateOrEditDynamicValueDto input)
         {
            var dynamicValue = ObjectMapper.Map<DynamicValue>(input);

			
			if (AbpSession.TenantId != null)
			{
				dynamicValue.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _dynamicValueRepository.InsertAsync(dynamicValue);
         }

		 [AbpAuthorize(AppPermissions.Pages_DynamicValues_Edit)]
		 protected virtual async Task Update(CreateOrEditDynamicValueDto input)
         {
            var dynamicValue = await _dynamicValueRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, dynamicValue);
         }

		 [AbpAuthorize(AppPermissions.Pages_DynamicValues_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _dynamicValueRepository.DeleteAsync(input.Id);
         } 
    }
}