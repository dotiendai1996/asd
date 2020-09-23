using Abp.AspNetCore.Mvc.ViewComponents;

namespace Hinnova.Web.Public.Views
{
    public abstract class HinnovaViewComponent : AbpViewComponent
    {
        protected HinnovaViewComponent()
        {
            LocalizationSourceName = HinnovaConsts.LocalizationSourceName;
        }
    }
}