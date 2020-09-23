using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.Management.Dtos
{
    public class KeywordDetailForView
    {
        public int KeywordId { get; set; }

        public int STT { get; set; }

        public string Keyword { get; set; }

        public int[] UserIds { get; set; }

        public string MainHandling { get; set; }

        public string CoHandling { get; set; }

        public string ToKnow { get; set; }
    }
}
