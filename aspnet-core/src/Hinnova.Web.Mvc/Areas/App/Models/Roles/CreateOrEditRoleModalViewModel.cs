using Abp.AutoMapper;
using Hinnova.Authorization.Roles.Dto;
using Hinnova.Web.Areas.App.Models.Common;

namespace Hinnova.Web.Areas.App.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode => Role.Id.HasValue;
    }
}