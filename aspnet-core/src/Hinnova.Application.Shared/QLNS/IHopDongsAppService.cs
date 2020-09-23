using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Hinnova.QLNS.Dtos;

namespace Hinnova.QLNS
{
    public interface IHopDongsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetHopDongForViewDto>> GetAll(GetAllHopDongsInput input);

        Task<GetHopDongForViewDto> GetHopDongForView(int id);

		Task<GetHopDongForEditOutput> GetHopDongForEdit(EntityDto input);

		Task<int> CreateOrEdit(CreateOrEditHopDongInput input);

		Task Delete(EntityDto input);

		Task<FileDto> GetHopDongsToExcel(GetAllHopDongsForExcelInput input);

		
    }
}