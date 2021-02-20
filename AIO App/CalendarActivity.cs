using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AIOApp.CalendarLib;
using AIOApp.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using static Android.App.DatePickerDialog;
using Calendar = AIOApp.CalendarLib.Calendar;

namespace AIOApp
{
	[Activity(Label = "Calendar", Theme = "@style/AppTheme")]
	public class CalendarActivity : Activity, AdapterView.IOnItemClickListener, IOnDateSetListener
	{
		private LinearLayout header;
		private Button btnSelection;
		private Button btnToday;
		private ImageView btnPrev, btnNext;
		private TextView txtDateDay, txtDisplayDate, txtDateYear, txtInfo;
		private GridView gridView;

		/// <summary>
		/// Ngày đang được chọn
		/// </summary>
		private DateTime userDateTime;
		/// <summary>
		/// Mảng Calendar
		/// </summary>
		private List<Calendar> monthArr = new List<Calendar>();
		/// <summary>
		/// Nếu thay đổi tháng thì biến này sẽ tạo lại mảng monthArr
		/// </summary>
		private bool monthHadChanged = true;

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
			txtInfo = FindViewById<TextView>(Resource.Id.txtInfo);
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
			UpdateDate(DateTime.Now);
		}

		private void BtnNext_Click(object sender, EventArgs e)
		{
			UpdateDate(userDateTime.AddDays(1));
		}

		private void BtnPrev_Click(object sender, EventArgs e)
		{
			UpdateDate(userDateTime.AddDays(-1));
		}

		public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
		{
			// Tháng trong này bắt đầu từ 0!!!
			UpdateDate(new DateTime(year, month + 1, dayOfMonth));
		}

		public void UpdateDate(DateTime dateTime)
		{
			if (dateTime.Month != userDateTime.Month || dateTime.Year != userDateTime.Year)
				monthHadChanged = true;
			userDateTime = dateTime;
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

			if (monthHadChanged)
			{
				monthArr.Clear();
				for (int i = 0; i < firstDayOfWeekOfMonth; i++)
				{
					monthArr.Add(new Calendar());
				}
				for (int i = 0; i < days; i++)
				{
					monthArr.Add(new Calendar(dt.AddDays(i)));
				}
				monthHadChanged = false;
			}

			CustomAdapter arrayAdapter = new CustomAdapter(this, Resource.Layout.grid_calendar_layout, monthArr, userDay);
			gridView.Adapter = arrayAdapter;
			gridView.OnItemClickListener = this;
			ShowInfo(userDay);
		}

		public void OnItemClick(AdapterView parent, View view, int position, long id)
		{
			DateTime dateTime = monthArr[position].SolarDate;
			OnDateSet(null, dateTime.Year, dateTime.Month - 1, dateTime.Day);
		}

		private void ShowInfo(int todayId)
		{
			LunarDate lunarDate = monthArr[todayId].LunarDate;
			txtInfo.Text = lunarDate.ToString(true);
		}
	}
}