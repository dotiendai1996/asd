

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
    [AbpAuthorize(AppPermissions.Pages_DocumentHandlingDetails)]
    public class DocumentHandlingDetailsAppService : HinnovaAppServiceBase, IDocumentHandlingDetailsAppService
    {
        private readonly IRepository<DocumentHandlingDetail> _documentHandlingDetailRepository;


        public DocumentHandlingDetailsAppService(IRepository<DocumentHandlingDetail> documentHandlingDetailRepository)
        {
            _documentHandlingDetailRepository = documentHandlingDetailRepository;

        }

        public async Task<PagedResultDto<GetDocumentHandlingDetailForViewDto>> GetAll(GetAllDocumentHandlingDetailsInput input)
        {

            var filteredDocumentHandlingDetails = _documentHandlingDetailRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Group.Contains(input.Filter) || e.Person.Contains(input.Filter) || e.Type.Contains(input.Filter) || e.Superios.Contains(input.Filter) || e.PersonalComment.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GroupFilter), e => e.Group == input.GroupFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PersonFilter), e => e.Person == input.PersonFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TypeFilter), e => e.Type == input.TypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SuperiosFilter), e => e.Superios == input.SuperiosFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PersonalCommentFilter), e => e.PersonalComment == input.PersonalCommentFilter)
                        .WhereIf(input.MinStartDateFilter != null, e => e.StartDate >= input.MinStartDateFilter)
                        .WhereIf(input.MaxStartDateFilter != null, e => e.StartDate <= input.MaxStartDateFilter)
                        .WhereIf(input.MinEndDateFilter != null, e => e.EndDate >= input.MinEndDateFilter)
                        .WhereIf(input.MaxEndDateFilter != null, e => e.EndDate <= input.MaxEndDateFilter)
                        .WhereIf(input.MinDocumentHandlingIdFilter != null, e => e.DocumentHandlingId >= input.MinDocumentHandlingIdFilter)
                        .WhereIf(input.MaxDocumentHandlingIdFilter != null, e => e.DocumentHandlingId <= input.MaxDocumentHandlingIdFilter)
                        .WhereIf(input.MainHandlingFilter > -1, e => (input.MainHandlingFilter == 1 && e.MainHandling) || (input.MainHandlingFilter == 0 && !e.MainHandling))
                        .WhereIf(input.CoHandlingFilter > -1, e => (input.CoHandlingFilter == 1 && e.CoHandling) || (input.CoHandlingFilter == 0 && !e.CoHandling))
                        .WhereIf(input.ToKnowFilter > -1, e => (input.ToKnowFilter == 1 && e.ToKnow) || (input.ToKnowFilter == 0 && !e.ToKnow));

            var pagedAndFilteredDocumentHandlingDetails = filteredDocumentHandlingDetails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var documentHandlingDetails = from o in pagedAndFilteredDocumentHandlingDetails
                                          select new GetDocumentHandlingDetailForViewDto()
                                          {
                                              DocumentHandlingDetail = new DocumentHandlingDetailDto
                                              {
                                                  Group = o.Group,
                                                  Person = o.Person,
                                                  Type = o.Type,
                                                  Superios = o.Superios,
                                                  PersonalComment = o.PersonalComment,
                                                  StartDate = o.StartDate,
                                                  EndDate = o.EndDate,
                                                  DocumentHandlingId = o.DocumentHandlingId,
                                                  MainHandling = o.MainHandling,
                                                  CoHandling = o.CoHandling,
                                                  ToKnow = o.ToKnow,
                                                  UserId = o.UserId,
                                                  IsHandled = o.IsHandled,
                                                  Id = o.Id
                                              }
                                          };

            var totalCount = await filteredDocumentHandlingDetails.CountAsync();

            return new PagedResultDto<GetDocumentHandlingDetailForViewDto>(
                totalCount,
                await documentHandlingDetails.ToListAsync()
            );
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentHandlingDetails_Edit)]
        public async Task<GetDocumentHandlingDetailForEditOutput> GetDocumentHandlingDetailForEdit(EntityDto input)
        {
            var documentHandlingDetail = await _documentHandlingDetailRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDocumentHandlingDetailForEditOutput { DocumentHandlingDetail = ObjectMapper.Map<CreateOrEditDocumentHandlingDetailDto>(documentHandlingDetail) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditDocumentHandlingDetailDto input)
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

        [AbpAuthorize(AppPermissions.Pages_DocumentHandlingDetails_Create)]
        protected virtual async Task Create(CreateOrEditDocumentHandlingDetailDto input)
        {
            var documentHandlingDetail = ObjectMapper.Map<DocumentHandlingDetail>(input);


            if (AbpSession.TenantId != null)
            {
                documentHandlingDetail.TenantId = (int?)AbpSession.TenantId;
            }


            await _documentHandlingDetailRepository.InsertAsync(documentHandlingDetail);
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentHandlingDetails_Edit)]
        protected virtual async Task Update(CreateOrEditDocumentHandlingDetailDto input)
        {
            var documentHandlingDetail = await _documentHandlingDetailRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, documentHandlingDetail);
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentHandlingDetails_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _documentHandlingDetailRepository.DeleteAsync(input.Id);
        }

        public async Task<List<DocumentHandlingDetailDto>> GetAllDocumentHandlingDetailByUserId(int userId)
        {
            var result = await _documentHandlingDetailRepository.GetAll().Where(x => x.UserId == userId).OrderBy("endDate asc").ToListAsync();
            return ObjectMapper.Map<List<DocumentHandlingDetailDto>>(result);
        }
    }
}