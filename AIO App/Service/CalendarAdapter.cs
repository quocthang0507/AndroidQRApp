using AIOApp.CalendarLib;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;

namespace AIOApp.Service
{
	public class CalendarAdapter : BaseAdapter<Calendar>
	{
		private readonly Calendar[] items;
		private readonly Activity context;
		private readonly int layout;
		private readonly int todayId;

		public CalendarAdapter(Activity context, Calendar[] items)
		{
			this.context = context;
			this.layout = Resource.Layout.grid_calendar_layout;
			this.items = items;
		}

		public CalendarAdapter(Activity context, IList<Calendar> items)
		{
			this.context = context;
			this.layout = Resource.Layout.grid_calendar_layout;
			this.items = items.ToArray();
		}

		public CalendarAdapter(Activity context, int layout, Calendar[] items)
		{
			this.context = context;
			this.layout = layout;
			this.items = items;
		}

		public CalendarAdapter(Activity context, int layout, IList<Calendar> items)
		{
			this.context = context;
			this.layout = layout;
			this.items = items.ToArray();
		}

		public CalendarAdapter(Activity context, int layout, Calendar[] items, int todayId)
		{
			this.context = context;
			this.layout = layout;
			this.items = items;
			this.todayId = todayId;
		}

		public CalendarAdapter(Activity context, int layout, IList<Calendar> items, int todayId)
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
			{
				view = context.LayoutInflater.Inflate(layout, null);
			}
			Calendar calendar = items[position];
			// Không hiển thị ngày trống
			if (items[position].LunarDate == null)
			{
				view.FindViewById<TextView>(Resource.Id.txtSolarDay).Text = string.Empty;
				view.FindViewById<TextView>(Resource.Id.txtLunarDay).Text = string.Empty;
			}
			else
			{
				view.FindViewById<TextView>(Resource.Id.txtSolarDay).Text = calendar.PrintedSolarDay.ToString();
				view.FindViewById<TextView>(Resource.Id.txtLunarDay).Text = calendar.PrintedLunarDay.ToString();
			}
			// Hightlight ngày hiện tại
			if (position == todayId)
			{
				view.SetBackgroundColor(Color.LightGray);
			}
			// Đổi màu các ngày lễ
			if (calendar.IsEvent || calendar.SolarDate.DayOfWeek == System.DayOfWeek.Sunday)
			{
				view.FindViewById<TextView>(Resource.Id.txtSolarDay).SetTextColor(Color.Red);
			}
			// Đổi màu các ngày không thuộc tháng đó
			if (calendar.NotInMonth)
			{
				view.FindViewById<TextView>(Resource.Id.txtSolarDay).SetTextColor(Color.LightGray);
				view.FindViewById<TextView>(Resource.Id.txtLunarDay).SetTextColor(Color.LightGray);
			}
			return view;
		}
	}
}