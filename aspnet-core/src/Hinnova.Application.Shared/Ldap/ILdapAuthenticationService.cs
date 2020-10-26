using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.Ldap
{
    public interface IAuthenticationService
    {
        bool ValidateUser(string domainName, string username, string password);
    }
}
