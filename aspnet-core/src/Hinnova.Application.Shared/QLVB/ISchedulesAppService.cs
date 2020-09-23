using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface ISchedulesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetScheduleForViewDto>> GetAll(GetAllSchedulesInput input);

        Task<GetScheduleForViewDto> GetScheduleForView(int id);

		Task<GetScheduleForEditOutput> GetScheduleForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditScheduleDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetSchedulesToExcel(GetAllSchedulesForExcelInput input);

		
    }
}