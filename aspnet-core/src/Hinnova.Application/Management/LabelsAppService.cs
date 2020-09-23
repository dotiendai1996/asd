using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.Management.Dtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using Hinnova.QLVB;
using Hinnova.Authorization.Roles;
using Abp.Authorization.Users;
using Hinnova.Configuration;
using Microsoft.Extensions.Configuration;
using Dapper;
using Hinnova.Authorization.Users;

namespace Hinnova.Management
{
    //[AbpAuthorize(AppPermissions.Pages_Administration_Labels)]
    public class LabelsAppService : HinnovaAppServiceBase, ILabelsAppService
    {
        private readonly IRepository<Label> _labelRepository;
        private readonly IRepository<Menu> _menuRepository;
        private readonly IRepository<RoleMapper> _roleMapperRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly RoleManager _roleManager;
        private readonly IRepository<SqlConfig> _sqlConfigRepository;
        private readonly string connectionString;
        private readonly ISqlConfigsAppService _sqlConfigsAppService;
        private readonly ISqlConfigDetailsAppService _sqlConfigDetailsAppService;
        private readonly IUserAppService _userAppService;

        public LabelsAppService(IRepository<Menu> menuRepository, IRepository<Label> labelRepository, IWebHostEnvironment env,
            IRepository<RoleMapper> roleMapperRepository, RoleManager roleManager, IRepository<UserRole, long> userRoleRepository,
            IRepository<SqlConfig> sqlConfigRepository, ISqlConfigsAppService sqlConfigsAppService, 
            ISqlConfigDetailsAppService sqlConfigDetailsAppService, IUserAppService userAppService)
        {
            _menuRepository = menuRepository;
            _labelRepository = labelRepository;
            _roleMapperRepository = roleMapperRepository;
            _roleManager = roleManager;
            _userRoleRepository = userRoleRepository;
            _sqlConfigRepository = sqlConfigRepository;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");
            _sqlConfigsAppService = sqlConfigsAppService;
            _sqlConfigDetailsAppService = sqlConfigDetailsAppService;
            _userAppService = userAppService;
        }

