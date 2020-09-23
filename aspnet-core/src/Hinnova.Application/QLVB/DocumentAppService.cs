

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
using Microsoft.AspNetCore.Hosting;
using Hinnova.Configuration;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Hinnova.Management.Dtos;
using Hinnova.Management;
using System.IO;

namespace Hinnova.QLVB
{
    [AbpAuthorize(AppPermissions.Pages_Documents)]
    public class DocumentAppService : HinnovaAppServiceBase, IDocumentAppService
    {

        private readonly IRepository<HistoryUpload> _historyUploadRepository;
        private readonly IRepository<Documents> _documentsRepository;
        private readonly IRepository<DocumentDetail> _documentDetailsRepository;
        private readonly IRepository<DocumentHandling> _documentHandlingRepository;
        private readonly IDocumentsesExcelExporter _documentsesExcelExporter;
        private readonly ISqlConfigsAppService _sqlConfigsAppService;
        private readonly ISqlConfigDetailsAppService _sqlConfigDetailsAppService;
        private List<SqlConfigDetailDto> _columns = new List<SqlConfigDetailDto>();
        private readonly IRepository<TypeHandle> _typeHandleRepository;
        private readonly string connectionString;
        private readonly IWebHostEnvironment _env;


        public DocumentAppService(IRepository<Documents> documentsRepository, IRepository<HistoryUpload> historyUploadRepository ,IRepository<DocumentDetail> documentDetailsRepository, IRepository<DocumentHandling> documentHandlingRepository, IDocumentsesExcelExporter documentsesExcelExporter, IWebHostEnvironment env, IRepository<TypeHandle> typeHandleRepository)
        {
            _documentsRepository = documentsRepository;
            _historyUploadRepository = historyUploadRepository;
            _documentsesExcelExporter = documentsesExcelExporter;
            _documentHandlingRepository = documentHandlingRepository;
            _documentDetailsRepository = documentDetailsRepository;
            _typeHandleRepository = typeHandleRepository;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");
        }

