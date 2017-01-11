using System;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;

namespace MobileAppDocDBOfflineSyncSample.Droid
{
    public class TodoItemDocDb
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }
        [JsonProperty(PropertyName = "version")]
        public byte[] Version { get; set; }

        [CreatedAt]
        [JsonProperty(PropertyName = "createdat")]
        public DateTimeOffset? CreatedAt { get; set; }

        [UpdatedAt]
        [JsonProperty(PropertyName = "updatedat")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; set; }
    }
}