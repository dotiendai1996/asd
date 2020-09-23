

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
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using Hinnova.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Parameter = Hinnova.Management.Dtos.Parameter;
using Hinnova.Managerment.Dtos;
using Dapper;
using System.Threading;
using Z.BulkOperations;

namespace Hinnova.Management
{
    [AbpAuthorize(AppPermissions.Pages_SqlConfigDetails)]
    public class SqlConfigDetailsAppService : HinnovaAppServiceBase, ISqlConfigDetailsAppService
    {
        private readonly IRepository<SqlConfigDetail> _sqlConfigDetailRepository;
        private readonly ISqlConfigDetailsExcelExporter _sqlConfigDetailsExcelExporter;
        private readonly string connectionString;
        private readonly IRepository<SqlConfig> _sqlConfigRepository;
        private readonly IRepository<SqlStoreParam> _sqlStoreParamRepository;
        private AutoResetEvent _stopped = new AutoResetEvent(false);

        public SqlConfigDetailsAppService(IRepository<SqlConfig> sqlConfigRepository, IRepository<SqlConfigDetail> sqlConfigDetailRepository, IWebHostEnvironment env, ISqlConfigDetailsExcelExporter sqlConfigDetailsExcelExporter, IRepository<SqlStoreParam> sqlStoreParamRepository)
        {
            _sqlStoreParamRepository = sqlStoreParamRepository;
            _sqlConfigDetailRepository = sqlConfigDetailRepository;
            _sqlConfigDetailsExcelExporter = sqlConfigDetailsExcelExporter;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");
            _sqlConfigRepository = sqlConfigRepository;
        }

        public List<SqlConfigDetailDto> GetColumnConfigBySqlId(int id)
        {
            var sqlConfigDetail = _sqlConfigDetailRepository.GetAll().Where(x => x.SqlConfigId == id);

            var sqlConfigDetailDto = ObjectMapper.Map<List<SqlConfigDetailDto>>(sqlConfigDetail);

            return sqlConfigDetailDto.ToList();
        }

