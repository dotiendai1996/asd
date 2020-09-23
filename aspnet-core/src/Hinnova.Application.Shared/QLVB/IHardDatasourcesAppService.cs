using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface IHardDatasourcesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetHardDatasourceForViewDto>> GetAll(GetAllHardDatasourcesInput input);

        Task<GetHardDatasourceForViewDto> GetHardDatasourceForView(int id);

		Task<GetHardDatasourceForEditOutput> GetHardDatasourceForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditHardDatasourceDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetHardDatasourcesToExcel(GetAllHardDatasourcesForExcelInput input);

		
    }
}