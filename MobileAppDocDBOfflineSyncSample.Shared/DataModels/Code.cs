using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppDocDBOfflineSyncSample.Shared.DataModels
{
    public class Code
    {

        [JsonProperty(PropertyName = "colorId")]
        public string ColorId { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "system")]
        public string System { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

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
