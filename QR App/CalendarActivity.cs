using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using QR.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using static Android.App.DatePickerDialog;
using Calendar = QR.CalendarLib.Calendar;

namespace QR
{
	[Activity(Label = "Calendar", Theme = "@style/AppTheme")]
	public class CalendarActivity : Activity, AdapterView.IOnItemClickListener, IOnDateSetListener
	{
		private LinearLayout header;
		private Button btnSelection;
		private Button btnToday;
		private ImageView btnPrev, btnNext;
		private TextView txtDateDay, txtDisplayDate, txtDateYear;
		private GridView gridView;
		private DateTime userDateTime;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_calendar);

			InitControl();
			ShowCalendar();
		}

		private void InitControl()
		{
			userDateTime = DateTime.Now;

			header = FindViewById<LinearLayout>(Resource.Id.calendar_header);
			btnPrev = FindViewById<ImageView>(Resource.Id.calendar_prev_button);
			btnNext = FindViewById<ImageView>(Resource.Id.calendar_next_button);
			txtDateDay = FindViewById<TextView>(Resource.Id.date_display_day);
			txtDateYear = FindViewById<TextView>(Resource.Id.date_display_year);
			txtDisplayDate = FindViewById<TextView>(Resource.Id.date_display_date);
			btnSelection = FindViewById<Button>(Resource.Id.date_selection);
			btnToday = FindViewById<Button>(Resource.Id.date_display_today);
			gridView = FindViewById<GridView>(Resource.Id.calendar_grid);

			btnPrev.Click += BtnPrev_Click;
			btnNext.Click += BtnNext_Click;
			btnToday.Click += BtnToday_Click;
			btnSelection.Click += BtnSelection_Click;
		}

		private void BtnSelection_Click(object sender, EventArgs e)
		{
			DatePickerDialog datePicker = new DatePickerDialog(this, this, userDateTime.Year, userDateTime.Month, userDateTime.Day);
			datePicker.Show();
		}

		private void BtnToday_Click(object sender, EventArgs e)
		{
			userDateTime = DateTime.Now;
			ShowCalendar();
		}

		private void BtnNext_Click(object sender, EventArgs e)
		{
			userDateTime = userDateTime.AddDays(1);
			ShowCalendar();
		}

		private void BtnPrev_Click(object sender, EventArgs e)
		{
			userDateTime = userDateTime.AddDays(-1);
			ShowCalendar();
		}

		private void ShowCalendar()
		{
			string[] dayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.DayNames;
			txtDateYear.Text = userDateTime.Year.ToString();
			txtDateDay.Text = dayOfWeek[(int)userDateTime.DayOfWeek];
			txtDisplayDate.Text = userDateTime.ToString("dd MMMM");
			PopulateCalendar();
		}

		private void PopulateCalendar()
		{
			DateTime dt = new DateTime(userDateTime.Year, userDateTime.Month, 1);
			int firstDayOfWeekOfMonth = (int)dt.DayOfWeek;
			int days = DateTime.DaysInMonth(userDateTime.Year, userDateTime.Month);
			int userDay = firstDayOfWeekOfMonth + userDateTime.Day - 1;

			List<Calendar> arr = new List<Calendar>();

			for (int i = 0; i < firstDayOfWeekOfMonth; i++)
			{
				arr.Add(new Calendar());
			}
			for (int i = 0; i < days; i++)
			{
				arr.Add(new Calendar(dt.AddDays(i)));
			}

			CustomAdapter arrayAdapter = new CustomAdapter(this, Resource.Layout.grid_calendar_layout, arr, userDay);
			gridView.Adapter = arrayAdapter;
			gridView.OnItemClickListener = this;
		}

		public void OnItemClick(AdapterView parent, View view, int position, long id)
		{

		}

		public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
		{
			userDateTime = new DateTime(year, month + 1, dayOfMonth);
			ShowCalendar();
		}
	}
}