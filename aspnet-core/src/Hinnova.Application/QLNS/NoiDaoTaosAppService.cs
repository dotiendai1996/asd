

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

namespace Hinnova.QLNS
{
    [AbpAuthorize(AppPermissions.Pages_NoiDaoTaos)]
    public class NoiDaoTaosAppService : HinnovaAppServiceBase, INoiDaoTaosAppService
    {
        private readonly IRepository<NoiDaoTao> _noiDaoTaoRepository;
        private readonly INoiDaoTaosExcelExporter _noiDaoTaosExcelExporter;


        public NoiDaoTaosAppService(IRepository<NoiDaoTao> noiDaoTaoRepository, INoiDaoTaosExcelExporter noiDaoTaosExcelExporter)
        {
            _noiDaoTaoRepository = noiDaoTaoRepository;
            _noiDaoTaosExcelExporter = noiDaoTaosExcelExporter;

        }

        public bool CheckNoiDaoTaoName(string ten)
        {
            if (ten.IsNullOrEmpty())
                return false;

            var x = _noiDaoTaoRepository.GetAll().Where(t => t.TenNoiDaoTao == ten).Count();
            if (x > 0)
                return true;
            return false;
        }

        public bool CheckNoiDaoTaoCode(string code)
        {
            if (code.IsNullOrEmpty())
                return false;

            var x = _noiDaoTaoRepository.GetAll().Where(t => t.MaNoiDaoTao == code.Trim()).Count();
            if (x > 0)
                return true;
            return false;
        }

        public List<NoiDaoTaoDto> GetAllNoiDaoTao()
        {
            return ObjectMapper.Map<List<NoiDaoTaoDto>>(_noiDaoTaoRepository.GetAll().ToList());
        }

        public async Task<PagedResultDto<GetNoiDaoTaoForViewDto>> GetAll(GetAllNoiDaoTaosInput input)
        {

            var filteredNoiDaoTaos = _noiDaoTaoRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.TenNoiDaoTao.Contains(input.Filter) || e.MaNoiDaoTao.Contains(input.Filter) || e.DiaChi.Contains(input.Filter) || e.KhuVuc.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenNoiDaoTaoFilter), e => e.TenNoiDaoTao.ToLower() == input.TenNoiDaoTaoFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaNoiDaoTaoFilter), e => e.MaNoiDaoTao.ToLower() == input.MaNoiDaoTaoFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiFilter), e => e.DiaChi.ToLower() == input.DiaChiFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KhuVucFilter), e => e.KhuVuc.ToLower() == input.KhuVucFilter.ToLower().Trim());

            var pagedAndFilteredNoiDaoTaos = filteredNoiDaoTaos
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var noiDaoTaos = from o in pagedAndFilteredNoiDaoTaos
                             select new GetNoiDaoTaoForViewDto()
                             {
                                 NoiDaoTao = new NoiDaoTaoDto
                                 {
                                     TenNoiDaoTao = o.TenNoiDaoTao,
                                     MaNoiDaoTao = o.MaNoiDaoTao,
                                     DiaChi = o.DiaChi,
                                     KhuVuc = o.KhuVuc,
                                     Id = o.Id
                                 }
                             };

            var totalCount = await filteredNoiDaoTaos.CountAsync();

            return new PagedResultDto<GetNoiDaoTaoForViewDto>(
                totalCount,
                await noiDaoTaos.ToListAsync()
            );
        }

        public async Task<GetNoiDaoTaoForViewDto> GetNoiDaoTaoForView(int id)
        {
            var noiDaoTao = await _noiDaoTaoRepository.GetAsync(id);

            var output = new GetNoiDaoTaoForViewDto { NoiDaoTao = ObjectMapper.Map<NoiDaoTaoDto>(noiDaoTao) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_NoiDaoTaos_Edit)]
        public async Task<GetNoiDaoTaoForEditOutput> GetNoiDaoTaoForEdit(EntityDto input)
        {
            var noiDaoTao = await _noiDaoTaoRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetNoiDaoTaoForEditOutput { NoiDaoTao = ObjectMapper.Map<CreateOrEditNoiDaoTaoDto>(noiDaoTao) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditNoiDaoTaoDto input)
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

        [AbpAuthorize(AppPermissions.Pages_NoiDaoTaos_Create)]
        protected virtual async Task Create(CreateOrEditNoiDaoTaoDto input)
        {
            var noiDaoTao = ObjectMapper.Map<NoiDaoTao>(input);



            await _noiDaoTaoRepository.InsertAsync(noiDaoTao);
        }

        [AbpAuthorize(AppPermissions.Pages_NoiDaoTaos_Edit)]
        protected virtual async Task Update(CreateOrEditNoiDaoTaoDto input)
        {
            var noiDaoTao = await _noiDaoTaoRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, noiDaoTao);
        }

        [AbpAuthorize(AppPermissions.Pages_NoiDaoTaos_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _noiDaoTaoRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetNoiDaoTaosToExcel(GetAllNoiDaoTaosForExcelInput input)
        {

            var filteredNoiDaoTaos = _noiDaoTaoRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.TenNoiDaoTao.Contains(input.Filter) || e.MaNoiDaoTao.Contains(input.Filter) || e.DiaChi.Contains(input.Filter) || e.KhuVuc.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenNoiDaoTaoFilter), e => e.TenNoiDaoTao.ToLower() == input.TenNoiDaoTaoFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MaNoiDaoTaoFilter), e => e.MaNoiDaoTao.ToLower() == input.MaNoiDaoTaoFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiFilter), e => e.DiaChi.ToLower() == input.DiaChiFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KhuVucFilter), e => e.KhuVuc.ToLower() == input.KhuVucFilter.ToLower().Trim());

            var query = (from o in filteredNoiDaoTaos
                         select new GetNoiDaoTaoForViewDto()
                         {
                             NoiDaoTao = new NoiDaoTaoDto
                             {
                                 TenNoiDaoTao = o.TenNoiDaoTao,
                                 MaNoiDaoTao = o.MaNoiDaoTao,
                                 DiaChi = o.DiaChi,
                                 KhuVuc = o.KhuVuc,
                                 Id = o.Id
                             }
                         });


            var noiDaoTaoListDtos = await query.ToListAsync();

            return _noiDaoTaosExcelExporter.ExportToFile(noiDaoTaoListDtos);
        }


    }
}