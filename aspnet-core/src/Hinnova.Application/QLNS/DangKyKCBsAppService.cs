

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
	[AbpAuthorize(AppPermissions.Pages_DangKyKCBs)]
    public class DangKyKCBsAppService : HinnovaAppServiceBase, IDangKyKCBsAppService
    {
        private readonly IRepository<DangKyKCB> _dangKyKCBRepository;
		private readonly IDangKyKCBsExcelExporter _dangKyKCBsExcelExporter;
		 

		  public DangKyKCBsAppService(IRepository<DangKyKCB> dangKyKCBRepository, IDangKyKCBsExcelExporter dangKyKCBsExcelExporter ) 
		  {
			_dangKyKCBRepository = dangKyKCBRepository;
			_dangKyKCBsExcelExporter = dangKyKCBsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetDangKyKCBForViewDto>> GetAll(GetAllDangKyKCBsInput input)
         {
			
			var filteredDangKyKCBs = _dangKyKCBRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.TenNoiKCB.Contains(input.Filter) || e.MaNoiKCB.Contains(input.Filter) || e.DiaChi.Contains(input.Filter) || e.GhiChu.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.TenNoiKCBFilter),  e => e.TenNoiKCB.ToLower() == input.TenNoiKCBFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaNoiKCBFilter),  e => e.MaNoiKCB.ToLower() == input.MaNoiKCBFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiFilter),  e => e.DiaChi.ToLower() == input.DiaChiFilter.ToLower().Trim())
						.WhereIf(input.MinTinhThanhIDFilter != null, e => e.TinhThanhID >= input.MinTinhThanhIDFilter)
						.WhereIf(input.MaxTinhThanhIDFilter != null, e => e.TinhThanhID <= input.MaxTinhThanhIDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.GhiChuFilter),  e => e.GhiChu.ToLower() == input.GhiChuFilter.ToLower().Trim());

			var pagedAndFilteredDangKyKCBs = filteredDangKyKCBs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var dangKyKCBs = from o in pagedAndFilteredDangKyKCBs
                         select new GetDangKyKCBForViewDto() {
							DangKyKCB = new DangKyKCBDto
							{
                                TenNoiKCB = o.TenNoiKCB,
                                MaNoiKCB = o.MaNoiKCB,
                                DiaChi = o.DiaChi,
                                TinhThanhID = o.TinhThanhID,
                                GhiChu = o.GhiChu,
                                Id = o.Id
							}
						};

            var totalCount = await filteredDangKyKCBs.CountAsync();

            return new PagedResultDto<GetDangKyKCBForViewDto>(
                totalCount,
                await dangKyKCBs.ToListAsync()
            );
         }
		public List<DangKyKCBDto> GetAllNoiDangKy()
		{
			return ObjectMapper.Map<List<DangKyKCBDto>>(_dangKyKCBRepository.GetAll().ToList());
		}

		public async Task<GetDangKyKCBForViewDto> GetDangKyKCBForView(int id)
         {
            var dangKyKCB = await _dangKyKCBRepository.GetAsync(id);

            var output = new GetDangKyKCBForViewDto { DangKyKCB = ObjectMapper.Map<DangKyKCBDto>(dangKyKCB) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_DangKyKCBs_Edit)]
		 public async Task<GetDangKyKCBForEditOutput> GetDangKyKCBForEdit(EntityDto input)
         {
            var dangKyKCB = await _dangKyKCBRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetDangKyKCBForEditOutput {DangKyKCB = ObjectMapper.Map<CreateOrEditDangKyKCBDto>(dangKyKCB)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditDangKyKCBDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_DangKyKCBs_Create)]
		 protected virtual async Task Create(CreateOrEditDangKyKCBDto input)
         {
            var dangKyKCB = ObjectMapper.Map<DangKyKCB>(input);

			

            await _dangKyKCBRepository.InsertAsync(dangKyKCB);
         }

		 [AbpAuthorize(AppPermissions.Pages_DangKyKCBs_Edit)]
		 protected virtual async Task Update(CreateOrEditDangKyKCBDto input)
         {
            var dangKyKCB = await _dangKyKCBRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, dangKyKCB);
         }

		 [AbpAuthorize(AppPermissions.Pages_DangKyKCBs_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _dangKyKCBRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetDangKyKCBsToExcel(GetAllDangKyKCBsForExcelInput input)
         {
			
			var filteredDangKyKCBs = _dangKyKCBRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.TenNoiKCB.Contains(input.Filter) || e.MaNoiKCB.Contains(input.Filter) || e.DiaChi.Contains(input.Filter) || e.GhiChu.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.TenNoiKCBFilter),  e => e.TenNoiKCB.ToLower() == input.TenNoiKCBFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaNoiKCBFilter),  e => e.MaNoiKCB.ToLower() == input.MaNoiKCBFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiFilter),  e => e.DiaChi.ToLower() == input.DiaChiFilter.ToLower().Trim())
						.WhereIf(input.MinTinhThanhIDFilter != null, e => e.TinhThanhID >= input.MinTinhThanhIDFilter)
						.WhereIf(input.MaxTinhThanhIDFilter != null, e => e.TinhThanhID <= input.MaxTinhThanhIDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.GhiChuFilter),  e => e.GhiChu.ToLower() == input.GhiChuFilter.ToLower().Trim());

			var query = (from o in filteredDangKyKCBs
                         select new GetDangKyKCBForViewDto() { 
							DangKyKCB = new DangKyKCBDto
							{
								TenNoiKCB = o.TenNoiKCB,
								MaNoiKCB = o.MaNoiKCB,
								DiaChi = o.DiaChi,
								TinhThanhID = o.TinhThanhID,
								GhiChu = o.GhiChu,
								Id = o.Id
							}
						 });


            var dangKyKCBListDtos = await query.ToListAsync();

            return _dangKyKCBsExcelExporter.ExportToFile(dangKyKCBListDtos);
         }


    }
}