using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hinnova.Web.Authentication.LDap
{
    public class LDapAuthConfiguration
    {
        /// <summary>
        /// Gets or sets the domain name to use as distinguished name in conjuction with the username
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the TCP port on which the LDAP server is running. 
        /// </summary>
        public int Port { get; set; } = 389;

        /// <summary>
        /// Gets or sets the name of the distinguished.
        /// </summary>
        public string DistinguishedName { get; set; }

        /// <summary>
        /// Gets or sets the search filter.
        /// </summary>
        public string SearchFilter { get; set; }

        /// <summary>
        ///  Gets or sets the search base.
        /// </summary>
        public string SearchBase { get; set; }
    }
}
