using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLNS
{
    public interface ITemplatesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetTemplateForViewDto>> GetAll(GetAllTemplatesInput input);

        Task<GetTemplateForViewDto> GetTemplateForView(int id);

		Task<GetTemplateForEditOutput> GetTemplateForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditTemplateDto input);

		Task Delete(EntityDto input);

		
    }
}