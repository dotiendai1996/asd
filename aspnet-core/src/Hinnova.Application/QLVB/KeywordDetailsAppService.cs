

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
    [AbpAuthorize(AppPermissions.Pages_KeywordDetails)]
    public class KeywordDetailsAppService : HinnovaAppServiceBase, IKeywordDetailsAppService
    {
        private readonly IRepository<KeywordDetail> _keywordDetailRepository;


        public KeywordDetailsAppService(IRepository<KeywordDetail> keywordDetailRepository)
        {
            _keywordDetailRepository = keywordDetailRepository;

        }

        public async Task<PagedResultDto<GetKeywordDetailForViewDto>> GetAll(GetAllKeywordDetailsInput input)
        {

            var filteredKeywordDetails = _keywordDetailRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FullName.Contains(input.Filter))
                        .WhereIf(input.MinKeywordIdFilter != null, e => e.KeywordId >= input.MinKeywordIdFilter)
                        .WhereIf(input.MaxKeywordIdFilter != null, e => e.KeywordId <= input.MaxKeywordIdFilter)
                        .WhereIf(input.IsLeaderFilter > -1, e => (input.IsLeaderFilter == 1 && e.IsLeader) || (input.IsLeaderFilter == 0 && !e.IsLeader))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FullNameFilter), e => e.FullName == input.FullNameFilter)
                        .WhereIf(input.MainHandlingFilter > -1, e => (input.MainHandlingFilter == 1 && e.MainHandling) || (input.MainHandlingFilter == 0 && !e.MainHandling))
                        .WhereIf(input.CoHandlingFilter > -1, e => (input.CoHandlingFilter == 1 && e.CoHandling) || (input.CoHandlingFilter == 0 && !e.CoHandling))
                        .WhereIf(input.ToKnowFilter > -1, e => (input.ToKnowFilter == 1 && e.ToKnow) || (input.ToKnowFilter == 0 && !e.ToKnow))
                        .WhereIf(input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
                        .WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter)
                        .WhereIf(input.UserIdFilter > 0, e => e.UserId == input.UserIdFilter);

            var pagedAndFilteredKeywordDetails = filteredKeywordDetails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var keywordDetails = from o in pagedAndFilteredKeywordDetails
                                 select new GetKeywordDetailForViewDto()
                                 {
                                     KeywordDetail = new KeywordDetailDto
                                     {
                                         KeywordId = o.KeywordId,
                                         IsLeader = o.IsLeader,
                                         FullName = o.FullName,
                                         MainHandling = o.MainHandling,
                                         CoHandling = o.CoHandling,
                                         ToKnow = o.ToKnow,
                                         IsActive = o.IsActive,
                                         Order = o.Order,
                                         UserId = o.UserId,
                                         Id = o.Id
                                     }
                                 };

            var totalCount = await filteredKeywordDetails.CountAsync();

            return new PagedResultDto<GetKeywordDetailForViewDto>(
                totalCount,
                await keywordDetails.ToListAsync()
            );
        }

        [AbpAuthorize(AppPermissions.Pages_KeywordDetails_Edit)]
        public async Task<GetKeywordDetailForEditOutput> GetKeywordDetailForEdit(EntityDto input)
        {
            var keywordDetail = await _keywordDetailRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetKeywordDetailForEditOutput { KeywordDetail = ObjectMapper.Map<CreateOrEditKeywordDetailDto>(keywordDetail) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditKeywordDetailDto input)
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

        [AbpAuthorize(AppPermissions.Pages_KeywordDetails_Create)]
        protected virtual async Task Create(CreateOrEditKeywordDetailDto input)
        {
            var keywordDetail = ObjectMapper.Map<KeywordDetail>(input);


            if (AbpSession.TenantId != null)
            {
                keywordDetail.TenantId = (int?)AbpSession.TenantId;
            }


            await _keywordDetailRepository.InsertAsync(keywordDetail);
        }

        [AbpAuthorize(AppPermissions.Pages_KeywordDetails_Edit)]
        protected virtual async Task Update(CreateOrEditKeywordDetailDto input)
        {
            var keywordDetail = await _keywordDetailRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, keywordDetail);
        }

        [AbpAuthorize(AppPermissions.Pages_KeywordDetails_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _keywordDetailRepository.DeleteAsync(input.Id);
        }

        public async Task<List<KeywordDetailDto>> GetAllKeywordDetailByKeywordId(int keywordId)
        {
            var result = await _keywordDetailRepository.GetAll().Where(x => x.CreatorUserId == AbpSession.UserId && x.TenantId == AbpSession.TenantId && x.KeywordId == keywordId).ToListAsync();
            return ObjectMapper.Map<List<KeywordDetailDto>>(result);
        }
    }
}