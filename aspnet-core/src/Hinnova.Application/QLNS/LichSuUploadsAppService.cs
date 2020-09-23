

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
using Hinnova.Configuration;
using Hinnova.QLNSDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace Hinnova.QLNS
{
    [AbpAuthorize(AppPermissions.Pages_LichSuUploads)]
    public class LichSuUploadsAppService : HinnovaAppServiceBase, ILichSuUploadsAppService
    {
        private readonly IRepository<LichSuUpload> _lichSuUploadRepository;
        private readonly string connectionString;
        private readonly IWebHostEnvironment _env;

        public LichSuUploadsAppService(IRepository<LichSuUpload> lichSuUploadRepository, IWebHostEnvironment env, IWebHostEnvironment hostingEnvironment)
        {
            _lichSuUploadRepository = lichSuUploadRepository;
            _env = hostingEnvironment;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");

        }

        public async Task<PagedResultDto<GetLichSuUploadForViewDto>> GetAll(GetAllLichSuUploadsInput input)
        {

            var filteredLichSuUploads = _lichSuUploadRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.TenFile.Contains(input.Filter) || e.DungLuong.Contains(input.Filter) || e.TieuDe.Contains(input.Filter) || e.Type.Contains(input.Filter) || e.TypeID.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenFileFilter), e => e.TenFile == input.TenFileFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DungLuongFilter), e => e.DungLuong == input.DungLuongFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TieuDeFilter), e => e.TieuDe == input.TieuDeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TypeFilter), e => e.Type == input.TypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TypeIDFilter), e => e.TypeID == input.TypeIDFilter);

            var pagedAndFilteredLichSuUploads = filteredLichSuUploads
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var lichSuUploads = from o in pagedAndFilteredLichSuUploads
                                select new GetLichSuUploadForViewDto()
                                {
                                    LichSuUpload = new LichSuUploadDto
                                    {
                                        TenFile = o.TenFile,
                                        DungLuong = o.DungLuong,
                                        TieuDe = o.TieuDe,
                                        Type = o.Type,
                                        TypeID = o.TypeID,
                                        Id = o.Id
                                    }
                                };

            var totalCount = await filteredLichSuUploads.CountAsync();

            return new PagedResultDto<GetLichSuUploadForViewDto>(
                totalCount,
                await lichSuUploads.ToListAsync()
            );
        }

        public async Task<List<LichSuUploadDto>> GetListLichSuUploadDto(string type, string id)
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    if (conn.State == ConnectionState.Closed)
            //    {
            //        await conn.OpenAsync();
            //    }

            //    var result =
            //        await conn.QueryAsync<LichSuUploadDto>(
            //            sql: "SELECT * FROM  LichSuUploads WHERE type = N'" + type + "' and typeID="+id );
            //    return result.ToList();


            //}
            var lichSuUpload = await _lichSuUploadRepository.GetAll().Where(x => x.Type.ToUpper().Equals(type.ToUpper()) && x.TypeID == id).ToListAsync();

            var lichSuUploadDto = ObjectMapper.Map<List<LichSuUploadDto>>(lichSuUpload);
            return lichSuUploadDto;

        }


        public async Task<List<LichSuUploadDto>> XoaLichSuUpLoadToID(string type, string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<LichSuUploadDto>(sql: "delete  from  LichSuUploads  WHERE type = N'" + type + "' and typeID = " + id);
                return result.ToList();
            }
        }

        public async Task<GetLichSuUploadForViewDto> GetLichSuUploadForView(int id)
        {
            var lichSuUpload = await _lichSuUploadRepository.GetAsync(id);

            var output = new GetLichSuUploadForViewDto { LichSuUpload = ObjectMapper.Map<LichSuUploadDto>(lichSuUpload) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_LichSuUploads_Edit)]
        public async Task<GetLichSuUploadForEditOutput> GetLichSuUploadForEdit(EntityDto input)
        {
            var lichSuUpload = await _lichSuUploadRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetLichSuUploadForEditOutput { LichSuUpload = ObjectMapper.Map<CreateOrEditLichSuUploadDto>(lichSuUpload) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditLichSuUploadDto input)
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

        [AbpAuthorize(AppPermissions.Pages_LichSuUploads_Create)]
        protected virtual async Task Create(CreateOrEditLichSuUploadDto input)
        {
            var lichSuUpload = ObjectMapper.Map<LichSuUpload>(input);



            await _lichSuUploadRepository.InsertAsync(lichSuUpload);
        }

        [AbpAuthorize(AppPermissions.Pages_LichSuUploads_Edit)]
        protected virtual async Task Update(CreateOrEditLichSuUploadDto input)
        {
            var lichSuUpload = await _lichSuUploadRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, lichSuUpload);
        }

        [AbpAuthorize(AppPermissions.Pages_LichSuUploads_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _lichSuUploadRepository.DeleteAsync(input.Id);
        }
    }
}