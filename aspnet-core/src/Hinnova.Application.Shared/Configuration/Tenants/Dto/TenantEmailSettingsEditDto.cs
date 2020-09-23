using Abp.Auditing;
using Hinnova.Configuration.Dto;

namespace Hinnova.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}