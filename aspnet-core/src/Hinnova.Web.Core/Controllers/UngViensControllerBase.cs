using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Hinnova.Authorization.Users.Dto;
using Hinnova.Storage;
using Abp.BackgroundJobs;
using Hinnova.Authorization;
using System;
using System.IO;
using System.Data;

using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Hinnova.Authorization.Users.Importing;

namespace Hinnova.Web.Controllers
{
    public abstract class UngVienControllerBase : HinnovaControllerBase
    {
        protected readonly IBinaryObjectManager BinaryObjectManager;
        protected readonly IBackgroundJobManager BackgroundJobManager;

        protected UngVienControllerBase(
            IBinaryObjectManager binaryObjectManager,
            IBackgroundJobManager backgroundJobManager)
        {
            BinaryObjectManager = binaryObjectManager;
            BackgroundJobManager = backgroundJobManager;
        }

      

        public async Task ImportFromExcel()
        {
        
        }
    }
}

    
    
