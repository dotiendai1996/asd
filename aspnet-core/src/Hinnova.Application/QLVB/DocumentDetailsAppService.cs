

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
    [AbpAuthorize(AppPermissions.Pages_DocumentDetails)]
    public class DocumentDetailsAppService : HinnovaAppServiceBase, IDocumentDetailsAppService
    {
        private readonly IRepository<DocumentDetail> _documentDetailRepository;
        private readonly IDocumentDetailsExcelExporter _documentDetailsExcelExporter;


        public DocumentDetailsAppService(IRepository<DocumentDetail> documentDetailRepository, IDocumentDetailsExcelExporter documentDetailsExcelExporter)
        {
            _documentDetailRepository = documentDetailRepository;
            _documentDetailsExcelExporter = documentDetailsExcelExporter;

        }

        public async Task<PagedResultDto<GetDocumentDetailForViewDto>> GetAll(GetAllDocumentDetailsInput input)
        {

            var filteredDocumentDetails = _documentDetailRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.TypeHandle.ToString().Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.IsStared || e.Priority.Contains(input.Filter))
                        .WhereIf(input.MinDatehandleFilter != null, e => e.DateHandle >= input.MinDatehandleFilter)
                        .WhereIf(input.MaxDatehandleFilter != null, e => e.DateHandle <= input.MaxDatehandleFilter)
                        .WhereIf(input.TypehandleFilter > -1, e => e.TypeHandle == input.TypehandleFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter), e => e.Status.ToLower() == input.StatusFilter.ToLower().Trim())
                        .WhereIf(input.IsStaredFilter, e => e.IsStared == input.IsStaredFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PriorityFilter), e => e.Priority.ToLower() == input.PriorityFilter.ToLower().Trim());

            var pagedAndFilteredDocumentDetails = filteredDocumentDetails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var documentDetails = from o in pagedAndFilteredDocumentDetails
                                  select new GetDocumentDetailForViewDto()
                                  {
                                      DocumentDetail = new DocumentDetailDto
                                      {
                                          Datehandle = o.DateHandle,
                                          Typehandle = o.TypeHandle,
                                          Description = o.Description,
                                          Status = o.Status,
                                          IsStared = o.IsStared,
                                          Priority = o.Priority,
                                          Id = o.Id
                                      }
                                  };

            var totalCount = await filteredDocumentDetails.CountAsync();

            return new PagedResultDto<GetDocumentDetailForViewDto>(
                totalCount,
                await documentDetails.ToListAsync()
            );
        }

        public async Task<GetDocumentDetailForViewDto> GetDocumentDetailForView(int id)
        {
            var documentDetail = await _documentDetailRepository.GetAsync(id);

            var output = new GetDocumentDetailForViewDto { DocumentDetail = ObjectMapper.Map<DocumentDetailDto>(documentDetail) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentDetails_Edit)]
        public async Task<GetDocumentDetailForEditOutput> GetDocumentDetailForEdit(EntityDto input)
        {
            var documentDetail = await _documentDetailRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDocumentDetailForEditOutput { DocumentDetail = ObjectMapper.Map<CreateOrEditDocumentDetailDto>(documentDetail) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditDocumentDetailDto input)
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

        [AbpAuthorize(AppPermissions.Pages_DocumentDetails_Create)]
        protected virtual async Task Create(CreateOrEditDocumentDetailDto input)
        {
            var documentDetail = ObjectMapper.Map<DocumentDetail>(input);


            if (AbpSession.TenantId != null)
            {
                documentDetail.TenantId = (int?)AbpSession.TenantId;
            }


            await _documentDetailRepository.InsertAsync(documentDetail);
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentDetails_Edit)]
        protected virtual async Task Update(CreateOrEditDocumentDetailDto input)
        {
            var documentDetail = await _documentDetailRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, documentDetail);
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentDetails_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _documentDetailRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetDocumentDetailsToExcel(GetAllDocumentDetailsForExcelInput input)
        {

            var filteredDocumentDetails = _documentDetailRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.TypeHandle.ToString().Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.IsStared || e.Priority.Contains(input.Filter))
                        .WhereIf(input.MinDatehandleFilter != null, e => e.DateHandle >= input.MinDatehandleFilter)
                        .WhereIf(input.MaxDatehandleFilter != null, e => e.DateHandle <= input.MaxDatehandleFilter)
                        .WhereIf(input.TypehandleFilter > -1, e => e.TypeHandle == input.TypehandleFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter), e => e.Status.ToLower() == input.StatusFilter.ToLower().Trim())
                        .WhereIf(input.IsStaredFilter, e => e.IsStared == input.IsStaredFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PriorityFilter), e => e.Priority.ToLower() == input.PriorityFilter.ToLower().Trim());

            var query = (from o in filteredDocumentDetails
                         select new GetDocumentDetailForViewDto()
                         {
                             DocumentDetail = new DocumentDetailDto
                             {
                                 Datehandle = o.DateHandle,
                                 Typehandle = o.TypeHandle,
                                 Description = o.Description,
                                 Status = o.Status,
                                 IsStared = o.IsStared,
                                 Priority = o.Priority,
                                 Id = o.Id
                             }
                         });


            var documentDetailListDtos = await query.ToListAsync();

            return _documentDetailsExcelExporter.ExportToFile(documentDetailListDtos);
        }

    }
}