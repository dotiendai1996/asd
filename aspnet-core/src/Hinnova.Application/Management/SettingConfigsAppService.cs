

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.Management.Exporting;
using Hinnova.Management.Dtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Hinnova.Management
{
    [AbpAuthorize(AppPermissions.Pages_Administration_SettingConfigs)]
    public class SettingConfigsAppService : HinnovaAppServiceBase, ISettingConfigsAppService
    {
        private readonly IRepository<SettingConfig> _settingConfigRepository;
        private readonly ISettingConfigsExcelExporter _settingConfigsExcelExporter;


        public SettingConfigsAppService(IRepository<SettingConfig> settingConfigRepository, ISettingConfigsExcelExporter settingConfigsExcelExporter)
        {
            _settingConfigRepository = settingConfigRepository;
            _settingConfigsExcelExporter = settingConfigsExcelExporter;

        }

        public async Task<PagedResultDto<GetSettingConfigForViewDto>> GetAll(GetAllSettingConfigsInput input)
        {

            var filteredSettingConfigs = _settingConfigRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Code.Contains(input.Filter) || e.ValueString.Contains(input.Filter) || e.ValueHtml.Contains(input.Filter) || e.Image.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter), e => e.Code.ToLower() == input.CodeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueStringFilter), e => e.ValueString.ToLower() == input.ValueStringFilter.ToLower().Trim())
                        .WhereIf(input.MinValueIntFilter != null, e => e.ValueInt >= input.MinValueIntFilter)
                        .WhereIf(input.MaxValueIntFilter != null, e => e.ValueInt <= input.MaxValueIntFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueHtmlFilter), e => e.ValueHtml.ToLower() == input.ValueHtmlFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ImageFilter), e => e.Image.ToLower() == input.ImageFilter.ToLower().Trim());

            var pagedAndFilteredSettingConfigs = filteredSettingConfigs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var settingConfigs = from o in pagedAndFilteredSettingConfigs
                                 select new GetSettingConfigForViewDto()
                                 {
                                     SettingConfig = new SettingConfigDto
                                     {
                                         Code = o.Code,
                                         ValueString = o.ValueString,
                                         ValueInt = o.ValueInt,
                                         ValueHtml = o.ValueHtml,
                                         Image = o.Image,
                                         Id = o.Id
                                     }
                                 };

            var totalCount = await filteredSettingConfigs.CountAsync();

            return new PagedResultDto<GetSettingConfigForViewDto>(
                totalCount,
                await settingConfigs.ToListAsync()
            );
        }

        public async Task<GetSettingConfigForViewDto> GetSettingConfigForView(int id)
        {
            var settingConfig = await _settingConfigRepository.GetAsync(id);

            var output = new GetSettingConfigForViewDto { SettingConfig = ObjectMapper.Map<SettingConfigDto>(settingConfig) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_SettingConfigs_Edit)]
        public async Task<GetSettingConfigForEditOutput> GetSettingConfigForEdit(EntityDto input)
        {
            var settingConfig = await _settingConfigRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetSettingConfigForEditOutput { SettingConfig = ObjectMapper.Map<CreateOrEditSettingConfigDto>(settingConfig) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditSettingConfigDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Administration_SettingConfigs_Create)]
        protected virtual async Task Create(CreateOrEditSettingConfigDto input)
        {
            var settingConfig = ObjectMapper.Map<SettingConfig>(input);



            await _settingConfigRepository.InsertAsync(settingConfig);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_SettingConfigs_Edit)]
        protected virtual async Task Update(CreateOrEditSettingConfigDto input)
        {
            var settingConfig = await _settingConfigRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, settingConfig);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_SettingConfigs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _settingConfigRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetSettingConfigsToExcel(GetAllSettingConfigsForExcelInput input)
        {

            var filteredSettingConfigs = _settingConfigRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Code.Contains(input.Filter) || e.ValueString.Contains(input.Filter) || e.ValueHtml.Contains(input.Filter) || e.Image.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter), e => e.Code.ToLower() == input.CodeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueStringFilter), e => e.ValueString.ToLower() == input.ValueStringFilter.ToLower().Trim())
                        .WhereIf(input.MinValueIntFilter != null, e => e.ValueInt >= input.MinValueIntFilter)
                        .WhereIf(input.MaxValueIntFilter != null, e => e.ValueInt <= input.MaxValueIntFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueHtmlFilter), e => e.ValueHtml.ToLower() == input.ValueHtmlFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ImageFilter), e => e.Image.ToLower() == input.ImageFilter.ToLower().Trim());

            var query = (from o in filteredSettingConfigs
                         select new GetSettingConfigForViewDto()
                         {
                             SettingConfig = new SettingConfigDto
                             {
                                 Code = o.Code,
                                 ValueString = o.ValueString,
                                 ValueInt = o.ValueInt,
                                 ValueHtml = o.ValueHtml,
                                 Image = o.Image,
                                 Id = o.Id
                             }
                         });


            var settingConfigListDtos = await query.ToListAsync();

            return _settingConfigsExcelExporter.ExportToFile(settingConfigListDtos);
        }


    }
}