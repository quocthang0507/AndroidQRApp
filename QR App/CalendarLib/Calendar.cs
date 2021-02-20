using System;

namespace AIOApp.CalendarLib
{
	public class Calendar
	{
		public DateTime SolarDate { get; set; }
		public LunarDate LunarDate { get; set; }
		public string PrintedSolarDay { get; set; }
		public string PrintedLunarDay { get; set; }
		public bool Event { get; set; }

		public Calendar()
		{

		}

		public Calendar(DateTime solarDate)
		{
			SolarDate = solarDate;
			LunarDate = solarDate.ToLunarDate();
			Event = false;

			PrintedSolarDay = solarDate.Day.ToString();
			PrintedLunarDay = LunarDate.Day.ToString();
		}

		public Calendar(DateTime solarDate, bool @event)
		{
			SolarDate = solarDate;
			LunarDate = solarDate.ToLunarDate();
			Event = @event;

			PrintedSolarDay = solarDate.Day.ToString();
			PrintedLunarDay = LunarDate.Day.ToString();
		}
	}
}