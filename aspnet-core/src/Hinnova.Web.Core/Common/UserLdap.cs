using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hinnova.Web.Common
{
    public class UserLdap
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string IsEnable { get; set; }
        public string Cn { get; set; }
        public string UserPrincipalName { get; set; }
        public bool IsAdmin { get; set; }
        public string Dn { get; set; }
        public string[] Category { get; set; }
    }
}
