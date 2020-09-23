

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLNS.Exporting;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Organizations;
using Microsoft.EntityFrameworkCore;

namespace Hinnova.QLNS
{
	[AbpAuthorize(AppPermissions.Pages_QuyTrinhCongTacs)]
    public class QuyTrinhCongTacsAppService : HinnovaAppServiceBase, IQuyTrinhCongTacsAppService
    {
		 private readonly IRepository<QuyTrinhCongTac> _quyTrinhCongTacRepository;
		 private readonly IQuyTrinhCongTacsExcelExporter _quyTrinhCongTacsExcelExporter;
         private readonly IRepository<TruongGiaoDich> _truongGiaoDichRepository;
        IRepository<OrganizationUnit, long> _organizationUnitRepository;


        public QuyTrinhCongTacsAppService(IRepository<QuyTrinhCongTac> quyTrinhCongTacRepository, IRepository<TruongGiaoDich> truongGiaoDichRepository, IRepository<OrganizationUnit, long> organizationUnitRepository, IQuyTrinhCongTacsExcelExporter quyTrinhCongTacsExcelExporter ) 
		  {
			_quyTrinhCongTacRepository = quyTrinhCongTacRepository;
			_quyTrinhCongTacsExcelExporter = quyTrinhCongTacsExcelExporter;
            _organizationUnitRepository = organizationUnitRepository;
            _truongGiaoDichRepository = truongGiaoDichRepository;
        }

		 public async Task<PagedResultDto<GetQuyTrinhCongTacForViewDto>> GetAll( int id)
         {

             var filteredQuyTrinhCongTacs = _quyTrinhCongTacRepository.GetAll()
                 .Where(x => x.MaHoSo == id);
						//.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.TenCty.Contains(input.Filter) || e.ViTriCongViecCode.Contains(input.Filter) || e.QuanLyTrucTiep.Contains(input.Filter) || e.TrangThaiCode.Contains(input.Filter) || e.GhiChu.Contains(input.Filter))
						//.WhereIf(!string.IsNullOrWhiteSpace(input.TenCtyFilter),  e => e.TenCty == input.TenCtyFilter)
						//.WhereIf(input.MinDateToFilter != null, e => e.DateTo >= input.MinDateToFilter)
						//.WhereIf(input.MaxDateToFilter != null, e => e.DateTo <= input.MaxDateToFilter)
						//.WhereIf(input.MinDateFromFilter != null, e => e.DateFrom >= input.MinDateFromFilter)
						//.WhereIf(input.MaxDateFromFilter != null, e => e.DateFrom <= input.MaxDateFromFilter)
						//.WhereIf(!string.IsNullOrWhiteSpace(input.ViTriCongViecCodeFilter),  e => e.ViTriCongViecCode == input.ViTriCongViecCodeFilter)
						//.WhereIf(input.MinDonViCongTacIDFilter != null, e => e.DonViCongTacID >= input.MinDonViCongTacIDFilter)
						//.WhereIf(input.MaxDonViCongTacIDFilter != null, e => e.DonViCongTacID <= input.MaxDonViCongTacIDFilter)
						//.WhereIf(!string.IsNullOrWhiteSpace(input.QuanLyTrucTiepFilter),  e => e.QuanLyTrucTiep == input.QuanLyTrucTiepFilter)
						//.WhereIf(!string.IsNullOrWhiteSpace(input.TrangThaiCodeFilter),  e => e.TrangThaiCode == input.TrangThaiCodeFilter)
						//.WhereIf(!string.IsNullOrWhiteSpace(input.GhiChuFilter),  e => e.GhiChu == input.GhiChuFilter);

                        var pagedAndFilteredQuyTrinhCongTacs = filteredQuyTrinhCongTacs;
                //.OrderBy(input.Sorting ?? "id asc")
                //.PageBy(input);

            var units = _organizationUnitRepository.GetAll();
            var tgd = _truongGiaoDichRepository.GetAll();
            var quyTrinhCongTacs = from o in pagedAndFilteredQuyTrinhCongTacs

                join unit in units on o.DonViCongTacID.Value equals unit.Id into unitjoin
                from joinedtunit in unitjoin.DefaultIfEmpty()

                join unit in units on o.ViTriCongViecCode equals unit.Code into unitjoin1
                from joinedtunit1 in unitjoin1.DefaultIfEmpty()

                join htlv in tgd.Where(x => x.Code == "TTNS") on o.TrangThaiCode equals htlv.CDName into htlvJoin
                from joinedhtlv in htlvJoin.DefaultIfEmpty()

                                   select new GetQuyTrinhCongTacForViewDto() {
							QuyTrinhCongTac = new QuyTrinhCongTacDto
							{
                                TenCty = o.TenCty,
                                DateTo = o.DateTo,
                                DateFrom = o.DateFrom,
                                ViTriCongViecCode = o.ViTriCongViecCode,
                                DonViCongTacID = o.DonViCongTacID,
                                QuanLyTrucTiep = o.QuanLyTrucTiep,
                                TrangThaiCode = o.TrangThaiCode,
                                GhiChu = o.GhiChu,
                                MaHoSo = o.MaHoSo,
                                Id = o.Id
							},

                            ViTriCongViecValue = joinedtunit1 == null ? "" : joinedtunit1.DisplayName.ToString(),
                            DonViCongTacValue = joinedtunit == null ? "" : joinedtunit.DisplayName.ToString(),
                            HinhThucLamViecValue = joinedhtlv == null ? "" : joinedhtlv.Value.ToString(),
                                   };
       

            var totalCount = await filteredQuyTrinhCongTacs.CountAsync();

            return new PagedResultDto<GetQuyTrinhCongTacForViewDto>(
                totalCount,
                await quyTrinhCongTacs.ToListAsync()
            );
         }


