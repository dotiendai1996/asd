

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLVB.Exporting;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Hinnova.QLVB
{
    [AbpAuthorize(AppPermissions.Pages_DynamicDatasource)]
    public class DynamicDatasourceAppService : HinnovaAppServiceBase, IDynamicDatasourceAppService
    {
        private readonly IRepository<DynamicDatasource> _dynamicDatasourceRepository;
        private readonly IDynamicDatasourceExcelExporter _dynamicDatasourceExcelExporter;


        public DynamicDatasourceAppService(IRepository<DynamicDatasource> dynamicDatasourceRepository, IDynamicDatasourceExcelExporter dynamicDatasourceExcelExporter)
        {
            _dynamicDatasourceRepository = dynamicDatasourceRepository;
            _dynamicDatasourceExcelExporter = dynamicDatasourceExcelExporter;

        }

        public List<DynamicDatasourceDto> GetDynamicDatasourceByType(int type)
        {
            //type gồm: 1 => cứng, 2 => theo store, 3 => theo lệnh
            var dynamicDatasource = _dynamicDatasourceRepository.GetAll().Where(x => x.Type == type);

            return ObjectMapper.Map<List<DynamicDatasourceDto>>(dynamicDatasource);
        }

        public async Task<PagedResultDto<GetDynamicDatasourceForViewDto>> GetAll(GetAllDynamicDatasourceInput input)
        {

            var filteredDynamicDatasource = _dynamicDatasourceRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinTypeFilter != null, e => e.Type >= input.MinTypeFilter)
                        .WhereIf(input.MaxTypeFilter != null, e => e.Type <= input.MaxTypeFilter)
                        .WhereIf(input.MinObjectIdFilter != null, e => e.ObjectId >= input.MinObjectIdFilter)
                        .WhereIf(input.MaxObjectIdFilter != null, e => e.ObjectId <= input.MaxObjectIdFilter)
                        .WhereIf(input.MinDynamicFieldIdFilter != null, e => e.DynamicFieldId >= input.MinDynamicFieldIdFilter)
                        .WhereIf(input.MaxDynamicFieldIdFilter != null, e => e.DynamicFieldId <= input.MaxDynamicFieldIdFilter)
                        .WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
                        .WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter)
                        .WhereIf(input.IsActiveFilter > -1, e => Convert.ToInt32(e.IsActive) == input.IsActiveFilter);

            var pagedAndFilteredDynamicDatasource = filteredDynamicDatasource
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var dynamicDatasource = from o in pagedAndFilteredDynamicDatasource
                                    select new GetDynamicDatasourceForViewDto()
                                    {
                                        DynamicDatasource = new DynamicDatasourceDto
                                        {
                                            Type = o.Type,
                                            ObjectId = o.ObjectId,
                                            DynamicFieldId = o.DynamicFieldId,
                                            Order = o.Order,
                                            IsActive = o.IsActive,
                                            Id = o.Id
                                        }
                                    };

            var totalCount = await filteredDynamicDatasource.CountAsync();

            return new PagedResultDto<GetDynamicDatasourceForViewDto>(
                totalCount,
                await dynamicDatasource.ToListAsync()
            );
        }

        public async Task<GetDynamicDatasourceForViewDto> GetDynamicDatasourceForView(int id)
        {
            var dynamicDatasource = await _dynamicDatasourceRepository.GetAsync(id);

            var output = new GetDynamicDatasourceForViewDto { DynamicDatasource = ObjectMapper.Map<DynamicDatasourceDto>(dynamicDatasource) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_DynamicDatasource_Edit)]
        public async Task<GetDynamicDatasourceForEditOutput> GetDynamicDatasourceForEdit(EntityDto input)
        {
            var dynamicDatasource = await _dynamicDatasourceRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDynamicDatasourceForEditOutput { DynamicDatasource = ObjectMapper.Map<CreateOrEditDynamicDatasourceDto>(dynamicDatasource) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditDynamicDatasourceDto input)
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

        [AbpAuthorize(AppPermissions.Pages_DynamicDatasource_Create)]
        protected virtual async Task Create(CreateOrEditDynamicDatasourceDto input)
        {
            var dynamicDatasource = ObjectMapper.Map<DynamicDatasource>(input);


            if (AbpSession.TenantId != null)
            {
                dynamicDatasource.TenantId = (int?)AbpSession.TenantId;
            }


            await _dynamicDatasourceRepository.InsertAsync(dynamicDatasource);
        }

        [AbpAuthorize(AppPermissions.Pages_DynamicDatasource_Edit)]
        protected virtual async Task Update(CreateOrEditDynamicDatasourceDto input)
        {
            var dynamicDatasource = await _dynamicDatasourceRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, dynamicDatasource);
        }

        [AbpAuthorize(AppPermissions.Pages_DynamicDatasource_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _dynamicDatasourceRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetDynamicDatasourceToExcel(GetAllDynamicDatasourceForExcelInput input)
        {

            var filteredDynamicDatasource = _dynamicDatasourceRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinTypeFilter != null, e => e.Type >= input.MinTypeFilter)
                        .WhereIf(input.MaxTypeFilter != null, e => e.Type <= input.MaxTypeFilter)
                        .WhereIf(input.MinObjectIdFilter != null, e => e.ObjectId >= input.MinObjectIdFilter)
                        .WhereIf(input.MaxObjectIdFilter != null, e => e.ObjectId <= input.MaxObjectIdFilter)
                        .WhereIf(input.MinDynamicFieldIdFilter != null, e => e.DynamicFieldId >= input.MinDynamicFieldIdFilter)
                        .WhereIf(input.MaxDynamicFieldIdFilter != null, e => e.DynamicFieldId <= input.MaxDynamicFieldIdFilter)
                        .WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
                        .WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter)
                        .WhereIf(input.IsActiveFilter > -1, e => Convert.ToInt32(e.IsActive) == input.IsActiveFilter);

            var query = (from o in filteredDynamicDatasource
                         select new GetDynamicDatasourceForViewDto()
                         {
                             DynamicDatasource = new DynamicDatasourceDto
                             {
                                 Type = o.Type,
                                 ObjectId = o.ObjectId,
                                 DynamicFieldId = o.DynamicFieldId,
                                 Order = o.Order,
                                 IsActive = o.IsActive,
                                 Id = o.Id
                             }
                         });


            var dynamicDatasourceListDtos = await query.ToListAsync();

            return _dynamicDatasourceExcelExporter.ExportToFile(dynamicDatasourceListDtos);
        }


    }
}