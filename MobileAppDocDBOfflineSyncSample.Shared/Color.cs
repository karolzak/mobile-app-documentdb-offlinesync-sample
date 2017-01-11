using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileAppDocDBOfflineSyncSample.Shared
{
    public class Color
    {
        public string Name { get; set; }
        public string Business { get; set; }
        public string Market { get; set; }
        public List<Code> Codes { get; set; }
    }
}
