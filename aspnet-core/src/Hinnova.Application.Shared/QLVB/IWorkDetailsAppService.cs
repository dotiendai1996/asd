using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface IWorkDetailsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWorkDetailForViewDto>> GetAll(GetAllWorkDetailsInput input);

        Task<GetWorkDetailForViewDto> GetWorkDetailForView(int id);

		Task<GetWorkDetailForEditOutput> GetWorkDetailForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditWorkDetailDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetWorkDetailsToExcel(GetAllWorkDetailsForExcelInput input);

		
    }
}