using System;
using System.Globalization;
using System.Management;
using System.ServiceProcess;

namespace DeviceAbriDoor.Utils
{
    public static class WindowsServiceUtils
    {
        public static bool ChangeStartupType(ServiceController service, string startupType)
        {
            const string MethodName = "ChangeStartMode";
            ManagementPath path = new ManagementPath();
            path.Server = ".";
            path.NamespacePath = @"root\CIMV2";
            path.RelativePath = string.Format(CultureInfo.InvariantCulture, "Win32_Service.Name='{FeesService}'", service.ServiceName);

            using (ManagementObject serviceObject = new ManagementObject(path))
            {
                ManagementBaseObject inputParameters = serviceObject.GetMethodParameters(MethodName);
                inputParameters["startmode"] = startupType;
                ManagementBaseObject outputParameters = serviceObject.InvokeMethod(MethodName, inputParameters, null);
                return (uint)outputParameters.Properties["ReturnValue"].Value == 0;
            }
        }

        public static bool StartService(ServiceController service, out string errorMessage)
        {
            TimeSpan timeout = TimeSpan.FromMilliseconds(120000);    // 2 minutes
            bool first = true;

            start_service:
            try
            {
                switch (service.Status)
                {
                    case ServiceControllerStatus.Running:
                    case ServiceControllerStatus.StartPending:
                        break;
                    case ServiceControllerStatus.Stopped:
                        // Start service if it stopped
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        break;
                    default:
                        // Restart service if it in another mode
                        service.Stop();
                        service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        break;
                }

                errorMessage = string.Empty;
                return true;
            }
            catch (System.InvalidOperationException ex)
            {
                if (first)
                {
                    if (ChangeStartupType(service, "Manual"))
                    {
                        first = false;
                        goto start_service;
                    }
                }

                errorMessage = ex.Message;
                return false;
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
            catch (System.ServiceProcess.TimeoutException ex)
            {
                errorMessage = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
