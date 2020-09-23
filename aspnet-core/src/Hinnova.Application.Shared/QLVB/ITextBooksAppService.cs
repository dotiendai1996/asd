using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface ITextBooksAppService : IApplicationService 
    {
        Task<PagedResultDto<GetTextBookForViewDto>> GetAll(GetAllTextBooksInput input);

        Task<GetTextBookForViewDto> GetTextBookForView(int id);

		Task<GetTextBookForEditOutput> GetTextBookForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditTextBookDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetTextBooksToExcel(GetAllTextBooksForExcelInput input);

		
    }
}