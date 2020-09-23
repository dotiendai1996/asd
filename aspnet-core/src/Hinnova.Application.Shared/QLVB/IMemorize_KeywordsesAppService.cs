using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface IMemorize_KeywordsesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetMemorize_KeywordsForViewDto>> GetAll(GetAllMemorize_KeywordsesInput input);

        Task<GetMemorize_KeywordsForViewDto> GetMemorize_KeywordsForView(int id);

		Task<GetMemorize_KeywordsForEditOutput> GetMemorize_KeywordsForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditMemorize_KeywordsDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetMemorize_KeywordsesToExcel(GetAllMemorize_KeywordsesForExcelInput input);

		
    }
}