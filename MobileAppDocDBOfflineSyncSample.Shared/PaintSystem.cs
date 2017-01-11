using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppDocDBOfflineSyncSample.Shared
{
    public class PaintSystem
    {
        public DateTime Version { get; set; }
        public List<FormulaComponent> Components { get; set; }
        public string State { get; set; }
    }
}
