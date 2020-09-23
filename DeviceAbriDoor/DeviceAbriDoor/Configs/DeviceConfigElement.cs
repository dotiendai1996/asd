using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceAbriDoor.Configs
{
    public class DeviceConfigElement : ConfigurationElement
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

        
        [ConfigurationProperty("device-address", IsRequired = true)]
        public String DeviceAddress
        {
            get
            {
                return (String)this["device-address"];
            }
            set
            {
                if ((String)this["device-address"] != value)
                {
                    this["device-address"] = value;
                }
            }
        }

        
        [ConfigurationProperty("port", IsRequired = true)]
        public int Port
        {
            get
            {
                return (int)this["port"];
            }
            set
            {
                if ((int)this["port"] != value)
                {
                    this["port"] = value;
                }
            }
        }
    }
}
