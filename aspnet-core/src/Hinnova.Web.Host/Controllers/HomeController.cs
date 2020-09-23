using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Timing;
using Hinnova.Management;
using Hinnova.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Hinnova.Web.Controllers
{
    public class HomeController : HinnovaControllerBase
    {
        private readonly IRepository<Menu> _menuRepository;
        private readonly IAppNotifier _appNotifier;
        public HomeController(IRepository<Menu> menuRepository, IAppNotifier appNotifier)
        {
            _menuRepository = menuRepository;
            _appNotifier = appNotifier;
        }

        [DisableAuditing]
        public async Task<IActionResult> Index()
        {
          
            //if (AbpSession.UserId != null)
            //{
            //    //Logger.Error("UserID "+ AbpSession.UserId.Value);
            //    var userIdentifier = new UserIdentifier(AbpSession.TenantId, AbpSession.UserId.Value);
            //    //string message = "";

            //    string severity = "info";

            //    var message = "Thông báo: " + Clock.Now;

            //    await _appNotifier.SendMessageAsync(
            //        userIdentifier,
            //        message,
            //        severity.ToPascalCase().ToEnum<NotificationSeverity>()
            //    );
            //}

            var result = _menuRepository.GetAll();
            //Logger.Error(result.Count()+"");
            return RedirectToAction("Index", "Ui");
        }
    }
}
