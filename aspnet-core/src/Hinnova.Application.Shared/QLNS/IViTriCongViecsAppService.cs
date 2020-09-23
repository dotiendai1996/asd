using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLNS
{
    public interface IViTriCongViecsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetViTriCongViecForViewDto>> GetAll(GetAllViTriCongViecsInput input);

        Task<GetViTriCongViecForViewDto> GetViTriCongViecForView(int id);

		Task<GetViTriCongViecForEditOutput> GetViTriCongViecForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditViTriCongViecDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetViTriCongViecsToExcel(GetAllViTriCongViecsForExcelInput input);

		
    }
}