using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppDocDBOfflineSyncSample.Shared
{
    public class Variant
    {
        public int Number { get; set; }
        public List<Imitation> Imitations { get; set; }
    }
}
