using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface IDocumentDetailsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetDocumentDetailForViewDto>> GetAll(GetAllDocumentDetailsInput input);

        Task<GetDocumentDetailForViewDto> GetDocumentDetailForView(int id);

		Task<GetDocumentDetailForEditOutput> GetDocumentDetailForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditDocumentDetailDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetDocumentDetailsToExcel(GetAllDocumentDetailsForExcelInput input);

		
    }
}