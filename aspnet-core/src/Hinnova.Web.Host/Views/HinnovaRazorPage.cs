using Abp.AspNetCore.Mvc.Views;

namespace Hinnova.Web.Views
{
    public abstract class HinnovaRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected HinnovaRazorPage()
        {
            LocalizationSourceName = HinnovaConsts.LocalizationSourceName;
        }
    }
}
