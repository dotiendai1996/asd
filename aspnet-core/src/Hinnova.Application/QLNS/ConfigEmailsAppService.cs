

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLNS.Exporting;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Hinnova.QLNS
{
	[AbpAuthorize(AppPermissions.Pages_ConfigEmails)]
    public class ConfigEmailsAppService : HinnovaAppServiceBase, IConfigEmailsAppService
    {
		 private readonly IRepository<ConfigEmail> _configEmailRepository;
		 private readonly IConfigEmailsExcelExporter _configEmailsExcelExporter;
		 

		  public ConfigEmailsAppService(IRepository<ConfigEmail> configEmailRepository, IConfigEmailsExcelExporter configEmailsExcelExporter ) 
		  {
			_configEmailRepository = configEmailRepository;
			_configEmailsExcelExporter = configEmailsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetConfigEmailForViewDto>> GetAll(GetAllConfigEmailsInput input)
         {
			
			var filteredConfigEmails = _configEmailRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.DiaChiEmail.Contains(input.Filter) || e.TenHienThi.Contains(input.Filter) || e.DiaChiIP.Contains(input.Filter) || e.TenMien.Contains(input.Filter) || e.TenTruyCap.Contains(input.Filter) || e.MatKhau.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiEmailFilter),  e => e.DiaChiEmail == input.DiaChiEmailFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TenHienThiFilter),  e => e.TenHienThi == input.TenHienThiFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiIPFilter),  e => e.DiaChiIP == input.DiaChiIPFilter)
						.WhereIf(input.MinCongSMTPFilter != null, e => e.CongSMTP >= input.MinCongSMTPFilter)
						.WhereIf(input.MaxCongSMTPFilter != null, e => e.CongSMTP <= input.MaxCongSMTPFilter)
						.WhereIf(input.CheckSSLFilter > -1,  e => (input.CheckSSLFilter == 1 && e.CheckSSL) || (input.CheckSSLFilter == 0 && !e.CheckSSL) )
						.WhereIf(input.CheckThongTinFilter > -1,  e => (input.CheckThongTinFilter == 1 && e.CheckThongTin) || (input.CheckThongTinFilter == 0 && !e.CheckThongTin) )
						.WhereIf(!string.IsNullOrWhiteSpace(input.TenMienFilter),  e => e.TenMien == input.TenMienFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TenTruyCapFilter),  e => e.TenTruyCap == input.TenTruyCapFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MatKhauFilter),  e => e.MatKhau == input.MatKhauFilter);

			var pagedAndFilteredConfigEmails = filteredConfigEmails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var configEmails = from o in pagedAndFilteredConfigEmails
                         select new GetConfigEmailForViewDto() {
							ConfigEmail = new ConfigEmailDto
							{
								TenCTY = o.TenCTY,
								DiaChiEmail = o.DiaChiEmail,
                                TenHienThi = o.TenHienThi,
                                DiaChiIP = o.DiaChiIP,
                                CongSMTP = o.CongSMTP,
                                CheckSSL = o.CheckSSL,
                                CheckThongTin = o.CheckThongTin,
                                TenMien = o.TenMien,
                                TenTruyCap = o.TenTruyCap,
                                MatKhau = o.MatKhau,
                                Id = o.Id
							}
						};

            var totalCount = await filteredConfigEmails.CountAsync();

            return new PagedResultDto<GetConfigEmailForViewDto>(
                totalCount,
                await configEmails.ToListAsync()
            );
         }
		 
		 public async Task<GetConfigEmailForViewDto> GetConfigEmailForView(int id)
         {
            var configEmail = await _configEmailRepository.GetAsync(id);

            var output = new GetConfigEmailForViewDto { ConfigEmail = ObjectMapper.Map<ConfigEmailDto>(configEmail) };
			
            return output;
         }

		public async Task<GetConfigEmailForViewDto> GetConfigEmailByTenCtyForView(string tenCongty)
		{
			var configEmail = await _configEmailRepository.FirstOrDefaultAsync(x=>x.TenCTY.ToUpper().Equals(tenCongty.ToUpper()));

			var output = new GetConfigEmailForViewDto { ConfigEmail = ObjectMapper.Map<ConfigEmailDto>(configEmail) };

			return output;
		}

		[AbpAuthorize(AppPermissions.Pages_ConfigEmails_Edit)]
		 public async Task<GetConfigEmailForEditOutput> GetConfigEmailForEdit(EntityDto input)
         {
            var configEmail = await _configEmailRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetConfigEmailForEditOutput {ConfigEmail = ObjectMapper.Map<CreateOrEditConfigEmailDto>(configEmail)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditConfigEmailDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ConfigEmails_Create)]
		 protected virtual async Task Create(CreateOrEditConfigEmailDto input)
         {
            var configEmail = ObjectMapper.Map<ConfigEmail>(input);

			

            await _configEmailRepository.InsertAsync(configEmail);
         }

		 [AbpAuthorize(AppPermissions.Pages_ConfigEmails_Edit)]
		 protected virtual async Task Update(CreateOrEditConfigEmailDto input)
         {
            var configEmail = await _configEmailRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, configEmail);
         }

		 [AbpAuthorize(AppPermissions.Pages_ConfigEmails_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _configEmailRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetConfigEmailsToExcel(GetAllConfigEmailsForExcelInput input)
         {
			
			var filteredConfigEmails = _configEmailRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.DiaChiEmail.Contains(input.Filter) || e.TenHienThi.Contains(input.Filter) || e.DiaChiIP.Contains(input.Filter) || e.TenMien.Contains(input.Filter) || e.TenTruyCap.Contains(input.Filter) || e.MatKhau.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiEmailFilter),  e => e.DiaChiEmail == input.DiaChiEmailFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TenHienThiFilter),  e => e.TenHienThi == input.TenHienThiFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiIPFilter),  e => e.DiaChiIP == input.DiaChiIPFilter)
						.WhereIf(input.MinCongSMTPFilter != null, e => e.CongSMTP >= input.MinCongSMTPFilter)
						.WhereIf(input.MaxCongSMTPFilter != null, e => e.CongSMTP <= input.MaxCongSMTPFilter)
						.WhereIf(input.CheckSSLFilter > -1,  e => (input.CheckSSLFilter == 1 && e.CheckSSL) || (input.CheckSSLFilter == 0 && !e.CheckSSL) )
						.WhereIf(input.CheckThongTinFilter > -1,  e => (input.CheckThongTinFilter == 1 && e.CheckThongTin) || (input.CheckThongTinFilter == 0 && !e.CheckThongTin) )
						.WhereIf(!string.IsNullOrWhiteSpace(input.TenMienFilter),  e => e.TenMien == input.TenMienFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TenTruyCapFilter),  e => e.TenTruyCap == input.TenTruyCapFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MatKhauFilter),  e => e.MatKhau == input.MatKhauFilter);

			var query = (from o in filteredConfigEmails
                         select new GetConfigEmailForViewDto() { 
							ConfigEmail = new ConfigEmailDto
							{
                                DiaChiEmail = o.DiaChiEmail,
                                TenHienThi = o.TenHienThi,
                                DiaChiIP = o.DiaChiIP,
                                CongSMTP = o.CongSMTP,
                                CheckSSL = o.CheckSSL,
                                CheckThongTin = o.CheckThongTin,
                                TenMien = o.TenMien,
                                TenTruyCap = o.TenTruyCap,
                                MatKhau = o.MatKhau,
                                Id = o.Id
							}
						 });


            var configEmailListDtos = await query.ToListAsync();

            return _configEmailsExcelExporter.ExportToFile(configEmailListDtos);
         }


    }
}