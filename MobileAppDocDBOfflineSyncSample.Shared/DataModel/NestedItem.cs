using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MobileAppDocDBOfflineSyncSample.Shared.DataModel
{
    public class NestedItem
    {
        [JsonProperty(PropertyName = "nestedText")]
        public string NestedText { get; set; }

        [JsonProperty(PropertyName = "nestedBool")]
        public bool NestedBool { get; set; }


        [JsonProperty(PropertyName = "doubleNestedItem")]
        public DoubleNestedItem DoubleNestedItem { get; set; }
    }

}