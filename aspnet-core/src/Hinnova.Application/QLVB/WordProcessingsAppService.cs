

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
	[AbpAuthorize(AppPermissions.Pages_WordProcessings)]
    public class WordProcessingsAppService : HinnovaAppServiceBase, IWordProcessingsAppService
    {
		 private readonly IRepository<WorkHandling> _wordProcessingRepository;
		 private readonly IWordProcessingsExcelExporter _wordProcessingsExcelExporter;
		 

		  public WordProcessingsAppService(IRepository<WorkHandling> wordProcessingRepository, IWordProcessingsExcelExporter wordProcessingsExcelExporter ) 
		  {
			_wordProcessingRepository = wordProcessingRepository;
			_wordProcessingsExcelExporter = wordProcessingsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetWordProcessingForViewDto>> GetAll(GetAllWordProcessingsInput input)
         {
			
			var filteredWordProcessings = _wordProcessingRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ReceivePlace.Contains(input.Filter) || e.Name.Contains(input.Filter) || e.Content.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.Comment.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ReceivePlaceFilter),  e => e.ReceivePlace.ToLower() == input.ReceivePlaceFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContentFilter),  e => e.Content.ToLower() == input.ContentFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter),  e => e.Status.ToLower() == input.StatusFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CommentFilter),  e => e.Comment.ToLower() == input.CommentFilter.ToLower().Trim())
						.WhereIf(input.MinKeyWordIdFilter != null, e => e.KeyWordId >= input.MinKeyWordIdFilter)
						.WhereIf(input.MaxKeyWordIdFilter != null, e => e.KeyWordId <= input.MaxKeyWordIdFilter);

			var pagedAndFilteredWordProcessings = filteredWordProcessings
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wordProcessings = from o in pagedAndFilteredWordProcessings
                         select new GetWordProcessingForViewDto() {
							WordProcessing = new WordProcessingDto
							{
                                ReceivePlace = o.ReceivePlace,
                                Name = o.Name,
                                Content = o.Content,
                                Status = o.Status,
                                Comment = o.Comment,
                                KeyWordId = o.KeyWordId,
                                Id = o.Id
							}
						};

            var totalCount = await filteredWordProcessings.CountAsync();

            return new PagedResultDto<GetWordProcessingForViewDto>(
                totalCount,
                await wordProcessings.ToListAsync()
            );
         }
		 
		 public async Task<GetWordProcessingForViewDto> GetWordProcessingForView(int id)
         {
            var wordProcessing = await _wordProcessingRepository.GetAsync(id);

            var output = new GetWordProcessingForViewDto { WordProcessing = ObjectMapper.Map<WordProcessingDto>(wordProcessing) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WordProcessings_Edit)]
		 public async Task<GetWordProcessingForEditOutput> GetWordProcessingForEdit(EntityDto input)
         {
            var wordProcessing = await _wordProcessingRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWordProcessingForEditOutput {WordProcessing = ObjectMapper.Map<CreateOrEditWordProcessingDto>(wordProcessing)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWordProcessingDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WordProcessings_Create)]
		 protected virtual async Task Create(CreateOrEditWordProcessingDto input)
         {
            var wordProcessing = ObjectMapper.Map<WorkHandling>(input);

			
			if (AbpSession.TenantId != null)
			{
				wordProcessing.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _wordProcessingRepository.InsertAsync(wordProcessing);
         }

		 [AbpAuthorize(AppPermissions.Pages_WordProcessings_Edit)]
		 protected virtual async Task Update(CreateOrEditWordProcessingDto input)
         {
            var wordProcessing = await _wordProcessingRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, wordProcessing);
         }

		 [AbpAuthorize(AppPermissions.Pages_WordProcessings_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _wordProcessingRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetWordProcessingsToExcel(GetAllWordProcessingsForExcelInput input)
         {
			
			var filteredWordProcessings = _wordProcessingRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ReceivePlace.Contains(input.Filter) || e.Name.Contains(input.Filter) || e.Content.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.Comment.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ReceivePlaceFilter),  e => e.ReceivePlace.ToLower() == input.ReceivePlaceFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContentFilter),  e => e.Content.ToLower() == input.ContentFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter),  e => e.Status.ToLower() == input.StatusFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CommentFilter),  e => e.Comment.ToLower() == input.CommentFilter.ToLower().Trim())
						.WhereIf(input.MinKeyWordIdFilter != null, e => e.KeyWordId >= input.MinKeyWordIdFilter)
						.WhereIf(input.MaxKeyWordIdFilter != null, e => e.KeyWordId <= input.MaxKeyWordIdFilter);

			var query = (from o in filteredWordProcessings
                         select new GetWordProcessingForViewDto() { 
							WordProcessing = new WordProcessingDto
							{
                                ReceivePlace = o.ReceivePlace,
                                Name = o.Name,
                                Content = o.Content,
                                Status = o.Status,
                                Comment = o.Comment,
                                KeyWordId = o.KeyWordId,
                                Id = o.Id
							}
						 });


            var wordProcessingListDtos = await query.ToListAsync();

            return _wordProcessingsExcelExporter.ExportToFile(wordProcessingListDtos);
         }


    }
}