        public async Task<PagedResultDto<GetLabelForViewDto>> GetAll(GetAllLabelsInput input)
        {

            var filteredLabels = _labelRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Title.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Icon.Contains(input.Filter) || e.Link.Contains(input.Filter) || e.RequiredPermissionName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.ToLower() == input.TitleFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IconFilter), e => e.Icon.ToLower() == input.IconFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LinkFilter), e => e.Link.ToLower() == input.LinkFilter.ToLower().Trim())
                        .WhereIf(input.MinParentFilter != null, e => e.Parent >= input.MinParentFilter)
                        .WhereIf(input.MaxParentFilter != null, e => e.Parent <= input.MaxParentFilter)
                        .WhereIf(input.MinIndexFilter != null, e => e.Index >= input.MinIndexFilter)
                        .WhereIf(input.MaxIndexFilter != null, e => e.Index <= input.MaxIndexFilter)
                        .WhereIf(input.SqlStringFilter != null, e => e.SqlString.ToLower() == input.SqlStringFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RequiredPermissionNameFilter), e => e.RequiredPermissionName.ToLower() == input.RequiredPermissionNameFilter.ToLower().Trim());

            var pagedAndFilteredLabels = filteredLabels
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var labels = from o in pagedAndFilteredLabels
                         select new GetLabelForViewDto()
                         {
                             Label = new LabelDto
                             {
                                 Name = o.Name,
                                 Title = o.Title,
                                 Description = o.Description,
                                 Icon = o.Icon,
                                 Link = o.Link,
                                 Parent = o.Parent,
                                 Index = o.Index,
                                 RequiredPermissionName = o.RequiredPermissionName,
                                 SqlString = o.SqlString,
                                 Id = o.Id
                             }
                         };

            var totalCount = await filteredLabels.CountAsync();

            return new PagedResultDto<GetLabelForViewDto>(
                totalCount,
                await labels.ToListAsync()
            );
        }

        public List<LabelDto> GetAllLabelForDynamicField()
        {
            var label = _labelRepository.GetAll().Where(x => x.Link.Length > 0);

            return ObjectMapper.Map<List<LabelDto>>(label);
        }

        public async Task<GetLabelForViewDto> GetLabelForView(int id)
        {
            var label = await _labelRepository.GetAsync(id);

            var output = new GetLabelForViewDto { Label = ObjectMapper.Map<LabelDto>(label) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Labels_Edit)]
        public async Task<GetLabelForEditOutput> GetLabelForEdit(EntityDto input)
        {
            var label = await _labelRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetLabelForEditOutput { Label = ObjectMapper.Map<CreateOrEditLabelDto>(label) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditLabelDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Administration_Labels_Create)]
        protected virtual async Task Create(CreateOrEditLabelDto input)
        {
            var label = ObjectMapper.Map<Label>(input);



            await _labelRepository.InsertAsync(label);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Labels_Edit)]
        protected virtual async Task Update(CreateOrEditLabelDto input)
        {
            var label = await _labelRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, label);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Labels_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _labelRepository.DeleteAsync(input.Id);
        }

        public async Task<List<LabelDto>> GetAllLabels()
        {
            var labels = await _labelRepository.GetAll().Where(x => (x.Parent == null || x.Parent == 0 ) && x.IsActive == true).OrderBy("index asc").ToListAsync();
            return ObjectMapper.Map<List<LabelDto>>(labels);
        }

        //public async Task<List<LabelDto>> GetFullLabels()
        //{
        //    var fatherLabels = await GetAllLabels();
        //    //var fatherLabels = _menuRepository.GetAll().Where(x => x.Type == "Top").ToList();
        //    try
        //    {
        //        foreach (var record in fatherLabels)
        //        {
        //            var child = await GetChildLabel(record.Id);
        //            if (child.Count() > 0)
        //            {
        //                record.ChildLabels = child;
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        //Logger.Error("10:00:00:00 " + ex);
        //        //throw new Exception(ex.Message);
        //    }

        //    return fatherLabels;
        //}

        public async Task<List<LabelDto>> GetAlllabel_()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var query = await conn.QueryAsync<Label>(sql: "SELECT * FROM label  " );
                return ObjectMapper.Map<List<LabelDto>>(query.ToList());
            }
        }
        public async Task<List<LabelDto>> GetMenu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var query = await conn.QueryAsync<Label>(sql: "SELECT * FROM label where parent = 0  ");
                return ObjectMapper.Map<List<LabelDto>>(query.ToList());
            }
        }
        public async Task<List<LabelDto>> GetListLabelByMenuId(int menuId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var query = await conn.QueryAsync<Label>(sql: " SELECT * FROM label where parent = " + menuId + " order by [Index] asc ");
                return ObjectMapper.Map<List<LabelDto>>(query.ToList());
            }
        }
        public async Task<LabelDto[]> GetChildLabel()
        {
            var child = _labelRepository.GetAll().Where(x => x.IsActive == true).OrderBy("index asc").ToArray();
            UserRole userRole = _userRoleRepository.GetAll().Where(x => x.UserId == AbpSession.UserId).FirstOrDefault();

           
            try
            {
                var role = _roleManager.GetRoleByIdAsync(userRole.RoleId);
                await role;
                var filteredMenus = await _roleMapperRepository.GetAll().Where(x => x.RoleId == userRole.RoleId && x.LabelId > 0).Distinct().ToDictionaryAsync(x => x.LabelId, x => x.RoleId);
                List<Label> result = new List<Label>();
                foreach (var label in child)
                {
                    if (filteredMenus.ContainsKey(label.Id) || role.Result.Name == "Admin")
                    {
                        result.Add(label);
                    }
                }
                return ObjectMapper.Map<LabelDto[]>(result);
            }
            catch
            {
                var roleId = await _userAppService.GetRoleIdOfUser((long)AbpSession.UserId);
                var roleName = await _userAppService.GetRoleNameOfUser((long)AbpSession.UserId);
                var filteredMenus = _roleMapperRepository.GetAll().Where(x => x.RoleId == roleId && x.LabelId > 0).Distinct().ToDictionary(x => x.LabelId, x => x.RoleId);
                List<Label> result = new List<Label>();
                foreach (var label in child)
                {
                    if (filteredMenus.ContainsKey(label.Id) || roleName == "Admin")
                    {
                        result.Add(label);
                    }
                }
                return ObjectMapper.Map<LabelDto[]>(result);
            }
           
        }

        public async Task<List<LabelDto>> GetAllForRoleMapper()
        {
            var result = await _labelRepository.GetAll().Where(x => x.IsActive == true).ToListAsync();
            return ObjectMapper.Map<List<LabelDto>>(result);
        }

        public async Task<List<SqlConfigDto>> GetAllSqlConfig()
        {
            var result = await _sqlConfigRepository.GetAll().Where(x => x.TenantId == AbpSession.TenantId).ToListAsync();
            return ObjectMapper.Map<List<SqlConfigDto>>(result);
        }


        // get all sql config detail
        public async Task<GetDataAndColumnConfig> GetAllDataAndColumnConfig(string sqlConfigCode)
        {
            var sqlConfig = _sqlConfigsAppService.GetSqlConfigByCodeAsync(sqlConfigCode).Result;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var columnConfig = _sqlConfigDetailsAppService.GetColumnConfigBySqlId(sqlConfig.Id);
                if (sqlConfig.IsRawQuery)
                {
                    var filterVanbans = conn.Query<object>(sqlConfig.SqlContent).ToList();

                    //Exec sqlConfig.SqlContent @tenantid + Sessions, 
                    return new GetDataAndColumnConfig(filterVanbans, columnConfig);
                }
                else
                {
                    var filterVanbans = conn.Query<object>(sql: sqlConfig.SqlContent , param: new { AbpSession.UserId, AbpSession.TenantId }, commandType: CommandType.StoredProcedure).ToList();

                    //Exec sqlConfig.SqlContent @tenantid + Sessions, 
                    return new GetDataAndColumnConfig(filterVanbans, columnConfig);
                }

            }
        }
    }
}