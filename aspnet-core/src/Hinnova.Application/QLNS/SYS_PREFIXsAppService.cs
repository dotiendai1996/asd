

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
	[AbpAuthorize(AppPermissions.Pages_SYS_PREFIXs)]
    public class SYS_PREFIXsAppService : HinnovaAppServiceBase, ISYS_PREFIXsAppService
    {
		 private readonly IRepository<SYS_PREFIX> _syS_PREFIXRepository;
		 private readonly ISYS_PREFIXsExcelExporter _syS_PREFIXsExcelExporter;
		 

		  public SYS_PREFIXsAppService(IRepository<SYS_PREFIX> syS_PREFIXRepository, ISYS_PREFIXsExcelExporter syS_PREFIXsExcelExporter ) 
		  {
			_syS_PREFIXRepository = syS_PREFIXRepository;
			_syS_PREFIXsExcelExporter = syS_PREFIXsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetSYS_PREFIXForViewDto>> GetAll(GetAllSYS_PREFIXsInput input)
         {
			
			var filteredSYS_PREFIXs = _syS_PREFIXRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Code.Contains(input.Filter) || e.Prefix.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code.ToLower() == input.CodeFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PrefixFilter),  e => e.Prefix.ToLower() == input.PrefixFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var pagedAndFilteredSYS_PREFIXs = filteredSYS_PREFIXs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var syS_PREFIXs = from o in pagedAndFilteredSYS_PREFIXs
                         select new GetSYS_PREFIXForViewDto() {
							SYS_PREFIX = new SYS_PREFIXDto
							{
                                Code = o.Code,
                                Prefix = o.Prefix,
                                Description = o.Description,
                                Id = o.Id
							}
						};

            var totalCount = await filteredSYS_PREFIXs.CountAsync();

            return new PagedResultDto<GetSYS_PREFIXForViewDto>(
                totalCount,
                await syS_PREFIXs.ToListAsync()
            );
         }
		 
		 public async Task<GetSYS_PREFIXForViewDto> GetSYS_PREFIXForView(int id)
         {
            var syS_PREFIX = await _syS_PREFIXRepository.GetAsync(id);

            var output = new GetSYS_PREFIXForViewDto { SYS_PREFIX = ObjectMapper.Map<SYS_PREFIXDto>(syS_PREFIX) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_SYS_PREFIXs_Edit)]
		 public async Task<GetSYS_PREFIXForEditOutput> GetSYS_PREFIXForEdit(EntityDto input)
         {
            var syS_PREFIX = await _syS_PREFIXRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetSYS_PREFIXForEditOutput {SYS_PREFIX = ObjectMapper.Map<CreateOrEditSYS_PREFIXDto>(syS_PREFIX)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditSYS_PREFIXDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_SYS_PREFIXs_Create)]
		 protected virtual async Task Create(CreateOrEditSYS_PREFIXDto input)
         {
            var syS_PREFIX = ObjectMapper.Map<SYS_PREFIX>(input);

			

            await _syS_PREFIXRepository.InsertAsync(syS_PREFIX);
         }

		 [AbpAuthorize(AppPermissions.Pages_SYS_PREFIXs_Edit)]
		 protected virtual async Task Update(CreateOrEditSYS_PREFIXDto input)
         {
            var syS_PREFIX = await _syS_PREFIXRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, syS_PREFIX);
         }

		 [AbpAuthorize(AppPermissions.Pages_SYS_PREFIXs_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _syS_PREFIXRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetSYS_PREFIXsToExcel(GetAllSYS_PREFIXsForExcelInput input)
         {
			
			var filteredSYS_PREFIXs = _syS_PREFIXRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Code.Contains(input.Filter) || e.Prefix.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code.ToLower() == input.CodeFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PrefixFilter),  e => e.Prefix.ToLower() == input.PrefixFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var query = (from o in filteredSYS_PREFIXs
                         select new GetSYS_PREFIXForViewDto() { 
							SYS_PREFIX = new SYS_PREFIXDto
							{
                                Code = o.Code,
                                Prefix = o.Prefix,
                                Description = o.Description,
                                Id = o.Id
							}
						 });


            var syS_PREFIXListDtos = await query.ToListAsync();

            return _syS_PREFIXsExcelExporter.ExportToFile(syS_PREFIXListDtos);
         }


    }
}