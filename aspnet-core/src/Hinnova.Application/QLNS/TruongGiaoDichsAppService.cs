

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLNSExporting;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;

namespace Hinnova.QLNS
{
    [AbpAuthorize(AppPermissions.Pages_TruongGiaoDichs)]
    public class TruongGiaoDichsAppService : HinnovaAppServiceBase, ITruongGiaoDichsAppService
    {
        private readonly IRepository<TruongGiaoDich> _truongGiaoDichRepository;
        private readonly ITruongGiaoDichsExcelExporter _truongGiaoDichsExcelExporter;


        public TruongGiaoDichsAppService(IRepository<TruongGiaoDich> truongGiaoDichRepository, ITruongGiaoDichsExcelExporter truongGiaoDichsExcelExporter)
        {
            _truongGiaoDichRepository = truongGiaoDichRepository;
            _truongGiaoDichsExcelExporter = truongGiaoDichsExcelExporter;

        }

        public List<TruongGiaoDichDto> GetAllTruongGiaoDich()
        {
            return ObjectMapper.Map<List<TruongGiaoDichDto>>(_truongGiaoDichRepository.GetAll().ToList());
        }

        public async Task<PagedResultDto<GetTruongGiaoDichForViewDto>> GetAll(GetAllTruongGiaoDichsInput input)
        {

            var filteredTruongGiaoDichs = _truongGiaoDichRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Code.Contains(input.Filter) || e.CDName.Contains(input.Filter) || e.Value.Contains(input.Filter) || e.GhiChu.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter), e => e.Code.ToLower() == input.CodeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CDNameFilter), e => e.CDName.ToLower() == input.CDNameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter), e => e.Value.ToLower() == input.ValueFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GhiChuFilter), e => e.GhiChu.ToLower() == input.GhiChuFilter.ToLower().Trim());

            var pagedAndFilteredTruongGiaoDichs = filteredTruongGiaoDichs
                .OrderBy(input.Sorting ?? "CDName asc")
                .ThenBy(x => x.DisplayOrder)
                .PageBy(input);

            var truongGiaoDichs = from o in pagedAndFilteredTruongGiaoDichs
                                  select new GetTruongGiaoDichForViewDto()
                                  {
                                      TruongGiaoDich = new TruongGiaoDichDto
                                      {
                                          DisplayOrder = o.DisplayOrder,
                                          Code = o.Code,
                                          CDName = o.CDName,
                                          Value = o.Value,
                                          GhiChu = o.GhiChu,
                                          SetDefault = o.SetDefault,
                                          Id = o.Id
                                      }
                                  };

            var totalCount = await filteredTruongGiaoDichs.CountAsync();

            return new PagedResultDto<GetTruongGiaoDichForViewDto>(
                totalCount,
                await truongGiaoDichs.ToListAsync()
            );
        }

        public async Task<GetTruongGiaoDichForViewDto> GetTruongGiaoDichForView(int id)
        {
            var truongGiaoDich = await _truongGiaoDichRepository.GetAsync(id);

            var output = new GetTruongGiaoDichForViewDto { TruongGiaoDich = ObjectMapper.Map<TruongGiaoDichDto>(truongGiaoDich) };

            return output;
        }

        public async Task SetDefaultValue(TruongGiaoDichDto input)
        {
            var tgd = _truongGiaoDichRepository.GetAll().Where(x => x.Code == input.Code);
            if (tgd.Count() > 0)
            {
                foreach (var item in tgd)
                {
                    var previousTruongGiaoDich = await _truongGiaoDichRepository.FirstOrDefaultAsync((int)item.Id);
                    previousTruongGiaoDich.SetDefault = false;
                    _truongGiaoDichRepository.Update(previousTruongGiaoDich);
                }
            }

            var truongGiaoDich = await _truongGiaoDichRepository.FirstOrDefaultAsync((int)input.Id);
            truongGiaoDich.SetDefault = true;
            ObjectMapper.Map(input, truongGiaoDich);
        }

        [AbpAuthorize(AppPermissions.Pages_TruongGiaoDichs_Edit)]
        public async Task<GetTruongGiaoDichForEditOutput> GetTruongGiaoDichForEdit(EntityDto input)
        {
            var truongGiaoDich = await _truongGiaoDichRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTruongGiaoDichForEditOutput { TruongGiaoDich = ObjectMapper.Map<CreateOrEditTruongGiaoDichDto>(truongGiaoDich) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditTruongGiaoDichDto input)
        {
            if (input.Id == null)
            {
                var tgd = _truongGiaoDichRepository.GetAll().Where(x => x.Code == input.Code);
                if (tgd.Count() > 0 && tgd.Any(x => x.CDName == input.CDName))
                {
                    throw new UserFriendlyException("Cd name đã bị trùng");
                    return;
                }


                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_TruongGiaoDichs_Create)]
        protected virtual async Task Create(CreateOrEditTruongGiaoDichDto input)
        {
            var truongGiaoDich = ObjectMapper.Map<TruongGiaoDich>(input);



            await _truongGiaoDichRepository.InsertAsync(truongGiaoDich);
        }

        [AbpAuthorize(AppPermissions.Pages_TruongGiaoDichs_Edit)]
        protected virtual async Task Update(CreateOrEditTruongGiaoDichDto input)
        {
            var tgd = _truongGiaoDichRepository.GetAll().Where(x => x.Code == input.Code);

            var truongGiaoDich = await _truongGiaoDichRepository.FirstOrDefaultAsync((int)input.Id);
            if (tgd.Count() > 0 && tgd.Any(x => x.CDName == input.CDName) && input.Id != truongGiaoDich.Id)
                throw new UserFriendlyException("Cd name đã bị trùng");
            ObjectMapper.Map(input, truongGiaoDich);
        }

        [AbpAuthorize(AppPermissions.Pages_TruongGiaoDichs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _truongGiaoDichRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetTruongGiaoDichsToExcel(GetAllTruongGiaoDichsForExcelInput input)
        {

            var filteredTruongGiaoDichs = _truongGiaoDichRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Code.Contains(input.Filter) || e.CDName.Contains(input.Filter) || e.Value.Contains(input.Filter) || e.GhiChu.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter), e => e.Code.ToLower() == input.CodeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CDNameFilter), e => e.CDName.ToLower() == input.CDNameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter), e => e.Value.ToLower() == input.ValueFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GhiChuFilter), e => e.GhiChu.ToLower() == input.GhiChuFilter.ToLower().Trim());

            var query = (from o in filteredTruongGiaoDichs
                         select new GetTruongGiaoDichForViewDto()
                         {
                             TruongGiaoDich = new TruongGiaoDichDto
                             {
                                 Code = o.Code,
                                 CDName = o.CDName,
                                 Value = o.Value,
                                 GhiChu = o.GhiChu,
                                 Id = o.Id
                             }
                         });


            var truongGiaoDichListDtos = await query.ToListAsync();

            return _truongGiaoDichsExcelExporter.ExportToFile(truongGiaoDichListDtos);
        }


    }
}