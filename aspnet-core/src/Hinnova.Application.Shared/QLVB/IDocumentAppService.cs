using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;
using Hinnova.Management.Dtos;

namespace Hinnova.QLVB
{
    public interface IDocumentAppService : IApplicationService 
    {
        Task<PagedResultDto<GetDocumentsForViewDto>> GetAll(GetAllDocumentsesInput input);

        Task<GetDocumentsForViewDto> GetDocumentsForView(int id);

		Task<DocumentsDto> GetDocumentsForEdit(EntityDto input);

		Task CreateOrEdit(DocumentsDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetDocumentsesToExcel(GetAllDocumentsesForExcelInput input);

        Task<List<DocumentDetailDto>> GetAllDocumentDetailWithId(int id);

        Task<PagedResultDto<DocumentsDto>> GetAllActiveDocument();

        Task<List<DocumentDetailDto>> GetAllDocumentDetailAsId(int id);

        Task<int> GetNextIncommingNumber();

        Task<List<DocumentsDto>> GetAllIncommingDocumentNotProcessed();

        //Task<GetDataAndColumnConfig> GetAllTableConfig();

        Task<List<CounterDto>> GetNumberOfAllDocumentType();

        Task<List<DocumentHandlingDto>> GetAllDocumentDetailByDocumentId(int documentId);

        Task UpdateStatusOfDocumentIntoTransfered(int documentId, string type);

        Task<int> CreateAndReturnId(DocumentsDto input);

        Task<List<Document_Waitting>> GetListDocumentNeedToComplete();

        Task<List<DocumentsDto>> GetAllDocumentWaitingHandling();
    }
}