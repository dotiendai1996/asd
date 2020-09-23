

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
	[AbpAuthorize(AppPermissions.Pages_Schedules)]
    public class SchedulesAppService : HinnovaAppServiceBase, ISchedulesAppService
    {
		 private readonly IRepository<Schedule> _scheduleRepository;
		 private readonly ISchedulesExcelExporter _schedulesExcelExporter;
		 

		  public SchedulesAppService(IRepository<Schedule> scheduleRepository, ISchedulesExcelExporter schedulesExcelExporter ) 
		  {
			_scheduleRepository = scheduleRepository;
			_schedulesExcelExporter = schedulesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetScheduleForViewDto>> GetAll(GetAllSchedulesInput input)
         {
			
			var filteredSchedules = _scheduleRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.FromTime.Contains(input.Filter) || e.ToTime.Contains(input.Filter) || e.Content.Contains(input.Filter) || e.Notes.Contains(input.Filter))
						.WhereIf(input.MinScheduleTypeIDFilter != null, e => e.ScheduleTypeID >= input.MinScheduleTypeIDFilter)
						.WhereIf(input.MaxScheduleTypeIDFilter != null, e => e.ScheduleTypeID <= input.MaxScheduleTypeIDFilter)
						.WhereIf(input.MinDateCreatedFilter != null, e => e.DateCreated >= input.MinDateCreatedFilter)
						.WhereIf(input.MaxDateCreatedFilter != null, e => e.DateCreated <= input.MaxDateCreatedFilter)
						.WhereIf(input.MinDateOccurFilter != null, e => e.DateOccur >= input.MinDateOccurFilter)
						.WhereIf(input.MaxDateOccurFilter != null, e => e.DateOccur <= input.MaxDateOccurFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.FromTimeFilter),  e => e.FromTime.ToLower() == input.FromTimeFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ToTimeFilter),  e => e.ToTime.ToLower() == input.ToTimeFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContentFilter),  e => e.Content.ToLower() == input.ContentFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NotesFilter),  e => e.Notes.ToLower() == input.NotesFilter.ToLower().Trim());

			var pagedAndFilteredSchedules = filteredSchedules
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var schedules = from o in pagedAndFilteredSchedules
                         select new GetScheduleForViewDto() {
							Schedule = new ScheduleDto
							{
                                ScheduleTypeID = o.ScheduleTypeID,
                                DateCreated = o.DateCreated,
                                DateOccur = o.DateOccur,
                                FromTime = o.FromTime,
                                ToTime = o.ToTime,
                                Content = o.Content,
                                Notes = o.Notes,
                                Id = o.Id
							}
						};

            var totalCount = await filteredSchedules.CountAsync();

            return new PagedResultDto<GetScheduleForViewDto>(
                totalCount,
                await schedules.ToListAsync()
            );
         }
		 
		 public async Task<GetScheduleForViewDto> GetScheduleForView(int id)
         {
            var schedule = await _scheduleRepository.GetAsync(id);

            var output = new GetScheduleForViewDto { Schedule = ObjectMapper.Map<ScheduleDto>(schedule) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Schedules_Edit)]
		 public async Task<GetScheduleForEditOutput> GetScheduleForEdit(EntityDto input)
         {
            var schedule = await _scheduleRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetScheduleForEditOutput {Schedule = ObjectMapper.Map<CreateOrEditScheduleDto>(schedule)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditScheduleDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Schedules_Create)]
		 protected virtual async Task Create(CreateOrEditScheduleDto input)
         {
            var schedule = ObjectMapper.Map<Schedule>(input);

			
			if (AbpSession.TenantId != null)
			{
				schedule.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _scheduleRepository.InsertAsync(schedule);
         }

		 [AbpAuthorize(AppPermissions.Pages_Schedules_Edit)]
		 protected virtual async Task Update(CreateOrEditScheduleDto input)
         {
            var schedule = await _scheduleRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, schedule);
         }

		 [AbpAuthorize(AppPermissions.Pages_Schedules_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _scheduleRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetSchedulesToExcel(GetAllSchedulesForExcelInput input)
         {
			
			var filteredSchedules = _scheduleRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.FromTime.Contains(input.Filter) || e.ToTime.Contains(input.Filter) || e.Content.Contains(input.Filter) || e.Notes.Contains(input.Filter))
						.WhereIf(input.MinScheduleTypeIDFilter != null, e => e.ScheduleTypeID >= input.MinScheduleTypeIDFilter)
						.WhereIf(input.MaxScheduleTypeIDFilter != null, e => e.ScheduleTypeID <= input.MaxScheduleTypeIDFilter)
						.WhereIf(input.MinDateCreatedFilter != null, e => e.DateCreated >= input.MinDateCreatedFilter)
						.WhereIf(input.MaxDateCreatedFilter != null, e => e.DateCreated <= input.MaxDateCreatedFilter)
						.WhereIf(input.MinDateOccurFilter != null, e => e.DateOccur >= input.MinDateOccurFilter)
						.WhereIf(input.MaxDateOccurFilter != null, e => e.DateOccur <= input.MaxDateOccurFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.FromTimeFilter),  e => e.FromTime.ToLower() == input.FromTimeFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ToTimeFilter),  e => e.ToTime.ToLower() == input.ToTimeFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContentFilter),  e => e.Content.ToLower() == input.ContentFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NotesFilter),  e => e.Notes.ToLower() == input.NotesFilter.ToLower().Trim());

			var query = (from o in filteredSchedules
                         select new GetScheduleForViewDto() { 
							Schedule = new ScheduleDto
							{
                                ScheduleTypeID = o.ScheduleTypeID,
                                DateCreated = o.DateCreated,
                                DateOccur = o.DateOccur,
                                FromTime = o.FromTime,
                                ToTime = o.ToTime,
                                Content = o.Content,
                                Notes = o.Notes,
                                Id = o.Id
							}
						 });


            var scheduleListDtos = await query.ToListAsync();

            return _schedulesExcelExporter.ExportToFile(scheduleListDtos);
         }


    }
}