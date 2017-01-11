using Microsoft.Azure.Documents;
using Microsoft.Azure.Mobile.Server.Tables;
using MobileAppDocDBOfflineSyncSampleService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppDocDBOfflineSyncSampleService.Models
{
    public class PaintSystem : DocumentResource, ITableData
    {
        public DateTime FormulaCreationDate { get; set; }

        public List<FormulaComponent> Components { get; set; }

        public string State { get; set; }
    }
}
