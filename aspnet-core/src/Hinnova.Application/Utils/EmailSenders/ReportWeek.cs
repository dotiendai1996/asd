using System.Collections.Generic;

namespace Hinnova.Utils.EmailSenders
{
    public class ReportWeek
    {
        public int stt { get; set; }
        public string Keyword { get; set; }
        public Dictionary<string, int> DateRank { get; set; }
    }
}
