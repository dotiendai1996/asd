

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
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Hinnova.Configuration;
using Microsoft.Extensions.Configuration;
using Hinnova.Management.Dtos;
using Hinnova.Management;
using Hinnova.Authorization.Users;

namespace Hinnova.QLVB
{
    [AbpAuthorize(AppPermissions.Pages_DocumentHandlings)]
    public class DocumentHandlingsAppService : HinnovaAppServiceBase, IDocumentHandlingsAppService
    {
        private readonly ISqlConfigDetailsAppService _sqlConfigDetailsAppService;
        private readonly ISqlConfigsAppService _sqlConfigsAppService;
        private readonly IRepository<DocumentHandling> _documentHandlingRepository;
        private readonly IDocumentHandlingsExcelExporter _documentHandlingsExcelExporter;
        private readonly string connectionString;
        private readonly IUserAppService _userAppService;

        public DocumentHandlingsAppService(IRepository<DocumentHandling> documentHandlingRepository, IDocumentHandlingsExcelExporter documentHandlingsExcelExporter, 
            IWebHostEnvironment env, ISqlConfigsAppService sqlConfigsAppService, ISqlConfigDetailsAppService sqlConfigDetailsAppService, IUserAppService userAppService)
        {
            _sqlConfigDetailsAppService = sqlConfigDetailsAppService;
            _sqlConfigsAppService = sqlConfigsAppService;
            _documentHandlingRepository = documentHandlingRepository;
            _documentHandlingsExcelExporter = documentHandlingsExcelExporter;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");
            _userAppService = userAppService;
        }

