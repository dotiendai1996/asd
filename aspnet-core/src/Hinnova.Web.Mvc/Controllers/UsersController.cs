using Abp.AspNetCore.Mvc.Authorization;
using Hinnova.Authorization;
using Hinnova.Storage;
using Abp.BackgroundJobs;
using Abp.Authorization;

namespace Hinnova.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}