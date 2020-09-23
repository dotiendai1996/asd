using System.Collections.Generic;
using Hinnova.Authorization.Permissions.Dto;

namespace Hinnova.Web.Areas.App.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}