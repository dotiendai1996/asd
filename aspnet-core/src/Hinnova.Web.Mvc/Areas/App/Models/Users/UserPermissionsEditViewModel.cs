using Abp.AutoMapper;
using Hinnova.Authorization.Users;
using Hinnova.Authorization.Users.Dto;
using Hinnova.Web.Areas.App.Models.Common;

namespace Hinnova.Web.Areas.App.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; set; }
    }
}