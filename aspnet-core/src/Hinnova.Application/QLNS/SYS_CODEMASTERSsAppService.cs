

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLNSExporting;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Hinnova.QLNS
{
	[AbpAuthorize(AppPermissions.Pages_SYS_CODEMASTERSs)]
    public class SYS_CODEMASTERSsAppService : HinnovaAppServiceBase, ISYS_CODEMASTERSsAppService
    {
		 private readonly IRepository<SYS_CODEMASTERS> _syS_CODEMASTERSRepository;
		 private readonly ISYS_CODEMASTERSsExcelExporter _syS_CODEMASTERSsExcelExporter;
		 

		  public SYS_CODEMASTERSsAppService(IRepository<SYS_CODEMASTERS> syS_CODEMASTERSRepository, ISYS_CODEMASTERSsExcelExporter syS_CODEMASTERSsExcelExporter ) 
		  {
			_syS_CODEMASTERSRepository = syS_CODEMASTERSRepository;
			_syS_CODEMASTERSsExcelExporter = syS_CODEMASTERSsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetSYS_CODEMASTERSForViewDto>> GetAll(GetAllSYS_CODEMASTERSsInput input)
         {
			
			var filteredSYS_CODEMASTERSs = _syS_CODEMASTERSRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Prefix.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Active.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PrefixFilter),  e => e.Prefix.ToLower() == input.PrefixFilter.ToLower().Trim())
						.WhereIf(input.MinCurValueFilter != null, e => e.CurValue >= input.MinCurValueFilter)
						.WhereIf(input.MaxCurValueFilter != null, e => e.CurValue <= input.MaxCurValueFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ActiveFilter),  e => e.Active.ToLower() == input.ActiveFilter.ToLower().Trim());

			var pagedAndFilteredSYS_CODEMASTERSs = filteredSYS_CODEMASTERSs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var syS_CODEMASTERSs = from o in pagedAndFilteredSYS_CODEMASTERSs
                         select new GetSYS_CODEMASTERSForViewDto() {
							SYS_CODEMASTERS = new SYS_CODEMASTERSDto
							{
                                Prefix = o.Prefix,
                                CurValue = o.CurValue,
                                Description = o.Description,
                                Active = o.Active,
                                Id = o.Id
							}
						};

            var totalCount = await filteredSYS_CODEMASTERSs.CountAsync();

            return new PagedResultDto<GetSYS_CODEMASTERSForViewDto>(
                totalCount,
                await syS_CODEMASTERSs.ToListAsync()
            );
         }
		 
		 public async Task<GetSYS_CODEMASTERSForViewDto> GetSYS_CODEMASTERSForView(int id)
         {
            var syS_CODEMASTERS = await _syS_CODEMASTERSRepository.GetAsync(id);

            var output = new GetSYS_CODEMASTERSForViewDto { SYS_CODEMASTERS = ObjectMapper.Map<SYS_CODEMASTERSDto>(syS_CODEMASTERS) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_SYS_CODEMASTERSs_Edit)]
		 public async Task<GetSYS_CODEMASTERSForEditOutput> GetSYS_CODEMASTERSForEdit(EntityDto input)
         {
            var syS_CODEMASTERS = await _syS_CODEMASTERSRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetSYS_CODEMASTERSForEditOutput {SYS_CODEMASTERS = ObjectMapper.Map<CreateOrEditSYS_CODEMASTERSDto>(syS_CODEMASTERS)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditSYS_CODEMASTERSDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_SYS_CODEMASTERSs_Create)]
		 protected virtual async Task Create(CreateOrEditSYS_CODEMASTERSDto input)
         {
            var syS_CODEMASTERS = ObjectMapper.Map<SYS_CODEMASTERS>(input);

			

            await _syS_CODEMASTERSRepository.InsertAsync(syS_CODEMASTERS);
         }

		 [AbpAuthorize(AppPermissions.Pages_SYS_CODEMASTERSs_Edit)]
		 protected virtual async Task Update(CreateOrEditSYS_CODEMASTERSDto input)
         {
            var syS_CODEMASTERS = await _syS_CODEMASTERSRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, syS_CODEMASTERS);
         }

		 [AbpAuthorize(AppPermissions.Pages_SYS_CODEMASTERSs_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _syS_CODEMASTERSRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetSYS_CODEMASTERSsToExcel(GetAllSYS_CODEMASTERSsForExcelInput input)
         {
			
			var filteredSYS_CODEMASTERSs = _syS_CODEMASTERSRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Prefix.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Active.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PrefixFilter),  e => e.Prefix.ToLower() == input.PrefixFilter.ToLower().Trim())
						.WhereIf(input.MinCurValueFilter != null, e => e.CurValue >= input.MinCurValueFilter)
						.WhereIf(input.MaxCurValueFilter != null, e => e.CurValue <= input.MaxCurValueFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ActiveFilter),  e => e.Active.ToLower() == input.ActiveFilter.ToLower().Trim());

			var query = (from o in filteredSYS_CODEMASTERSs
                         select new GetSYS_CODEMASTERSForViewDto() { 
							SYS_CODEMASTERS = new SYS_CODEMASTERSDto
							{
                                Prefix = o.Prefix,
                                CurValue = o.CurValue,
                                Description = o.Description,
                                Active = o.Active,
                                Id = o.Id
							}
						 });


            var syS_CODEMASTERSListDtos = await query.ToListAsync();

            return _syS_CODEMASTERSsExcelExporter.ExportToFile(syS_CODEMASTERSListDtos);
         }


    }
}