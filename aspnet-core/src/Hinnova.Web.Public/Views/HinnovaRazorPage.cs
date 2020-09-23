using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Hinnova.Web.Public.Views
{
    public abstract class HinnovaRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected HinnovaRazorPage()
        {
            LocalizationSourceName = HinnovaConsts.LocalizationSourceName;
        }
    }
}
