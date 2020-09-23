using Abp.AutoMapper;
using Hinnova.MultiTenancy.Dto;

namespace Hinnova.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}
