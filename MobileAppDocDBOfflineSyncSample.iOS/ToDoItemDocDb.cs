using System;
using Newtonsoft.Json;

namespace MobileAppDocDBOfflineSyncSample.iOS
{
	public class ToDoItemDocDb
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "complete")]
		public bool Complete { get; set; }
        [JsonProperty(PropertyName = "version")]
        public byte[] Version { get; set; }

        [JsonProperty(PropertyName = "createdat")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updatedat")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; set; }
    }
}

