using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface IDocumentStatusesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetDocumentStatusForViewDto>> GetAll(GetAllDocumentStatusesInput input);

        Task<GetDocumentStatusForViewDto> GetDocumentStatusForView(int id);

		Task<GetDocumentStatusForEditOutput> GetDocumentStatusForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditDocumentStatusDto input);

		Task Delete(EntityDto input);

		
    }
}