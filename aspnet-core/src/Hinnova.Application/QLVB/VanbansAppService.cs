

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
using Microsoft.AspNetCore.Hosting;
using Hinnova.Configuration;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using System.Reflection.Metadata;
using Hinnova.Management;
using Hinnova.Management.Dtos;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Twilio.Http;
using System.Net.Http.Headers;

namespace Hinnova.QLVB
{
    [AbpAuthorize(AppPermissions.Pages_Vanbans)]
    public class VanbansAppService : HinnovaAppServiceBase, IVanbansAppService
    {
        private readonly ISqlConfigDetailsAppService _sqlConfigDetailsAppService;
        private readonly ISqlConfigsAppService _sqlConfigsAppService;
        private readonly IRepository<Vanban> _vanbanRepository;
        private readonly IVanbansExcelExporter _vanbansExcelExporter;
        private readonly string connectionString;

        public VanbansAppService(IRepository<Vanban> vanbanRepository, IVanbansExcelExporter vanbansExcelExporter, IWebHostEnvironment env, ISqlConfigsAppService sqlConfigsAppService, ISqlConfigDetailsAppService sqlConfigDetailsAppService)
        {
            _sqlConfigDetailsAppService = sqlConfigDetailsAppService;
            _sqlConfigsAppService = sqlConfigsAppService;
            _vanbanRepository = vanbanRepository;
            _vanbansExcelExporter = vanbansExcelExporter;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");
        }

        public async Task<GetDataAndColumnConfig> GetAllVanBan()
        {
            var sqlConfig = _sqlConfigsAppService.GetSqlConfigByCodeAsync("GAVB").Result;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var columnConfig = _sqlConfigDetailsAppService.GetColumnConfigBySqlId(sqlConfig.Id);

                var filterVanbans = conn.Query<object>(sqlConfig.SqlContent).ToList();

                //var vanbanMap = ObjectMapper.Map<List<VanbanDto>>(filterVanbans);

                ////var pagedAndFilteredVanbans = filterVanbans
                ////.OrderBy(input.Sorting ?? "id asc")
                ////.PageBy(input);





                //return new VanbanAndColumnConfigDto(vanbanMap, columnConfig);

                //    return new PagedResultDto<VanbanDto>(
                //    totalCount,
                //    vanbans.ToList()
                //);
                return new GetDataAndColumnConfig(filterVanbans, columnConfig);

                //return new DataVm { Code = "200", isSucceeded = dataString.Length > 0 ? true : false, Data = dataString, Message = "Success" };
            }
        }

