using System;

namespace Hinnova.Utils.EmailSenders
{
    public class EmailSender
    {
        public string FileName { get; set; }
        public long Size { get; set; }
        public DateTime CreateDT { get; set; }
        public int Request { get; set; }
    }
}
