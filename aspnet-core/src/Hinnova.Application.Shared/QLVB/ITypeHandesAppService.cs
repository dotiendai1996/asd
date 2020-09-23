using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface ITypeHandesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetTypeHandeForViewDto>> GetAll(GetAllTypeHandesInput input);

        Task<GetTypeHandeForViewDto> GetTypeHandeForView(int id);

		Task<GetTypeHandeForEditOutput> GetTypeHandeForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditTypeHandeDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetTypeHandesToExcel(GetAllTypeHandesForExcelInput input);

		
    }
}