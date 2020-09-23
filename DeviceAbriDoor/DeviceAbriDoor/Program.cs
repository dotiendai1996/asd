using DeviceAbriDoor;
using DeviceAbriDoor.Configs;
using DeviceAbriDoor.Utils;
using ScheduledService.Schedules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDeviceAbriDoor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            var config = AppConfigSection.GetInstance();
            if (config.AppMode)
            {
                // Kiểm tra xem đã có instance nào đang chạy hay không
                string current = Process.GetCurrentProcess().ProcessName;
                Process[] procs = Process.GetProcessesByName(current);
                if (procs.Length > 1)
                {
                    Application.ExitThread();
                    Application.Exit();
                    SchedulerUtils.Instance.ShutdownAsync();
                    return;
                }

                AppDomain.CurrentDomain.UnhandledException += AppDomain_UnhandledException;
                Application.ThreadException += Application_ThreadException;

                try
                {
                    SchedulerUtils.Instance.StartAsync();
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMain());
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    HandleException(ex);
                    return;
                }
            }
            else
            {
                try
                {
                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[]
                    {
                        new AbrivisionDeviceService()
                    };
                    ServiceBase.Run(ServicesToRun);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    return;
                }
            }
        }

        #region Exception Handling

        private static void HandleException(Exception ex)
        {
            if (ex == null)
            {
                return;
            }
            if (ex.GetType() == typeof(ObjectDisposedException))
            {
                return;
            }

            LogUtils.WirteLogDebug("Unexpected Error", ex);
        }

        private static void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException((Exception)e.ExceptionObject);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        #endregion
    }
}