        public async Task<PagedResultDto<GetDocumentsForViewDto>> GetAll(GetAllDocumentsesInput input)
        {
            var name = GetCurrentUser();
            var filteredDocumentses = _documentsRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.PlaceReceive.Contains(input.Filter) || e.SaveTo.Contains(input.Filter) || e.Summary.Contains(input.Filter) || e.ApprovedBy.Contains(input.Filter) || e.Attachment.Contains(input.Filter) || e.TypeReceive.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.Note.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NumberFilter), e => e.Number == input.NumberFilter)
                        .WhereIf(input.MinDocumentTypeIdFilter != null, e => e.DocumentTypeId >= input.MinDocumentTypeIdFilter)
                        .WhereIf(input.MaxDocumentTypeIdFilter != null, e => e.DocumentTypeId <= input.MaxDocumentTypeIdFilter)
                        .WhereIf(input.MinIncommingNumberFilter != null, e => e.IncommingNumber >= input.MinIncommingNumberFilter)
                        .WhereIf(input.MaxIncommingNumberFilter != null, e => e.IncommingNumber <= input.MaxIncommingNumberFilter)
                        .WhereIf(input.MinPagesFilter != null, e => e.Pages >= input.MinPagesFilter)
                        .WhereIf(input.MaxPagesFilter != null, e => e.Pages <= input.MaxPagesFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PlaceRecevieFilter), e => e.PlaceReceive.ToLower() == input.PlaceRecevieFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SaveToFilter), e => e.SaveTo.ToLower() == input.SaveToFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SummaryFilter), e => e.Summary.ToLower() == input.SummaryFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ApprovedByFilter), e => e.ApprovedBy.ToLower() == input.ApprovedByFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AttachmentFilter), e => e.Attachment.ToLower() == input.AttachmentFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TypeReceiveFilter), e => e.TypeReceive.ToLower() == input.TypeReceiveFilter.ToLower().Trim())
                        .WhereIf(input.MinStartDateFilter != null, e => e.StartDate >= input.MinStartDateFilter)
                        .WhereIf(input.MaxStartDateFilter != null, e => e.StartDate <= input.MaxStartDateFilter)
                        .WhereIf(input.MinEndDateFilter != null, e => e.EndDate >= input.MinEndDateFilter)
                        .WhereIf(input.MaxEndDateFilter != null, e => e.EndDate <= input.MaxEndDateFilter)
                        .WhereIf(input.PriorityFilter != null, e => e.Priority <= input.PriorityFilter)
                        .WhereIf(input.IncommingDateFilter != null, e => e.IncommingDate <= input.IncommingDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RangeFilter), e => e.Range == input.RangeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PositionFilter), e => e.Position == input.PositionFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter), e => e.Status.ToLower() == input.StatusFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LinkedDocumentFilter), e => e.LinkedDocument.ToLower() == input.LinkedDocumentFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NoteFilter), e => e.Note.ToLower() == input.NoteFilter.ToLower().Trim());

            var pagedAndFilteredDocumentses = filteredDocumentses
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var documentses = from o in pagedAndFilteredDocumentses
                              select new GetDocumentsForViewDto()
                              {
                                  Documents = new DocumentsDto
                                  {
                                      Number = o.Number,
                                      DocumentTypeId = o.DocumentTypeId,
                                      PlaceReceive = o.PlaceReceive,
                                      SaveTo = o.SaveTo,
                                      Summary = o.Summary,
                                      ApprovedBy = o.ApprovedBy,
                                      Attachment = o.Attachment,
                                      TypeReceive = o.TypeReceive,
                                      StartDate = o.StartDate,
                                      EndDate = o.EndDate,
                                      Status = o.Status,
                                      Note = o.Note,
                                      Id = o.Id,
                                      Priority = o.Priority,
                                      IncommingNumber = o.IncommingNumber,
                                      IncommingDate = o.IncommingDate,
                                      Pages = o.Pages,
                                      Author = o.Author,
                                      GroupAuthor = o.GroupAuthor,
                                      IsActive = o.IsActive,
                                      MoreInformation = o.MoreInformation,
                                      Order = o.Order,
                                      Position = o.Position,
                                      Range = o.Range,
                                      LinkedDocument = o.LinkedDocument,
                                      Action = o.Action
                                  }
                              };

            var totalCount = await filteredDocumentses.CountAsync();

            return new PagedResultDto<GetDocumentsForViewDto>(
                totalCount,
                await documentses.ToListAsync()
            );
        }

        public async Task<GetDocumentsForViewDto> GetDocumentsForView(int id)
        {
            var documents = await _documentsRepository.GetAsync(id);

            var output = new GetDocumentsForViewDto { Documents = ObjectMapper.Map<DocumentsDto>(documents) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Documents_Edit)]
        public async Task<DocumentsDto> GetDocumentsForEdit(EntityDto input)
        {
            var documents = await _documentsRepository.FirstOrDefaultAsync(input.Id);

            //var output = new GetDocumentsForEditOutput { Documents =  };

            return ObjectMapper.Map<DocumentsDto>(documents);
        }

        public async Task CreateOrEdit(DocumentsDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Documents_Create)]
        protected virtual async Task<int> Create(DocumentsDto input)
        {
            var documents = ObjectMapper.Map<Documents>(input);
            string date = DateTime.Today.ToString("dd-MM-yyyy");

                if (AbpSession.TenantId != null)
                {
                    documents.TenantId = (int?)AbpSession.TenantId;
                }


            var docID = await _documentsRepository.InsertAndGetIdAsync(documents);
            CurrentUnitOfWork.SaveChanges();
            //System.Diagnostics.Debug.WriteLine("DocID = " + docID);
            var documentHandling = new DocumentHandling
            {
                DocumentId = docID,
                Handler = GetCurrentUser().FullName,
                Content = input.MoreInformation,
                IsActive = true,
                Order = 1,
                PlaceReceive = input.ApprovedBy,
                TenantId = AbpSession.TenantId,
                Status = input.Status
            };
            await _documentHandlingRepository.InsertAsync(documentHandling);

            var typeHandles = _typeHandleRepository.GetAll().FirstOrDefault(x => x.Name == "Tiếp nhận").Id;

            var documentDetail = new DocumentDetail
            {
                DocumentId = docID,
                DateHandle = DateTime.Now,
                IsActive = true,
                TypeHandle = typeHandles,
                Status = "Văn thư tiếp nhận văn bản"
            };

            await _documentDetailsRepository.InsertAsync(documentDetail);

            string newFileName = date + "/" + docID + "_" + Path.GetFileNameWithoutExtension(input.Attachment) + Path.GetExtension(input.Attachment);
            if (File.Exists("wwwroot/" + input.Attachment))
            {
                File.Move("wwwroot/" + input.Attachment, "wwwroot/" + newFileName);
            }
            input.Attachment = newFileName;
            input.Id = docID;
            await Update(input);

            await _historyUploadRepository.InsertAsync(new HistoryUpload { File = newFileName, Version = 1, documentID = docID });
            
            return docID;
        }

        //public async Task UpdateStarForDocumentByDocId(int documentDetailId, bool isStared)
        //{
        //    var record = _documentDetailsRepository.GetAll().FirstOrDefault(x => x.DocumentId == documentDetailId);
        //    record.IsStared = isStared;
        //    await Update(ObjectMapper.Map<DocumentDetailDto>(record));
        //}

        [AbpAuthorize(AppPermissions.Pages_Documents_Edit)]
        protected virtual async Task Update(DocumentsDto input)
        {
            var documents = await _documentsRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, documents);
        }

        [AbpAuthorize(AppPermissions.Pages_Documents_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _documentsRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetDocumentsesToExcel(GetAllDocumentsesForExcelInput input)
        {

            var filteredDocumentses = _documentsRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.PlaceReceive.Contains(input.Filter) || e.SaveTo.Contains(input.Filter) || e.Summary.Contains(input.Filter) || e.ApprovedBy.Contains(input.Filter) || e.Attachment.Contains(input.Filter) || e.TypeReceive.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.Note.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NumberFilter), e => e.Number == input.NumberFilter)
                        .WhereIf(input.MinDocumentTypeIdFilter != null, e => e.DocumentTypeId >= input.MinDocumentTypeIdFilter)
                        .WhereIf(input.MaxDocumentTypeIdFilter != null, e => e.DocumentTypeId <= input.MaxDocumentTypeIdFilter)
                        .WhereIf(input.MinIncommingNumberFilter != null, e => e.IncommingNumber >= input.MinIncommingNumberFilter)
                        .WhereIf(input.MaxIncommingNumberFilter != null, e => e.IncommingNumber <= input.MaxIncommingNumberFilter)
                        .WhereIf(input.MinPagesFilter != null, e => e.Pages >= input.MinPagesFilter)
                        .WhereIf(input.MaxPagesFilter != null, e => e.Pages <= input.MaxPagesFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PlaceRecevieFilter), e => e.PlaceReceive.ToLower() == input.PlaceRecevieFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SaveToFilter), e => e.SaveTo.ToLower() == input.SaveToFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SummaryFilter), e => e.Summary.ToLower() == input.SummaryFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ApprovedByFilter), e => e.ApprovedBy.ToLower() == input.ApprovedByFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AttachmentFilter), e => e.Attachment.ToLower() == input.AttachmentFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TypeReceiveFilter), e => e.TypeReceive.ToLower() == input.TypeReceiveFilter.ToLower().Trim())
                        .WhereIf(input.MinStartDateFilter != null, e => e.StartDate >= input.MinStartDateFilter)
                        .WhereIf(input.MaxStartDateFilter != null, e => e.StartDate <= input.MaxStartDateFilter)
                        .WhereIf(input.MinEndDateFilter != null, e => e.EndDate >= input.MinEndDateFilter)
                        .WhereIf(input.MaxEndDateFilter != null, e => e.EndDate <= input.MaxEndDateFilter)
                        .WhereIf(input.PriorityFilter != null, e => e.Priority <= input.PriorityFilter)
                        .WhereIf(input.IncommingDateFilter != null, e => e.IncommingDate <= input.IncommingDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RangeFilter), e => e.Range == input.RangeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PositionFilter), e => e.Position == input.PositionFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter), e => e.Status.ToLower() == input.StatusFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NoteFilter), e => e.Note.ToLower() == input.NoteFilter.ToLower().Trim());

            var query = (from o in filteredDocumentses
                         select new GetDocumentsForViewDto()
                         {
                             Documents = new DocumentsDto
                             {
                                 Number = o.Number,
                                 DocumentTypeId = o.DocumentTypeId,
                                 PlaceReceive = o.PlaceReceive,
                                 SaveTo = o.SaveTo,
                                 Summary = o.Summary,
                                 ApprovedBy = o.ApprovedBy,
                                 Attachment = o.Attachment,
                                 TypeReceive = o.TypeReceive,
                                 StartDate = o.StartDate,
                                 EndDate = o.EndDate,
                                 Status = o.Status,
                                 Note = o.Note,
                                 Id = o.Id,
                                 Priority = o.Priority,
                                 IncommingNumber = o.IncommingNumber,
                                 IncommingDate = o.IncommingDate,
                                 Pages = o.Pages,
                                 Author = o.Author,
                                 GroupAuthor = o.GroupAuthor,
                                 IsActive = o.IsActive,
                                 MoreInformation = o.MoreInformation,
                                 Order = o.Order,
                                 Position = o.Position,
                                 Range = o.Range,
                                 LinkedDocument = o.LinkedDocument
                             }
                         });


            var documentsListDtos = await query.ToListAsync();

            return _documentsesExcelExporter.ExportToFile(documentsListDtos);
        }

        public async Task<List<DocumentDetailDto>> GetAllDocumentDetailWithId(int id)
        {
            var result = await _documentDetailsRepository.GetAll().Where(x => x.DocumentId == id).ToListAsync();
            return ObjectMapper.Map<List<DocumentDetailDto>>(result);
        }

        public async Task<PagedResultDto<DocumentsDto>> GetAllActiveDocument()
        {
            var result = await _documentsRepository.GetAll().Where(x => x.IsActive == true).OrderBy("id asc").ToListAsync();

            return new PagedResultDto<DocumentsDto>(
                result.Count(),
                ObjectMapper.Map<List<DocumentsDto>>(result)
            );
        }

        public async Task<List<DocumentDetailDto>> GetAllDocumentDetailAsId(int id)
        {
            var result = await _documentDetailsRepository.GetAll().Where(x => x.IsActive == true && x.DocumentId == id).OrderBy("creationTime desc").ToListAsync();

            return ObjectMapper.Map<List<DocumentDetailDto>>(result);
        }

        public async Task<int> GetNextIncommingNumber()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if(conn.State == ConnectionState.Closed)
                    {
                        await conn.OpenAsync();
                    }
                    var num = await conn.QueryAsync<int>(sql: "dbo.GetNextIncommingNumber", commandType: CommandType.StoredProcedure);
                    return num.AsQueryable().First();
                }
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        //public async Task<GetDataAndColumnConfig> GetAllTableConfig()
        //{
        //    var sqlConfig = _sqlConfigsAppService.GetSqlConfigByCodeAsync("QLVB_IncommingDoc_Not_Processed").Result;
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        if (conn.State == ConnectionState.Closed)
        //            await conn.OpenAsync();
        //        var columnConfig = _sqlConfigDetailsAppService.GetColumnConfigBySqlId(sqlConfig.Id);

        //        var filterVanbans = conn.Query<object>(sqlConfig.SqlContent).ToList();

        //        return new GetDataAndColumnConfig(filterVanbans, columnConfig);
        //    }
        //}

        public async Task<List<DocumentsDto>> GetAllIncommingDocumentNotProcessed()
        {
            var tenantId = AbpSession.TenantId;
            var userId = AbpSession.UserId;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        await conn.OpenAsync();
                    }
                    var query = await conn.QueryAsync<Documents>(sql: "dbo.GetAllIncommingDocumentNotProcessed_NEW", param: new { userId, tenantId }, commandType: CommandType.StoredProcedure);
                    var result = ObjectMapper.Map<List<DocumentsDto>>(query.ToList());
                    result.Sort((a, b) => a.IncommingNumber.CompareTo(b.IncommingNumber));
                    for(int i = 0; i < result.Count; i++)
                    {
                        result[i].STT = i + 1;
                    }
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<DocumentsDto>> GetAllIncommingDocumentNotTransfered_VanThu()
        {
            var tenantId = AbpSession.TenantId;
            var userId = AbpSession.UserId;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        await conn.OpenAsync();
                    }
                    var query = await conn.QueryAsync<Documents>(sql: "dbo.GetAllIncommingDocumentNotProcessed", param: new { userId, tenantId }, commandType: CommandType.StoredProcedure);
                    var result = ObjectMapper.Map<List<DocumentsDto>>(query.ToList());
                    result.Sort((a, b) => a.IncommingNumber.CompareTo(b.IncommingNumber));
                    for (int i = 0; i < result.Count; i++)
                    {
                        result[i].STT = i + 1;
                    }
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<CounterDto>> GetNumberOfAllDocumentType()
        {
            try
            {
                //var tenantId = AbpSession.TenantId;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    List<CounterDto> result = new List<CounterDto>();

                    if (conn.State == ConnectionState.Closed)
                    {
                        await conn.OpenAsync();
                    }
                    var statusTypes = await conn.QueryAsync<DocumentStatus>(sql: "SELECT * FROM dbo.CA_DocumentStatus");
                    foreach(var type in statusTypes.ToList())
                    {
                        var count = await conn.QueryAsync<int>(sql: "SELECT COUNT(*) FROM dbo.CA_Document WHERE Action = " + type.Id);
                        result.Add(new CounterDto { Code = type.Value, Number = count.AsQueryable().First() });
                    }

                    
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<DocumentHandlingDto>> GetAllDocumentDetailByDocumentId(int documentId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var query = await conn.QueryAsync<DocumentHandling>(sql: "SELECT * FROM dbo.CA_DocumentHandling WHERE DocumentId = " + documentId);
                return ObjectMapper.Map<List<DocumentHandlingDto>>(query.ToList());
            }
        }

        public async Task<List<Document_Waitting>>GetListDocumentNeedToComplete()
        {
            var tenantId = AbpSession.TenantId;
            var userId = AbpSession.UserId;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        await conn.OpenAsync();
                    }
                    var query = await conn.QueryAsync(sql: "dbo.GetListDocumentNeedToComplete", param: new { userId, tenantId }, commandType: CommandType.StoredProcedure);
                    var result = ObjectMapper.Map<List<Document_Waitting>>(query.ToList());
                    result.Sort((a, b) => a.IncommingNumber.CompareTo(b.IncommingNumber));
                    for (int i = 0; i < result.Count; i++)
                    {
                        result[i].STT = i + 1;
                    }

                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task UpdateStatusOfDocumentIntoTransfered(int documentId, string type)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                await conn.QueryAsync(sql: "dbo.ChangeActionOfDocument", param: new { documentId, type }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> CreateAndReturnId(DocumentsDto input)
        {
            var result = await Create(input);
            return result;
        }

        public async Task<List<DocumentsDto>> GetAllDocumentWaitingHandling()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<Documents>(sql: "dbo.GetAllDocumentWaitingHandling", param: new { AbpSession.UserId, AbpSession.TenantId }, commandType: CommandType.StoredProcedure);
                return ObjectMapper.Map<List<DocumentsDto>>(result.ToList());
            }
        }
    }
}
