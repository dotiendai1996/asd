using System.Collections.Generic;
using Hinnova.Editions.Dto;

namespace Hinnova.Web.Areas.App.Models.Tenants
{
    public class TenantIndexViewModel
    {
        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }
    }
}