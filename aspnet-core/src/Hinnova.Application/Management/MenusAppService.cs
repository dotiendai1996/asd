using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Dapper;
using Hinnova.Authorization;
using Hinnova.Authorization.Roles;
using Hinnova.Configuration;
using Hinnova.Dto;
using Hinnova.Management.Dtos;
using Hinnova.Management.Exporting;
using Hinnova.QLVB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Hinnova.Management
{
    //[AbpAuthorize(AppPermissions.Pages_Administration_Menus)]
    public class MenusAppService : HinnovaAppServiceBase, IMenusAppService
    {
        private readonly IRepository<Menu> _menuRepository;
        private readonly IRepository<RoleMapper> _roleMapperRepository;
        private readonly RoleManager _roleManager;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IMenusExcelExporter _menusExcelExporter;
        private readonly string connectionString;

        public MenusAppService(IRepository<Menu> menuRepository, IMenusExcelExporter menusExcelExporter, IWebHostEnvironment env,
            IRepository<RoleMapper> roleMapperRepository, RoleManager roleManager, IRepository<UserRole, long> userRoleRepository)
        {
            _menuRepository = menuRepository;
            _menusExcelExporter = menusExcelExporter;
            _roleMapperRepository = roleMapperRepository;
            _roleManager = roleManager;
            _userRoleRepository = userRoleRepository;

            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //    .Build();
            //connectionString = configuration.GetConnectionString("Default");
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");

        }

        public async Task<PagedResultDto<GetMenuForViewDto>> GetAll(GetAllMenusInput input)
        {
            var filteredMenus = _menuRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Title.Contains(input.Filter) || e.Icon.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Link.Contains(input.Filter) || e.RequiredPermissionName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.ToLower() == input.TitleFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IconFilter), e => e.Icon.ToLower() == input.IconFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
                        .WhereIf(input.MinParentFilter != null, e => e.Parent >= input.MinParentFilter)
                        .WhereIf(input.MaxParentFilter != null, e => e.Parent <= input.MaxParentFilter)
                        //.WhereIf(input.IsParentFilter > -1, e => Convert.ToInt32(e.IsParent) == input.IsParentFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LinkFilter), e => e.Link.ToLower() == input.LinkFilter.ToLower().Trim())
                        //.WhereIf(!string.IsNullOrWhiteSpace(input.TypeFilter), e => e.Type.ToLower() == input.TypeFilter.ToLower().Trim())
                        .WhereIf(input.MinCreationTimeFilter != null, e => e.CreationTime >= input.MinCreationTimeFilter)
                        .WhereIf(input.MaxCreationTimeFilter != null, e => e.CreationTime <= input.MaxCreationTimeFilter)
                        .WhereIf(input.MinLastModificationTimeFilter != null, e => e.LastModificationTime >= input.MinLastModificationTimeFilter)
                        .WhereIf(input.MaxLastModificationTimeFilter != null, e => e.LastModificationTime <= input.MaxLastModificationTimeFilter)
                        .WhereIf(input.IsDeletedFilter > -1, e => Convert.ToInt32(e.IsDeleted) == input.IsDeletedFilter)
                        .WhereIf(input.MinDeletionTimeFilter != null, e => e.DeletionTime >= input.MinDeletionTimeFilter)
                        .WhereIf(input.MaxDeletionTimeFilter != null, e => e.DeletionTime <= input.MaxDeletionTimeFilter)
                        .WhereIf(input.MinIndexFilter != null, e => e.Index >= input.MinIndexFilter)
                        .WhereIf(input.MaxIndexFilter != null, e => e.Index <= input.MaxIndexFilter)
                        //.WhereIf(input.IsDelimiterFilter > -1, e => Convert.ToInt32(e.IsDelimiter) == input.IsDelimiterFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RequiredPermissionNameFilter), e => e.RequiredPermissionName.ToLower() == input.RequiredPermissionNameFilter.ToLower().Trim());
                        //.WhereIf(!string.IsNullOrWhiteSpace(input.UserRoleNameFilter), e => e.UserRoleName.ToLower() == input.UserRoleNameFilter.ToLower().Trim());

            var pagedAndFilteredMenus = filteredMenus
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var menus = from o in pagedAndFilteredMenus
                        select new GetMenuForViewDto()
                        {
                            Menu = new MenuDto
                            {
                                Name = o.Name,
                                Title = o.Title,
                                Icon = o.Icon,
                                Description = o.Description,
                                Parent = o.Parent,
                                //IsParent = o.IsParent,
                                Link = o.Link,
                                //Type = o.Type,
                                CreationTime = o.CreationTime,
                                LastModificationTime = o.LastModificationTime,
                                IsDeleted = o.IsDeleted,
                                DeletionTime = o.DeletionTime,
                                Index = o.Index,
                                //IsDelimiter = o.IsDelimiter,
                                RequiredPermissionName = o.RequiredPermissionName,
                                //UserRoleName = o.UserRoleName,
                                Id = o.Id
                            }
                        };

            var totalCount = await filteredMenus.CountAsync();

            return new PagedResultDto<GetMenuForViewDto>(
                totalCount,
                await menus.ToListAsync()
            );
        }

        public async Task<GetMenuForViewDto> GetMenuForView(int id)
        {
            var menu = await _menuRepository.GetAsync(id);

            var output = new GetMenuForViewDto { Menu = ObjectMapper.Map<MenuDto>(menu) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Menus_Edit)]
        public async Task<GetMenuForEditOutput> GetMenuForEdit(EntityDto input)
        {
            var menu = await _menuRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetMenuForEditOutput { Menu = ObjectMapper.Map<CreateOrEditMenuDto>(menu) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditMenuDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Menus_Create)]
        protected virtual async Task Create(CreateOrEditMenuDto input)
        {
            var menu = ObjectMapper.Map<Menu>(input);

            if (AbpSession.TenantId != null)
            {
                //menu.TenantId = (int?)AbpSession.TenantId;
            }

            await _menuRepository.InsertAsync(menu);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Menus_Edit)]
        protected virtual async Task Update(CreateOrEditMenuDto input)
        {
            var menu = await _menuRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, menu);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Menus_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _menuRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetMenusToExcel(GetAllMenusForExcelInput input)
        {
            var filteredMenus = _menuRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Title.Contains(input.Filter) || e.Icon.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Link.Contains(input.Filter) || e.RequiredPermissionName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.ToLower() == input.TitleFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IconFilter), e => e.Icon.ToLower() == input.IconFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
                        .WhereIf(input.MinParentFilter != null, e => e.Parent >= input.MinParentFilter)
                        .WhereIf(input.MaxParentFilter != null, e => e.Parent <= input.MaxParentFilter)
                        //.WhereIf(input.IsParentFilter > -1, e => Convert.ToInt32(e.IsParent) == input.IsParentFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LinkFilter), e => e.Link.ToLower() == input.LinkFilter.ToLower().Trim())
                        //.WhereIf(!string.IsNullOrWhiteSpace(input.TypeFilter), e => e.Type.ToLower() == input.TypeFilter.ToLower().Trim())
                        .WhereIf(input.MinCreationTimeFilter != null, e => e.CreationTime >= input.MinCreationTimeFilter)
                        .WhereIf(input.MaxCreationTimeFilter != null, e => e.CreationTime <= input.MaxCreationTimeFilter)
                        .WhereIf(input.MinLastModificationTimeFilter != null, e => e.LastModificationTime >= input.MinLastModificationTimeFilter)
                        .WhereIf(input.MaxLastModificationTimeFilter != null, e => e.LastModificationTime <= input.MaxLastModificationTimeFilter)
                        .WhereIf(input.IsDeletedFilter > -1, e => Convert.ToInt32(e.IsDeleted) == input.IsDeletedFilter)
                        .WhereIf(input.MinDeletionTimeFilter != null, e => e.DeletionTime >= input.MinDeletionTimeFilter)
                        .WhereIf(input.MaxDeletionTimeFilter != null, e => e.DeletionTime <= input.MaxDeletionTimeFilter)
                        .WhereIf(input.MinIndexFilter != null, e => e.Index >= input.MinIndexFilter)
                        .WhereIf(input.MaxIndexFilter != null, e => e.Index <= input.MaxIndexFilter)
                        //.WhereIf(input.IsDelimiterFilter > -1, e => Convert.ToInt32(e.IsDelimiter) == input.IsDelimiterFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RequiredPermissionNameFilter), e => e.RequiredPermissionName.ToLower() == input.RequiredPermissionNameFilter.ToLower().Trim());

            var query = (from o in filteredMenus
                         select new GetMenuForViewDto()
                         {
                             Menu = new MenuDto
                             {
                                 Name = o.Name,
                                 Title = o.Title,
                                 Icon = o.Icon,
                                 Description = o.Description,
                                 Parent = o.Parent,
                                 //IsParent = o.IsParent,
                                 Link = o.Link,
                                 //Type = o.Type,
                                 CreationTime = o.CreationTime,
                                 LastModificationTime = o.LastModificationTime,
                                 IsDeleted = o.IsDeleted,
                                 DeletionTime = o.DeletionTime,
                                 Index = o.Index,
                                 //IsDelimiter = o.IsDelimiter,
                                 RequiredPermissionName = o.RequiredPermissionName,
                                 Id = o.Id
                             }
                         });

            var menuListDtos = await query.ToListAsync();

            return _menusExcelExporter.ExportToFile(menuListDtos);
        }

        public Task<List<PermissionDto>> GetRootPermissionsAsync()
        {
            var permissions = PermissionManager.GetAllPermissions().Where(p => p.Parent != null).ToList();

            return Task.FromResult(ObjectMapper.Map<List<PermissionDto>>(permissions));
        }

        public async Task<List<MenuDto>> GetListAsync(GetMenuListInput input)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                        await conn.OpenAsync();
                    var menus = await conn.QueryAsync<Menu>(sql: "dbo.Menu_Search", param: new { input.Name, input.Type }, commandType: CommandType.StoredProcedure);
                    return ObjectMapper.Map<List<MenuDto>>(menus);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex);
                throw new UserFriendlyException("No data to show");
            }
        }

        public List<MenuDto> GetAllMenuDto()
        {
            var filteredMenus = _menuRepository.GetAll().Where(m => m.IsDeleted == false).AsNoTracking().ToList();
            return ObjectMapper.Map<List<MenuDto>>(filteredMenus);
        }

        public async Task<PagedResultDto<GetMenusForViewDto>> GetAllActiveAsync()
        {
            var filteredMenus = _menuRepository.GetAll().Where(m => m.IsDeleted == false).AsNoTracking();
            var menus = from o in filteredMenus
                        select new GetMenusForViewDto()
                        {
                            Menus = new MenuDto
                            {
                                Name = o.Name,
                                Title = o.Title,
                                Icon = o.Icon,
                                Description = o.Description,
                                Parent = o.Parent,
                                //IsParent = o.IsParent,
                                Link = o.Link,
                                //Type = o.Type,
                                CreationTime = o.CreationTime,
                                LastModificationTime = o.LastModificationTime,
                                IsDeleted = o.IsDeleted,
                                DeletionTime = o.DeletionTime,
                                Index = o.Index,
                                //IsDelimiter = o.IsDelimiter,
                                RequiredPermissionName = o.RequiredPermissionName,
                                Id = o.Id
                            }
                        };

            var totalCount = await filteredMenus.CountAsync();

            return new PagedResultDto<GetMenusForViewDto>(
                totalCount,
                await menus.ToListAsync()
            );
            //return ObjectMapper.Map<PagedResultDto<MenuListDto>>(menus);
        }

        public async Task<MenuDto> GetDetailAsync(int input)
        {
            var menu = await _menuRepository
            .GetAll()
            .Where(e => e.Id == input)
            .FirstOrDefaultAsync();

            if (menu == null)
            {
                throw new UserFriendlyException("Could not found the menu, maybe it's deleted.");
            }

            return ObjectMapper.Map<MenuDto>(menu);
        }

        public async Task<List<IdNameDto>> GetAllIndicesAsync()
        {
            var indices = await (from m in _menuRepository.GetAll().AsNoTracking()
                                 orderby m.Index descending
                                 select new { Id = m.Index, Name = m.Index + " - " + m.Name }).ToListAsync();
            return ObjectMapper.Map<List<IdNameDto>>(indices);
        }

        public async Task<IdNameDto> GetIndexByIdAsync(int id, int index)
        {
            var item = await (from m in _menuRepository.GetAll().AsNoTracking().Where(p => p.Id.Equals(id) && p.Index.Equals(index))
                              select new { Id = m.Index, Name = m.Index + " - " + m.Name }).SingleOrDefaultAsync();
            var result = item == null ? new IdNameDto() { Id = index, Name = index.ToString() } : ObjectMapper.Map<IdNameDto>(item);
            return ObjectMapper.Map<IdNameDto>(item);
        }

        public async Task<List<MenuDto>> GetAllChildMenu(int parent)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var menus = await conn.QueryAsync<Menu>(sql: "dbo.searchMenuChild", param: new { ParentID = parent }, commandType: CommandType.StoredProcedure);
                return ObjectMapper.Map<List<MenuDto>>(menus);
            }
        }

        public async Task<PagedResultDto<MenuDto>> GetAllTopMenu()
        {
            //var filteredMenus = await _menuRepository.GetAll().Where(x => x.Type == "Top" && x.IsDeleted == false).OrderBy("index asc").ToListAsync();
            var queryedMenus = await _menuRepository.GetAll().Where(x => x.IsDeleted == false).OrderBy("index asc").ToListAsync();
            var userRole = await _userRoleRepository.GetAll().Where(x => x.UserId == AbpSession.UserId).FirstOrDefaultAsync();
            var role = _roleManager.GetRoleByIdAsync(userRole.RoleId);
            await role;
            var filteredMenus = await _roleMapperRepository.GetAll().Where(x => x.RoleId == userRole.RoleId && x.MenuId > 0).ToDictionaryAsync(x => x.MenuId, x => x.RoleId);
            List<Menu> result = new List<Menu>();
            foreach(var menu in queryedMenus)
            {
                if (filteredMenus.ContainsKey(menu.Id) || role.Result.Name == "Admin")
                {
                    result.Add(menu);
                }
            }
            var menus = ObjectMapper.Map<List<MenuDto>>(result);

            var totalCount = result.Count();

            foreach (var menu in menus)
            {
                var child = GetAllChildMenus(menu.Id);
                if (child.Count > 0)
                {
                    menu.Children = child.ToArray();
                }
            }

            return new PagedResultDto<MenuDto>(
                totalCount,
                menus
            );
            //return ObjectMapper.Map<List<MenuDto>>(result);
        }

        private List<MenuDto> GetAllChildMenus(int id)
        {
            var result = _menuRepository.GetAll().Where(x => x.Parent == id && x.IsDeleted == false).ToList();
            return ObjectMapper.Map<List<MenuDto>>(result);
        }

        public async Task<PagedResultDto<MenuDto>> GetAllSideBarMenu(int parentMenuId = 0)
        {
            //var filteredMenus = await _menuRepository.GetAll().WhereIf(parentMenuId != 0, x => x.Parent == parentMenuId).Where(x => x.Type == "Side" && x.IsDeleted == false).ToListAsync();
            var filteredMenus = await _menuRepository.GetAll().WhereIf(parentMenuId != 0, x => x.Parent == parentMenuId).Where(x => x.IsDeleted == false).ToListAsync();
            var menus = ObjectMapper.Map<List<MenuDto>>(filteredMenus);

            var totalCount = filteredMenus.Count();

            return new PagedResultDto<MenuDto>(
                totalCount,
                menus
            );
        }

        public async Task<List<MenuDto>> GetFullMenu(GetMenuListInput input)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                        await conn.OpenAsync();
                    var menus = await conn.QueryAsync<MenuDto>(sql: "dbo.Menu_Search", param: new { input.Name, input.Type }, commandType: CommandType.StoredProcedure);
                    //var menuQuery = menus.AsQueryable();
                    foreach (var item in menus)
                    {
                        var num = item.Num;
                        if (num.Contains('.'))
                        {
                            item.NumBeforeComma = int.Parse(num.Split('.')[0]);
                            item.NumAfterComma = int.Parse(num.Split('.')[1]);
                        }
                        else
                        {
                            item.NumBeforeComma = int.Parse(num);
                            item.NumAfterComma = 0;
                        }
                    }
                    var god = new MenuDto();
                    var menuMap = ObjectMapper.Map<List<MenuDto>>(menus);
                    return menuMap;


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex);
                throw new UserFriendlyException("No data to show");
            }
        }

        // Get all menu for RoleMapper
        public async Task<List<MenuDto>> GetAllMenuActive()
        {
            var menus = await _menuRepository.GetAll().Where(x => x.IsDeleted == false).ToListAsync();
            return ObjectMapper.Map<List<MenuDto>>(menus);
        }
    }
}