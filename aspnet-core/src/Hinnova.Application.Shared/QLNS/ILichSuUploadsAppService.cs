using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.QLNS
{
    public interface ILichSuUploadsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLichSuUploadForViewDto>> GetAll(GetAllLichSuUploadsInput input);

        Task<GetLichSuUploadForViewDto> GetLichSuUploadForView(int id);

		Task<GetLichSuUploadForEditOutput> GetLichSuUploadForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditLichSuUploadDto input);

		Task Delete(EntityDto input);

        Task<List<LichSuUploadDto>> GetListLichSuUploadDto(string type, string id);
    }
}