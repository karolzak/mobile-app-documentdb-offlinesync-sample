using System;
using Newtonsoft.Json;

namespace MobileAppDocDBOfflineSyncSample.Droid
{
	public class ToDoItemDocDb
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "complete")]
		public bool Complete { get; set; }
	}

	public class ToDoItemDocDbWrapper : Java.Lang.Object
	{
		public ToDoItemDocDbWrapper(ToDoItemDocDb item)
		{
			ToDoItem = item;
		}

		public ToDoItemDocDb ToDoItem { get; private set; }
	}
}

