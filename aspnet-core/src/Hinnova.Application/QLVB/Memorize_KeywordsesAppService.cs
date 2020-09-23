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
using Microsoft.AspNetCore.Mvc;
using Twilio.Http;
using System.Net.Http.Headers;

namespace Hinnova.QLVB
{
	[AbpAuthorize(AppPermissions.Pages_Memorize_Keywordses)]
	public class Memorize_KeywordsesAppService : HinnovaAppServiceBase, IMemorize_KeywordsesAppService
	{
		private readonly ISqlConfigDetailsAppService _sqlConfigDetailsAppService;
		private readonly ISqlConfigsAppService _sqlConfigsAppService;
		private readonly IRepository<Memorize_Keywords> _memorize_KeywordsRepository;
		private readonly IMemorize_KeywordsesExcelExporter _memorize_KeywordsesExcelExporter;
		private readonly string connectionString;

		public Memorize_KeywordsesAppService(IRepository<Memorize_Keywords> memorize_KeywordsRepository, IMemorize_KeywordsesExcelExporter memorize_KeywordsesExcelExporter, IWebHostEnvironment env, ISqlConfigsAppService sqlConfigsAppService, ISqlConfigDetailsAppService sqlConfigDetailsAppService)
		{
			_sqlConfigDetailsAppService = sqlConfigDetailsAppService;
			_sqlConfigsAppService = sqlConfigsAppService;
			connectionString = env.GetAppConfiguration().GetConnectionString("Default");
			_memorize_KeywordsRepository = memorize_KeywordsRepository;
			_memorize_KeywordsesExcelExporter = memorize_KeywordsesExcelExporter;

		}

		public async Task<GetDataAndColumnConfigMemorize> GetAllMemorize_Keywordses()
		{
			var sqlConfig = _sqlConfigsAppService.GetSqlConfigByCodeAsync("GAMK").Result;
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				if (conn.State == ConnectionState.Closed)
					await conn.OpenAsync();
				var columnConfig = _sqlConfigDetailsAppService.GetColumnConfigBySqlId(sqlConfig.Id);

				var filterVanbans = conn.Query<object>(sqlConfig.SqlContent).ToList();

				return new GetDataAndColumnConfigMemorize(filterVanbans, columnConfig);

				//return new DataVm { Code = "200", isSucceeded = dataString.Length > 0 ? true : false, Data = dataString, Message = "Success" };
			}
		}

