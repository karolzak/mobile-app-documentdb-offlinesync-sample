using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppDocDBOfflineSyncSample.Shared.DataModel
{
    public class DoubleNestedItem
    {
        [JsonProperty(PropertyName = "doubleNestedBool")]
        public bool DoubleNestedBool { get; set; }
    }
}
