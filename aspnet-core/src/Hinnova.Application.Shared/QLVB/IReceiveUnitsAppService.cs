using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface IReceiveUnitsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetReceiveUnitForViewDto>> GetAll(GetAllReceiveUnitsInput input);

        Task<GetReceiveUnitForViewDto> GetReceiveUnitForView(int id);

		Task<GetReceiveUnitForEditOutput> GetReceiveUnitForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditReceiveUnitDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetReceiveUnitsToExcel(GetAllReceiveUnitsForExcelInput input);

		
    }
}