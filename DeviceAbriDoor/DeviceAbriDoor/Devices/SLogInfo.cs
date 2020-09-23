using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDeviceAbriDoor.Devices
{
    public class SLogInfo
    {
        public long EnrollNumber { get; set; }
        public long TMachineNumber { get; set; }
        public long Param1 { get; set; }
        public long Param2 { get; set; }
        public long Param3 { get; set; }
        public long Param4 { get; set; }
        public long Operation { get; set; }
        public DateTime Date { get; set; }

        public SLogInfo() { }

        public SLogInfo(
          long enrollNumber,
          long TMachineNumber,
          long param1,
          long param2,
          long param3,
          long param4,
          long operation,
          int year,
          int month,
          int day,
          int hour,
          int minute)
        {
            this.EnrollNumber = enrollNumber;
            this.TMachineNumber = TMachineNumber;
            this.Param1 = param1;
            this.Param2 = param2;
            this.Param3 = param3;
            this.Param4 = param4;
            this.Operation = operation;
            this.Date = new DateTime(year, month, day, hour, minute, 0);
        }
    }
}
