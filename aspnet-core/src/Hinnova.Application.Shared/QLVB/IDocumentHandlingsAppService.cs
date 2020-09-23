using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.QLVB
{
    public interface IDocumentHandlingsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetDocumentHandlingForViewDto>> GetAll(GetAllDocumentHandlingsInput input);

        Task<GetDocumentHandlingForViewDto> GetDocumentHandlingForView(int id);

		Task<GetDocumentHandlingForEditOutput> GetDocumentHandlingForEdit(EntityDto input);

		Task<int> CreateOrEdit(CreateOrEditDocumentHandlingDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetDocumentHandlingsToExcel(GetAllDocumentHandlingsForExcelInput input);

        Task<List<string>> GetLeaderTypes();

        //Task<List<HandlingUser>> GetLeaderList(string organizationName);
        //Task<List<HandlingUser>> GetLeaderList();

    }
}