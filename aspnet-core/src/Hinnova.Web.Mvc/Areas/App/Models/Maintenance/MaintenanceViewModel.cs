using System.Collections.Generic;
using Hinnova.Caching.Dto;

namespace Hinnova.Web.Areas.App.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}