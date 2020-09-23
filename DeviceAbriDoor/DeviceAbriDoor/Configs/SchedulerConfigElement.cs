using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceAbriDoor.Configs
{
    public class SchedulerConfigElement : ConfigurationElement
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


        [ConfigurationProperty("time-start-job", IsRequired = true)]
        public String TimeStartJob
        {
            get
            {
                return (String)this["time-start-job"];
            }
            set
            {
                if ((String)this["time-start-job"] != value)
                {
                    this["time-start-job"] = value;
                }
            }
        }


        [ConfigurationProperty("hours-repeat", IsRequired = true)]
        public int HoursRepeat
        {
            get
            {
                return (int)this["hours-repeat"];
            }
            set
            {
                if ((int)this["hours-repeat"] != value)
                {
                    this["hours-repeat"] = value;
                }
            }
        }
    }
}
