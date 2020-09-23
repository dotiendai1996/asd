using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Hinnova.Authorization.Users;
using Hinnova.MultiTenancy;

namespace Hinnova.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}