using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;

namespace Hinnova.QLNS
{
    public interface IQuyTrinhCongTacsAppService : IApplicationService
    {
        Task<PagedResultDto<GetQuyTrinhCongTacForViewDto>> GetAll( string id);

        Task<GetQuyTrinhCongTacForEditOutput> GetQuyTrinhCongTacForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditQuyTrinhCongTacDto input);

        Task<int> CreateOrEditForMobile(CreateOrEditQuyTrinhCongTacDto input);


        Task Delete(EntityDto input);

        Task<FileDto> GetQuyTrinhCongTacsToExcel(GetAllQuyTrinhCongTacsForExcelInput input);


    }
}