using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using Hinnova.Management.Dtos;
using System.Collections.Generic;

namespace Hinnova.QLNS
{
    public interface IQuanLyNghiPhepsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetQuanLyNghiPhepForViewDto>> GetAll(GetAllQuanLyNghiPhepsInput input);

        Task<GetQuanLyNghiPhepForViewDto> GetQuanLyNghiPhepForView(int id);

		Task<GetQuanLyNghiPhepForEditOutput> GetQuanLyNghiPhepForEdit(EntityDto input);

        Task<int> CreateOrEdit(CreateOrEditNghiPhepInput inputNghiPhep);

        Task<List<QuanLyNghiPhepDto>> GetQuanLyNghiPhepForDate(GetQuanLyNghiPhepForDateInput input);
        Task Delete(EntityDto input);

        Task<GetOrganizationUnitForEditOutput> GetOrganizationUnitForEdit(int id);


        //  Task<FileDto> GetQuanLyNghiPhepsToExcel(GetAllQuanLyNghiPhepsForExcelInput input);



    }
}