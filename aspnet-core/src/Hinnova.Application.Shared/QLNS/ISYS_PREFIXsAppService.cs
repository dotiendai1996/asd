using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNSDtos;
using Hinnova.Dto;

namespace Hinnova.QLNS
{
    public interface ISYS_PREFIXsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetSYS_PREFIXForViewDto>> GetAll(GetAllSYS_PREFIXsInput input);

        Task<GetSYS_PREFIXForViewDto> GetSYS_PREFIXForView(int id);

		Task<GetSYS_PREFIXForEditOutput> GetSYS_PREFIXForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditSYS_PREFIXDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetSYS_PREFIXsToExcel(GetAllSYS_PREFIXsForExcelInput input);

		
    }
}