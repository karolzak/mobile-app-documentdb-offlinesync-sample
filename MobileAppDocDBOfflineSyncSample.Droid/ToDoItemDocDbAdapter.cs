using System;
using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using MobileAppDocDBOfflineSyncSample.Shared.DataModels;

namespace MobileAppDocDBOfflineSyncSample.Droid
{
	public class ToDoItemDocDbAdapter : BaseAdapter<ToDoItemDocDb>
	{
		Activity activity;
		int layoutResourceId;
		List<ToDoItemDocDb> items = new List<ToDoItemDocDb> ();

		public ToDoItemDocDbAdapter(Activity activity, int layoutResourceId)
		{
			this.activity = activity;
			this.layoutResourceId = layoutResourceId;
		}

		//Returns the view for a specific item on the list
		public override View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			var row = convertView;
			var currentItem = this [position];
			CheckBox checkBox;

			if (row == null) {
				var inflater = activity.LayoutInflater;
				row = inflater.Inflate (layoutResourceId, parent, false);

				checkBox = row.FindViewById <CheckBox> (Resource.Id.checkToDoItem);

				checkBox.CheckedChange += async (sender, e) => {
					var cbSender = sender as CheckBox;
					if (cbSender != null && cbSender.Tag is ToDoItemDocDbWrapper && cbSender.Checked) {
						cbSender.Enabled = false;
						if (activity is ToDoActivity)
							await ((ToDoActivity)activity).CheckItem ((cbSender.Tag as ToDoItemDocDbWrapper).ToDoItem);
					}
				};
			} else
				checkBox = row.FindViewById <CheckBox> (Resource.Id.checkToDoItem);

			checkBox.Text = currentItem.Text;
			checkBox.Checked = false;
			checkBox.Enabled = true;
			checkBox.Tag = new ToDoItemDocDbWrapper (currentItem);

			return row;
		}

		public void Add (ToDoItemDocDb item)
		{
			items.Add (item);
			NotifyDataSetChanged ();
		}

		public void Clear ()
		{
			items.Clear ();
			NotifyDataSetChanged ();
		}

		public void Remove (ToDoItemDocDb item)
		{
			items.Remove (item);
			NotifyDataSetChanged ();
		}

		#region implemented abstract members of BaseAdapter

		public override long GetItemId (int position)
		{
			return position;
		}

		public override int Count {
			get {
				return items.Count;
			}
		}

		public override ToDoItemDocDb this [int position] {
			get {
				return items [position];
			}
		}

		#endregion
	}
}

