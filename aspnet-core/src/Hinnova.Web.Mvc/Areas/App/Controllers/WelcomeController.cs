using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hinnova.Web.Controllers;

namespace Hinnova.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class WelcomeController : HinnovaControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}