

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
	[AbpAuthorize(AppPermissions.Pages_TypeHandes)]
    public class TypeHandesAppService : HinnovaAppServiceBase, ITypeHandesAppService
    {
		 private readonly IRepository<TypeHandle> _typeHandeRepository;
		 private readonly ITypeHandesExcelExporter _typeHandesExcelExporter;
		 

		  public TypeHandesAppService(IRepository<TypeHandle> typeHandeRepository, ITypeHandesExcelExporter typeHandesExcelExporter ) 
		  {
			_typeHandeRepository = typeHandeRepository;
			_typeHandesExcelExporter = typeHandesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetTypeHandeForViewDto>> GetAll(GetAllTypeHandesInput input)
         {
			
			var filteredTypeHandes = _typeHandeRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name.ToLower() == input.NameFilter.ToLower().Trim());

			var pagedAndFilteredTypeHandes = filteredTypeHandes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var typeHandes = from o in pagedAndFilteredTypeHandes
                         select new GetTypeHandeForViewDto() {
							TypeHande = new TypeHandeDto
							{
                                Name = o.Name,
                                Id = o.Id
							}
						};

            var totalCount = await filteredTypeHandes.CountAsync();

            return new PagedResultDto<GetTypeHandeForViewDto>(
                totalCount,
                await typeHandes.ToListAsync()
            );
         }
		 
		 public async Task<GetTypeHandeForViewDto> GetTypeHandeForView(int id)
         {
            var typeHande = await _typeHandeRepository.GetAsync(id);

            var output = new GetTypeHandeForViewDto { TypeHande = ObjectMapper.Map<TypeHandeDto>(typeHande) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_TypeHandes_Edit)]
		 public async Task<GetTypeHandeForEditOutput> GetTypeHandeForEdit(EntityDto input)
         {
            var typeHande = await _typeHandeRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetTypeHandeForEditOutput {TypeHande = ObjectMapper.Map<CreateOrEditTypeHandeDto>(typeHande)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditTypeHandeDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_TypeHandes_Create)]
		 protected virtual async Task Create(CreateOrEditTypeHandeDto input)
         {
            var typeHande = ObjectMapper.Map<TypeHandle>(input);

			
			if (AbpSession.TenantId != null)
			{
				typeHande.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _typeHandeRepository.InsertAsync(typeHande);
         }

		 [AbpAuthorize(AppPermissions.Pages_TypeHandes_Edit)]
		 protected virtual async Task Update(CreateOrEditTypeHandeDto input)
         {
            var typeHande = await _typeHandeRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, typeHande);
         }

		 [AbpAuthorize(AppPermissions.Pages_TypeHandes_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _typeHandeRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetTypeHandesToExcel(GetAllTypeHandesForExcelInput input)
         {
			
			var filteredTypeHandes = _typeHandeRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name.ToLower() == input.NameFilter.ToLower().Trim());

			var query = (from o in filteredTypeHandes
                         select new GetTypeHandeForViewDto() { 
							TypeHande = new TypeHandeDto
							{
                                Name = o.Name,
                                Id = o.Id
							}
						 });


            var typeHandeListDtos = await query.ToListAsync();

            return _typeHandesExcelExporter.ExportToFile(typeHandeListDtos);
         }


    }
}