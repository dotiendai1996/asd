using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Castle.Core.Logging;
using Dapper;
using Hinnova.Authorization.Users;
using Hinnova.Configuration;
using Hinnova.QLNS;
using Hinnova.QLNS.Dtos;
using Hinnova.Web.Chat.SignalR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Hinnova.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AnnouncementController : HinnovaControllerBase
    {
        private readonly IHubContext<ChatHubMobile> _chatMobileHubContext;

        public AnnouncementController(IHubContext<ChatHubMobile> chatMobileHubContext)
        {
            _chatMobileHubContext = chatMobileHubContext;
        }

        #region Mobile

        public async Task SendMessageToClient(AnnouncementDto message, string userReceiveMessage)
        {
            try
            {
                await _chatMobileHubContext.Clients.User(userReceiveMessage)
                    .SendAsync("ReceiveMessage", message);
            }
            catch (Exception ex)
            {
                Logger.Warn("Internal Server Error");
                Logger.Warn("Could not send chat message to user");
                Logger.Warn(ex.ToString(), ex);
            }


        }
        #endregion
    }
}