         public async Task<List<QuyTrinhCongTacDto>> GetListQuaTrinhCongTac( int id)
         {

             var listQTCT = await _quyTrinhCongTacRepository.GetAll().Where(x =>  x.MaHoSo == id).ToListAsync();

             var listQTCTDto = ObjectMapper.Map<List<QuyTrinhCongTacDto>>(listQTCT);
             return listQTCTDto;

         }

        //public List<QuyTrinhCongTacDto> GetAllQuyTrinhCongTacList(int id)
        //{
		
        //    return _quyTrinhCongTacRepository.GetAll().Where(t => t.IsDeleted == false).Select(t => new QuyTrinhCongTacDto { Id=t.Id , MaHoSo = t.MaHoSo  }).ToList();
        //}

		[AbpAuthorize(AppPermissions.Pages_QuyTrinhCongTacs_Edit)]
		 public async Task<GetQuyTrinhCongTacForEditOutput> GetQuyTrinhCongTacForEdit(EntityDto input)
         {
            var quyTrinhCongTac = await _quyTrinhCongTacRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetQuyTrinhCongTacForEditOutput {QuyTrinhCongTac = ObjectMapper.Map<CreateOrEditQuyTrinhCongTacDto>(quyTrinhCongTac)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditQuyTrinhCongTacDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_QuyTrinhCongTacs_Create)]
		 protected virtual async Task Create(CreateOrEditQuyTrinhCongTacDto input)
         {
            var quyTrinhCongTac = ObjectMapper.Map<QuyTrinhCongTac>(input);

			

            await _quyTrinhCongTacRepository.InsertAsync(quyTrinhCongTac);
         }

		 [AbpAuthorize(AppPermissions.Pages_QuyTrinhCongTacs_Edit)]
		 protected virtual async Task Update(CreateOrEditQuyTrinhCongTacDto input)
         {
            var quyTrinhCongTac = await _quyTrinhCongTacRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, quyTrinhCongTac);
         }

		 [AbpAuthorize(AppPermissions.Pages_QuyTrinhCongTacs_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _quyTrinhCongTacRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetQuyTrinhCongTacsToExcel(GetAllQuyTrinhCongTacsForExcelInput input)
         {
			
			var filteredQuyTrinhCongTacs = _quyTrinhCongTacRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.TenCty.Contains(input.Filter) || e.ViTriCongViecCode.Contains(input.Filter) || e.QuanLyTrucTiep.Contains(input.Filter) || e.TrangThaiCode.Contains(input.Filter) || e.GhiChu.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.TenCtyFilter),  e => e.TenCty == input.TenCtyFilter)
						.WhereIf(input.MinDateToFilter != null, e => e.DateTo >= input.MinDateToFilter)
						.WhereIf(input.MaxDateToFilter != null, e => e.DateTo <= input.MaxDateToFilter)
						.WhereIf(input.MinDateFromFilter != null, e => e.DateFrom >= input.MinDateFromFilter)
						.WhereIf(input.MaxDateFromFilter != null, e => e.DateFrom <= input.MaxDateFromFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ViTriCongViecCodeFilter),  e => e.ViTriCongViecCode == input.ViTriCongViecCodeFilter)
						.WhereIf(input.MinDonViCongTacIDFilter != null, e => e.DonViCongTacID >= input.MinDonViCongTacIDFilter)
						.WhereIf(input.MaxDonViCongTacIDFilter != null, e => e.DonViCongTacID <= input.MaxDonViCongTacIDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuanLyTrucTiepFilter),  e => e.QuanLyTrucTiep == input.QuanLyTrucTiepFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TrangThaiCodeFilter),  e => e.TrangThaiCode == input.TrangThaiCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.GhiChuFilter),  e => e.GhiChu == input.GhiChuFilter);

			var query = (from o in filteredQuyTrinhCongTacs
                         select new GetQuyTrinhCongTacForViewDto() { 
							QuyTrinhCongTac = new QuyTrinhCongTacDto
							{
                                TenCty = o.TenCty,
                                DateTo = o.DateTo,
                                DateFrom = o.DateFrom,
                                ViTriCongViecCode = o.ViTriCongViecCode,
                                DonViCongTacID = o.DonViCongTacID,
                                QuanLyTrucTiep = o.QuanLyTrucTiep,
                                TrangThaiCode = o.TrangThaiCode,
                                GhiChu = o.GhiChu,
                                Id = o.Id
							}
						 });


            var quyTrinhCongTacListDtos = await query.ToListAsync();

            return _quyTrinhCongTacsExcelExporter.ExportToFile(quyTrinhCongTacListDtos);
         }


    }
}