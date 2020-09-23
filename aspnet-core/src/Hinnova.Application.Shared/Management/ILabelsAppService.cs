using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.Management.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.Management
{
    public interface ILabelsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLabelForViewDto>> GetAll(GetAllLabelsInput input);

        Task<GetLabelForViewDto> GetLabelForView(int id);

		Task<GetLabelForEditOutput> GetLabelForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditLabelDto input);

		Task Delete(EntityDto input);

        Task<List<LabelDto>> GetAllLabels();

        //Task<List<LabelDto>> GetFullLabels();

        Task<LabelDto[]> GetChildLabel();

        List<LabelDto> GetAllLabelForDynamicField();

        Task<List<LabelDto>> GetAllForRoleMapper();

        Task<List<SqlConfigDto>> GetAllSqlConfig();

        Task<GetDataAndColumnConfig> GetAllDataAndColumnConfig(string sqlConfigCode);
    }
}