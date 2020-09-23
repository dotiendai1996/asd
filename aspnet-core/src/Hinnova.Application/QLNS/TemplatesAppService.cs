

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLNS.Dtos;
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

namespace Hinnova.QLNS
{
	[AbpAuthorize(AppPermissions.Pages_Administration_Templates)]
    public class TemplatesAppService : HinnovaAppServiceBase, ITemplatesAppService
    {
		 private readonly IRepository<Template> _templateRepository;
		private readonly string connectionString;
		private readonly IWebHostEnvironment _env;

		public TemplatesAppService(IRepository<Template> templateRepository , IWebHostEnvironment hostingEnvironment, IWebHostEnvironment env ) 
		  {
			_templateRepository = templateRepository;
			_env = hostingEnvironment;
			connectionString = env.GetAppConfiguration().GetConnectionString("Default");
		}

		 public async Task<PagedResultDto<GetTemplateForViewDto>> GetAll(GetAllTemplatesInput input)
         {
			
			var filteredTemplates = _templateRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.MaTemplate.Contains(input.Filter) || e.TenTemplate.Contains(input.Filter) || e.LinkTemplate.Contains(input.Filter) || e.GhiChu.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaTemplateFilter),  e => e.MaTemplate == input.MaTemplateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TenTemplateFilter),  e => e.TenTemplate == input.TenTemplateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.LinkTemplateFilter),  e => e.LinkTemplate == input.LinkTemplateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.GhiChuFilter),  e => e.GhiChu == input.GhiChuFilter);

			var pagedAndFilteredTemplates = filteredTemplates
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var templates = from o in pagedAndFilteredTemplates
                         select new GetTemplateForViewDto() {
							Template = new TemplateDto
							{
                                MaTemplate = o.MaTemplate,
                                TenTemplate = o.TenTemplate,
                                LinkTemplate = o.LinkTemplate,
                                GhiChu = o.GhiChu,
                                Id = o.Id
							}
						};

            var totalCount = await filteredTemplates.CountAsync();

            return new PagedResultDto<GetTemplateForViewDto>(
                totalCount,
                await templates.ToListAsync()
            );
         }
		public List<TemplateDto> GetAllTemplate()
		{

			return ObjectMapper.Map<List<TemplateDto>>(_templateRepository.GetAll().ToList());
		}
		public async Task<GetTemplateForViewDto> GetTemplateForView(int id)
         {
            var template = await _templateRepository.GetAsync(id);

            var output = new GetTemplateForViewDto { Template = ObjectMapper.Map<TemplateDto>(template) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_Templates_Edit)]
		 public async Task<GetTemplateForEditOutput> GetTemplateForEdit(EntityDto input)
         {
            var template = await _templateRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetTemplateForEditOutput {Template = ObjectMapper.Map<CreateOrEditTemplateDto>(template)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditTemplateDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Templates_Create)]
		 protected virtual async Task Create(CreateOrEditTemplateDto input)
         {
            var template = ObjectMapper.Map<Template>(input);

			

            await _templateRepository.InsertAsync(template);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Templates_Edit)]
		 protected virtual async Task Update(CreateOrEditTemplateDto input)
         {
            var template = await _templateRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, template);
         }

		public async Task<List<TemplateDto>> GetListTemplate(string TenCty)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				if (conn.State == ConnectionState.Closed)
				{
					await conn.OpenAsync();
				}
				var result = await conn.QueryAsync<TemplateDto>(sql: "SELECT * FROM  Templates WHERE GhiChu=N'" + TenCty + "'");
				return result.ToList();
			}
		}

		public async Task<List<TemplateDto>> CapNhatLink(string TenCty)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				if (conn.State == ConnectionState.Closed)
				{
					await conn.OpenAsync();
				}
				var result = await conn.QueryAsync<TemplateDto>(sql: "SELECT * FROM  Templates WHERE GhiChu=N'" + TenCty + "'");
				return result.ToList();
			}
		}


		public async Task ThayDoiLink(string link , string MaTemplate)
		{

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				if (conn.State == ConnectionState.Closed)
				{
					await conn.OpenAsync();
				}
				await conn.QueryAsync(sql: "dbo.CapNhatLink", param: new {link , MaTemplate }, commandType: CommandType.StoredProcedure);
			}
		}

		[AbpAuthorize(AppPermissions.Pages_Administration_Templates_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _templateRepository.DeleteAsync(input.Id);
         } 
    }
}