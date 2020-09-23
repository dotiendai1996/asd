

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
using Hinnova.Management;
using Hinnova.Configuration;
using Microsoft.Extensions.Configuration;
using Hinnova.Management.Dtos;
using System.Data.SqlClient;
using System.Data;
using Dapper;


namespace Hinnova.QLVB
{
	[AbpAuthorize(AppPermissions.Pages_HistoryUploads)]
    public class HistoryUploadsAppService : HinnovaAppServiceBase, IHistoryUploadsAppService
    {
		 private readonly IRepository<HistoryUpload> _historyUploadRepository;
		private readonly ISqlConfigDetailsAppService _sqlConfigDetailsAppService;
		private readonly ISqlConfigsAppService _sqlConfigsAppService;
		private readonly string connectionString;


		public HistoryUploadsAppService(IRepository<HistoryUpload> historyUploadRepository, IWebHostEnvironment env, ISqlConfigsAppService sqlConfigsAppService, ISqlConfigDetailsAppService sqlConfigDetailsAppService)
		{
			_historyUploadRepository = historyUploadRepository;
			_sqlConfigDetailsAppService = sqlConfigDetailsAppService;
			_sqlConfigsAppService = sqlConfigsAppService;
			connectionString = env.GetAppConfiguration().GetConnectionString("Default");

		}


		public async Task<GetDataAndColumnConfig_HistoryUploads> GetAllHistoryUploads()
		{
			var sqlConfig = _sqlConfigsAppService.GetSqlConfigByCodeAsync("MHTL").Result;
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				if (conn.State == ConnectionState.Closed)
					await conn.OpenAsync();
				var columnConfig = _sqlConfigDetailsAppService.GetColumnConfigBySqlId(sqlConfig.Id);

				var filterHistoryUploads = conn.Query<object>(sqlConfig.SqlContent).ToList();

				return new GetDataAndColumnConfig_HistoryUploads(filterHistoryUploads , columnConfig);

				//return new DataVm { Code = "200", isSucceeded = dataString.Length > 0 ? true : false, Data = dataString, Message = "Success" };
			}
		}
		public async Task<PagedResultDto<GetHistoryUploadForViewDto>> GetAll(GetAllHistoryUploadsInput input)
         {
			
			var filteredHistoryUploads = _historyUploadRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.File.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.FileFilter),  e => e.File == input.FileFilter)
						.WhereIf(input.MinVersionFilter != null, e => e.Version >= input.MinVersionFilter)
						.WhereIf(input.MaxVersionFilter != null, e => e.Version <= input.MaxVersionFilter)
						.WhereIf(input.MindocumentIDFilter != null, e => e.documentID >= input.MindocumentIDFilter)
						.WhereIf(input.MaxdocumentIDFilter != null, e => e.documentID <= input.MaxdocumentIDFilter);

			var pagedAndFilteredHistoryUploads = filteredHistoryUploads
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var historyUploads = from o in pagedAndFilteredHistoryUploads
                         select new GetHistoryUploadForViewDto() {
							HistoryUpload = new HistoryUploadDto
							{
                                File = o.File,
                                Version = o.Version,
                                documentID = o.documentID,
                                Id = o.Id
							}
						};

            var totalCount = await filteredHistoryUploads.CountAsync();

            return new PagedResultDto<GetHistoryUploadForViewDto>(
                totalCount,
                await historyUploads.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_HistoryUploads_Edit)]
		 public async Task<GetHistoryUploadForEditOutput> GetHistoryUploadForEdit(EntityDto input)
         {
            var historyUpload = await _historyUploadRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetHistoryUploadForEditOutput {HistoryUpload = ObjectMapper.Map<CreateOrEditHistoryUploadDto>(historyUpload)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditHistoryUploadDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_HistoryUploads_Create)]
		 protected virtual async Task Create(CreateOrEditHistoryUploadDto input)
         {
            var historyUpload = ObjectMapper.Map<HistoryUpload>(input);

			
			if (AbpSession.TenantId != null)
			{
				historyUpload.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _historyUploadRepository.InsertAsync(historyUpload);
         }

		 [AbpAuthorize(AppPermissions.Pages_HistoryUploads_Edit)]
		 protected virtual async Task Update(CreateOrEditHistoryUploadDto input)
         {
            var historyUpload = await _historyUploadRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, historyUpload);
         }

		 [AbpAuthorize(AppPermissions.Pages_HistoryUploads_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _historyUploadRepository.DeleteAsync(input.Id);
         }
		public async Task<List<HistoryUploadDto>> GetListAsyncById(GetHistoryUploadListInputById input)
		{
			try
			{

				var historyUpload = ObjectMapper.Map<HistoryUploadDto>(await _historyUploadRepository.GetAsync(input.documentID));
				var historyUploads = new HistoryUploadDto[] { historyUpload };
				//foreach (var item in announcements)
				//{
				//    item.DateTime = item.NgayBaoCao.ToString("dd/MM/yyyy");
				//}
				return historyUploads.ToList();
			}
			catch (Exception)
			{
				return null;
			}
		}
		public async Task<List<HistoryUploadDto>> GetList( int documentID)
		{
			//var tenantId = AbpSession.TenantId;
			//var userId = AbpSession.UserId;
			
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					if (conn.State == ConnectionState.Closed)
					{
						await conn.OpenAsync();
					}
					var query = await conn.QueryAsync<HistoryUpload>(sql: "dbo.GetListHistoryUploadDocID", param: new { documentID }, commandType: CommandType.StoredProcedure);
					var result = ObjectMapper.Map<List<HistoryUploadDto>>(query.ToList());
					
					return result;
				}
			}
			catch
			{
				return null;
			}
		}
	}
}