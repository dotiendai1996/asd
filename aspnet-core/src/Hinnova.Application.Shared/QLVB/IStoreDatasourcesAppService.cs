using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface IStoreDatasourcesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetStoreDatasourceForViewDto>> GetAll(GetAllStoreDatasourcesInput input);

        Task<GetStoreDatasourceForViewDto> GetStoreDatasourceForView(int id);

		Task<GetStoreDatasourceForEditOutput> GetStoreDatasourceForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditStoreDatasourceDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetStoreDatasourcesToExcel(GetAllStoreDatasourcesForExcelInput input);

		
    }
}