        public async Task<PagedResultDto<GetDocumentHandlingForViewDto>> GetAll(GetAllDocumentHandlingsInput input)
        {

            var filteredDocumentHandlings = _documentHandlingRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Handler.Contains(input.Filter) || e.PlaceReceive.Contains(input.Filter) || e.Content.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.Comment.Contains(input.Filter))
                        .WhereIf(input.MinDocumentIdFilter != null, e => e.DocumentId >= input.MinDocumentIdFilter)
                        .WhereIf(input.MaxDocumentIdFilter != null, e => e.DocumentId <= input.MaxDocumentIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.HandlerFilter), e => e.Handler.ToLower() == input.HandlerFilter.ToLower().Trim())
                        .WhereIf(input.MinHandlingDetailIdFilter != null, e => e.HandlingDetailId >= input.MinHandlingDetailIdFilter)
                        .WhereIf(input.MaxHandlingDetailIdFilter != null, e => e.HandlingDetailId <= input.MaxHandlingDetailIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PlaceReceiveFilter), e => e.PlaceReceive.ToLower() == input.PlaceReceiveFilter.ToLower().Trim())
                        //.WhereIf(input.MinKeywordIdFilter != null, e => e.KeywordId >= input.MinKeywordIdFilter)
                        //.WhereIf(input.MaxKeywordIdFilter != null, e => e.KeywordId <= input.MaxKeywordIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ContentFilter), e => e.Content.ToLower() == input.ContentFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter), e => e.Status.ToLower() == input.StatusFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CommentFilter), e => e.Comment.ToLower() == input.CommentFilter.ToLower().Trim())
                        ;
            var pagedAndFilteredDocumentHandlings = filteredDocumentHandlings
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var documentHandlings = from o in pagedAndFilteredDocumentHandlings
                                    select new GetDocumentHandlingForViewDto()
                                    {
                                        DocumentHandling = new DocumentHandlingDto
                                        {
                                            DocumentId = o.DocumentId,
                                            Handler = o.Handler,
                                            HandlingDetailId = o.HandlingDetailId,
                                            PlaceReceive = o.PlaceReceive,
                                            //KeywordId = o.KeywordId,
                                            Content = o.Content,
                                            Status = o.Status,
                                            Comment = o.Comment,
                                            Id = o.Id,
                                            CreationTime = o.CreationTime,
                                            EndDate = o.EndDate
                                        }
                                    };

            var totalCount = await filteredDocumentHandlings.CountAsync();

            return new PagedResultDto<GetDocumentHandlingForViewDto>(
                totalCount,
                await documentHandlings.ToListAsync()
            );
        }

        public async Task<GetDocumentHandlingForViewDto> GetDocumentHandlingForView(int id)
        {
            var documentHandling = await _documentHandlingRepository.GetAsync(id);

            var output = new GetDocumentHandlingForViewDto { DocumentHandling = ObjectMapper.Map<DocumentHandlingDto>(documentHandling) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentHandlings_Edit)]
        public async Task<GetDocumentHandlingForEditOutput> GetDocumentHandlingForEdit(EntityDto input)
        {
            var documentHandling = await _documentHandlingRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDocumentHandlingForEditOutput { DocumentHandling = ObjectMapper.Map<CreateOrEditDocumentHandlingDto>(documentHandling) };

            return output;
        }

        public async Task<int> CreateOrEdit(CreateOrEditDocumentHandlingDto input)
        {
            int id = 0;
            if (input.Id == null)
            {
                id = await Create(input);
            }
            else
            {
                await Update(input);
            }
            return id;
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentHandlings_Create)]
        protected virtual async Task<int> Create(CreateOrEditDocumentHandlingDto input)
        {
            var documentHandling = ObjectMapper.Map<DocumentHandling>(input);


            if (AbpSession.TenantId != null)
            {
                documentHandling.TenantId = (int?)AbpSession.TenantId;
            }


            var insertedID = await _documentHandlingRepository.InsertAndGetIdAsync(documentHandling);
            return insertedID;
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentHandlings_Edit)]
        protected virtual async Task Update(CreateOrEditDocumentHandlingDto input)
        {
            var documentHandling = await _documentHandlingRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, documentHandling);
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentHandlings_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _documentHandlingRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetDocumentHandlingsToExcel(GetAllDocumentHandlingsForExcelInput input)
        {

            var filteredDocumentHandlings = _documentHandlingRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Handler.Contains(input.Filter) || e.PlaceReceive.Contains(input.Filter) || e.Content.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.Comment.Contains(input.Filter))
                        .WhereIf(input.MinDocumentIdFilter != null, e => e.DocumentId >= input.MinDocumentIdFilter)
                        .WhereIf(input.MaxDocumentIdFilter != null, e => e.DocumentId <= input.MaxDocumentIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.HandlerFilter), e => e.Handler.ToLower() == input.HandlerFilter.ToLower().Trim())
                        .WhereIf(input.MinHandlingDetailIdFilter != null, e => e.HandlingDetailId >= input.MinHandlingDetailIdFilter)
                        .WhereIf(input.MaxHandlingDetailIdFilter != null, e => e.HandlingDetailId <= input.MaxHandlingDetailIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PlaceReceiveFilter), e => e.PlaceReceive.ToLower() == input.PlaceReceiveFilter.ToLower().Trim())
                        //.WhereIf(input.MinKeywordIdFilter != null, e => e.KeywordId >= input.MinKeywordIdFilter)
                        //.WhereIf(input.MaxKeywordIdFilter != null, e => e.KeywordId <= input.MaxKeywordIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ContentFilter), e => e.Content.ToLower() == input.ContentFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter), e => e.Status.ToLower() == input.StatusFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CommentFilter), e => e.Comment.ToLower() == input.CommentFilter.ToLower().Trim());

            var query = (from o in filteredDocumentHandlings
                         select new GetDocumentHandlingForViewDto()
                         {
                             DocumentHandling = new DocumentHandlingDto
                             {
                                 DocumentId = o.DocumentId,
                                 Handler = o.Handler,
                                 HandlingDetailId = o.HandlingDetailId,
                                 PlaceReceive = o.PlaceReceive,
                                 //KeywordId = o.KeywordId,
                                 Content = o.Content,
                                 Status = o.Status,
                                 Comment = o.Comment,
                                 Id = o.Id,
                                 CreationTime = o.CreationTime,
                                 EndDate = o.EndDate
                             }
                         });


            var documentHandlingListDtos = await query.ToListAsync();

            return _documentHandlingsExcelExporter.ExportToFile(documentHandlingListDtos);
        }

        public async Task<List<string>> GetLeaderTypes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<string>(sql: "SELECT DisplayName FROM dbo.AbpOrganizationUnits WHERE IsDeleted = 0");
                return result.ToList();
            }
        }


        //public async Task<List<HandlingUser>> GetLeaderList()
        //public async Task<List<HandlingUser>> GetLeaderList()
        //{
        //    var tenantId = AbpSession.TenantId;
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            await conn.OpenAsync();
        //        }
        //        var roleName = await _userAppService.GetRoleNameOfUser((long)AbpSession.UserId);
        //        var roleId = await _userAppService.GetRoleIdOfUser((long)AbpSession.UserId);
        //        Logger.Error("RoleName " + roleName);
        //        if (roleName == "Văn thư tổng")
        //        {
        //            int i = 1;
        //            var list = await conn.QueryAsync<HandlingUser>(sql: "dbo.GetLeaderList1", param: new { AbpSession.UserId, tenantId, i }, commandType: CommandType.StoredProcedure);
        //            return list.ToList();
        //        }
        //        //var currentUserRoleId = conn.Query<OrganizationDto>(sql: "SELECT  FROM AbpUserRoles WHERE UserId = " + AbpSession.UserId).FirstOrDefault();
        //        //var currentOrganizationId = conn.Query(sql: "SELECT * FROM AbpOrganizationUnits WHERE Id = " + currentUserRoleId.RoleId).Fir;
        //        else
        //        {
        //            return null;
        //        }

        //        //var result = await conn.QueryAsync<HandlingUser>(sql: "dbo.GetLeaderList", param: new { organizationName, tenantId }, commandType: CommandType.StoredProcedure);
        //        //return result.ToList();
        //    }
        //}

        public async Task<List<HandlingUser>> GetLeaderList()
        {
            var tenantId = AbpSession.TenantId;
            var organizationName = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<HandlingUser>(sql: "dbo.GetLeaderList", param: new { organizationName, tenantId }, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<GetDataAndColumnConfig_ProcesHanding> GetAllProcesHanding()
        {
            var sqlConfig = _sqlConfigsAppService.GetSqlConfigByCodeAsync("GAPC").Result;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var columnConfig = _sqlConfigDetailsAppService.GetColumnConfigBySqlId(sqlConfig.Id);

                var filterProcesHanding = conn.Query<object>(sqlConfig.SqlContent).ToList();

                return new GetDataAndColumnConfig_ProcesHanding(filterProcesHanding, columnConfig);


            }
        }
    }

    //public class OrganizationDto
    //{
    //    public 
    //}
}