using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface IPromulgatedsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPromulgatedForViewDto>> GetAll(GetAllPromulgatedsInput input);

        Task<GetPromulgatedForViewDto> GetPromulgatedForView(int id);

		Task<GetPromulgatedForEditOutput> GetPromulgatedForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPromulgatedDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPromulgatedsToExcel(GetAllPromulgatedsForExcelInput input);
    }
}