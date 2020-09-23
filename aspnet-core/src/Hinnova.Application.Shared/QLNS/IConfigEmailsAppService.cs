using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLNS
{
    public interface IConfigEmailsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetConfigEmailForViewDto>> GetAll(GetAllConfigEmailsInput input);

        Task<GetConfigEmailForViewDto> GetConfigEmailForView(int id);

        Task<GetConfigEmailForViewDto> GetConfigEmailByTenCtyForView(string tenCongty);

        Task<GetConfigEmailForEditOutput> GetConfigEmailForEdit(EntityDto input);


        Task CreateOrEdit(CreateOrEditConfigEmailDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetConfigEmailsToExcel(GetAllConfigEmailsForExcelInput input);

		
    }
}