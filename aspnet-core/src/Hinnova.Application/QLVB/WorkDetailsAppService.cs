

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
	[AbpAuthorize(AppPermissions.Pages_WorkDetails)]
    public class WorkDetailsAppService : HinnovaAppServiceBase, IWorkDetailsAppService
    {
		 private readonly IRepository<WorkDetail> _workDetailRepository;
		 private readonly IWorkDetailsExcelExporter _workDetailsExcelExporter;
		 

		  public WorkDetailsAppService(IRepository<WorkDetail> workDetailRepository, IWorkDetailsExcelExporter workDetailsExcelExporter ) 
		  {
			_workDetailRepository = workDetailRepository;
			_workDetailsExcelExporter = workDetailsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetWorkDetailForViewDto>> GetAll(GetAllWorkDetailsInput input)
         {
			
			var filteredWorkDetails = _workDetailRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.NameID.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Repply.Contains(input.Filter) || e.Attachment.Contains(input.Filter))
						.WhereIf(input.MinWorkAssignIdFilter != null, e => e.WorkAssignId >= input.MinWorkAssignIdFilter)
						.WhereIf(input.MaxWorkAssignIdFilter != null, e => e.WorkAssignId <= input.MaxWorkAssignIdFilter)
						.WhereIf(input.MinDonePersentageFilter != null, e => e.DonePersentage >= input.MinDonePersentageFilter)
						.WhereIf(input.MaxDonePersentageFilter != null, e => e.DonePersentage <= input.MaxDonePersentageFilter)
						.WhereIf(input.MinDateFilter != null, e => e.Date >= input.MinDateFilter)
						.WhereIf(input.MaxDateFilter != null, e => e.Date <= input.MaxDateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameIDFilter),  e => e.NameID.ToLower() == input.NameIDFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.RepplyFilter),  e => e.Repply.ToLower() == input.RepplyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.AttachmentFilter),  e => e.Attachment.ToLower() == input.AttachmentFilter.ToLower().Trim());

			var pagedAndFilteredWorkDetails = filteredWorkDetails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var workDetails = from o in pagedAndFilteredWorkDetails
                         select new GetWorkDetailForViewDto() {
							WorkDetail = new WorkDetailDto
							{
                                WorkAssignId = o.WorkAssignId,
                                DonePersentage = o.DonePersentage,
                                Date = o.Date,
                                NameID = o.NameID,
                                Description = o.Description,
                                Repply = o.Repply,
                                Attachment = o.Attachment,
                                Id = o.Id
							}
						};

            var totalCount = await filteredWorkDetails.CountAsync();

            return new PagedResultDto<GetWorkDetailForViewDto>(
                totalCount,
                await workDetails.ToListAsync()
            );
         }
		 
		 public async Task<GetWorkDetailForViewDto> GetWorkDetailForView(int id)
         {
            var workDetail = await _workDetailRepository.GetAsync(id);

            var output = new GetWorkDetailForViewDto { WorkDetail = ObjectMapper.Map<WorkDetailDto>(workDetail) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WorkDetails_Edit)]
		 public async Task<GetWorkDetailForEditOutput> GetWorkDetailForEdit(EntityDto input)
         {
            var workDetail = await _workDetailRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWorkDetailForEditOutput {WorkDetail = ObjectMapper.Map<CreateOrEditWorkDetailDto>(workDetail)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWorkDetailDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WorkDetails_Create)]
		 protected virtual async Task Create(CreateOrEditWorkDetailDto input)
         {
            var workDetail = ObjectMapper.Map<WorkDetail>(input);

			
			if (AbpSession.TenantId != null)
			{
				workDetail.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _workDetailRepository.InsertAsync(workDetail);
         }

		 [AbpAuthorize(AppPermissions.Pages_WorkDetails_Edit)]
		 protected virtual async Task Update(CreateOrEditWorkDetailDto input)
         {
            var workDetail = await _workDetailRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, workDetail);
         }

		 [AbpAuthorize(AppPermissions.Pages_WorkDetails_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _workDetailRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetWorkDetailsToExcel(GetAllWorkDetailsForExcelInput input)
         {
			
			var filteredWorkDetails = _workDetailRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.NameID.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Repply.Contains(input.Filter) || e.Attachment.Contains(input.Filter))
						.WhereIf(input.MinWorkAssignIdFilter != null, e => e.WorkAssignId >= input.MinWorkAssignIdFilter)
						.WhereIf(input.MaxWorkAssignIdFilter != null, e => e.WorkAssignId <= input.MaxWorkAssignIdFilter)
						.WhereIf(input.MinDonePersentageFilter != null, e => e.DonePersentage >= input.MinDonePersentageFilter)
						.WhereIf(input.MaxDonePersentageFilter != null, e => e.DonePersentage <= input.MaxDonePersentageFilter)
						.WhereIf(input.MinDateFilter != null, e => e.Date >= input.MinDateFilter)
						.WhereIf(input.MaxDateFilter != null, e => e.Date <= input.MaxDateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameIDFilter),  e => e.NameID.ToLower() == input.NameIDFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.RepplyFilter),  e => e.Repply.ToLower() == input.RepplyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.AttachmentFilter),  e => e.Attachment.ToLower() == input.AttachmentFilter.ToLower().Trim());

			var query = (from o in filteredWorkDetails
                         select new GetWorkDetailForViewDto() { 
							WorkDetail = new WorkDetailDto
							{
                                WorkAssignId = o.WorkAssignId,
                                DonePersentage = o.DonePersentage,
                                Date = o.Date,
                                NameID = o.NameID,
                                Description = o.Description,
                                Repply = o.Repply,
                                Attachment = o.Attachment,
                                Id = o.Id
							}
						 });


            var workDetailListDtos = await query.ToListAsync();

            return _workDetailsExcelExporter.ExportToFile(workDetailListDtos);
         }


    }
}