        public async Task<PagedResultDto<GetVanbanForViewDto>> GetAll(GetAllVanbansInput input)
        {

            var filteredVanbans = _vanbanRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.TenCongViec.Contains(input.Filter) || e.NguoiXuLy.Contains(input.Filter) || e.TinhTrang.Contains(input.Filter) || e.NoiDung.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenCongViecFilter), e => e.TenCongViec.ToLower() == input.TenCongViecFilter.ToLower().Trim())
                        .WhereIf(input.MinNgayGiaoViecFilter != null, e => e.NgayGiaoViec >= input.MinNgayGiaoViecFilter)
                        .WhereIf(input.MaxNgayGiaoViecFilter != null, e => e.NgayGiaoViec <= input.MaxNgayGiaoViecFilter)
                        .WhereIf(input.MinHanKetThucFilter != null, e => e.HanKetThuc >= input.MinHanKetThucFilter)
                        .WhereIf(input.MaxHanKetThucFilter != null, e => e.HanKetThuc <= input.MaxHanKetThucFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NguoiXuLyFilter), e => e.NguoiXuLy.ToLower() == input.NguoiXuLyFilter.ToLower().Trim())
                        .WhereIf(input.MinTienDoChungFilter != null, e => e.TienDoChung >= input.MinTienDoChungFilter)
                        .WhereIf(input.MaxTienDoChungFilter != null, e => e.TienDoChung <= input.MaxTienDoChungFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TinhTrangFilter), e => e.TinhTrang.ToLower() == input.TinhTrangFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NoiDungFilter), e => e.NoiDung.ToLower() == input.NoiDungFilter.ToLower().Trim());

            var pagedAndFilteredVanbans = filteredVanbans
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var vanbans = from o in pagedAndFilteredVanbans
                          select new GetVanbanForViewDto()
                          {
                              Vanban = new VanbanDto
                              {
                                  TenCongViec = o.TenCongViec,
                                  NgayGiaoViec = o.NgayGiaoViec,
                                  HanKetThuc = o.HanKetThuc,
                                  NguoiXuLy = o.NguoiXuLy,
                                  TienDoChung = o.TienDoChung,
                                  TinhTrang = o.TinhTrang,
                                  NoiDung = o.NoiDung,
                                  Id = o.Id
                              }
                          };

            var totalCount = await filteredVanbans.CountAsync();

            return new PagedResultDto<GetVanbanForViewDto>(
                totalCount,
                await vanbans.ToListAsync()
            );
        }

        public async Task<GetVanbanForViewDto> GetVanbanForView(int id)
        {
            var vanban = await _vanbanRepository.GetAsync(id);

            var output = new GetVanbanForViewDto { Vanban = ObjectMapper.Map<VanbanDto>(vanban) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Vanbans_Edit)]
        public async Task<GetVanbanForEditOutput> GetVanbanForEdit(EntityDto input)
        {
            var vanban = await _vanbanRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetVanbanForEditOutput { Vanban = ObjectMapper.Map<CreateOrEditVanbanDto>(vanban) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditVanbanDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Vanbans_Create)]
        protected virtual async Task Create(CreateOrEditVanbanDto input)
        {
            var vanban = ObjectMapper.Map<Vanban>(input);



            await _vanbanRepository.InsertAsync(vanban);
        }

        [AbpAuthorize(AppPermissions.Pages_Vanbans_Edit)]
        protected virtual async Task Update(CreateOrEditVanbanDto input)
        {
            var vanban = await _vanbanRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, vanban);
        }

        [AbpAuthorize(AppPermissions.Pages_Vanbans_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _vanbanRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetVanbansToExcel(GetAllVanbansForExcelInput input)
        {

            var filteredVanbans = _vanbanRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.TenCongViec.Contains(input.Filter) || e.NguoiXuLy.Contains(input.Filter) || e.TinhTrang.Contains(input.Filter) || e.NoiDung.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TenCongViecFilter), e => e.TenCongViec.ToLower() == input.TenCongViecFilter.ToLower().Trim())
                        .WhereIf(input.MinNgayGiaoViecFilter != null, e => e.NgayGiaoViec >= input.MinNgayGiaoViecFilter)
                        .WhereIf(input.MaxNgayGiaoViecFilter != null, e => e.NgayGiaoViec <= input.MaxNgayGiaoViecFilter)
                        .WhereIf(input.MinHanKetThucFilter != null, e => e.HanKetThuc >= input.MinHanKetThucFilter)
                        .WhereIf(input.MaxHanKetThucFilter != null, e => e.HanKetThuc <= input.MaxHanKetThucFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NguoiXuLyFilter), e => e.NguoiXuLy.ToLower() == input.NguoiXuLyFilter.ToLower().Trim())
                        .WhereIf(input.MinTienDoChungFilter != null, e => e.TienDoChung >= input.MinTienDoChungFilter)
                        .WhereIf(input.MaxTienDoChungFilter != null, e => e.TienDoChung <= input.MaxTienDoChungFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TinhTrangFilter), e => e.TinhTrang.ToLower() == input.TinhTrangFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NoiDungFilter), e => e.NoiDung.ToLower() == input.NoiDungFilter.ToLower().Trim());

            var query = (from o in filteredVanbans
                         select new GetVanbanForViewDto()
                         {
                             Vanban = new VanbanDto
                             {
                                 TenCongViec = o.TenCongViec,
                                 NgayGiaoViec = o.NgayGiaoViec,
                                 HanKetThuc = o.HanKetThuc,
                                 NguoiXuLy = o.NguoiXuLy,
                                 TienDoChung = o.TienDoChung,
                                 TinhTrang = o.TinhTrang,
                                 NoiDung = o.NoiDung,
                                 Id = o.Id
                             }
                         });


            var vanbanListDtos = await query.ToListAsync();

            return _vanbansExcelExporter.ExportToFile(vanbanListDtos);
        }

        public async Task<List<VanbanListDto>> GetListAsyncById(GetVanbanListInputById input)
        {
            try
            {

                var vanban = ObjectMapper.Map<VanbanListDto>(await _vanbanRepository.GetAsync(input.Id));
                var vanbans = new VanbanListDto[] { vanban };
                //foreach (var item in announcements)
                //{
                //    item.DateTime = item.NgayBaoCao.ToString("dd/MM/yyyy");
                //}
                return vanbans.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [AbpAllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {

            string path_upload = "";
            string Upload_FileName = ""; 
            //if (file == null || file.Length == 0)
            //    return Content("file not selected");


            var filePath = Path.Combine(
            Directory.GetCurrentDirectory(), "TempUpload");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var fileUniqueId = Guid.NewGuid().ToString().ToLower().Replace("-", string.Empty);
            var uniqueFileName = $"{fileUniqueId}_{file.FileName}";

            using (var fileStream = new FileStream(Path.Combine(filePath, uniqueFileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
                path_upload = filePath + "\\" + file.FileName;
                Upload_FileName = Upload_FileName + path_upload; 

            }

            var result = new
            {
                UploadFileName = Upload_FileName
            };
         
            return new JsonResult(result);
        }

    }

    }
