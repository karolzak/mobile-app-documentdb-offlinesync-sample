using System;
using Newtonsoft.Json;
using MobileAppDocDBOfflineSyncSample.Shared.DataModels;

namespace MobileAppDocDBOfflineSyncSample.Droid
{

	public class ToDoItemDocDbWrapper : Java.Lang.Object
	{
		public ToDoItemDocDbWrapper(ToDoItemDocDb item)
		{
			ToDoItem = item;
		}

		public ToDoItemDocDb ToDoItem { get; private set; }
	}
}