		public async Task<PagedResultDto<GetMemorize_KeywordsForViewDto>> GetAll(GetAllMemorize_KeywordsesInput input)
         {
			
			var filteredMemorize_Keywordses = _memorize_KeywordsRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.TenGoiNho.Contains(input.Filter) || e.XuLyChinh.Contains(input.Filter) || e.DongXuLy.Contains(input.Filter) || e.DeBiet.Contains(input.Filter) || e.Full_Name.Contains(input.Filter) || e.Prefix.Contains(input.Filter) || e.KeyWord.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.TenGoiNhoFilter),  e => e.TenGoiNho == input.TenGoiNhoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.XuLyChinhFilter),  e => e.XuLyChinh == input.XuLyChinhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DongXuLyFilter),  e => e.DongXuLy == input.DongXuLyFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DeBietFilter),  e => e.DeBiet == input.DeBietFilter)
						.WhereIf(input.MinHead_IDFilter != null, e => e.Head_ID >= input.MinHead_IDFilter)
						.WhereIf(input.MaxHead_IDFilter != null, e => e.Head_ID <= input.MaxHead_IDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Full_NameFilter),  e => e.Full_Name == input.Full_NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PrefixFilter),  e => e.Prefix == input.PrefixFilter)
						.WhereIf(input.MinHire_DateFilter != null, e => e.Hire_Date >= input.MinHire_DateFilter)
						.WhereIf(input.MaxHire_DateFilter != null, e => e.Hire_Date <= input.MaxHire_DateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyWordFilter),  e => e.KeyWord == input.KeyWordFilter)
						.WhereIf(input.MinIsActiveFilter != null, e => e.IsActive >= input.MinIsActiveFilter)
						.WhereIf(input.MaxIsActiveFilter != null, e => e.IsActive <= input.MaxIsActiveFilter);

			var pagedAndFilteredMemorize_Keywordses = filteredMemorize_Keywordses
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var memorize_Keywordses = from o in pagedAndFilteredMemorize_Keywordses
                         select new GetMemorize_KeywordsForViewDto() {
							Memorize_Keywords = new Memorize_KeywordsDto
							{
                                TenGoiNho = o.TenGoiNho,
                                XuLyChinh = o.XuLyChinh,
                                DongXuLy = o.DongXuLy,
                                DeBiet = o.DeBiet,
                                Head_ID = o.Head_ID,
                                Full_Name = o.Full_Name,
                                Prefix = o.Prefix,
                                Hire_Date = o.Hire_Date,
                                KeyWord = o.KeyWord,
                                IsActive = o.IsActive,
                                Id = o.Id
							}
						};

            var totalCount = await filteredMemorize_Keywordses.CountAsync();

            return new PagedResultDto<GetMemorize_KeywordsForViewDto>(
                totalCount,
                await memorize_Keywordses.ToListAsync()
            );
         }
		 
		 public async Task<GetMemorize_KeywordsForViewDto> GetMemorize_KeywordsForView(int id)
         {
            var memorize_Keywords = await _memorize_KeywordsRepository.GetAsync(id);

            var output = new GetMemorize_KeywordsForViewDto { Memorize_Keywords = ObjectMapper.Map<Memorize_KeywordsDto>(memorize_Keywords) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Memorize_Keywordses_Edit)]
		 public async Task<GetMemorize_KeywordsForEditOutput> GetMemorize_KeywordsForEdit(EntityDto input)
         {
            var memorize_Keywords = await _memorize_KeywordsRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetMemorize_KeywordsForEditOutput {Memorize_Keywords = ObjectMapper.Map<CreateOrEditMemorize_KeywordsDto>(memorize_Keywords)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditMemorize_KeywordsDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Memorize_Keywordses_Create)]
		 protected virtual async Task Create(CreateOrEditMemorize_KeywordsDto input)
         {
            var memorize_Keywords = ObjectMapper.Map<Memorize_Keywords>(input);

			
			if (AbpSession.TenantId != null)
			{
				memorize_Keywords.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _memorize_KeywordsRepository.InsertAsync(memorize_Keywords);
         }

		 [AbpAuthorize(AppPermissions.Pages_Memorize_Keywordses_Edit)]
		 protected virtual async Task Update(CreateOrEditMemorize_KeywordsDto input)
         {
            var memorize_Keywords = await _memorize_KeywordsRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, memorize_Keywords);
         }

		 [AbpAuthorize(AppPermissions.Pages_Memorize_Keywordses_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _memorize_KeywordsRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetMemorize_KeywordsesToExcel(GetAllMemorize_KeywordsesForExcelInput input)
         {
			
			var filteredMemorize_Keywordses = _memorize_KeywordsRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.TenGoiNho.Contains(input.Filter) || e.XuLyChinh.Contains(input.Filter) || e.DongXuLy.Contains(input.Filter) || e.DeBiet.Contains(input.Filter) || e.Full_Name.Contains(input.Filter) || e.Prefix.Contains(input.Filter) || e.KeyWord.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.TenGoiNhoFilter),  e => e.TenGoiNho == input.TenGoiNhoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.XuLyChinhFilter),  e => e.XuLyChinh == input.XuLyChinhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DongXuLyFilter),  e => e.DongXuLy == input.DongXuLyFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DeBietFilter),  e => e.DeBiet == input.DeBietFilter)
						.WhereIf(input.MinHead_IDFilter != null, e => e.Head_ID >= input.MinHead_IDFilter)
						.WhereIf(input.MaxHead_IDFilter != null, e => e.Head_ID <= input.MaxHead_IDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Full_NameFilter),  e => e.Full_Name == input.Full_NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PrefixFilter),  e => e.Prefix == input.PrefixFilter)
						.WhereIf(input.MinHire_DateFilter != null, e => e.Hire_Date >= input.MinHire_DateFilter)
						.WhereIf(input.MaxHire_DateFilter != null, e => e.Hire_Date <= input.MaxHire_DateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyWordFilter),  e => e.KeyWord == input.KeyWordFilter)
						.WhereIf(input.MinIsActiveFilter != null, e => e.IsActive >= input.MinIsActiveFilter)
						.WhereIf(input.MaxIsActiveFilter != null, e => e.IsActive <= input.MaxIsActiveFilter);

			var query = (from o in filteredMemorize_Keywordses
                         select new GetMemorize_KeywordsForViewDto() { 
							Memorize_Keywords = new Memorize_KeywordsDto
							{
                                TenGoiNho = o.TenGoiNho,
                                XuLyChinh = o.XuLyChinh,
                                DongXuLy = o.DongXuLy,
                                DeBiet = o.DeBiet,
                                Head_ID = o.Head_ID,
                                Full_Name = o.Full_Name,
                                Prefix = o.Prefix,
                                Hire_Date = o.Hire_Date,
                                KeyWord = o.KeyWord,
                                IsActive = o.IsActive,
                                Id = o.Id
							}
						 });


            var memorize_KeywordsListDtos = await query.ToListAsync();

            return _memorize_KeywordsesExcelExporter.ExportToFile(memorize_KeywordsListDtos);
         }


    }
}