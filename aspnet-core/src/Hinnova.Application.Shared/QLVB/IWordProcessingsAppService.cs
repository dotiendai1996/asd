using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface IWordProcessingsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWordProcessingForViewDto>> GetAll(GetAllWordProcessingsInput input);

        Task<GetWordProcessingForViewDto> GetWordProcessingForView(int id);

		Task<GetWordProcessingForEditOutput> GetWordProcessingForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditWordProcessingDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetWordProcessingsToExcel(GetAllWordProcessingsForExcelInput input);

		
    }
}