

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
using Hinnova.Authorization.Users;

namespace Hinnova.QLVB
{
    [AbpAuthorize(AppPermissions.Pages_Administration_DynamicActions)]
    public class DynamicActionsAppService : HinnovaAppServiceBase, IDynamicActionsAppService
    { 
        private readonly IRepository<DynamicAction> _dynamicActionRepository;
        private readonly IDynamicActionsExcelExporter _dynamicActionsExcelExporter;
        private readonly IUserAppService _userAppService;


        public DynamicActionsAppService(IRepository<DynamicAction> dynamicActionRepository, IDynamicActionsExcelExporter dynamicActionsExcelExporter, IUserAppService userAppService)

        {
            _dynamicActionRepository = dynamicActionRepository;
            _dynamicActionsExcelExporter = dynamicActionsExcelExporter;
            _userAppService = userAppService;
        }

        //public async Task<PagedResultDto<GetDynamicActionForViewDto>> GetAll(GetAllDynamicActionsInput input)
        //{

        //    var filteredDynamicActions = _dynamicActionRepository.GetAll()
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Description.Contains(input.Filter))
        //                .WhereIf(input.MinLabelIdFilter != null, e => e.LabelId >= input.MinLabelIdFilter)
        //                .WhereIf(input.MaxLabelIdFilter != null, e => e.LabelId <= input.MaxLabelIdFilter)
        //                .WhereIf(input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
        //                .WhereIf(input.HasSaveFilter > -1, e => (input.HasSaveFilter == 1 && e.HasSave) || (input.HasSaveFilter == 0 && !e.HasSave))
        //                .WhereIf(input.HasReturnFilter > -1, e => (input.HasReturnFilter == 1 && e.HasReturn) || (input.HasReturnFilter == 0 && !e.HasReturn))
        //                .WhereIf(input.HasTransferFilter > -1, e => (input.HasTransferFilter == 1 && e.HasTransfer) || (input.HasTransferFilter == 0 && !e.HasTransfer))
        //                .WhereIf(input.HasSaveAndTransferFilter > -1, e => (input.HasSaveAndTransferFilter == 1 && e.HasSaveAndTransfer) || (input.HasSaveAndTransferFilter == 0 && !e.HasSaveAndTransfer))
        //                .WhereIf(input.HasFinishFilter > -1, e => (input.HasFinishFilter == 1 && e.HasFinish) || (input.HasFinishFilter == 0 && !e.HasFinish))
        //                .WhereIf(input.PositionFilter > 0, e => e.Position == input.PositionFilter)
        //                .WhereIf(input.IsBackFilter > -1, e => (input.IsBackFilter == 1 && e.IsBack) || (input.IsBackFilter == 0 && !e.IsBack))
        //                .WhereIf(input.HasAssignWorkFilter > -1, e => (input.HasAssignWorkFilter == 1 && e.HasAssignWork) || (input.HasAssignWorkFilter == 0 && !e.HasAssignWork))
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description == input.DescriptionFilter)
        //                .WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
        //                .WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter)
        //                .WhereIf(input.HasDeleteFilter > -1, e => e.HasDelete == input.HasDeleteFilter)
        //                ;

        //    var pagedAndFilteredDynamicActions = filteredDynamicActions
        //        .OrderBy(input.Sorting ?? "id asc")
        //        .PageBy(input);

        //    var dynamicActions = from o in pagedAndFilteredDynamicActions
        //                         select new GetDynamicActionForViewDto()
        //                         {
        //                             DynamicAction = new DynamicActionDto
        //                             {
        //                                 LabelId = o.LabelId,
        //                                 IsActive = o.IsActive,
        //                                 HasSave = o.HasSave,
        //                                 HasReturn = o.HasReturn,
        //                                 HasTransfer = o.HasTransfer,
        //                                 HasSaveAndTransfer = o.HasSaveAndTransfer,
        //                                 HasFinish = o.HasFinish,
        //                                 IsTopPosition = o.IsTopPosition,
        //                                 IsBack = o.IsBack,
        //                                 HasAssignWork = o.HasAssignWork,
        //                                 Description = o.Description,
        //                                 Order = o.Order,
        //                                 Id = o.Id
        //                             }
        //                         };

        //    var totalCount = await filteredDynamicActions.CountAsync();

        //    return new PagedResultDto<GetDynamicActionForViewDto>(
        //        totalCount,
        //        await dynamicActions.ToListAsync()
        //    );
        //}

        public async Task<GetDynamicActionForViewDto> GetDynamicActionForView(int id)
        {
            var dynamicAction = await _dynamicActionRepository.GetAsync(id);

            var output = new GetDynamicActionForViewDto { DynamicAction = ObjectMapper.Map<DynamicActionDto>(dynamicAction) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_DynamicActions_Edit)]
        public async Task<GetDynamicActionForEditOutput> GetDynamicActionForEdit(EntityDto input)
        {
            var dynamicAction = await _dynamicActionRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDynamicActionForEditOutput { DynamicAction = ObjectMapper.Map<CreateOrEditDynamicActionDto>(dynamicAction) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditDynamicActionDto input)
        {
            var oldData = await _dynamicActionRepository.GetAll().FirstOrDefaultAsync(x => x.LabelId == input.LabelId && x.RoleId == input.RoleId && x.TenantId == input.TenantId);
            if (oldData == null)
            {
                await Create(input);
            }
            else
            {
                input.Id = oldData.Id;
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_DynamicActions_Create)]
        protected virtual async Task Create(CreateOrEditDynamicActionDto input)
        {
            var dynamicAction = ObjectMapper.Map<DynamicAction>(input);


            if (AbpSession.TenantId != null)
            {
                dynamicAction.TenantId = (int?)AbpSession.TenantId;
            }


            await _dynamicActionRepository.InsertAsync(dynamicAction);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_DynamicActions_Edit)]
        protected virtual async Task Update(CreateOrEditDynamicActionDto input)
        {
            var dynamicAction = await _dynamicActionRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, dynamicAction);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_DynamicActions_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _dynamicActionRepository.DeleteAsync(input.Id);
        }

        //public async Task<FileDto> GetDynamicActionsToExcel(GetAllDynamicActionsForExcelInput input)
        //{

        //    var filteredDynamicActions = _dynamicActionRepository.GetAll()
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Description.Contains(input.Filter))
        //                .WhereIf(input.MinLabelIdFilter != null, e => e.LabelId >= input.MinLabelIdFilter)
        //                .WhereIf(input.MaxLabelIdFilter != null, e => e.LabelId <= input.MaxLabelIdFilter)
        //                .WhereIf(input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
        //                .WhereIf(input.HasSaveFilter > -1, e => (input.HasSaveFilter == 1 && e.HasSave) || (input.HasSaveFilter == 0 && !e.HasSave))
        //                .WhereIf(input.HasReturnFilter > -1, e => (input.HasReturnFilter == 1 && e.HasReturn) || (input.HasReturnFilter == 0 && !e.HasReturn))
        //                .WhereIf(input.HasTransferFilter > -1, e => (input.HasTransferFilter == 1 && e.HasTransfer) || (input.HasTransferFilter == 0 && !e.HasTransfer))
        //                .WhereIf(input.HasSaveAndTransferFilter > -1, e => (input.HasSaveAndTransferFilter == 1 && e.HasSaveAndTransfer) || (input.HasSaveAndTransferFilter == 0 && !e.HasSaveAndTransfer))
        //                .WhereIf(input.HasFinishFilter > -1, e => (input.HasFinishFilter == 1 && e.HasFinish) || (input.HasFinishFilter == 0 && !e.HasFinish))
        //                .WhereIf(input.IsTopPositionFilter > -1, e => (input.IsTopPositionFilter == 1 && e.IsTopPosition) || (input.IsTopPositionFilter == 0 && !e.IsTopPosition))
        //                .WhereIf(input.IsBackFilter > -1, e => (input.IsBackFilter == 1 && e.IsBack) || (input.IsBackFilter == 0 && !e.IsBack))
        //                .WhereIf(input.HasAssignWorkFilter > -1, e => (input.HasAssignWorkFilter == 1 && e.HasAssignWork) || (input.HasAssignWorkFilter == 0 && !e.HasAssignWork))
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description == input.DescriptionFilter)
        //                .WhereIf(input.MinOrderFilter != null, e => e.Order >= input.MinOrderFilter)
        //                .WhereIf(input.MaxOrderFilter != null, e => e.Order <= input.MaxOrderFilter);

        //    var query = (from o in filteredDynamicActions
        //                 select new GetDynamicActionForViewDto()
        //                 {
        //                     DynamicAction = new DynamicActionDto
        //                     {
        //                         LabelId = o.LabelId,
        //                         IsActive = o.IsActive,
        //                         HasSave = o.HasSave,
        //                         HasReturn = o.HasReturn,
        //                         HasTransfer = o.HasTransfer,
        //                         HasSaveAndTransfer = o.HasSaveAndTransfer,
        //                         HasFinish = o.HasFinish,
        //                         IsTopPosition = o.IsTopPosition,
        //                         IsBack = o.IsBack,
        //                         HasAssignWork = o.HasAssignWork,
        //                         Description = o.Description,
        //                         Order = o.Order,
        //                         Id = o.Id
        //                     }
        //                 });


        //    var dynamicActionListDtos = await query.ToListAsync();

        //    return _dynamicActionsExcelExporter.ExportToFile(dynamicActionListDtos);
        //}

        public async Task<DynamicActionDto> GetDynamicActionByLabelId(int labelId)
        {
            //var result = await _dynamicActionRepository.GetAll().Where(x => x.LabelId == labelId).FirstOrDefaultAsync();
            //var user = _abpSession.
            var roleId = await _userAppService.GetRoleIdOfUser((long)AbpSession.UserId);
            var result = await _dynamicActionRepository.GetAll().Where(x => x.LabelId == labelId && x.RoleId == roleId && x.TenantId == AbpSession.TenantId).FirstOrDefaultAsync();
            return ObjectMapper.Map<DynamicActionDto>(result);
        }

        public async Task<CreateOrEditDynamicActionDto> GetAllDynamicActionByLabelId(int labelId, int roleId, int? tenantId)
        {
            if (tenantId == 0) tenantId = null;
            var result = await _dynamicActionRepository.GetAll().Where(x => x.LabelId == labelId && x.RoleId == roleId && x.TenantId == tenantId).FirstOrDefaultAsync();
            return ObjectMapper.Map<CreateOrEditDynamicActionDto>(result);
        }
    }
}