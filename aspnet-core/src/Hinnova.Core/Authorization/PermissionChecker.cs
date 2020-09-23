using Abp.Authorization;
using Hinnova.Authorization.Roles;
using Hinnova.Authorization.Users;

namespace Hinnova.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
