using System;

namespace AIOApp.CalendarLib
{
	public class Calendar
	{
		public DateTime SolarDate { get; set; }
		public LunarDate LunarDate { get; set; }
		public string PrintedSolarDay { get; set; }
		public string PrintedLunarDay { get; set; }
		public bool IsEvent { get; set; }
		public bool NotInMonth { get; set; }

		public Calendar()
		{

		}

		public Calendar(DateTime solarDate, bool notInMonth = false, bool @event = false)
		{
			SolarDate = solarDate;
			LunarDate = solarDate.ToLunarDate();
			IsEvent = false;
			NotInMonth = notInMonth;
			IsEvent = @event;

			PrintedSolarDay = solarDate.Day.ToString();
			if (LunarDate.Day == 1)
			{
				PrintedLunarDay = $"{LunarDate.Day}/{LunarDate.Month}";
			}
			else
			{
				PrintedLunarDay = LunarDate.Day.ToString();
			}
		}
	}
}