using Microsoft.Azure.Documents;
using Microsoft.Azure.Mobile.Server;
using Newtonsoft.Json;
using MobileAppDocDBOfflineSyncSampleService.Helpers;
using MobileAppDocDBOfflineSyncSample.API.DataObjects;
using System.Collections.Generic;

namespace MobileAppDocDBOfflineSyncSampleService.DataObjects
{
    public class ComplexItem : DocumentResource
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }

        [JsonProperty(PropertyName ="nested")]
        public NestedItem Nested { get; set; }


        [JsonProperty(PropertyName = "nestedItems")]
        public ICollection<NestedItem> NestedItems { get; set; }
    }
}