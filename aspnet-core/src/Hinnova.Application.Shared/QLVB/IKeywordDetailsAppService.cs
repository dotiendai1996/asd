using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.QLVB
{
    public interface IKeywordDetailsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetKeywordDetailForViewDto>> GetAll(GetAllKeywordDetailsInput input);

		Task<GetKeywordDetailForEditOutput> GetKeywordDetailForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditKeywordDetailDto input);

		Task Delete(EntityDto input);

        Task<List<KeywordDetailDto>> GetAllKeywordDetailByKeywordId(int keywordId);
    }
}