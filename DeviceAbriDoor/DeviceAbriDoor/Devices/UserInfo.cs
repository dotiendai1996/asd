using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDeviceAbriDoor.Devices
{
    public class UserInfo
    {
        public int tz1 = 0;
        public int tz2 = 0;
        public int tz3 = 0;
        private string sTimezone;
        public int iUserID;
        public int iPrivilege;
        public string Name;
        public string Password;
        public bool Enabled;
        public int Group;
        public string data { get; set; }

        public string UserID
        {
            get
            {
                return this.iUserID.ToString().PadLeft(5, '0');
            }
            set
            {
                this.iUserID = int.Parse(value);
            }
        }

        public string Privilege
        {
            get
            {
                if (this.iPrivilege == 0)
                    return "User";
                if (this.iPrivilege == 1)
                    return "Enroller";
                return this.iPrivilege == 2 ? "Admin" : "Supervisor";
            }
        }

        public string Timezone
        {
            set
            {
                this.sTimezone = value;
                string[] strArray = this.sTimezone.Split(':');
                Exception exception;
                try
                {
                    this.tz1 = strArray[0].Length > 2 ? int.Parse(strArray[0].Substring(0, 2)) : int.Parse(strArray[0]);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                try
                {
                    this.tz2 = strArray[1].Length > 2 ? int.Parse(strArray[1].Substring(0, 2)) : int.Parse(strArray[1]);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                try
                {
                    if (strArray[2].Length <= 2)
                        this.tz3 = int.Parse(strArray[2]);
                    else
                        this.tz3 = int.Parse(strArray[2].Substring(0, 2));
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            }
            get
            {
                string str1 = "";
                if (this.tz1 > 0)
                    str1 += this.tz1.ToString();
                string str2 = str1 + ":";
                if (this.tz2 > 0)
                    str2 += this.tz2.ToString();
                string str3 = str2 + ":";
                if (this.tz3 > 0)
                    str3 += this.tz3.ToString();
                return str3;
            }
        }
    }
}
