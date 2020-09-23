

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
    [AbpAuthorize(AppPermissions.Pages_Promulgateds)]
    public class PromulgatedsAppService : HinnovaAppServiceBase, IPromulgatedsAppService
    {
        private readonly IRepository<Promulgated> _promulgatedRepository;
        private readonly IPromulgatedsExcelExporter _promulgatedsExcelExporter;


        public PromulgatedsAppService(IRepository<Promulgated> promulgatedRepository, IPromulgatedsExcelExporter promulgatedsExcelExporter)
        {
            _promulgatedRepository = promulgatedRepository;
            _promulgatedsExcelExporter = promulgatedsExcelExporter;

        }

        public async Task<PagedResultDto<GetPromulgatedForViewDto>> GetAll(GetAllPromulgatedsInput input)
        {

            var filteredPromulgateds = _promulgatedRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.DisplayName.Contains(input.Filter) || e.Representative.Contains(input.Filter) || e.Leader.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DisplayNameFilter), e => e.DisplayName.ToLower() == input.DisplayNameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RepresentativeFilter), e => e.Representative.ToLower() == input.RepresentativeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LeaderFilter), e => e.Leader.ToLower() == input.LeaderFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PositionFilter), e => e.Position.ToLower() == input.PositionFilter.ToLower().Trim());

            var pagedAndFilteredPromulgateds = filteredPromulgateds
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var promulgateds = from o in pagedAndFilteredPromulgateds
                               select new GetPromulgatedForViewDto()
                               {
                                   Promulgated = new PromulgatedDto
                                   {
                                       Name = o.Name,
                                       DisplayName = o.DisplayName,
                                       Representative = o.Representative,
                                       Leader = o.Leader,
                                       Position = o.Position,
                                       Id = o.Id
                                   }
                               };

            var totalCount = await filteredPromulgateds.CountAsync();

            return new PagedResultDto<GetPromulgatedForViewDto>(
                totalCount,
                await promulgateds.ToListAsync()
            );
        }

        public async Task<GetPromulgatedForViewDto> GetPromulgatedForView(int id)
        {
            var promulgated = await _promulgatedRepository.GetAsync(id);

            var output = new GetPromulgatedForViewDto { Promulgated = ObjectMapper.Map<PromulgatedDto>(promulgated) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Promulgateds_Edit)]
        public async Task<GetPromulgatedForEditOutput> GetPromulgatedForEdit(EntityDto input)
        {
            var promulgated = await _promulgatedRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPromulgatedForEditOutput { Promulgated = ObjectMapper.Map<CreateOrEditPromulgatedDto>(promulgated) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPromulgatedDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Promulgateds_Create)]
        protected virtual async Task Create(CreateOrEditPromulgatedDto input)
        {
            var promulgated = ObjectMapper.Map<Promulgated>(input);


            if (AbpSession.TenantId != null)
            {
                promulgated.TenantId = (int?)AbpSession.TenantId;
            }


            await _promulgatedRepository.InsertAsync(promulgated);
        }

        [AbpAuthorize(AppPermissions.Pages_Promulgateds_Edit)]
        protected virtual async Task Update(CreateOrEditPromulgatedDto input)
        {
            var promulgated = await _promulgatedRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, promulgated);
        }

        [AbpAuthorize(AppPermissions.Pages_Promulgateds_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _promulgatedRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetPromulgatedsToExcel(GetAllPromulgatedsForExcelInput input)
        {

            var filteredPromulgateds = _promulgatedRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.DisplayName.Contains(input.Filter) || e.Representative.Contains(input.Filter) || e.Leader.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DisplayNameFilter), e => e.DisplayName.ToLower() == input.DisplayNameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RepresentativeFilter), e => e.Representative.ToLower() == input.RepresentativeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LeaderFilter), e => e.Leader.ToLower() == input.LeaderFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PositionFilter), e => e.Position.ToLower() == input.PositionFilter.ToLower().Trim());

            var query = (from o in filteredPromulgateds
                         select new GetPromulgatedForViewDto()
                         {
                             Promulgated = new PromulgatedDto
                             {
                                 Name = o.Name,
                                 DisplayName = o.DisplayName,
                                 Representative = o.Representative,
                                 Leader = o.Leader,
                                 Position = o.Position,
                                 Id = o.Id
                             }
                         });


            var promulgatedListDtos = await query.ToListAsync();

            return _promulgatedsExcelExporter.ExportToFile(promulgatedListDtos);
        }

        public async Task<List<PromulgatedDto>> GetAllPromulgated()
        {
            var result = await _promulgatedRepository.GetAll().ToListAsync();
            return ObjectMapper.Map<List<PromulgatedDto>>(result);
        }
    }
}