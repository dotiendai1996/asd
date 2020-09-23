using DeviceAbriDoor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace DeviceAbriDoor
{
    [RunInstaller(true)]
    public partial class AbrivisionDeviceInstaller : System.Configuration.Install.Installer
    {
        private const string ServiceName = "ChamCongQLNSService";

        public AbrivisionDeviceInstaller()
        {
            InitializeComponent();

            // Set service name, display name, description...
            serviceInstaller1.ServiceName = ServiceName;
            serviceInstaller1.DisplayName = ServiceName;
            serviceInstaller1.Description = "This is the service for get data form Abrivision Device, version 1";

            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;

            // Start service after committed
            serviceProcessInstaller1.Committed += serviceProcessInstaller1_Committed;
        }

        private void serviceProcessInstaller1_Committed(object sender, InstallEventArgs e)
        {
            ServiceController sparking6Service = ServiceController.GetServices().Where(s => s.ServiceName.Equals(ServiceName)).FirstOrDefault();
            if (sparking6Service != null)
            {
                string errorMessage = null;

                Console.WriteLine();
                Console.Write("Trying to start get data form Abrivision Device...");

                if (WindowsServiceUtils.StartService(sparking6Service, out errorMessage))
                {
                    Console.WriteLine("Succeed!");
                }
                else
                {
                    Console.WriteLine("Failed!");
                }
            }
        }
    }
}
