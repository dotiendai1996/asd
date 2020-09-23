
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
    [AbpAuthorize(AppPermissions.Pages_DocumentTypes)]

    public class DocumentTypesAppService : HinnovaAppServiceBase, IDocumentTypesAppService
    {
        private readonly IRepository<DocumentType> _documentTypeRepository;
        private readonly IDocumentTypesExcelExporter _documentTypesExcelExporter;


        public DocumentTypesAppService(IRepository<DocumentType> documentTypeRepository, IDocumentTypesExcelExporter documentTypesExcelExporter)
        {
            _documentTypeRepository = documentTypeRepository;
            _documentTypesExcelExporter = documentTypesExcelExporter;

        }

        public async Task<PagedResultDto<GetDocumentTypeForViewDto>> GetAll(GetAllDocumentTypesInput input)
        {

            var filteredDocumentTypes = _documentTypeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.TypeName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TypeNameFilter), e => e.TypeName.ToLower() == input.TypeNameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SignalFilter), e => e.Signal.ToLower() == input.SignalFilter.ToLower())
                        .WhereIf(input.IsActiveFilter > -1, e => Convert.ToInt32(e.IsActive) == input.IsActiveFilter);

            var pagedAndFilteredDocumentTypes = filteredDocumentTypes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var documentTypes = from o in pagedAndFilteredDocumentTypes
                                select new GetDocumentTypeForViewDto()
                                {
                                    DocumentType = new DocumentTypeDto
                                    {
                                        Id = o.Id,
                                        TypeName = o.TypeName,
                                        Signal = o.Signal,
                                        IsActive = o.IsActive
                                    }
                                };

            var totalCount = await filteredDocumentTypes.CountAsync();

            return new PagedResultDto<GetDocumentTypeForViewDto>(
                totalCount,
                await documentTypes.ToListAsync()
            );
        }

        public async Task<GetDocumentTypeForViewDto> GetDocumentTypeForView(int id)
        {
            var documentType = await _documentTypeRepository.GetAsync(id);

            var output = new GetDocumentTypeForViewDto { DocumentType = ObjectMapper.Map<DocumentTypeDto>(documentType) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentTypes_Edit)]
        public async Task<GetDocumentTypeForEditOutput> GetDocumentTypeForEdit(EntityDto input)
        {
            var documentType = await _documentTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDocumentTypeForEditOutput { DocumentType = ObjectMapper.Map<CreateOrEditDocumentTypeDto>(documentType) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditDocumentTypeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_DocumentTypes_Create)]
        protected virtual async Task Create(CreateOrEditDocumentTypeDto input)
        {
            var documentType = ObjectMapper.Map<DocumentType>(input);


            if (AbpSession.TenantId != null)
            {
                documentType.TenantId = (int?)AbpSession.TenantId;
            }


            await _documentTypeRepository.InsertAsync(documentType);
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentTypes_Edit)]
        protected virtual async Task Update(CreateOrEditDocumentTypeDto input)
        {
            var documentType = await _documentTypeRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, documentType);
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _documentTypeRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetDocumentTypesToExcel(GetAllDocumentTypesForExcelInput input)
        {

            var filteredDocumentTypes = _documentTypeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.TypeName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TypeNameFilter), e => e.TypeName.ToLower() == input.TypeNameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SignalFilter), e => e.Signal.ToLower() == input.SignalFilter.ToLower())
                        .WhereIf(input.IsActiveFilter > -1, e => Convert.ToInt32(e.IsActive) == input.IsActiveFilter);

            var query = (from o in filteredDocumentTypes
                         select new GetDocumentTypeForViewDto()
                         {
                             DocumentType = new DocumentTypeDto
                             {
                                 TypeName = o.TypeName,
                                 Signal = o.Signal,
                                 IsActive = o.IsActive,
                                 Id = o.Id
                             }
                         });


            var documentTypeListDtos = await query.ToListAsync();

            return _documentTypesExcelExporter.ExportToFile(documentTypeListDtos);
        }

        public async Task<List<DocumentTypeDto>> GetAllDocumentType()
        {
            var result = await _documentTypeRepository.GetAll().Where(x => x.IsActive == true).ToListAsync();
            return ObjectMapper.Map<List<DocumentTypeDto>>(result);
        }
    }
}