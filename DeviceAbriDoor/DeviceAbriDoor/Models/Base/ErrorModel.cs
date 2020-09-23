using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceAbriDoor.Models
{
    public class ErrorModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public ValidationErrors ValidationErrors { get; set; }
    }

    public class ValidationErrors
    {
        public string Message { get; set; }
    }
}
