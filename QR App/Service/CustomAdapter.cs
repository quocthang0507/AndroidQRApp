using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using QR.CalendarLib;
using System.Collections.Generic;
using System.Linq;

namespace QR.Service
{
	public class CustomAdapter : BaseAdapter<Calendar>
	{
		private readonly Calendar[] items;
		private readonly Activity context;
		private readonly int layout;
		private readonly int todayId;

		public CustomAdapter(Activity context, Calendar[] items)
		{
			this.context = context;
			this.layout = Resource.Layout.grid_calendar_layout;
			this.items = items;
		}

		public CustomAdapter(Activity context, IList<Calendar> items)
		{
			this.context = context;
			this.layout = Resource.Layout.grid_calendar_layout;
			this.items = items.ToArray();
		}

		public CustomAdapter(Activity context, int layout, Calendar[] items)
		{
			this.context = context;
			this.layout = layout;
			this.items = items;
		}

		public CustomAdapter(Activity context, int layout, IList<Calendar> items)
		{
			this.context = context;
			this.layout = layout;
			this.items = items.ToArray();
		}

		public CustomAdapter(Activity context, int layout, Calendar[] items, int todayId)
		{
			this.context = context;
			this.layout = layout;
			this.items = items;
			this.todayId = todayId;
		}

		public CustomAdapter(Activity context, int layout, IList<Calendar> items, int todayId)
		{
			this.context = context;
			this.layout = layout;
			this.items = items.ToArray();
			this.todayId = todayId;
		}

		public override Calendar this[int position] => items[position];

		public override int Count => items.Length;

		public override long GetItemId(int position) => position;

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView;
			if (view == null)
				view = context.LayoutInflater.Inflate(layout, null);
			if (items[position].LunarDate == null)
			{
				view.FindViewById<TextView>(Resource.Id.txtSolarDay).Text = string.Empty;
				view.FindViewById<TextView>(Resource.Id.txtLunarDay).Text = string.Empty;
			}
			else
			{
				view.FindViewById<TextView>(Resource.Id.txtSolarDay).Text = items[position].PrintedSolarDay.ToString();
				view.FindViewById<TextView>(Resource.Id.txtLunarDay).Text = items[position].PrintedLunarDay.ToString();
			}
			if (position == todayId)
				view.SetBackgroundColor(Color.LightSteelBlue);
			return view;
		}
	}
}