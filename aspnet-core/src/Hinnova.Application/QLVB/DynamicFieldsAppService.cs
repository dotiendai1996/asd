

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Abp.Dapper.Repositories;
using Microsoft.AspNetCore.Hosting;
using Hinnova.Configuration;
using Microsoft.Extensions.Configuration;

namespace Hinnova.QLVB
{
    [AbpAuthorize(AppPermissions.Pages_DynamicFields)]
    public class DynamicFieldsAppService : HinnovaAppServiceBase, IDynamicFieldsAppService
    {
        private readonly IRepository<DynamicField> _dynamicFieldRepository;
        private readonly IRepository<DynamicValue> _dynamicValueRepository;
        private readonly IDapperRepository<DynamicValue> _dynamicFieldDapperRepository;
        private readonly string connectionString;
        public DynamicFieldsAppService(IWebHostEnvironment env, IRepository<DynamicValue> dynamicValueRepository, IDapperRepository<DynamicValue> dynamicFieldDapperRepository, IRepository<DynamicField> dynamicFieldRepository)
        {
            _dynamicFieldRepository = dynamicFieldRepository;
            _dynamicFieldRepository = dynamicFieldRepository;
            _dynamicValueRepository = dynamicValueRepository;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");
        }

        //lấy ra tất cả module đang có cột động
        public async Task<List<DynamicFieldDto>> GetAllDynamicFieldDistinctAsync()
        {
            //var dynamicField = _dynamicFieldRepository.GetAll().Select(i => new DynamicFieldDto { i.ModuleId, i.TableName}).Distinct().ToList();
            var dynamicField = (await _dynamicFieldRepository.GetAll().ToListAsync()).GroupBy(m => m.ModuleId).Select(x => x.First()).ToList();
            return ObjectMapper.Map<List<DynamicFieldDto>>(dynamicField);
        }

        public List<DynamicFieldDto> GetDynamicFieldByModuleId(int moduleId)
        {
            var dynamicField = _dynamicFieldRepository.GetAll().Where(x => x.ModuleId == moduleId).ToList();
            return ObjectMapper.Map<List<DynamicFieldDto>>(dynamicField);
        }

        public async Task<List<DynamicFieldListDto>> GetDynamicFields(GetDynamicFieldListInput input)
        {
            //var tenantId = (AbpSession.TenantId == null) ? 1 : AbpSession.TenantId;
            var tenantId = AbpSession.TenantId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var dynamicValue = await conn.QueryAsync<DynamicFieldListDto>(sql: "dbo.GetDynamicFields", param: new { input.Link, tenantId, input.ObjectId, AbpSession.UserId }, commandType: CommandType.StoredProcedure);
                return dynamicValue.ToList();
            }

            //var dynamicValue = await _dynamicFieldDapperRepository.QueryAsync<DynamicFieldListDto>("GetDynamicFields @Link, @TenantId, @ObjectId", new { input.Link, tenantId, input.ObjectId });
            //return dynamicValue.ToList();
        }

        public async Task<List<GetDataSourceDynamicDto>> GetDataSourceDynamic(GetDataSourceDynamicInput input)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                        await conn.OpenAsync();
                    var dynamicValue = await conn.QueryAsync<GetDataSourceDynamicDto>(sql: "GetDataSourceDynamic", param: new { input.DynamicFieldId, input.ObjectId, input.Parameters }, commandType: CommandType.StoredProcedure);
                    return dynamicValue.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<DynamicFieldDto> GetCbbField(int? moduleId)
        {
            //TypeField = 3 => Combobox
            var dynamicField = _dynamicFieldRepository.GetAll().Where(x => x.TypeField == 3).WhereIf(moduleId.HasValue, x => x.ModuleId == moduleId).ToList();
            return ObjectMapper.Map<List<DynamicFieldDto>>(dynamicField);
        }

        public async Task CreateDynamicFieldForModule(List<CreateOrEditDynamicFieldDto> input)
        {
            foreach (var data in input)
            {
                if(data.IsActive == null || data.IsActive.ToString() == "")
                {
                    data.IsActive = true;
                }
                await CreateOrEdit(data);
            }
        }

        public async Task InsertUpdateDynamicFields(List<DynamicValueDto> input)
        {
            foreach (var data in input)
            {
                if (data.Id == 0)
                {
                    var dynamicValue = ObjectMapper.Map<DynamicValue>(data);
                    if (AbpSession.TenantId != null)
                    {
                        dynamicValue.TenantId = (int?)AbpSession.TenantId;
                    }
                    await _dynamicValueRepository.InsertAsync(dynamicValue);
                }
                else
                {
                    //await _dynamicValueRepository.UpdateAsync(data);
                    var dynamicValue = await _dynamicValueRepository.FirstOrDefaultAsync((int)data.Id);
                    ObjectMapper.Map(data, dynamicValue);
                }
            }
        }

