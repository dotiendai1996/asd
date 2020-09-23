using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceAbriDoor.Configs
{
    public class WebApiConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("enable", DefaultValue = "NO", IsRequired = true)]
        [RegexStringValidator("^(Yes|yEs|yeS|YEs|YeS|yES|YES|yes|No|NO|no)$")]
        private string enable
        {
            get
            {
                return this["enable"].ToString();
            }
            set
            {
                this["enable"] = value;
            }
        }
        public bool Enable
        {
            get
            {
                return enable.ToUpper().Equals("YES");
            }
            set
            {
                string temp = value ? "YES" : "NO";
                if (!temp.Equals(enable))
                {
                    enable = temp;
                }
            }
        }

        
        [ConfigurationProperty("server-address", IsRequired = true)]
        public String ServerAddress
        {
            get
            {
                return (String)this["server-address"];
            }
            set
            {
                if ((String)this["server-address"] != value)
                {
                    this["server-address"] = value;
                }
            }
        }

        
        [ConfigurationProperty("max-result-count", IsRequired = true)]
        public int MaxResultCount
        {
            get
            {
                return (int)this["max-result-count"];
            }
            set
            {
                if ((int)this["max-result-count"] != value)
                {
                    this["max-result-count"] = value;
                }
            }
        }
    }
}
