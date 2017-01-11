using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppDocDBOfflineSyncSample.Shared
{
    public class FormulaDto
    {
        public string Name { get; set; }
        public int ProductId { get; set; }
        public DateTime Version { get; set; }
        public List<Layer> Layers { get; set; }
    }
}
