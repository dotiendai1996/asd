

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
using Abp.UI;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Hinnova.Configuration;
using Microsoft.Extensions.Configuration;

namespace Hinnova.QLVB
{
    [AbpAuthorize(AppPermissions.Pages_MemorizeKeywords)]
    public class MemorizeKeywordsAppService : HinnovaAppServiceBase, IMemorizeKeywordsAppService
    {
        private readonly IRepository<MemorizeKeyword> _memorizeKeywordRepository;
        private readonly IMemorizeKeywordsExcelExporter _memorizeKeywordsExcelExporter;
        private readonly string connectionString;

        public MemorizeKeywordsAppService(IRepository<MemorizeKeyword> memorizeKeywordRepository, IMemorizeKeywordsExcelExporter memorizeKeywordsExcelExporter, IWebHostEnvironment env)
        {
            _memorizeKeywordRepository = memorizeKeywordRepository;
            _memorizeKeywordsExcelExporter = memorizeKeywordsExcelExporter;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");
        }

        public async Task<PagedResultDto<GetMemorizeKeywordForViewDto>> GetAll(GetAllMemorizeKeywordsInput input)
        {

            var filteredMemorizeKeywords = _memorizeKeywordRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.KeyWord.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KeyWordFilter), e => e.KeyWord.ToLower() == input.KeyWordFilter.ToLower().Trim());

            var pagedAndFilteredMemorizeKeywords = filteredMemorizeKeywords
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var memorizeKeywords = from o in pagedAndFilteredMemorizeKeywords
                                   select new GetMemorizeKeywordForViewDto()
                                   {
                                       MemorizeKeyword = new MemorizeKeywordDto
                                       {
                                           KeyWord = o.KeyWord,
                                           IsActive = o.IsActive,
                                           Id = o.Id
                                       }
                                   };

            var totalCount = await filteredMemorizeKeywords.CountAsync();

            return new PagedResultDto<GetMemorizeKeywordForViewDto>(
                totalCount,
                await memorizeKeywords.ToListAsync()
            );
        }

        public async Task<GetMemorizeKeywordForViewDto> GetMemorizeKeywordForView(int id)
        {
            var memorizeKeyword = await _memorizeKeywordRepository.GetAsync(id);

            var output = new GetMemorizeKeywordForViewDto { MemorizeKeyword = ObjectMapper.Map<MemorizeKeywordDto>(memorizeKeyword) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_MemorizeKeywords_Edit)]
        public async Task<GetMemorizeKeywordForEditOutput> GetMemorizeKeywordForEdit(EntityDto input)
        {
            var memorizeKeyword = await _memorizeKeywordRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetMemorizeKeywordForEditOutput { MemorizeKeyword = ObjectMapper.Map<CreateOrEditMemorizeKeywordDto>(memorizeKeyword) };

            return output;
        }

        public async Task<int> CreateOrEdit(CreateOrEditMemorizeKeywordDto input)
        {
            int insertedId = 0;

            try
            {
                if (input.Id == null)
                {
                    insertedId = await Create(input);
                }
                else
                {
                    await Update(input);
                }
            }
            catch (Exception e)
            {
                if (e.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    // handle your violation
                    throw new UserFriendlyException(L("UniqueKeyword"));
                }
            }
            
            return insertedId;
        }

        [AbpAuthorize(AppPermissions.Pages_MemorizeKeywords_Create)]
        protected virtual async Task<int> Create(CreateOrEditMemorizeKeywordDto input)
        {
            var memorizeKeyword = ObjectMapper.Map<MemorizeKeyword>(input);

            if (AbpSession.TenantId != null)
            {
                memorizeKeyword.TenantId = (int?)AbpSession.TenantId;
            }

            var id = await _memorizeKeywordRepository.InsertAndGetIdAsync(memorizeKeyword);
            return id;
        }

        [AbpAuthorize(AppPermissions.Pages_MemorizeKeywords_Edit)]
        protected virtual async Task Update(CreateOrEditMemorizeKeywordDto input)
        {
            var memorizeKeyword = await _memorizeKeywordRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, memorizeKeyword);
        }

        [AbpAuthorize(AppPermissions.Pages_MemorizeKeywords_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _memorizeKeywordRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetMemorizeKeywordsToExcel(GetAllMemorizeKeywordsForExcelInput input)
        {

            var filteredMemorizeKeywords = _memorizeKeywordRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.KeyWord.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KeyWordFilter), e => e.KeyWord.ToLower() == input.KeyWordFilter.ToLower().Trim());

            var query = (from o in filteredMemorizeKeywords
                         select new GetMemorizeKeywordForViewDto()
                         {
                             MemorizeKeyword = new MemorizeKeywordDto
                             {
                                 KeyWord = o.KeyWord,
                                 IsActive = o.IsActive,
                                 Id = o.Id
                             }
                         });


            var memorizeKeywordListDtos = await query.ToListAsync();

            return _memorizeKeywordsExcelExporter.ExportToFile(memorizeKeywordListDtos);
        }

        public async Task<List<MemorizeKeywordDto>> GetAllMemorizeKeyword()
        {
            var result = await _memorizeKeywordRepository.GetAll().Where(x => x.CreatorUserId == AbpSession.UserId && x.TenantId == AbpSession.TenantId).ToListAsync();
            return ObjectMapper.Map<List<MemorizeKeywordDto>>(result);
        }

        public async Task GetKeywordDetails()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var queryData = await conn.QueryAsync<KeywordDetailDto>(sql: "dbo.GetMemoryKeywordDetail", param: new { AbpSession.UserId, AbpSession.TenantId }, commandType: CommandType.StoredProcedure);
                var listKeyword = queryData.ToList();
                foreach (var record in listKeyword)
                {

                }
            }
        }
    }
}