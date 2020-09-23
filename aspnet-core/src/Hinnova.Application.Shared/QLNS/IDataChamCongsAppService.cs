using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.QLNS
{
    public interface IDataChamCongsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetDataChamCongForViewDto>> GetAll(GetAllDataChamCongsInput input);

        Task<GetDataChamCongForViewDto> GetDataChamCongForView(int id);

		Task<GetDataChamCongForEditOutput> GetDataChamCongForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditDataChamCongDto input);

        Task<List<DataChamCongDto>> kiemTraCheckTrongNgay(int MaChamCong, DateTime TimeCheckDate);

        Task CreateOrEditMobile(CreateOrEditMobileDataChamCongDto input);

        Task Delete(EntityDto input);

		Task<FileDto> GetDataChamCongsToExcel(GetAllDataChamCongsForExcelInput input);
        Task<FileDto> GetDataChamCongsFilterToExcel(GetAllDataChamCongsInput input);

        Task<string> GetProcessDateMax();
    }
}