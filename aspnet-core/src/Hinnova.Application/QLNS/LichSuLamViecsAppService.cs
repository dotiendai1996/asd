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
using Microsoft.AspNetCore.Hosting;
using Hinnova.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Abp.UI;

namespace Hinnova.QLNS
{
    [AbpAuthorize(AppPermissions.Pages_LichSuLamViecs)]
    public class LichSuLamViecsAppService : HinnovaAppServiceBase, ILichSuLamViecsAppService
    {
        private readonly IRepository<LichSuLamViec> _lichSuLamViecRepository;
        private readonly ILichSuLamViecsExcelExporter _lichSuLamViecsExcelExporter;
        private readonly string connectionString;

        public LichSuLamViecsAppService(IWebHostEnvironment env, IRepository<LichSuLamViec> lichSuLamViecRepository, ILichSuLamViecsExcelExporter lichSuLamViecsExcelExporter)
        {
            _lichSuLamViecRepository = lichSuLamViecRepository;
            _lichSuLamViecsExcelExporter = lichSuLamViecsExcelExporter;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");
        }

        public int CreateAndGetIdComment(LichSuLamViecDto input)
        {
            var lichSuLamViec = ObjectMapper.Map<LichSuLamViec>(input);

            return _lichSuLamViecRepository.InsertAndGetId(lichSuLamViec);
        }

        public async Task CreateOrEditListComment(List<LichSuLamViecDto> input)
        {
            foreach(var lslv in input)
            {
                var lslvDto = new CreateOrEditLichSuLamViecDto();
                lslvDto.NoiDung = lslv.NoiDung;
                lslvDto.UngVienId = lslv.UngVienId;
                await Create(lslvDto);
            }       
        }

        public async Task<List<LichSuLamViecDto>> GetLichSuLamViecByUngVienAsync(int ungvienId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                        await conn.OpenAsync();
                    var lslv = await conn.QueryAsync<LichSuLamViecDto>(sql: "GetLichSuLamViecByUngVienId", param: new { ungvienId }, commandType: CommandType.StoredProcedure);
                    //return ObjectMapper.Map<List<LichSuLamViecDto>>(lslv);
                    return lslv.ToList();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex);
                throw new UserFriendlyException("No data to show");
            }
            }

        public async Task<List<LichSuLamViecDto>> GetLichSuLamViecByPhieuNghiPhepAsync(int phieuNghiID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                        await conn.OpenAsync();
                    var lslv = await conn.QueryAsync<LichSuLamViecDto>(sql: "GetLichSuLamViecByPhieuNghiPhepId", param: new { phieuNghiID }, commandType: CommandType.StoredProcedure);
                    //return ObjectMapper.Map<List<LichSuLamViecDto>>(lslv);
                    return lslv.ToList();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex);
                throw new UserFriendlyException("No data to show");
            }

           }


        public async Task<PagedResultDto<GetLichSuLamViecForViewDto>> GetAll(GetAllLichSuLamViecsInput input)
        {

            var filteredLichSuLamViecs = _lichSuLamViecRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.NoiDung.Contains(input.Filter) || e.TepDinhKem.Contains(input.Filter))
                        .WhereIf(input.MinUngVienIdFilter != null, e => e.UngVienId >= input.MinUngVienIdFilter)
                        .WhereIf(input.MaxUngVienIdFilter != null, e => e.UngVienId <= input.MaxUngVienIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NoiDungFilter), e => e.NoiDung.ToLower() == input.NoiDungFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TepDinhKemFilter), e => e.TepDinhKem.ToLower() == input.TepDinhKemFilter.ToLower().Trim());

            var pagedAndFilteredLichSuLamViecs = filteredLichSuLamViecs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var lichSuLamViecs = from o in pagedAndFilteredLichSuLamViecs
                                 select new GetLichSuLamViecForViewDto()
                                 {
                                     LichSuLamViec = new LichSuLamViecDto
                                     {
                                         UngVienId = o.UngVienId,
                                         PhieuNghiPhepID = o.PhieuNghiPhepID,
                                         NoiDung = o.NoiDung,
                                         TepDinhKem = o.TepDinhKem,
                                         Id = o.Id
                                     }
                                 };

            var totalCount = await filteredLichSuLamViecs.CountAsync();

            return new PagedResultDto<GetLichSuLamViecForViewDto>(
                totalCount,
                await lichSuLamViecs.ToListAsync()
            );
        }

        public async Task<GetLichSuLamViecForViewDto> GetLichSuLamViecForView(int id)
        {
            var lichSuLamViec = await _lichSuLamViecRepository.GetAsync(id);

            var output = new GetLichSuLamViecForViewDto { LichSuLamViec = ObjectMapper.Map<LichSuLamViecDto>(lichSuLamViec) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_LichSuLamViecs_Edit)]
        public async Task<GetLichSuLamViecForEditOutput> GetLichSuLamViecForEdit(EntityDto input)
        {
            var lichSuLamViec = await _lichSuLamViecRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetLichSuLamViecForEditOutput { LichSuLamViec = ObjectMapper.Map<CreateOrEditLichSuLamViecDto>(lichSuLamViec) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditLichSuLamViecDto input)
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

        [AbpAuthorize(AppPermissions.Pages_LichSuLamViecs_Create)]
        protected virtual async Task Create(CreateOrEditLichSuLamViecDto input)
        {
            var lichSuLamViec = ObjectMapper.Map<LichSuLamViec>(input);



            await _lichSuLamViecRepository.InsertAsync(lichSuLamViec);
        }

        [AbpAuthorize(AppPermissions.Pages_LichSuLamViecs_Edit)]
        protected virtual async Task Update(CreateOrEditLichSuLamViecDto input)
        {
            var lichSuLamViec = await _lichSuLamViecRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, lichSuLamViec);
        }

        [AbpAuthorize(AppPermissions.Pages_LichSuLamViecs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _lichSuLamViecRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetLichSuLamViecsToExcel(GetAllLichSuLamViecsForExcelInput input)
        {

            var filteredLichSuLamViecs = _lichSuLamViecRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.NoiDung.Contains(input.Filter) || e.TepDinhKem.Contains(input.Filter))
                        .WhereIf(input.MinUngVienIdFilter != null, e => e.UngVienId >= input.MinUngVienIdFilter)
                        .WhereIf(input.MaxUngVienIdFilter != null, e => e.UngVienId <= input.MaxUngVienIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NoiDungFilter), e => e.NoiDung.ToLower() == input.NoiDungFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TepDinhKemFilter), e => e.TepDinhKem.ToLower() == input.TepDinhKemFilter.ToLower().Trim());

            var query = (from o in filteredLichSuLamViecs
                         select new GetLichSuLamViecForViewDto()
                         {
                             LichSuLamViec = new LichSuLamViecDto
                             {
                                 UngVienId = o.UngVienId,
                                 NoiDung = o.NoiDung,
                                 TepDinhKem = o.TepDinhKem,
                                 Id = o.Id
                             }
                         });


            var lichSuLamViecListDtos = await query.ToListAsync();

            return _lichSuLamViecsExcelExporter.ExportToFile(lichSuLamViecListDtos);
        }


    }
}