        public async Task DeleteAllConfigByConfigId(int sqlConfigId)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                if(conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();

                }
                await conn.QueryAsync(sql: "update dbo.SqlConfigDetail set IsDeleted = 1 where SqlConfigId = " + sqlConfigId);               
            }
        }

        public async Task CreateConfigIfNotExistsAsync2(int sqlConfigId, SqlConfigDetailDto[] listdata)
        {
            try
                {
                var mapperData = ObjectMapper.Map<SqlConfigDetail[]>(listdata);
                var oldConfig = GetColumnConfigBySqlId(sqlConfigId).ToArray();
                var oldConfigMap = oldConfig.Select(x => x.Code);

                var newConfig = listdata.Select(x => x.Code);
                
                    using (var conn = new SqlConnection(this.connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                            await conn.OpenAsync();
                    if (oldConfig.Length > 0 && oldConfigMap != newConfig)
                    {
                        using (var bulk = new BulkOperation<SqlConfigDetail>(conn))
                        {
                            bulk.DestinationTableName = "SqlConfigDetail";
                            bulk.InsertIfNotExists = true;
                            bulk.ColumnPrimaryKeyExpression = c => c.Id;
                            bulk.BatchSize = 10000;
                            bulk.BatchTimeout = 10000;
                            bulk.ColumnPrimaryKeyExpression = c => new { c.Code };
                            //bulk.AutoMapValueFactory =
                            //bulk.ColumnInputExpression = c => new
                            //{
                            //    c.LastModificationTime,
                            //    c.LastModifierUserId,
                            //    c.IsDisplay,
                            //    c.IsSum,
                            //    c.GroupLevel,
                            //    c.Width,
                            //    c.Name,
                            //    c.GroupSort,
                            //    c.Type,
                            //    c.Format,
                            //    c.ColNum
                            //};
                            await bulk.BulkInsertAsync(mapperData);
                        }
                    }
                    else
                    {
                        using (var bulk = new BulkOperation<SqlConfigDetail>(conn))
                        {
                            bulk.DestinationTableName = "SqlConfigDetail";
                            bulk.InsertIfNotExists = true;
                            bulk.ColumnPrimaryKeyExpression = c => c.Id;
                            bulk.BatchSize = 10000;
                            bulk.BatchTimeout = 10000;
                            //bulk.AutoMapValueFactory =
                            bulk.ColumnInputExpression = c => new
                            {
                                c.LastModificationTime,
                                c.LastModifierUserId,
                                c.IsDisplay,
                                c.IsSum,
                                c.GroupLevel,
                                c.Width,
                                c.Name,
                                c.GroupSort,
                                c.Type,
                                c.Format,
                                c.ColNum
                            };
                            await bulk.BulkUpdateAsync(mapperData);
                        }
                    }
                }
                
            }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error: ", ex);
                }
}

        public async Task CreateConfigIfNotExistsAsync(int sqlConfigId, SqlConfigDetailDto[] listdata)
        {
            
            try
            {
                if(listdata != null)
                {
                    RunTask(sqlConfigId, listdata);
                    _stopped.WaitOne();
                    _stopped.Reset();
                    //config mới khác config cũ
                    foreach (SqlConfigDetailDto temp in listdata)
                    {
                        var sqlConfigDetail = ObjectMapper.Map<CreateOrEditSqlConfigDetailDto>(temp);
                        await CreateOrEdit(sqlConfigDetail);
                        Logger.Error("create success " + sqlConfigDetail.Id);
                    }
                    //if (oldConfigMap != newConfig && oldConfig.Length > 0)
                    //{
                    //    using (SqlConnection conn = new SqlConnection(this.connectionString))
                    //    {
                    //        if (conn.State == ConnectionState.Closed)
                    //        {
                    //            conn.Open();
                    //            //conn.Query(sql: "DELETE FROM dbo.SqlConfigDetail WHERE SqlConfigId = " + sqlConfigId);
                    //            Logger.Error("delete success");
                    //        }
                    //        //CurrentUnitOfWork.SaveChanges();
                    //        foreach (SqlConfigDetailDto temp in listdata)
                    //        {
                    //            var sqlConfigDetail = ObjectMapper.Map<CreateOrEditSqlConfigDetailDto>(temp);
                    //            await Update(sqlConfigDetail);
                    //            Logger.Error("create success " + sqlConfigDetail.Id);
                    //        }
                    //    }

                    //    //CurrentUnitOfWork.SaveChanges();
                    //}
                    //else
                    //{
                    //    foreach (SqlConfigDetailDto temp in listdata)
                    //    {
                    //        var sqlConfigDetail = ObjectMapper.Map<CreateOrEditSqlConfigDetailDto>(temp);
                    //        await CreateOrEdit(sqlConfigDetail);
                    //        Logger.Error("create first success ");

                    //    }
                    //}
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                //return ResultVm.Fail("Fail", "Thất bại");
            }
        }

        private void RunTask(int sqlConfigId, SqlConfigDetailDto[] listdata)
        {
            //try
            //{
                var oldConfig = GetColumnConfigBySqlId(sqlConfigId).ToArray();
                var oldConfigMap = oldConfig.Select(x => x.Code);

                var newConfig = listdata.Select(x => x.Code);

                new Task(() =>
                {
                    _stopped.Set();
                    using (SqlConnection conn = new SqlConnection(this.connectionString))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.OpenAsync();
                        }
                        conn.QueryAsync(sql: "DELETE FROM dbo.SqlConfigDetail WHERE SqlConfigId = " + sqlConfigId);

                    }
                }).Start();
            //}
            //catch (Exception e)
            //{
            //    Logger.Error(e.Message);
            //}
            //finally
            //{
            //    _stopped.Set();
            //}
        }

        public DataVm GetColumn(int sqlConfigId, string sqlContent)
        {
            try
            {
                //tạo parameter ảo, tạm thời chưa dùng
                var storeParam = _sqlStoreParamRepository.GetAll().Where(x => x.SqlConfigId == sqlConfigId).ToList();
                var storeParamMap = ObjectMapper.Map<List<SqlStoreParamDto>>(storeParam);
                    //(from c in _context.ConfigReportFilter
                    //                where c.ReportId == reportid && c.IsDelete == false
                    //                select c).ToList();

                List<Parameter> param = new List<Parameter>();
                var config = _sqlConfigRepository.FirstOrDefault(sqlConfigId);
                if (sqlContent.IsNullOrEmpty())
                {
                    sqlContent = config.SqlContent;
                }
                ////var config = (from c in _context.ConfigReport
                ////              where c.Id == reportid
                ////              select c).SingleOrDefault();
                //long datasourceId = config.DataSourceId;
                // dataSource = _context.ConfigReportDataSource.SingleOrDefault(m => m.Id == datasourceId);
                //string sqlType = dataSource.SqlType;

                //Bảo cmt, param của store. tạm thời chưa dùng
                if(storeParamMap.Count > 0)
                {
                    foreach (SqlStoreParamDto element in storeParamMap)
                    {
                        Parameter a = new Parameter("", null);
                        //tạo danh sách param test
                        
                            if (element.ValueString != null)
                            {
                                a.Varible = "@" + element.Code;
                                a.Value = element.ValueString;
                                param.Add(a);
                            }
                            else
                            {
                                a.Varible = "@" + element.Code;
                                a.Value = DBNull.Value;
                                param.Add(a);
                            }
                        
                    }
                }
                

                //Get danh sách report display hiện có
                var reportDisplayOld = _sqlConfigDetailRepository.GetAll().Where(x => x.SqlConfigId == sqlConfigId).ToList();
                var reportDisplayOldMap = ObjectMapper.Map<List<SqlConfigDetailDto>>(reportDisplayOld);
                    //(from c in _context.ConfigReportDisplay
                    //                    where c.ReportId == reportid && c.IsDelete == false
                    //                    select c).ToList();

                //get danh sách cột mới theo câu sql
                var items = new List<Dictionary<string, object>>();

                using (var conn = new SqlConnection(connectionString))
                    if (config.IsRawQuery == true)
                    {
                        using (var command = new SqlCommand(sqlContent, conn)
                        {
                            CommandType = CommandType.Text
                        })
                        {
                            conn.Open();
                            if (param != null)
                                foreach (Parameter _param in param)
                                {
                                    SqlParameter iparam = new SqlParameter();
                                    iparam.ParameterName = _param.Varible;
                                    iparam.Value = _param.Value;
                                    command.Parameters.Add(iparam);
                                }
                            //command.Parameters.Add(new SqlParameter("@"+param.Varible.ToString(), param.Value));
                            IDataReader reader = command.ExecuteReader();

                            var table = reader.GetSchemaTable();
                            foreach (DataRow column in table.Rows)
                            {
                                var item = new Dictionary<string, object>(reader.FieldCount);
                                item["col"] = column.ItemArray[0];
                                items.Add(item);
                            }
                            reader.Close();
                            conn.Close();
                        }
                    }
                    else
                    {
                        using (var command = new SqlCommand(sqlContent, conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        })
                        {
                            conn.Open();
                            
                            if (param != null)
                                foreach (Parameter _param in param)
                                {
                                    SqlParameter iparam = new SqlParameter();
                                    iparam.ParameterName = _param.Varible;
                                    iparam.Value = _param.Value;
                                    command.Parameters.Add(iparam);
                                }
                            //command.Parameters.Add(new SqlParameter("@"+param.Varible.ToString(), param.Value));
                            IDataReader reader = command.ExecuteReader();

                            var table = reader.GetSchemaTable();
                            foreach (DataRow column in table.Rows)
                            {
                                var item = new Dictionary<string, object>(reader.FieldCount);
                                item["col"] = column.ItemArray[0];
                                items.Add(item);
                            }
                            reader.Close();
                            conn.Close();
                        }
                    }

                //so sánh danh sách col cũ và mới giữ lại những col trùng và xóa đi những col không trùng
                List<SqlConfigDetailDto> reportDisplayNew = new List<SqlConfigDetailDto>();
                foreach (SqlConfigDetailDto temp in reportDisplayOldMap)
                {
                    bool key = false;
                    foreach (var a in items)
                    {
                        if (temp.Code == a["col"].ToString())
                        {
                            key = true;
                        }
                    }
                    if (key == true)
                        reportDisplayNew.Add(temp);
                }
                foreach (var a in items)
                {
                    bool key = false;
                    foreach (SqlConfigDetailDto temp in reportDisplayOldMap)
                    {
                        if (temp.Code == a["col"].ToString())
                        {
                            key = true;
                        }
                    }
                    if (key == false)
                    {
                        SqlConfigDetailDto reportDisplay = new SqlConfigDetailDto();
                        reportDisplay.Code = a["col"].ToString();
                        reportDisplay.GroupLevel = -1;
                        reportDisplay.Name = a["col"].ToString();
                        reportDisplay.IsDisplay = true;
                        reportDisplay.GroupSort = "";
                        reportDisplay.SqlConfigId = sqlConfigId;
                        reportDisplayNew.Add(reportDisplay);
                    }
                }

                return DataVm.Success("Success", "Thành công", reportDisplayNew);
            }
            catch (Exception e)
            {
                return DataVm.Fail("Error", "Thất bại");
            }
        }

        public async Task<PagedResultDto<GetSqlConfigDetailForViewDto>> GetAll(GetAllSqlConfigDetailsInput input)
        {

            var filteredSqlConfigDetails = _sqlConfigDetailRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter) || e.Format.Contains(input.Filter) || e.Type.Contains(input.Filter) || e.Width.Contains(input.Filter) || e.TextAlign.Contains(input.Filter) || e.ParentCode.Contains(input.Filter) || e.GroupSort.Contains(input.Filter))
                        .WhereIf(input.MinSqlConfigIdFilter != null, e => e.SqlConfigId >= input.MinSqlConfigIdFilter)
                        .WhereIf(input.MaxSqlConfigIdFilter != null, e => e.SqlConfigId <= input.MaxSqlConfigIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter), e => e.Code.ToLower() == input.CodeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FormatFilter), e => e.Format.ToLower() == input.FormatFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TypeFilter), e => e.Type.ToLower() == input.TypeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WidthFilter), e => e.Width.ToLower() == input.WidthFilter.ToLower().Trim())
                        .WhereIf(input.MinColNumFilter != null, e => e.ColNum >= input.MinColNumFilter)
                        .WhereIf(input.MaxColNumFilter != null, e => e.ColNum <= input.MaxColNumFilter)
                        .WhereIf(input.MinGroupLevelFilter != null, e => e.GroupLevel >= input.MinGroupLevelFilter)
                        .WhereIf(input.MaxGroupLevelFilter != null, e => e.GroupLevel <= input.MaxGroupLevelFilter)
                        .WhereIf(input.IsDisplayFilter > -1, e => Convert.ToInt32(e.IsDisplay) == input.IsDisplayFilter)
                        .WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
                        .WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TextAlignFilter), e => e.TextAlign.ToLower() == input.TextAlignFilter.ToLower().Trim())
                        .WhereIf(input.MinVersionFilter != null, e => e.Version >= input.MinVersionFilter)
                        .WhereIf(input.MaxVersionFilter != null, e => e.Version <= input.MaxVersionFilter)
                        .WhereIf(input.IsSumFilter > -1, e => Convert.ToInt32(e.IsSum) == input.IsSumFilter)
                        .WhereIf(input.IsFreePaneFilter > -1, e => Convert.ToInt32(e.IsFreePane) == input.IsFreePaneFilter)
                        .WhereIf(input.IsParentFilter > -1, e => Convert.ToInt32(e.IsParent) == input.IsParentFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ParentCodeFilter), e => e.ParentCode.ToLower() == input.ParentCodeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GroupSortFilter), e => e.GroupSort.ToLower() == input.GroupSortFilter.ToLower().Trim());

            var pagedAndFilteredSqlConfigDetails = filteredSqlConfigDetails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var sqlConfigDetails = from o in pagedAndFilteredSqlConfigDetails
                                   select new GetSqlConfigDetailForViewDto()
                                   {
                                       SqlConfigDetail = new SqlConfigDetailDto
                                       {
                                           SqlConfigId = o.SqlConfigId,
                                           Code = o.Code,
                                           Name = o.Name,
                                           Format = o.Format,
                                           Type = o.Type,
                                           Width = o.Width,
                                           ColNum = o.ColNum,
                                           GroupLevel = o.GroupLevel,
                                           IsDisplay = o.IsDisplay,
                                           Order = o.Order,
                                           TextAlign = o.TextAlign,
                                           Version = o.Version,
                                           IsSum = o.IsSum,
                                           IsFreePane = o.IsFreePane,
                                           IsParent = o.IsParent,
                                           ParentCode = o.ParentCode,
                                           GroupSort = o.GroupSort,
                                           Id = o.Id,
                                           CellTemplate = o.CellTemplate
                                       }
                                   };

            var totalCount = await filteredSqlConfigDetails.CountAsync();

            return new PagedResultDto<GetSqlConfigDetailForViewDto>(
                totalCount,
                await sqlConfigDetails.ToListAsync()
            );
        }

        public async Task<GetSqlConfigDetailForViewDto> GetSqlConfigDetailForView(int id)
        {
            var sqlConfigDetail = await _sqlConfigDetailRepository.GetAsync(id);

            var output = new GetSqlConfigDetailForViewDto { SqlConfigDetail = ObjectMapper.Map<SqlConfigDetailDto>(sqlConfigDetail) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_SqlConfigDetails_Edit)]
        public async Task<GetSqlConfigDetailForEditOutput> GetSqlConfigDetailForEdit(EntityDto input)
        {
            var sqlConfigDetail = await _sqlConfigDetailRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetSqlConfigDetailForEditOutput { SqlConfigDetail = ObjectMapper.Map<CreateOrEditSqlConfigDetailDto>(sqlConfigDetail) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditSqlConfigDetailDto input)
        {
            if (input.Id == null || input.Id == 0)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_SqlConfigDetails_Create)]
        protected virtual async Task Create(CreateOrEditSqlConfigDetailDto input)
        {
            var sqlConfigDetail = ObjectMapper.Map<SqlConfigDetail>(input);


            if (AbpSession.TenantId != null)
            {
                sqlConfigDetail.TenantId = (int?)AbpSession.TenantId;
            }


            await _sqlConfigDetailRepository.InsertAsync(sqlConfigDetail);
        }

        [AbpAuthorize(AppPermissions.Pages_SqlConfigDetails_Edit)]
        protected virtual async Task Update(CreateOrEditSqlConfigDetailDto input)
        {
            var sqlConfigDetail = await _sqlConfigDetailRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, sqlConfigDetail);
        }

        [AbpAuthorize(AppPermissions.Pages_SqlConfigDetails_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _sqlConfigDetailRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetSqlConfigDetailsToExcel(GetAllSqlConfigDetailsForExcelInput input)
        {

            var filteredSqlConfigDetails = _sqlConfigDetailRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter) || e.Format.Contains(input.Filter) || e.Type.Contains(input.Filter) || e.Width.Contains(input.Filter) || e.TextAlign.Contains(input.Filter) || e.ParentCode.Contains(input.Filter) || e.GroupSort.Contains(input.Filter))
                        .WhereIf(input.MinSqlConfigIdFilter != null, e => e.SqlConfigId >= input.MinSqlConfigIdFilter)
                        .WhereIf(input.MaxSqlConfigIdFilter != null, e => e.SqlConfigId <= input.MaxSqlConfigIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter), e => e.Code.ToLower() == input.CodeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FormatFilter), e => e.Format.ToLower() == input.FormatFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TypeFilter), e => e.Type.ToLower() == input.TypeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WidthFilter), e => e.Width.ToLower() == input.WidthFilter.ToLower().Trim())
                        .WhereIf(input.MinColNumFilter != null, e => e.ColNum >= input.MinColNumFilter)
                        .WhereIf(input.MaxColNumFilter != null, e => e.ColNum <= input.MaxColNumFilter)
                        .WhereIf(input.MinGroupLevelFilter != null, e => e.GroupLevel >= input.MinGroupLevelFilter)
                        .WhereIf(input.MaxGroupLevelFilter != null, e => e.GroupLevel <= input.MaxGroupLevelFilter)
                        .WhereIf(input.IsDisplayFilter > -1, e => Convert.ToInt32(e.IsDisplay) == input.IsDisplayFilter)
                        .WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
                        .WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TextAlignFilter), e => e.TextAlign.ToLower() == input.TextAlignFilter.ToLower().Trim())
                        .WhereIf(input.MinVersionFilter != null, e => e.Version >= input.MinVersionFilter)
                        .WhereIf(input.MaxVersionFilter != null, e => e.Version <= input.MaxVersionFilter)
                        .WhereIf(input.IsSumFilter > -1, e => Convert.ToInt32(e.IsSum) == input.IsSumFilter)
                        .WhereIf(input.IsFreePaneFilter > -1, e => Convert.ToInt32(e.IsFreePane) == input.IsFreePaneFilter)
                        .WhereIf(input.IsParentFilter > -1, e => Convert.ToInt32(e.IsParent) == input.IsParentFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ParentCodeFilter), e => e.ParentCode.ToLower() == input.ParentCodeFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GroupSortFilter), e => e.GroupSort.ToLower() == input.GroupSortFilter.ToLower().Trim());

            var query = (from o in filteredSqlConfigDetails
                         select new GetSqlConfigDetailForViewDto()
                         {
                             SqlConfigDetail = new SqlConfigDetailDto
                             {
                                 SqlConfigId = o.SqlConfigId,
                                 Code = o.Code,
                                 Name = o.Name,
                                 Format = o.Format,
                                 Type = o.Type,
                                 Width = o.Width,
                                 ColNum = o.ColNum,
                                 GroupLevel = o.GroupLevel,
                                 IsDisplay = o.IsDisplay,
                                 Order = o.Order,
                                 TextAlign = o.TextAlign,
                                 Version = o.Version,
                                 IsSum = o.IsSum,
                                 IsFreePane = o.IsFreePane,
                                 IsParent = o.IsParent,
                                 ParentCode = o.ParentCode,
                                 GroupSort = o.GroupSort,
                                 Id = o.Id
                             }
                         });


            var sqlConfigDetailListDtos = await query.ToListAsync();

            return _sqlConfigDetailsExcelExporter.ExportToFile(sqlConfigDetailListDtos);
        }


    }
}