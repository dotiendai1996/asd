using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface ICommandDatasourcesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetCommandDatasourceForViewDto>> GetAll(GetAllCommandDatasourcesInput input);

        Task<GetCommandDatasourceForViewDto> GetCommandDatasourceForView(int id);

		Task<GetCommandDatasourceForEditOutput> GetCommandDatasourceForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditCommandDatasourceDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetCommandDatasourcesToExcel(GetAllCommandDatasourcesForExcelInput input);

		
    }
}