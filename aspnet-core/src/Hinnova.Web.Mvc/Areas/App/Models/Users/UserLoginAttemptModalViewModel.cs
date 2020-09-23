using System.Collections.Generic;
using Hinnova.Authorization.Users.Dto;

namespace Hinnova.Web.Areas.App.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}