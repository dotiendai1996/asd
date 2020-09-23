using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.QLNS
{
    public interface INoiDaoTaosAppService : IApplicationService 
    {
        Task<PagedResultDto<GetNoiDaoTaoForViewDto>> GetAll(GetAllNoiDaoTaosInput input);

        Task<GetNoiDaoTaoForViewDto> GetNoiDaoTaoForView(int id);

		Task<GetNoiDaoTaoForEditOutput> GetNoiDaoTaoForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditNoiDaoTaoDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetNoiDaoTaosToExcel(GetAllNoiDaoTaosForExcelInput input);

        List<NoiDaoTaoDto> GetAllNoiDaoTao();
    }
}