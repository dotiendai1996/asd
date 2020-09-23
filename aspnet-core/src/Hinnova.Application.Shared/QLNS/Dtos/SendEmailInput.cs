using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.QLNS.Dtos
{
    public  class  SendEmailInput
    {
        public string mailTo { get; set; }
        public string mailForm { get; set; }
        public string subject { get; set; }

        public string body { get; set; }
        public string filedinhkem { get; set; }
    }
}
