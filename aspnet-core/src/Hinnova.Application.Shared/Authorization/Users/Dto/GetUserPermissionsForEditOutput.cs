using System.Collections.Generic;
using Hinnova.Authorization.Permissions.Dto;

namespace Hinnova.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}