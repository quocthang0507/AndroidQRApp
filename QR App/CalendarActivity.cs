using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace QR
{
	public class CalendarActivity : LinearLayout
	{
		private LinearLayout header;
		private Button btnToday;
		private ImageView btnPrev, btnNext;
		private TextView txtDateDay, txtDisplayDate, txtDateYear;
		private GridView gridView;

		public CalendarActivity(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
			inflater.Inflate(Resource.Layout.activity_calendar, );
			InitControl();
		}

		private void InitControl()
		{
			header = FindViewById<LinearLayout>(Resource.Id.calendar_header);
			btnPrev = FindViewById<ImageView>(Resource.Id.calendar_prev_button);
			btnNext = FindViewById<ImageView>(Resource.Id.calendar_next_button);
			txtDateDay = FindViewById<TextView>(Resource.Id.date_display_day);
			txtDateYear = FindViewById<TextView>(Resource.Id.date_display_year);
			txtDisplayDate = FindViewById<TextView>(Resource.Id.date_display_date);
			btnToday = FindViewById<Button>(Resource.Id.date_display_today);
			gridView = FindViewById<GridView>(Resource.Id.calendar_grid);
		}
	}
}