using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.QLNS.Dtos
{
    public class CM_BRANCHDto
    {
        public string Id { get; set; }
        public string FATHER_ID { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BRANCH_NAME { get; set; }
        public string BRANCH_TYPE { get; set; }
        public string AUTH_STATUS { get; set; }
    }
}
