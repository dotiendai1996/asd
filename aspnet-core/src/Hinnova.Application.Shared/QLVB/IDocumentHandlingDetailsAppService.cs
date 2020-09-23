using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.QLVB
{
    public interface IDocumentHandlingDetailsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetDocumentHandlingDetailForViewDto>> GetAll(GetAllDocumentHandlingDetailsInput input);

		Task<GetDocumentHandlingDetailForEditOutput> GetDocumentHandlingDetailForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditDocumentHandlingDetailDto input);

		Task Delete(EntityDto input);

        Task<List<DocumentHandlingDetailDto>> GetAllDocumentHandlingDetailByUserId(int userId);
    }
}