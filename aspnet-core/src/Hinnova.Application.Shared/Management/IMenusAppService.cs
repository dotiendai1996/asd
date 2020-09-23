using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hinnova.Management.Dtos;
using Hinnova.Dto;
using System.Collections.Generic;

namespace Hinnova.Management
{
    public interface IMenusAppService : IApplicationService 
    {
        Task<PagedResultDto<GetMenuForViewDto>> GetAll(GetAllMenusInput input);

        Task<GetMenuForViewDto> GetMenuForView(int id);

		Task<GetMenuForEditOutput> GetMenuForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditMenuDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetMenusToExcel(GetAllMenusForExcelInput input);

        Task<List<PermissionDto>> GetRootPermissionsAsync();

        Task<List<MenuDto>> GetListAsync(GetMenuListInput input);

        Task<PagedResultDto<GetMenusForViewDto>> GetAllActiveAsync();


        Task<MenuDto> GetDetailAsync(int input);

        Task<List<IdNameDto>> GetAllIndicesAsync();

        Task<IdNameDto> GetIndexByIdAsync(int id, int index);

        Task<List<MenuDto>> GetAllChildMenu(int parent);

        Task<PagedResultDto<MenuDto>> GetAllTopMenu();

        //Task<PagedResultDto<GetMenusForViewDto>> GetAllMenuByType(string menuType);

        Task<List<MenuDto>> GetFullMenu(GetMenuListInput input);

        Task<PagedResultDto<MenuDto>> GetAllSideBarMenu(int parentMenuId);

        List<MenuDto> GetAllMenuDto();

        Task<List<MenuDto>> GetAllMenuActive();

        //Task<List<MenuDto>> GetAllTopMenu();
    }
}