using System.Collections.Generic;
using Abp.Localization;
using Hinnova.Install.Dto;

namespace Hinnova.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}
