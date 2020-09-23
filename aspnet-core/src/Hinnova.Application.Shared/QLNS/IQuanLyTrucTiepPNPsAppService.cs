using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLNS
{
    public interface IQuanLyTrucTiepPNPsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetQuanLyTrucTiepPNPForViewDto>> GetAll(GetAllQuanLyTrucTiepPNPsInput input);

		Task<GetQuanLyTrucTiepPNPForEditOutput> GetQuanLyTrucTiepPNPForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditQuanLyTrucTiepPNPDto input);

		Task Delete(EntityDto input);
        Task<List<QuanLyTrucTiepPNPDto>> GetListQuanLyTrucTiepDto(int id);


    }
}