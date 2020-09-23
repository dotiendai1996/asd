using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.QLNS
{
    public interface ILichSuLamViecsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLichSuLamViecForViewDto>> GetAll(GetAllLichSuLamViecsInput input);

        Task<GetLichSuLamViecForViewDto> GetLichSuLamViecForView(int id);

		Task<GetLichSuLamViecForEditOutput> GetLichSuLamViecForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditLichSuLamViecDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetLichSuLamViecsToExcel(GetAllLichSuLamViecsForExcelInput input);

        Task<List<LichSuLamViecDto>> GetLichSuLamViecByUngVienAsync(int ungvienId);
    }
}