using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Auditing;
using Abp.RealTime;
using Abp.Runtime.Session;
using Abp.UI;
using Castle.Core.Logging;
using Castle.Windsor;
using Hinnova.QLNS.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace Hinnova.Web.Chat.SignalR
{
    public class ChatHubMobile : OnlineClientHubBase
    {
        private readonly IWindsorContainer _windsorContainer;
        private bool _isCallByRelease;
        public ChatHubMobile(IOnlineClientManager onlineClientManager, IClientInfoProvider clientInfoProvider, IWindsorContainer windsorContainer) : base(onlineClientManager, clientInfoProvider)
        {
            _windsorContainer = windsorContainer;
            Logger = NullLogger.Instance;
            AbpSession = NullAbpSession.Instance;
        }
        public async Task SendMessage(AnnouncementDto message)
        {

            try
            {
                await Clients.All.SendAsync("ReceiveMessage", AbpSession.UserId, message);
            }
            catch (UserFriendlyException ex)
            {
                Logger.Warn("Could not send chat message to user");
                Logger.Warn(ex.ToString(), ex);
            }
            catch (Exception ex)
            {
                Logger.Warn("Internal Server Error");
                Logger.Warn("Could not send chat message to user");
                Logger.Warn(ex.ToString(), ex);
            }
        }


        public void Register()
        {
            Logger.Debug("A client is registered: " + Context.ConnectionId);
        }

        protected override void Dispose(bool disposing)
        {
            if (_isCallByRelease)
            {
                return;
            }
            base.Dispose(disposing);
            if (disposing)
            {
                _isCallByRelease = true;
                _windsorContainer.Release(this);
            }
        }
    }
}
