

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

namespace Hinnova.QLVB
{
	[AbpAuthorize(AppPermissions.Pages_CommandDatasources)]
    public class CommandDatasourcesAppService : HinnovaAppServiceBase, ICommandDatasourcesAppService
    {
		 private readonly IRepository<CommandDatasource> _commandDatasourceRepository;
		 private readonly ICommandDatasourcesExcelExporter _commandDatasourcesExcelExporter;
		 

		  public CommandDatasourcesAppService(IRepository<CommandDatasource> commandDatasourceRepository, ICommandDatasourcesExcelExporter commandDatasourcesExcelExporter ) 
		  {
			_commandDatasourceRepository = commandDatasourceRepository;
			_commandDatasourcesExcelExporter = commandDatasourcesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetCommandDatasourceForViewDto>> GetAll(GetAllCommandDatasourcesInput input)
         {
			
			var filteredCommandDatasources = _commandDatasourceRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Command.Contains(input.Filter) || e.Key.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CommandFilter),  e => e.Command.ToLower() == input.CommandFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter),  e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter),  e => e.Value.ToLower() == input.ValueFilter.ToLower().Trim())
						.WhereIf(input.MinDynamicDatasourceIdFilter != null, e => e.DynamicDatasourceId >= input.MinDynamicDatasourceIdFilter)
						.WhereIf(input.MaxDynamicDatasourceIdFilter != null, e => e.DynamicDatasourceId <= input.MaxDynamicDatasourceIdFilter)
						.WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
						.WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter)
						.WhereIf(input.IsActiveFilter > -1,  e => Convert.ToInt32(e.IsActive) == input.IsActiveFilter );

			var pagedAndFilteredCommandDatasources = filteredCommandDatasources
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var commandDatasources = from o in pagedAndFilteredCommandDatasources
                         select new GetCommandDatasourceForViewDto() {
							CommandDatasource = new CommandDatasourceDto
							{
                                Command = o.Command,
                                Key = o.Key,
                                Value = o.Value,
                                DynamicDatasourceId = o.DynamicDatasourceId,
                                Order = o.Order,
                                IsActive = o.IsActive,
                                Id = o.Id
							}
						};

            var totalCount = await filteredCommandDatasources.CountAsync();

            return new PagedResultDto<GetCommandDatasourceForViewDto>(
                totalCount,
                await commandDatasources.ToListAsync()
            );
         }
		 
		 public async Task<GetCommandDatasourceForViewDto> GetCommandDatasourceForView(int id)
         {
            var commandDatasource = await _commandDatasourceRepository.GetAsync(id);

            var output = new GetCommandDatasourceForViewDto { CommandDatasource = ObjectMapper.Map<CommandDatasourceDto>(commandDatasource) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_CommandDatasources_Edit)]
		 public async Task<GetCommandDatasourceForEditOutput> GetCommandDatasourceForEdit(EntityDto input)
         {
            var commandDatasource = await _commandDatasourceRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetCommandDatasourceForEditOutput {CommandDatasource = ObjectMapper.Map<CreateOrEditCommandDatasourceDto>(commandDatasource)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditCommandDatasourceDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_CommandDatasources_Create)]
		 protected virtual async Task Create(CreateOrEditCommandDatasourceDto input)
         {
            var commandDatasource = ObjectMapper.Map<CommandDatasource>(input);

			
			if (AbpSession.TenantId != null)
			{
				commandDatasource.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _commandDatasourceRepository.InsertAsync(commandDatasource);
         }

		 [AbpAuthorize(AppPermissions.Pages_CommandDatasources_Edit)]
		 protected virtual async Task Update(CreateOrEditCommandDatasourceDto input)
         {
            var commandDatasource = await _commandDatasourceRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, commandDatasource);
         }

		 [AbpAuthorize(AppPermissions.Pages_CommandDatasources_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _commandDatasourceRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetCommandDatasourcesToExcel(GetAllCommandDatasourcesForExcelInput input)
         {
			
			var filteredCommandDatasources = _commandDatasourceRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Command.Contains(input.Filter) || e.Key.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CommandFilter),  e => e.Command.ToLower() == input.CommandFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter),  e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter),  e => e.Value.ToLower() == input.ValueFilter.ToLower().Trim())
						.WhereIf(input.MinDynamicDatasourceIdFilter != null, e => e.DynamicDatasourceId >= input.MinDynamicDatasourceIdFilter)
						.WhereIf(input.MaxDynamicDatasourceIdFilter != null, e => e.DynamicDatasourceId <= input.MaxDynamicDatasourceIdFilter)
						.WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
						.WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter)
						.WhereIf(input.IsActiveFilter > -1,  e => Convert.ToInt32(e.IsActive) == input.IsActiveFilter );

			var query = (from o in filteredCommandDatasources
                         select new GetCommandDatasourceForViewDto() { 
							CommandDatasource = new CommandDatasourceDto
							{
                                Command = o.Command,
                                Key = o.Key,
                                Value = o.Value,
                                DynamicDatasourceId = o.DynamicDatasourceId,
                                Order = o.Order,
                                IsActive = o.IsActive,
                                Id = o.Id
							}
						 });


            var commandDatasourceListDtos = await query.ToListAsync();

            return _commandDatasourcesExcelExporter.ExportToFile(commandDatasourceListDtos);
         }


    }
}