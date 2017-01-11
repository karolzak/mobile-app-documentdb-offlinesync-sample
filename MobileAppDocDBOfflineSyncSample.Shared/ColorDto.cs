using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppDocDBOfflineSyncSample.Shared
{
    public class ColorDto
    {
        public string Name { get; set; }
        public string Business { get; set; }
        public string Market { get; set; }
        public List<Code> Codes { get; set; }
        public List<int> AvailableForProducts { get; set; }
    }
}
