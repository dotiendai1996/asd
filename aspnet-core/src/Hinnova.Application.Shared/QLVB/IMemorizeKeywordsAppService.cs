using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.QLVB
{
    public interface IMemorizeKeywordsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetMemorizeKeywordForViewDto>> GetAll(GetAllMemorizeKeywordsInput input);

        Task<GetMemorizeKeywordForViewDto> GetMemorizeKeywordForView(int id);

		Task<GetMemorizeKeywordForEditOutput> GetMemorizeKeywordForEdit(EntityDto input);

		Task<int> CreateOrEdit(CreateOrEditMemorizeKeywordDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetMemorizeKeywordsToExcel(GetAllMemorizeKeywordsForExcelInput input);

        Task<List<MemorizeKeywordDto>> GetAllMemorizeKeyword();
    }
}