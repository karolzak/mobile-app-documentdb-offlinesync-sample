using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppDocDBOfflineSyncSample.Shared.DataModels
{
    public class Color
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "version")]
        public byte[] Version { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updatedAt")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "business")]
        public string Business { get; set; }

        [JsonProperty(PropertyName = "market")]
        public string Market { get; set; }
        
        public Code Code { get; set; }
    }
}