        public async Task<PagedResultDto<GetDynamicFieldForViewDto>> GetAll(GetAllDynamicFieldsInput input)
        {

            var filteredDynamicFields = _dynamicFieldRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.TableName.Contains(input.Filter) || e.Name.Contains(input.Filter) || e.NameDescription.Contains(input.Filter) || e.ClassAttach.Contains(input.Filter))
                        .WhereIf(input.MinModuleIdFilter != null, e => e.ModuleId >= input.MinModuleIdFilter)
                        .WhereIf(input.MaxModuleIdFilter != null, e => e.ModuleId <= input.MaxModuleIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TableNameFilter), e => e.TableName == input.TableNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name == input.NameFilter)
                        .WhereIf(input.MinTypeFieldFilter != null, e => e.TypeField >= input.MinTypeFieldFilter)
                        .WhereIf(input.MaxTypeFieldFilter != null, e => e.TypeField <= input.MaxTypeFieldFilter)
                        .WhereIf(input.MinWidthFilter != null, e => e.Width >= input.MinWidthFilter)
                        .WhereIf(input.MaxWidthFilter != null, e => e.Width <= input.MaxWidthFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameDescriptionFilter), e => e.NameDescription == input.NameDescriptionFilter)
                        .WhereIf(input.MinDepartmentIdFilter != null, e => e.DepartmentId >= input.MinDepartmentIdFilter)
                        .WhereIf(input.MaxDepartmentIdFilter != null, e => e.DepartmentId <= input.MaxDepartmentIdFilter)
                        .WhereIf(input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
                        .WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter)
                        .WhereIf(input.MinWidthDescriptionFilter != null, e => e.WidthDescription >= input.MinWidthDescriptionFilter)
                        .WhereIf(input.MaxWidthDescriptionFilter != null, e => e.WidthDescription <= input.MaxWidthDescriptionFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClassAttachFilter), e => e.ClassAttach == input.ClassAttachFilter);

            var pagedAndFilteredDynamicFields = filteredDynamicFields
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var dynamicFields = from o in pagedAndFilteredDynamicFields
                                select new GetDynamicFieldForViewDto()
                                {
                                    DynamicField = new DynamicFieldDto
                                    {
                                        ModuleId = o.ModuleId,
                                        TableName = o.TableName,
                                        Name = o.Name,
                                        TypeField = o.TypeField,
                                        Width = o.Width,
                                        NameDescription = o.NameDescription,
                                        DepartmentId = o.DepartmentId,
                                        IsActive = o.IsActive,
                                        Order = o.Order,
                                        WidthDescription = o.WidthDescription,
                                        ClassAttach = o.ClassAttach,
                                        Id = o.Id
                                    }
                                };

            var totalCount = await filteredDynamicFields.CountAsync();

            return new PagedResultDto<GetDynamicFieldForViewDto>(
                totalCount,
                await dynamicFields.ToListAsync()
            );
        }

        public async Task<GetDynamicFieldForViewDto> GetDynamicFieldForView(int id)
        {
            var dynamicField = await _dynamicFieldRepository.GetAsync(id);

            var output = new GetDynamicFieldForViewDto { DynamicField = ObjectMapper.Map<DynamicFieldDto>(dynamicField) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_DynamicFields_Edit)]
        public async Task<GetDynamicFieldForEditOutput> GetDynamicFieldForEdit(EntityDto input)
        {
            var dynamicField = await _dynamicFieldRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDynamicFieldForEditOutput { DynamicField = ObjectMapper.Map<CreateOrEditDynamicFieldDto>(dynamicField) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditDynamicFieldDto input)
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

        [AbpAuthorize(AppPermissions.Pages_DynamicFields_Create)]
        protected virtual async Task Create(CreateOrEditDynamicFieldDto input)
        {
            var dynamicField = ObjectMapper.Map<DynamicField>(input);


            if (AbpSession.TenantId != null)
            {
                dynamicField.TenantId = (int?)AbpSession.TenantId;
            }


            await _dynamicFieldRepository.InsertAsync(dynamicField);
        }

        [AbpAuthorize(AppPermissions.Pages_DynamicFields_Edit)]
        protected virtual async Task Update(CreateOrEditDynamicFieldDto input)
        {
            var dynamicField = await _dynamicFieldRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, dynamicField);
        }

        [AbpAuthorize(AppPermissions.Pages_DynamicFields_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _dynamicFieldRepository.DeleteAsync(input.Id);
        }
    }
}