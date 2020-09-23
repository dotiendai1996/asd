using Microsoft.AspNetCore.Mvc;
using Hinnova.Web.Controllers;

namespace Hinnova.Web.Public.Controllers
{
    public class AboutController : HinnovaControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}