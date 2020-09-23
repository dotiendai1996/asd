using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Hinnova.Configuration.Tenants.Dto;

namespace Hinnova.Web.Areas.App.Models.Settings
{
    public class SettingsViewModel
    {
        public TenantSettingsEditDto Settings { get; set; }
        
        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}