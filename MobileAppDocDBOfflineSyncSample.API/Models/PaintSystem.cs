using Microsoft.Azure.Documents;
using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppDocDBOfflineSyncSampleService.Models
{
    public class PaintSystem : Document, ITableData
    {
        public DateTime FormulaCreationDate { get; set; }

        public List<FormulaComponent> Components { get; set; }

        public string State { get; set; }

        public byte[] Version { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public bool Deleted { get; set; }
    }
}
