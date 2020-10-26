using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;
using Hinnova.Authorization.Users.Dto;
using Hinnova.Organizations.Dto;

namespace Hinnova.QLNS
{
    public interface IDataChamCongsAppService : IApplicationService
    {
        Task<List<string>> GetAllFullNameByFilterKeyWord(string search);

        Task<DataChamCongFilter> GetDataChamCongFilter();

        Task<List<OrganizationUnitDto>> GetCongViecByTenCty(string tenCty);

        Task<List<DataChamCongDto>> GetAllByBranchId(string branchId, DateTime dateTo, DateTime dateFrom,
            int maChamCong, string search, bool flagGlobal);

        Task<PagedResultDto<GetDataChamCongForViewDto>> GetAll(GetAllDataChamCongsInput input);

        Task<GetDataChamCongForViewDto> GetDataChamCongForView(int id);

        Task<GetDataChamCongForEditOutput> GetDataChamCongForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditDataChamCongDto input);

        Task<List<DataChamCongDto>> kiemTraCheckTrongNgay(int MaChamCong, DateTime TimeCheckDate);

        Task Delete(EntityDto input);

        Task<List<DataChamCongDto>> GetAllByMaChamCong(int MaChamCong);

        Task<FileDto> GetDataChamCongsToExcel(GetAllDataChamCongsForExcelInput input);
        Task<FileDto> GetDataChamCongsFilterToExcel(GetAllDataChamCongsInput input);

        Task<string> GetProcessDateMax();
        Task CreateOrEditMobile(int maChamCong);
        DateTime getCurrentDateTime();
    }
}