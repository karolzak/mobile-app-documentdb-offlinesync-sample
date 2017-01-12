using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileAppDocDBOfflineSyncSample.API.DataObjects
{
    public class DoubleNestedItem
    {
        [JsonProperty(PropertyName = "doubleNestedBool")]
        public bool DoubleNestedBool { get; set; }
    }
}