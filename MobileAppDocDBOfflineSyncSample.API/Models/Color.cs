using Microsoft.Azure.Documents;
using Microsoft.Azure.Mobile.Server.Tables;
using MobileAppDocDBOfflineSyncSampleService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileAppDocDBOfflineSyncSampleService.Models
{
    public class Color : DocumentResource, ITableData
    {
        public string Name { get; set; }

        public string Business { get; set; }

        public string Market { get; set; }

        public List<Code> Codes { get; set; }
    }
}
