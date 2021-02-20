using System;

namespace QR.CalendarLib
{
	public class LunarDate
	{
		public int Day { get; set; }
		public int Month { get; set; }
		public int Year { get; set; }
		public bool IsLeapYear { get; set; }

		public LunarDate() { }

		public LunarDate(int day, int month, int year, bool leap)
		{
			Day = day;
			Month = month;
			Year = year;
			IsLeapYear = leap;
		}

		public override string ToString()
		{
			return Day.ToString() + "/" + Month.ToString() + "/" + Year.ToString() + (IsLeapYear ? "N" : "");
		}

		public DateTime ToSolarDate(int timeZone)
		{
			return LunarYearTools.LunarToSolar(this);
		}

		public DateTime ToSolarDate()
		{
			return ToSolarDate(7);
		}
	}
}