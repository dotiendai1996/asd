

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
	[AbpAuthorize(AppPermissions.Pages_WorkAssigns)]
    public class WorkAssignsAppService : HinnovaAppServiceBase, IWorkAssignsAppService
    {
		 private readonly IRepository<WorkAssign> _workAssignRepository;
		 private readonly IWorkAssignsExcelExporter _workAssignsExcelExporter;
		 

		  public WorkAssignsAppService(IRepository<WorkAssign> workAssignRepository, IWorkAssignsExcelExporter workAssignsExcelExporter ) 
		  {
			_workAssignRepository = workAssignRepository;
			_workAssignsExcelExporter = workAssignsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetWorkAssignForViewDto>> GetAll(GetAllWorkAssignsInput input)
         {
			
			var filteredWorkAssigns = _workAssignRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Assignee.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Action.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
						.WhereIf(input.MinStartDateFilter != null, e => e.StartDate >= input.MinStartDateFilter)
						.WhereIf(input.MaxStartDateFilter != null, e => e.StartDate <= input.MaxStartDateFilter)
						.WhereIf(input.MinEndDateFilter != null, e => e.EndDate >= input.MinEndDateFilter)
						.WhereIf(input.MaxEndDateFilter != null, e => e.EndDate <= input.MaxEndDateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.AssigneeFilter),  e => e.Assignee.ToLower() == input.AssigneeFilter.ToLower().Trim())
						.WhereIf(input.MinProgressFilter != null, e => e.Progress >= input.MinProgressFilter)
						.WhereIf(input.MaxProgressFilter != null, e => e.Progress <= input.MaxProgressFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter),  e => e.Status.ToLower() == input.StatusFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ActionFilter),  e => e.Action.ToLower() == input.ActionFilter.ToLower().Trim());

			var pagedAndFilteredWorkAssigns = filteredWorkAssigns
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var workAssigns = from o in pagedAndFilteredWorkAssigns
                         select new GetWorkAssignForViewDto() {
							WorkAssign = new WorkAssignDto
							{
                                Name = o.Name,
                                StartDate = o.StartDate,
                                EndDate = o.EndDate,
                                Assignee = o.Assignee,
                                Progress = o.Progress,
                                Status = o.Status,
                                Description = o.Description,
                                Action = o.Action,
                                Id = o.Id
							}
						};

            var totalCount = await filteredWorkAssigns.CountAsync();

            return new PagedResultDto<GetWorkAssignForViewDto>(
                totalCount,
                await workAssigns.ToListAsync()
            );
         }
		 
		 public async Task<GetWorkAssignForViewDto> GetWorkAssignForView(int id)
         {
            var workAssign = await _workAssignRepository.GetAsync(id);

            var output = new GetWorkAssignForViewDto { WorkAssign = ObjectMapper.Map<WorkAssignDto>(workAssign) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WorkAssigns_Edit)]
		 public async Task<GetWorkAssignForEditOutput> GetWorkAssignForEdit(EntityDto input)
         {
            var workAssign = await _workAssignRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWorkAssignForEditOutput {WorkAssign = ObjectMapper.Map<CreateOrEditWorkAssignDto>(workAssign)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWorkAssignDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WorkAssigns_Create)]
		 protected virtual async Task Create(CreateOrEditWorkAssignDto input)
         {
            var workAssign = ObjectMapper.Map<WorkAssign>(input);

			
			if (AbpSession.TenantId != null)
			{
				workAssign.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _workAssignRepository.InsertAsync(workAssign);
         }

		 [AbpAuthorize(AppPermissions.Pages_WorkAssigns_Edit)]
		 protected virtual async Task Update(CreateOrEditWorkAssignDto input)
         {
            var workAssign = await _workAssignRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, workAssign);
         }

		 [AbpAuthorize(AppPermissions.Pages_WorkAssigns_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _workAssignRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetWorkAssignsToExcel(GetAllWorkAssignsForExcelInput input)
         {
			
			var filteredWorkAssigns = _workAssignRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Assignee.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Action.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
						.WhereIf(input.MinStartDateFilter != null, e => e.StartDate >= input.MinStartDateFilter)
						.WhereIf(input.MaxStartDateFilter != null, e => e.StartDate <= input.MaxStartDateFilter)
						.WhereIf(input.MinEndDateFilter != null, e => e.EndDate >= input.MinEndDateFilter)
						.WhereIf(input.MaxEndDateFilter != null, e => e.EndDate <= input.MaxEndDateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.AssigneeFilter),  e => e.Assignee.ToLower() == input.AssigneeFilter.ToLower().Trim())
						.WhereIf(input.MinProgressFilter != null, e => e.Progress >= input.MinProgressFilter)
						.WhereIf(input.MaxProgressFilter != null, e => e.Progress <= input.MaxProgressFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter),  e => e.Status.ToLower() == input.StatusFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ActionFilter),  e => e.Action.ToLower() == input.ActionFilter.ToLower().Trim());

			var query = (from o in filteredWorkAssigns
                         select new GetWorkAssignForViewDto() { 
							WorkAssign = new WorkAssignDto
							{
                                Name = o.Name,
                                StartDate = o.StartDate,
                                EndDate = o.EndDate,
                                Assignee = o.Assignee,
                                Progress = o.Progress,
                                Status = o.Status,
                                Description = o.Description,
                                Action = o.Action,
                                Id = o.Id
							}
						 });


            var workAssignListDtos = await query.ToListAsync();

            return _workAssignsExcelExporter.ExportToFile(workAssignListDtos);
         }


    }
}