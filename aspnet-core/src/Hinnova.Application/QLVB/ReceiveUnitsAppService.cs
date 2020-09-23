

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
	[AbpAuthorize(AppPermissions.Pages_ReceiveUnits)]
    public class ReceiveUnitsAppService : HinnovaAppServiceBase, IReceiveUnitsAppService
    {
		 private readonly IRepository<ReceiveUnit> _receiveUnitRepository;
		 private readonly IReceiveUnitsExcelExporter _receiveUnitsExcelExporter;
		 

		  public ReceiveUnitsAppService(IRepository<ReceiveUnit> receiveUnitRepository, IReceiveUnitsExcelExporter receiveUnitsExcelExporter ) 
		  {
			_receiveUnitRepository = receiveUnitRepository;
			_receiveUnitsExcelExporter = receiveUnitsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetReceiveUnitForViewDto>> GetAll(GetAllReceiveUnitsInput input)
         {
			
			var filteredReceiveUnits = _receiveUnitRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Position.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PositionFilter),  e => e.Position.ToLower() == input.PositionFilter.ToLower().Trim())
						.WhereIf(input.IsActiveFilter > -1,  e => Convert.ToInt32(e.IsActive) == input.IsActiveFilter );

			var pagedAndFilteredReceiveUnits = filteredReceiveUnits
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var receiveUnits = from o in pagedAndFilteredReceiveUnits
                         select new GetReceiveUnitForViewDto() {
							ReceiveUnit = new ReceiveUnitDto
							{
                                Name = o.Name,
                                Position = o.Position,
                                IsActive = o.IsActive,
                                Id = o.Id
							}
						};

            var totalCount = await filteredReceiveUnits.CountAsync();

            return new PagedResultDto<GetReceiveUnitForViewDto>(
                totalCount,
                await receiveUnits.ToListAsync()
            );
         }
		 
		 public async Task<GetReceiveUnitForViewDto> GetReceiveUnitForView(int id)
         {
            var receiveUnit = await _receiveUnitRepository.GetAsync(id);

            var output = new GetReceiveUnitForViewDto { ReceiveUnit = ObjectMapper.Map<ReceiveUnitDto>(receiveUnit) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ReceiveUnits_Edit)]
		 public async Task<GetReceiveUnitForEditOutput> GetReceiveUnitForEdit(EntityDto input)
         {
            var receiveUnit = await _receiveUnitRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetReceiveUnitForEditOutput {ReceiveUnit = ObjectMapper.Map<CreateOrEditReceiveUnitDto>(receiveUnit)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditReceiveUnitDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ReceiveUnits_Create)]
		 protected virtual async Task Create(CreateOrEditReceiveUnitDto input)
         {
            var receiveUnit = ObjectMapper.Map<ReceiveUnit>(input);

			
			if (AbpSession.TenantId != null)
			{
				receiveUnit.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _receiveUnitRepository.InsertAsync(receiveUnit);
         }

		 [AbpAuthorize(AppPermissions.Pages_ReceiveUnits_Edit)]
		 protected virtual async Task Update(CreateOrEditReceiveUnitDto input)
         {
            var receiveUnit = await _receiveUnitRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, receiveUnit);
         }

		 [AbpAuthorize(AppPermissions.Pages_ReceiveUnits_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _receiveUnitRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetReceiveUnitsToExcel(GetAllReceiveUnitsForExcelInput input)
         {
			
			var filteredReceiveUnits = _receiveUnitRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Position.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PositionFilter),  e => e.Position.ToLower() == input.PositionFilter.ToLower().Trim())
						.WhereIf(input.IsActiveFilter > -1,  e => Convert.ToInt32(e.IsActive) == input.IsActiveFilter );

			var query = (from o in filteredReceiveUnits
                         select new GetReceiveUnitForViewDto() { 
							ReceiveUnit = new ReceiveUnitDto
							{
                                Name = o.Name,
                                Position = o.Position,
                                IsActive = o.IsActive,
                                Id = o.Id
							}
						 });


            var receiveUnitListDtos = await query.ToListAsync();

            return _receiveUnitsExcelExporter.ExportToFile(receiveUnitListDtos);
         }


    }
}