using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.QLVB
{
    public interface IDocumentTypesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetDocumentTypeForViewDto>> GetAll(GetAllDocumentTypesInput input);

        Task<GetDocumentTypeForViewDto> GetDocumentTypeForView(int id);

		Task<GetDocumentTypeForEditOutput> GetDocumentTypeForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditDocumentTypeDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetDocumentTypesToExcel(GetAllDocumentTypesForExcelInput input);

        Task<List<DocumentTypeDto>> GetAllDocumentType();
    }
}