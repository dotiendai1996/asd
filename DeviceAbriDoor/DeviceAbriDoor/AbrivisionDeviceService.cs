using DeviceAbriDoor.Utils;
using ScheduledService.Schedules;
using System;
using System.ServiceProcess;
using TestDeviceAbriDoor;

namespace DeviceAbriDoor
{
    partial class AbrivisionDeviceService : ServiceBase
    {
        public AbrivisionDeviceService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                SchedulerUtils.Instance.StartAsync();
            }
            catch (Exception ex)
            {
                LogUtils.WirteLogDebug("AbrivisionDeviceService Error", ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                SchedulerUtils.Instance.ShutdownAsync();
            }
            catch (Exception ex)
            {
                LogUtils.WirteLogDebug("AbrivisionDeviceService Error", ex);
            }
        }
    }
}
