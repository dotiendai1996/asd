using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNSDtos;
using Hinnova.Dto;

namespace Hinnova.QLNS
{
    public interface ISYS_CODEMASTERSsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetSYS_CODEMASTERSForViewDto>> GetAll(GetAllSYS_CODEMASTERSsInput input);

        Task<GetSYS_CODEMASTERSForViewDto> GetSYS_CODEMASTERSForView(int id);

		Task<GetSYS_CODEMASTERSForEditOutput> GetSYS_CODEMASTERSForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditSYS_CODEMASTERSDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetSYS_CODEMASTERSsToExcel(GetAllSYS_CODEMASTERSsForExcelInput input);

		
    }
}