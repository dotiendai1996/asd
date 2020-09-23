using System;
using System.Configuration;
using System.Reflection;

namespace DeviceAbriDoor.Configs
{
    public class AppConfigSection : ConfigurationSection
    {
        private static AppConfigSection instance = null;
        private static readonly object lob = new object();

        private static string configFilePath = null;
        private const string SectionName = "system";

        public static AppConfigSection GetInstance()
        {
            if (instance == null)
            {
                lock (lob)
                {
                    if (instance == null)
                    {
                        configFilePath = Assembly.GetEntryAssembly().Location;
                        if (configFilePath.EndsWith(".config", StringComparison.InvariantCultureIgnoreCase))
                        {
                            configFilePath = configFilePath.Remove(configFilePath.Length - 7);
                        }

                        Configuration config = ConfigurationManager.OpenExeConfiguration(configFilePath);

                        if (config.Sections[SectionName] == null)
                        {
                            instance = new AppConfigSection();
                            config.Sections.Add(SectionName, instance);
                            config.Save(ConfigurationSaveMode.Modified);
                        }
                        else
                        {
                            instance = (AppConfigSection)config.Sections[SectionName];
                        }
                    }
                }
            }
            return instance;
        }

        public void Save()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(configFilePath);
            AppConfigSection section = (AppConfigSection)config.Sections[SectionName];

            section.WebApi = this.WebApi;
            section.Device = this.Device;
            section.Scheduler = this.Scheduler;
            section.AppMode = this.AppMode;

            config.Save(ConfigurationSaveMode.Modified);
        }

        [ConfigurationProperty("appMode", DefaultValue = "NO", IsRequired = true)]
        [RegexStringValidator("^(Yes|yEs|yeS|YEs|YeS|yES|YES|yes|No|NO|no)$")]
        private string appMode
        {
            get
            {
                return this["appMode"].ToString();
            }
            set
            {
                this["appMode"] = value;
            }
        }
        public bool AppMode
        {
            get
            {
                return appMode.ToUpper().Equals("YES");
            }
            set
            {
                string temp = value ? "YES" : "NO";
                if (!temp.Equals(appMode))
                {
                    appMode = temp;
                }
            }
        }


        [ConfigurationProperty("scheduler", IsRequired = true)]
        public SchedulerConfigElement Scheduler
        {
            get
            {
                return (SchedulerConfigElement)this["scheduler"];
            }
            set
            {
                this["scheduler"] = value;
            }
        }


        [ConfigurationProperty("web-api", IsRequired = true)]
        public WebApiConfigElement WebApi
        {
            get
            {
                return (WebApiConfigElement)this["web-api"];
            }
            set
            {
                this["web-api"] = value;
            }
        }

        [ConfigurationProperty("device", IsRequired = true)]
        public DeviceConfigElement Device
        {
            get
            {
                return (DeviceConfigElement)this["device"];
            }
            set
            {
                this["device"] = value;
            }
        }
    }
}
