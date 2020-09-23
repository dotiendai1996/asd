

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Dapper;
using Hinnova.Configuration;
using Microsoft.Extensions.Configuration;

namespace Hinnova.QLNS
{
    //[AbpAuthorize(AppPermissions.Pages_QuanLyTrucTiepPNPs)]
    public class QuanLyTrucTiepPNPsAppService : HinnovaAppServiceBase, IQuanLyTrucTiepPNPsAppService
    {

        private readonly IRepository<QuanLyTrucTiepPNP> _quanLyTrucTiepPNPRepository;
        private readonly string connectionString;
        private readonly IWebHostEnvironment _env;

        public QuanLyTrucTiepPNPsAppService(IRepository<QuanLyTrucTiepPNP> quanLyTrucTiepPNPRepository, IWebHostEnvironment env, IWebHostEnvironment hostingEnvironment)
        {
            _quanLyTrucTiepPNPRepository = quanLyTrucTiepPNPRepository;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");

        }

        public async Task<List<QuanLyTrucTiepPNPDto>> GetListQuanLyTrucTiepDto(int id)
        {

            var QuanLyTrucTiepPNP = await _quanLyTrucTiepPNPRepository.GetAll().Where(x => x.PhieuNghiPhepID == id).ToListAsync();

            var quanLyTrucTiepPNP = ObjectMapper.Map<List<QuanLyTrucTiepPNPDto>>(QuanLyTrucTiepPNP);
            return quanLyTrucTiepPNP;

        }

        public async Task<PagedResultDto<GetQuanLyTrucTiepPNPForViewDto>> GetAll(GetAllQuanLyTrucTiepPNPsInput input)
         {
			
			var filteredQuanLyTrucTiepPNPs = _quanLyTrucTiepPNPRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.QLTT.Contains(input.Filter) || e.GhiChu.Contains(input.Filter) || e.MaHoSo.Contains(input.Filter))
						.WhereIf(input.MinPhieuNghiPhepIDFilter != null, e => e.PhieuNghiPhepID >= input.MinPhieuNghiPhepIDFilter)
						.WhereIf(input.MaxPhieuNghiPhepIDFilter != null, e => e.PhieuNghiPhepID <= input.MaxPhieuNghiPhepIDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.QLTTFilter),  e => e.QLTT == input.QLTTFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.GhiChuFilter),  e => e.GhiChu == input.GhiChuFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaHoSoFilter),  e => e.MaHoSo == input.MaHoSoFilter);

			var pagedAndFilteredQuanLyTrucTiepPNPs = filteredQuanLyTrucTiepPNPs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var quanLyTrucTiepPNPs = from o in pagedAndFilteredQuanLyTrucTiepPNPs
                         select new GetQuanLyTrucTiepPNPForViewDto() {
							QuanLyTrucTiepPNP = new QuanLyTrucTiepPNPDto
							{
                                PhieuNghiPhepID = o.PhieuNghiPhepID,
                                QLTT = o.QLTT,
                                GhiChu = o.GhiChu,
                                MaHoSo = o.MaHoSo,
                                Id = o.Id
							}
						};

            var totalCount = await filteredQuanLyTrucTiepPNPs.CountAsync();

            return new PagedResultDto<GetQuanLyTrucTiepPNPForViewDto>(
                totalCount,
                await quanLyTrucTiepPNPs.ToListAsync()
            );
         }

        public async Task<List<QuanLyTrucTiepPNPDto>> XoaQuanLyTrucTiepId(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                string sql = $" delete from  QuanLyTrucTiepPNPs  WHERE  PhieuNghiPhepID  = {id} ";

                var result = await conn.QueryAsync<QuanLyTrucTiepPNPDto>(sql: sql);
                //     var result = await conn.QueryAsync<QuanLyTrucTiepPNPDto>(sql: "delete from  QuanLyTrucTiepPNPs  WHERE  phieuNghiPhepID = "+id);
                return result.ToList();
            }
        }
		 public async Task<GetQuanLyTrucTiepPNPForEditOutput> GetQuanLyTrucTiepPNPForEdit(EntityDto input)
         {
            var quanLyTrucTiepPNP = await _quanLyTrucTiepPNPRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetQuanLyTrucTiepPNPForEditOutput {QuanLyTrucTiepPNP = ObjectMapper.Map<CreateOrEditQuanLyTrucTiepPNPDto>(quanLyTrucTiepPNP)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditQuanLyTrucTiepPNPDto input)
         {

            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 //[AbpAuthorize(AppPermissions.Pages_QuanLyTrucTiepPNPs_Create)]
		 protected virtual async Task Create(CreateOrEditQuanLyTrucTiepPNPDto input)
         {
            var quanLyTrucTiepPNP = ObjectMapper.Map<QuanLyTrucTiepPNP>(input);

			

            await _quanLyTrucTiepPNPRepository.InsertAsync(quanLyTrucTiepPNP);
         }

		 //[AbpAuthorize(AppPermissions.Pages_QuanLyTrucTiepPNPs_Edit)]
		 protected virtual async Task Update(CreateOrEditQuanLyTrucTiepPNPDto input)
         {
            var quanLyTrucTiepPNP = await _quanLyTrucTiepPNPRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, quanLyTrucTiepPNP);
         }

		 //[AbpAuthorize(AppPermissions.Pages_QuanLyTrucTiepPNPs_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _quanLyTrucTiepPNPRepository.DeleteAsync(input.Id);
         } 
    }
}