using System;

namespace AIOApp.CalendarLib
{
	public class LunarDate
	{
		public int Day { get; set; }
		public int Month { get; set; }
		public int Year { get; set; }
		public string Ganzhi_Day { get; }
		public string Ganzhi_Month { get; }
		public string Ganzhi_Year { get; }
		public bool IsLeapYear { get; set; }
		public DateTime SolarDate { get; }

		public LunarDate() { }

		public LunarDate(int day, int month, int year, bool leap, DateTime solarDate)
		{
			Day = day;
			Month = month;
			Year = year;
			IsLeapYear = leap;
			this.SolarDate = solarDate;
			Ganzhi_Day = LunarYearTools.GetGanzhiOfSolarDay(solarDate.Day, solarDate.Month, solarDate.Year);
			Ganzhi_Month = LunarYearTools.GetGanzhiOfMonth(month, year);
			Ganzhi_Year = LunarYearTools.GetGanzhiOfYear(year);
		}

		public override string ToString()
		{
			string leap = IsLeapYear ? "Năm nhuận" : "Năm không nhuận";
			return $"Âm lịch ngày {Day} tháng {Month} năm {Year}, {leap}";
		}

		public string ToString(bool full)
		{
			string str = ToString();
			if (full)
			{
				str += $" (Ngày {Ganzhi_Day} tháng {Ganzhi_Month} năm {Ganzhi_Year})";
			}
			return str;
		}

		public DateTime ToSolarDate(int timeZone)
		{
			return LunarYearTools.LunarToSolar(this, timeZone);
		}

		public DateTime ToSolarDate()
		{
			return ToSolarDate(7);
		}
	}
}