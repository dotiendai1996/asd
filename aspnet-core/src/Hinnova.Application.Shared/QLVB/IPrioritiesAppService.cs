using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.QLVB
{
    public interface IPrioritiesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPriorityForViewDto>> GetAll(GetAllPrioritiesInput input);

        Task<GetPriorityForViewDto> GetPriorityForView(int id);

		Task<GetPriorityForEditOutput> GetPriorityForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPriorityDto input);

		Task Delete(EntityDto input);

        Task<List<PriorityDto>> GetAllPriorities();
    }
}