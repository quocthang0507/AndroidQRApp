using System;

namespace QR.CalendarLib
{
	public static class LunarDateExt
	{
		public static LunarDate ToLunarDate(this DateTime d, int timeZone)
		{
			return LunarYearTools.ConvertSolarToLunar(d, timeZone);
		}

		public static LunarDate ToLunarDate(this DateTime d)
		{
			return ToLunarDate(d, 7);
		}
	}

}