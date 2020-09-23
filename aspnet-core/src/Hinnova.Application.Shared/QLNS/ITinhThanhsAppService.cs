using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.QLNS
{
    public interface ITinhThanhsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetTinhThanhForViewDto>> GetAll(GetAllTinhThanhsInput input);

        Task<GetTinhThanhForViewDto> GetTinhThanhForView(int id);

		Task<GetTinhThanhForEditOutput> GetTinhThanhForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditTinhThanhDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetTinhThanhsToExcel(GetAllTinhThanhsForExcelInput input);

        List<TinhThanhDto> GetAllTinhThanh();
    }
}