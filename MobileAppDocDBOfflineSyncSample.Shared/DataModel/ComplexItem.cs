
using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace MobileAppDocDBOfflineSyncSample.Shared.DataModel
{
    public class ComplexItem 
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }

        [JsonProperty(PropertyName = "nestedItems")]
        public ICollection<NestedItem> NestedItems { get; set; }

        public string Id { get; set; }

        [JsonProperty(PropertyName = "nested")]
        public NestedItem Nested { get; set; }
        
        [JsonProperty(PropertyName = "version")]
        public byte[] Version { get; set; }
        
        [JsonProperty(PropertyName = "createdAt")]
        public DateTimeOffset? CreatedAt { get; set; }
        
        [JsonProperty(PropertyName = "updatedAt")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; set; }


    }
}