using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLVB
{
    public interface IHistoryUploadsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetHistoryUploadForViewDto>> GetAll(GetAllHistoryUploadsInput input);

		Task<GetHistoryUploadForEditOutput> GetHistoryUploadForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditHistoryUploadDto input);

		Task Delete(EntityDto input);

		
    }
}