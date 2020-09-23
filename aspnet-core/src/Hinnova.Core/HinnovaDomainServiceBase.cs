using Abp.Domain.Services;

namespace Hinnova
{
    public abstract class HinnovaDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected HinnovaDomainServiceBase()
        {
            LocalizationSourceName = HinnovaConsts.LocalizationSourceName;
        }
    }
}
