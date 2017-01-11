using Microsoft.Azure.Documents;
using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileAppDocDBOfflineSyncSampleService.Models
{
    public class Color : Document, ITableData
    {
        public string Name { get; set; }
        public string Business { get; set; }
        public string Market { get; set; }
        public List<Code> Codes { get; set; }

        public byte[] Version { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public bool Deleted { get; set; }
    }
}
