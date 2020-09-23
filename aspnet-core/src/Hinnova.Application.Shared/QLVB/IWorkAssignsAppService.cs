using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface IWorkAssignsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWorkAssignForViewDto>> GetAll(GetAllWorkAssignsInput input);

        Task<GetWorkAssignForViewDto> GetWorkAssignForView(int id);

		Task<GetWorkAssignForEditOutput> GetWorkAssignForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditWorkAssignDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetWorkAssignsToExcel(GetAllWorkAssignsForExcelInput input);

		
    }
}