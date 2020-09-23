using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.QLNS
{
    public interface ITruongGiaoDichsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetTruongGiaoDichForViewDto>> GetAll(GetAllTruongGiaoDichsInput input);

        Task<GetTruongGiaoDichForViewDto> GetTruongGiaoDichForView(int id);

		Task<GetTruongGiaoDichForEditOutput> GetTruongGiaoDichForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditTruongGiaoDichDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetTruongGiaoDichsToExcel(GetAllTruongGiaoDichsForExcelInput input);

        List<TruongGiaoDichDto> GetAllTruongGiaoDich();
    }
}