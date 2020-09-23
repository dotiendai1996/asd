using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Hinnova.QLNS.Dtos;

namespace Hinnova.QLNS
{
    public interface IUngViensAppService : IApplicationService 
    {
        Task<string> importToExcel(string currentTime, string path);
        //Task<PagedResultDto<GetUngVienForViewDto>> GetAll(GetAllUngViensInput input);

        Task<GetUngVienForViewDto> GetUngVienForView(int id);

		Task<GetUngVienForEditOutput> GetUngVienForEdit(EntityDto input);

        /// <summary>
        /// chinhqn@gsoft.com.vn
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetUngVienForEditOutput> GetUngVienForUpdate(EntityDto input);


        Task<int> CreateOrEdit(CreateOrEditUngVienInput ungVienInput);

        Task Delete(EntityDto input);

		//Task<FileDto> GetUngViensToExcel(GetAllUngViensForExcelInput input);

		
    }
}