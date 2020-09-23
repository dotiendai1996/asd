

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLVB.Dtos;
using Hinnova.Management;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Hinnova.Authorization.Roles;

namespace Hinnova.QLVB
{
    [AbpAuthorize(AppPermissions.Pages_Administration_RoleMappers)]
    public class RoleMappersAppService : HinnovaAppServiceBase, IRoleMappersAppService
    {
        private readonly IRepository<RoleMapper> _roleMapperRepository;
        private readonly IRepository<Label> _labelRepository;
        private readonly IRepository<Role> _roleRepository;

        public RoleMappersAppService(IRepository<RoleMapper> roleMapperRepository, IRepository<Label> labelRepository, IRepository<Role> roleRepository)
        {
            _roleMapperRepository = roleMapperRepository;
            _labelRepository = labelRepository;
            _roleRepository = roleRepository;
        }

        public async Task<PagedResultDto<GetRoleMapperForViewDto>> GetAll(GetAllRoleMappersInput input)
        {

            var filteredRoleMappers = _roleMapperRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinRoleIdFilter != null, e => e.RoleId >= input.MinRoleIdFilter)
                        .WhereIf(input.MaxRoleIdFilter != null, e => e.RoleId <= input.MaxRoleIdFilter)
                        .WhereIf(input.MinLabelIdFilter != null, e => e.LabelId >= input.MinLabelIdFilter)
                        .WhereIf(input.MaxLabelIdFilter != null, e => e.LabelId <= input.MaxLabelIdFilter)
                        .WhereIf(input.MinMenuIdFilter != null, e => e.MenuId >= input.MinMenuIdFilter)
                        .WhereIf(input.MaxMenuIdFilter != null, e => e.MenuId <= input.MaxMenuIdFilter)
                        .WhereIf(input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive));

            var pagedAndFilteredRoleMappers = filteredRoleMappers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);
            var lbls = _labelRepository.GetAll();
           var roles = _roleRepository.GetAll();
            var roleMappers = from o in pagedAndFilteredRoleMappers

                              join lbl in lbls on o.LabelId equals lbl.Id into lblJoin
                              from joinelbl in lblJoin.DefaultIfEmpty()

                              join role in roles on o.RoleId equals role.Id into roleJoin
                              from joinerole in roleJoin.DefaultIfEmpty()


                              select new GetRoleMapperForViewDto()
                              {
                                  RoleMapper = new RoleMapperDto
                                  {
                                      RoleId = o.RoleId,
                                      Name = o.Name,
                                      LabelId = o.LabelId,
                                      MenuId = o.MenuId,
                                      IsActive = o.IsActive,
                                      Id = o.Id
                                  },
                                  LabelNameValue = joinelbl == null ? "" : joinelbl.Name.ToString(),
                                 RoleNameValue = joinerole == null ? "" : joinerole.DisplayName.ToString(),
                              };

            var totalCount = await filteredRoleMappers.CountAsync();

            return new PagedResultDto<GetRoleMapperForViewDto>(
                totalCount,
                await roleMappers.ToListAsync()
            );
        }

        public async Task<GetRoleMapperForViewDto> GetRoleMapperForView(int id)
        {
            var roleMapper = await _roleMapperRepository.GetAsync(id);

            var output = new GetRoleMapperForViewDto { RoleMapper = ObjectMapper.Map<RoleMapperDto>(roleMapper) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_RoleMappers_Edit)]
        public async Task<GetRoleMapperForEditOutput> GetRoleMapperForEdit(EntityDto input)
        {
            var roleMapper = await _roleMapperRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRoleMapperForEditOutput { RoleMapper = ObjectMapper.Map<CreateOrEditRoleMapperDto>(roleMapper) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditRoleMapperDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Administration_RoleMappers_Create)]
        protected virtual async Task Create(CreateOrEditRoleMapperDto input)
        {
            var roleMapper = ObjectMapper.Map<RoleMapper>(input);


            if (AbpSession.TenantId != null)
            {
                roleMapper.TenantId = (int?)AbpSession.TenantId;
            }


            await _roleMapperRepository.InsertAsync(roleMapper);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_RoleMappers_Edit)]
        protected virtual async Task Update(CreateOrEditRoleMapperDto input)
        {
            var roleMapper = await _roleMapperRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, roleMapper);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_RoleMappers_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _roleMapperRepository.DeleteAsync(input.Id);
        }

        //    public async Task<List<dynamic>> GetAllRoleMapperForView()
        //    {

        //    }

        public async Task CreateOrEditListRoleMapper(CreateOrEditListRoleMapper input)
        {
            var oldMenu = await _roleMapperRepository.GetAll().Where(x => x.RoleId == input.RoleId && x.MenuId == input.MenuId).FirstOrDefaultAsync();
            //if (oldMenu == null)
            //{
            //    await _roleMapperRepository.InsertAsync(new RoleMapper { RoleId = input.RoleId, MenuId = input.MenuId, LabelId = 0, IsActive = true });
            //}
            foreach (var label in input.LabelId)
            {
                var oldLabel = await _roleMapperRepository.GetAll().Where(x => x.RoleId == input.RoleId && x.LabelId == label).FirstOrDefaultAsync();
                if (oldLabel == null)
                {
                    await _roleMapperRepository.InsertAsync(new RoleMapper { RoleId = input.RoleId, LabelId = label, MenuId = 0, IsActive = true });
                }
            }
        }
    }
}