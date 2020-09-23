using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceAbriDoor.Models
{
    public class ResultModel
    {
        public object Result { get; set; }
        public string TargetUrl { get; set; }
        public bool Success { get; set; }
        public ErrorModel Error { get; set; }
        public bool UnAuthorizedRequest { get; set; }
        public bool __Abp { get; set; }
    }
}
