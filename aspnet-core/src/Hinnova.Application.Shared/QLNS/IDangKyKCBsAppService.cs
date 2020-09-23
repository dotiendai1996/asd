using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNSDtos;
using Hinnova.Dto;

namespace Hinnova.QLNS
{
    public interface IDangKyKCBsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetDangKyKCBForViewDto>> GetAll(GetAllDangKyKCBsInput input);

        Task<GetDangKyKCBForViewDto> GetDangKyKCBForView(int id);

		Task<GetDangKyKCBForEditOutput> GetDangKyKCBForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditDangKyKCBDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetDangKyKCBsToExcel(GetAllDangKyKCBsForExcelInput input);

		
    }
}