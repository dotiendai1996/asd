

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
	[AbpAuthorize(AppPermissions.Pages_DocumentStatuses)]
    public class DocumentStatusesAppService : HinnovaAppServiceBase, IDocumentStatusesAppService
    {
		 private readonly IRepository<DocumentStatus> _documentStatusRepository;
		 

		  public DocumentStatusesAppService(IRepository<DocumentStatus> documentStatusRepository ) 
		  {
			_documentStatusRepository = documentStatusRepository;
			
		  }

		 public async Task<PagedResultDto<GetDocumentStatusForViewDto>> GetAll(GetAllDocumentStatusesInput input)
         {
			
			var filteredDocumentStatuses = _documentStatusRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter),  e => e.Key == input.KeyFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter),  e => e.Value == input.ValueFilter);

			var pagedAndFilteredDocumentStatuses = filteredDocumentStatuses
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var documentStatuses = from o in pagedAndFilteredDocumentStatuses
                         select new GetDocumentStatusForViewDto() {
							DocumentStatus = new DocumentStatusDto
							{
                                Key = o.Key,
                                Value = o.Value,
                                Id = o.Id
							}
						};

            var totalCount = await filteredDocumentStatuses.CountAsync();

            return new PagedResultDto<GetDocumentStatusForViewDto>(
                totalCount,
                await documentStatuses.ToListAsync()
            );
         }
		 
		 public async Task<GetDocumentStatusForViewDto> GetDocumentStatusForView(int id)
         {
            var documentStatus = await _documentStatusRepository.GetAsync(id);

            var output = new GetDocumentStatusForViewDto { DocumentStatus = ObjectMapper.Map<DocumentStatusDto>(documentStatus) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_DocumentStatuses_Edit)]
		 public async Task<GetDocumentStatusForEditOutput> GetDocumentStatusForEdit(EntityDto input)
         {
            var documentStatus = await _documentStatusRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetDocumentStatusForEditOutput {DocumentStatus = ObjectMapper.Map<CreateOrEditDocumentStatusDto>(documentStatus)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditDocumentStatusDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_DocumentStatuses_Create)]
		 protected virtual async Task Create(CreateOrEditDocumentStatusDto input)
         {
            var documentStatus = ObjectMapper.Map<DocumentStatus>(input);

			
			if (AbpSession.TenantId != null)
			{
				documentStatus.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _documentStatusRepository.InsertAsync(documentStatus);
         }

		 [AbpAuthorize(AppPermissions.Pages_DocumentStatuses_Edit)]
		 protected virtual async Task Update(CreateOrEditDocumentStatusDto input)
         {
            var documentStatus = await _documentStatusRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, documentStatus);
         }

		 [AbpAuthorize(AppPermissions.Pages_DocumentStatuses_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _documentStatusRepository.DeleteAsync(input.Id);
         } 
    }
}