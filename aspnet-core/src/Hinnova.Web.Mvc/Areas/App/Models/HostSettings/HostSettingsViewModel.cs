using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Hinnova.Configuration.Host.Dto;
using Hinnova.Editions.Dto;

namespace Hinnova.Web.Areas.App.Models.HostSettings
{
    public class HostSettingsViewModel
    {
        public HostSettingsEditDto Settings { get; set; }

        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }

        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}