using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Hinnova.Authorization.Permissions.Dto;
using Hinnova.Web.Areas.App.Models.Common;

namespace Hinnova.Web.Areas.App.Models.Roles
{
    public class RoleListViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}