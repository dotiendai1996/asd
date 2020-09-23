

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
    [AbpAuthorize(AppPermissions.Pages_Priorities)]
    public class PrioritiesAppService : HinnovaAppServiceBase, IPrioritiesAppService
    {
        private readonly IRepository<Priority> _priorityRepository;


        public PrioritiesAppService(IRepository<Priority> priorityRepository)
        {
            _priorityRepository = priorityRepository;

        }

        public async Task<PagedResultDto<GetPriorityForViewDto>> GetAll(GetAllPrioritiesInput input)
        {

            var filteredPriorities = _priorityRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter), e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
                        .WhereIf(input.MinValueFilter != null, e => e.Value >= input.MinValueFilter)
                        .WhereIf(input.MaxValueFilter != null, e => e.Value <= input.MaxValueFilter);

            var pagedAndFilteredPriorities = filteredPriorities
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var priorities = from o in pagedAndFilteredPriorities
                             select new GetPriorityForViewDto()
                             {
                                 Priority = new PriorityDto
                                 {
                                     Key = o.Key,
                                     Value = o.Value,
                                     Id = o.Id
                                 }
                             };

            var totalCount = await filteredPriorities.CountAsync();

            return new PagedResultDto<GetPriorityForViewDto>(
                totalCount,
                await priorities.ToListAsync()
            );
        }

        public async Task<GetPriorityForViewDto> GetPriorityForView(int id)
        {
            var priority = await _priorityRepository.GetAsync(id);

            var output = new GetPriorityForViewDto { Priority = ObjectMapper.Map<PriorityDto>(priority) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Priorities_Edit)]
        public async Task<GetPriorityForEditOutput> GetPriorityForEdit(EntityDto input)
        {
            var priority = await _priorityRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPriorityForEditOutput { Priority = ObjectMapper.Map<CreateOrEditPriorityDto>(priority) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPriorityDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Priorities_Create)]
        protected virtual async Task Create(CreateOrEditPriorityDto input)
        {
            var priority = ObjectMapper.Map<Priority>(input);


            if (AbpSession.TenantId != null)
            {
                priority.TenantId = (int?)AbpSession.TenantId;
            }


            await _priorityRepository.InsertAsync(priority);
        }

        [AbpAuthorize(AppPermissions.Pages_Priorities_Edit)]
        protected virtual async Task Update(CreateOrEditPriorityDto input)
        {
            var priority = await _priorityRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, priority);
        }

        [AbpAuthorize(AppPermissions.Pages_Priorities_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _priorityRepository.DeleteAsync(input.Id);
        }

        public async Task<List<PriorityDto>> GetAllPriorities()
        {
            var result = await _priorityRepository.GetAll().ToListAsync();
            return ObjectMapper.Map<List<PriorityDto>>(result);
        }
    }
}