using System.Collections.Generic;
using System.IO;

namespace Hinnova.Utils.EmailSenders
{
    public class AttachFile
    {
        public AttachFile()
        {
            this.names = new List<string>();
            this.dataAttachs = new List<MemoryStream>();
        }
        public List<string> names { get; set; }
        public bool isMulti { get; set; }
        public List<MemoryStream> dataAttachs { get; set; }
    }
}
