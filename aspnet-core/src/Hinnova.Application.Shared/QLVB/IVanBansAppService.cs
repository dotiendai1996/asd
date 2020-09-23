using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Management.Dtos;

namespace Hinnova.QLVB
{
    public interface IVanbansAppService : IApplicationService 
    {
        Task<PagedResultDto<GetVanbanForViewDto>> GetAll(GetAllVanbansInput input);

        Task<GetVanbanForViewDto> GetVanbanForView(int id);

		Task<GetVanbanForEditOutput> GetVanbanForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditVanbanDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetVanbansToExcel(GetAllVanbansForExcelInput input);

        Task<GetDataAndColumnConfig> GetAllVanBan();
    }
}