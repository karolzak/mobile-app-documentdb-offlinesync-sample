using System;
using Newtonsoft.Json;

namespace MobileAppDocDBOfflineSyncSample.Droid
{
    public class TodoItemDocDb
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }
    }
}