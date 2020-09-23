

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
	[AbpAuthorize(AppPermissions.Pages_TextBooks)]
	public class TextBooksAppService : HinnovaAppServiceBase, ITextBooksAppService
	{
		private readonly IRepository<TextBook> _textBookRepository;
		private readonly ITextBooksExcelExporter _textBooksExcelExporter;
		private readonly ISqlConfigDetailsAppService _sqlConfigDetailsAppService;
		private readonly ISqlConfigsAppService _sqlConfigsAppService;
		private readonly string connectionString;

		public TextBooksAppService(IRepository<TextBook> textBookRepository, ITextBooksExcelExporter textBooksExcelExporter, IWebHostEnvironment env, ISqlConfigsAppService sqlConfigsAppService, ISqlConfigDetailsAppService sqlConfigDetailsAppService)
		{

			_sqlConfigDetailsAppService = sqlConfigDetailsAppService;
			_sqlConfigsAppService = sqlConfigsAppService;
			connectionString = env.GetAppConfiguration().GetConnectionString("Default");
			_textBookRepository = textBookRepository;
			_textBooksExcelExporter = textBooksExcelExporter;

		}

		public async Task<GetDataAndColumnConfig_TextBook> GetAllTextBook()
		{
			var sqlConfig = _sqlConfigsAppService.GetSqlConfigByCodeAsync("GATB").Result;
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				if (conn.State == ConnectionState.Closed)
					await conn.OpenAsync();
				var columnConfig = _sqlConfigDetailsAppService.GetColumnConfigBySqlId(sqlConfig.Id);

				var filterTextBooks = conn.Query<object>(sqlConfig.SqlContent).ToList();

				return new GetDataAndColumnConfig_TextBook(filterTextBooks, columnConfig);

				//return new DataVm { Code = "200", isSucceeded = dataString.Length > 0 ? true : false, Data = dataString, Message = "Success" };
			}
		}

		public async Task<PagedResultDto<GetTextBookForViewDto>> GetAll(GetAllTextBooksInput input)
         {
			
			var filteredTextBooks = _textBookRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.SoHieuGoc.Contains(input.Filter) || e.CoQuanBanHanh.Contains(input.Filter) || e.TrichYeu.Contains(input.Filter) || e.NguoiChiDao.Contains(input.Filter) || e.Nguoi_DonVi.Contains(input.Filter) || e.FileDinhKem.Contains(input.Filter))
						.WhereIf(input.MinSoDenFilter != null, e => e.SoDen >= input.MinSoDenFilter)
						.WhereIf(input.MaxSoDenFilter != null, e => e.SoDen <= input.MaxSoDenFilter)
						.WhereIf(input.MinNgayDenFilter != null, e => e.NgayDen >= input.MinNgayDenFilter)
						.WhereIf(input.MaxNgayDenFilter != null, e => e.NgayDen <= input.MaxNgayDenFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoHieuGocFilter),  e => e.SoHieuGoc == input.SoHieuGocFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.CoQuanBanHanhFilter),  e => e.CoQuanBanHanh == input.CoQuanBanHanhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TrichYeuFilter),  e => e.TrichYeu == input.TrichYeuFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NguoiChiDaoFilter),  e => e.NguoiChiDao == input.NguoiChiDaoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Nguoi_DonViFilter),  e => e.Nguoi_DonVi == input.Nguoi_DonViFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.FileDinhKemFilter),  e => e.FileDinhKem == input.FileDinhKemFilter);

			var pagedAndFilteredTextBooks = filteredTextBooks
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var textBooks = from o in pagedAndFilteredTextBooks
                         select new GetTextBookForViewDto() {
							TextBook = new TextBookDto
							{
                                SoDen = o.SoDen,
                                NgayDen = o.NgayDen,
                                SoHieuGoc = o.SoHieuGoc,
                                CoQuanBanHanh = o.CoQuanBanHanh,
                                TrichYeu = o.TrichYeu,
                                NguoiChiDao = o.NguoiChiDao,
                                Nguoi_DonVi = o.Nguoi_DonVi,
                                FileDinhKem = o.FileDinhKem,
                                Id = o.Id
							}
						};

            var totalCount = await filteredTextBooks.CountAsync();

            return new PagedResultDto<GetTextBookForViewDto>(
                totalCount,
                await textBooks.ToListAsync()
            );
         }
		 
		 public async Task<GetTextBookForViewDto> GetTextBookForView(int id)
         {
            var textBook = await _textBookRepository.GetAsync(id);

            var output = new GetTextBookForViewDto { TextBook = ObjectMapper.Map<TextBookDto>(textBook) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_TextBooks_Edit)]
		 public async Task<GetTextBookForEditOutput> GetTextBookForEdit(EntityDto input)
         {
            var textBook = await _textBookRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetTextBookForEditOutput {TextBook = ObjectMapper.Map<CreateOrEditTextBookDto>(textBook)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditTextBookDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_TextBooks_Create)]
		 protected virtual async Task Create(CreateOrEditTextBookDto input)
         {
            var textBook = ObjectMapper.Map<TextBook>(input);

			
			//if (AbpSession.TenantId != null)
			//{
			//	textBook.TenantId = (int?) AbpSession.TenantId;
			//}
		

            await _textBookRepository.InsertAsync(textBook);
         }

		 [AbpAuthorize(AppPermissions.Pages_TextBooks_Edit)]
		 protected virtual async Task Update(CreateOrEditTextBookDto input)
         {
            var textBook = await _textBookRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, textBook);
         }

		 [AbpAuthorize(AppPermissions.Pages_TextBooks_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _textBookRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetTextBooksToExcel(GetAllTextBooksForExcelInput input)
         {
			
			var filteredTextBooks = _textBookRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.SoHieuGoc.Contains(input.Filter) || e.CoQuanBanHanh.Contains(input.Filter) || e.TrichYeu.Contains(input.Filter) || e.NguoiChiDao.Contains(input.Filter) || e.Nguoi_DonVi.Contains(input.Filter) || e.FileDinhKem.Contains(input.Filter))
						.WhereIf(input.MinSoDenFilter != null, e => e.SoDen >= input.MinSoDenFilter)
						.WhereIf(input.MaxSoDenFilter != null, e => e.SoDen <= input.MaxSoDenFilter)
						.WhereIf(input.MinNgayDenFilter != null, e => e.NgayDen >= input.MinNgayDenFilter)
						.WhereIf(input.MaxNgayDenFilter != null, e => e.NgayDen <= input.MaxNgayDenFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoHieuGocFilter),  e => e.SoHieuGoc == input.SoHieuGocFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.CoQuanBanHanhFilter),  e => e.CoQuanBanHanh == input.CoQuanBanHanhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TrichYeuFilter),  e => e.TrichYeu == input.TrichYeuFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NguoiChiDaoFilter),  e => e.NguoiChiDao == input.NguoiChiDaoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Nguoi_DonViFilter),  e => e.Nguoi_DonVi == input.Nguoi_DonViFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.FileDinhKemFilter),  e => e.FileDinhKem == input.FileDinhKemFilter);

			var query = (from o in filteredTextBooks
                         select new GetTextBookForViewDto() { 
							TextBook = new TextBookDto
							{
                                SoDen = o.SoDen,
                                NgayDen = o.NgayDen,
                                SoHieuGoc = o.SoHieuGoc,
                                CoQuanBanHanh = o.CoQuanBanHanh,
                                TrichYeu = o.TrichYeu,
                                NguoiChiDao = o.NguoiChiDao,
                                Nguoi_DonVi = o.Nguoi_DonVi,
                                FileDinhKem = o.FileDinhKem,
                                Id = o.Id
							}
						 });


            var textBookListDtos = await query.ToListAsync();

            return _textBooksExcelExporter.ExportToFile(textBookListDtos);
         }


    }
}