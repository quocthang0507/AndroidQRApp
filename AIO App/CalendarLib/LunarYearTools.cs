using System;

namespace AIOApp.CalendarLib
{
	public static class LunarYearTools
	{
		#region Các hàm tính toán chung

		/// <summary>
		/// Discard the fractional part of a number, e.g., INT(3.2) = 3
		/// </summary>
		public static long INT(double d)
		{
			return (long)Math.Floor(d);
		}

		public static long MOD(int x, int y)
		{
			long z = x - (long)(y * Math.Floor(((double)x / y)));
			if (z == 0)
			{
				z = y;
			}
			return z;
		}

		/// <summary>
		/// Compute the (integral) Julian day number of day dd/mm/yyyy, i.e., the number 
		/// of days between 1/1/4713 BC (Julian calendar) and dd/mm/yyyy. 
		/// Formula from http://www.tondering.dk/claus/calendar.html
		/// </summary>
		/// <param name="dd"></param>
		/// <param name="mm"></param>
		/// <param name="yy"></param>
		/// <returns></returns>
		public static long GetJDFromDate(int dd, int mm, int yy)
		{
			long a, y, m, jd;
			a = INT((14 - mm) / 12);
			y = yy + 4800 - a;
			m = mm + 12 * a - 3;
			jd = dd + INT((153 * m + 2) / 5) + 365 * y + INT(y / 4) - INT(y / 100) + INT(y / 400) - 32045;
			if (jd < 2299161)
				jd = dd + INT((153 * m + 2) / 5) + 365 * y + INT(y / 4) - 32083;
			return jd;
		}

		/// <summary>
		/// Convert a Julian day number to day/month/year. Parameter jd is an integer 
		/// </summary>
		/// <param name="jd"></param>
		/// <returns></returns>
		public static DateTime GetJDToDate(long jd)
		{
			long a, b, c, d, e, m, day, month, year;
			if (jd > 2299160)
			{ // After 5/10/1582, Gregorian calendar
				a = jd + 32044;
				b = INT((4 * a + 3) / 146097);
				c = a - INT((b * 146097) / 4);
			}
			else
			{
				b = 0;
				c = jd + 32082;
			}
			d = INT((4 * c + 3) / 1461);
			e = c - INT((1461 * d) / 4);
			m = INT((5 * e + 2) / 153);
			day = e - INT((153 * m + 2) / 5) + 1;
			month = m + 3 - 12 * INT(m / 10);
			year = b * 100 + d - 4800 + INT(m / 10);
			return new DateTime((int)year, (int)month, (int)day);
		}

		/// <summary>
		/// Compute the time of the k-th new moon after the new moon of 1/1/1900 13:52 UCT 
		/// (measured as the number of days since 1/1/4713 BC noon UCT, e.g., 2451545.125 is 1/1/2000 15:00 UTC).
		/// Returns a floating number, e.g., 2415079.9758617813 for k=2 or 2414961.935157746 for k=-2
		/// Algorithm from: "Astronomical Algorithms" by Jean Meeus, 1998
		/// </summary>
		/// <param name="k"></param>
		/// <returns></returns>
		public static long GetNewMoon(long k, double timeZone = 7.0)
		{
			var T = k / 1236.85;
			var T2 = T * T;
			var T3 = T2 * T;
			var dr = Math.PI / 180;
			var Jd1 = 2415020.75933 + 29.53058868 * k + 0.0001178 * T2 - 0.000000155 * T3;
			Jd1 = Jd1 + 0.00033 * Math.Sin((166.56 + 132.87 * T - 0.009173 * T2) * dr);
			var M = 359.2242 + 29.10535608 * k - 0.0000333 * T2 - 0.00000347 * T3;
			var Mpr = 306.0253 + 385.81691806 * k + 0.0107306 * T2 + 0.00001236 * T3;
			var F = 21.2964 + 390.67050646 * k - 0.0016528 * T2 - 0.00000239 * T3;
			var C1 = (0.1734 - 0.000393 * T) * Math.Sin(M * dr) + 0.0021 * Math.Sin(2 * dr * M);
			C1 = C1 - 0.4068 * Math.Sin(Mpr * dr) + 0.0161 * Math.Sin(dr * 2 * Mpr);
			C1 = C1 - 0.0004 * Math.Sin(dr * 3 * Mpr);
			C1 = C1 + 0.0104 * Math.Sin(dr * 2 * F) - 0.0051 * Math.Sin(dr * (M + Mpr));
			C1 = C1 - 0.0074 * Math.Sin(dr * (M - Mpr)) + 0.0004 * Math.Sin(dr * (2 * F + M));
			C1 = C1 - 0.0004 * Math.Sin(dr * (2 * F - M)) - 0.0006 * Math.Sin(dr * (2 * F + Mpr));
			C1 = C1 + 0.0010 * Math.Sin(dr * (2 * F - Mpr)) + 0.0005 * Math.Sin(dr * (2 * Mpr + M));
			double deltat, JdNew;
			if (T < -11)
				deltat = 0.001 + 0.000839 * T + 0.0002261 * T2 - 0.00000845 * T3 - 0.000000081 * T * T3;
			else
				deltat = -0.000278 + 0.000265 * T + 0.000262 * T2;
			JdNew = Jd1 + C1 - deltat;
			return INT(JdNew + 0.5 + timeZone / 24);
		}

		/// <summary>
		/// Compute the longitude of the sun at any time. 
		/// Parameter: floating number jdn, the number of days since 1/1/4713 BC noon
		/// Algorithm from: "Astronomical Algorithms" by Jean Meeus, 1998
		/// </summary>
		/// <param name="jdn"></param>
		/// <returns></returns>
		public static long GetSunLongitude(double jdn, double timeZone = 7.0)
		{
			double T, T2, dr, M, L0, DL, L;
			T = (jdn - 2451545.5 - timeZone / 24) / 36525;
			T2 = T * T;
			dr = Math.PI / 180;
			M = 357.52910 + 35999.05030 * T - 0.0001559 * T2 - 0.00000048 * T * T2;
			L0 = 280.46645 + 36000.76983 * T + 0.0003032 * T2;
			DL = (1.914600 - 0.004817 * T - 0.000014 * T2) * Math.Sin(dr * M);
			DL = DL + (0.019993 - 0.000101 * T) * Math.Sin(dr * 2 * M) + 0.000290 * Math.Sin(dr * 3 * M);
			L = L0 + DL;
			L = L * dr;
			L = L - Math.PI * 2 * (INT(L / (Math.PI * 2)));
			return INT(L / Math.PI * 6);
		}

		/// <summary>
		/// Find the day that starts the luner month 11 of the given year for the given time zone
		/// </summary>
		/// <param name="yy"></param>
		/// <param name="timeZone"></param>
		/// <returns></returns>
		public static long GetLunarMonth11(int yy, double timeZone = 7.0)
		{
			long k, off, nm, sunLong;
			//off = jdFromDate(31, 12, yy) - 2415021.076998695;
			off = GetJDFromDate(31, 12, yy) - 2415021;
			k = INT(off / 29.530588853);
			nm = GetNewMoon(k, timeZone);
			sunLong = GetSunLongitude((double)nm, timeZone); // sun longitude at local midnight
			if (sunLong >= 9)
			{
				nm = GetNewMoon(k - 1, timeZone);
			}
			return nm;
		}

		/// <summary>
		/// Find the index of the leap month after the month starting on the day a11.
		/// </summary>
		/// <param name="a11"></param>
		/// <param name="timeZone"></param>
		/// <returns></returns>
		public static long GetLeapMonthOffset(long a11, double timeZone)
		{
			long k, last, arc, i;
			k = INT((a11 - 2415021.076998695) / 29.530588853 + 0.5);
			i = 1; // We start with the month following lunar month 11
			arc = GetSunLongitude(GetNewMoon(k + i, timeZone), timeZone);
			do
			{
				last = arc;
				i++;
				arc = GetSunLongitude(GetNewMoon(k + i, timeZone), timeZone);
			} while (arc != last && i < 14);
			return i - 1;
		}
		#endregion

		#region Các hàm chuyển đổi
		/// <summary>
		/// Convert solar date dd/mm/yyyy to the corresponding lunar date
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public static LunarDate SolarToLunar(DateTime date)
		{
			return ConvertSolarToLunar(date, 7);
		}

		/// <summary>
		/// Tính can chi cho ngày dương lịch
		/// </summary>
		/// <param name="d"></param>
		/// <param name="m"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static string GetGanzhiOfSolarDay(int d, int m, int y)
		{
			string CHI = "";
			string CAN = "";
			long X = INT(GetJDFromDate(d, m, y) + 9.5) % 10;
			switch (X)
			{
				case 0:
					CAN = "Giáp";
					break;
				case 1:
					CAN = "Ất";
					break;
				case 2:
					CAN = "Bính";
					break;
				case 3:
					CAN = "Đinh";
					break;
				case 4:
					CAN = "Mậu";
					break;
				case 5:
					CAN = "Kỷ";
					break;
				case 6:
					CAN = "Canh";
					break;
				case 7:
					CAN = "Tân";
					break;
				case 8:
					CAN = "Nhâm";
					break;
				case 9:
					CAN = "Quý";
					break;
			}
			long Y = INT(GetJDFromDate(d, m, y) + 1.5) % 12;
			switch (Y)
			{
				case 0:
					CHI = "Tý";
					break;
				case 1:
					CHI = "Sửu";
					break;
				case 2:
					CHI = "Dần";
					break;
				case 3:
					CHI = "Mão";
					break;
				case 4:
					CHI = "Thìn";
					break;
				case 5:
					CHI = "Tị";
					break;
				case 6:
					CHI = "Ngọ";
					break;
				case 7:
					CHI = "Mùi";
					break;
				case 8:
					CHI = "Thân";
					break;
				case 9:
					CHI = "Dậu";
					break;
				case 10:
					CHI = "Tuất";
					break;
				case 11:
					CHI = "Hợi";
					break;
			}
			return $"{CAN} {CHI}";
		}

		/// <summary>
		/// Tính can chi cho tháng âm lịch
		/// </summary>
		/// <param name="m"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static string GetGanzhiOfMonth(int m, int y)
		{
			string CHI = "";
			string CAN = "";
			int X = (y * 12 + m + 3) % 10;
			switch (X)
			{
				case 0:
					CAN = "Giáp";
					break;
				case 1:
					CAN = "Ất";
					break;
				case 2:
					CAN = "Bính";
					break;
				case 3:
					CAN = "Đinh";
					break;
				case 4:
					CAN = "Mậu";
					break;
				case 5:
					CAN = "Kỷ";
					break;
				case 6:
					CAN = "Canh";
					break;
				case 7:
					CAN = "Tân";
					break;
				case 8:
					CAN = "Nhâm";
					break;
				case 9:
					CAN = "Quý";
					break;
			}
			switch (m)
			{
				case 11:
					CHI = "Tý";
					break;
				case 12:
					CHI = "Sửu";
					break;
				case 1:
					CHI = "Dần";
					break;
				case 2:
					CHI = "Mão";
					break;
				case 3:
					CHI = "Thìn";
					break;
				case 4:
					CHI = "Tị";
					break;
				case 5:
					CHI = "Ngọ";
					break;
				case 6:
					CHI = "Mùi";
					break;
				case 7:
					CHI = "Thân";
					break;
				case 8:
					CHI = "Dậu";
					break;
				case 9:
					CHI = "Tuất";
					break;
				case 10:
					CHI = "Hợi";
					break;
			}
			return $"{CAN} {CHI}";
		}

		/// <summary>
		/// Tính can chi cho năm âm lịch (dương lịch)
		/// </summary>
		/// <param name="m"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static string GetGanzhiOfYear(int y)
		{
			string CHI = "";
			string CAN = "";
			int X = y % 10;
			switch (X)
			{
				case 0:
					CAN = "Canh";
					break;
				case 1:
					CAN = "Tân";
					break;
				case 2:
					CAN = "Nhâm";
					break;
				case 3:
					CAN = "Quý";
					break;
				case 4:
					CAN = "Giáp";
					break;
				case 5:
					CAN = "Ất";
					break;
				case 6:
					CAN = "Bính";
					break;
				case 7:
					CAN = "Đinh";
					break;
				case 8:
					CAN = "Mậu";
					break;
				case 9:
					CAN = "Kỷ";
					break;
			}
			int Y = y % 12;
			switch (Y)
			{
				case 0:
					CHI = "Thân";
					break;
				case 1:
					CHI = "Dậu";
					break;
				case 2:
					CHI = "Tuất";
					break;
				case 3:
					CHI = "Hợi";
					break;
				case 4:
					CHI = "Tý";
					break;
				case 5:
					CHI = "Sửu";
					break;
				case 6:
					CHI = "Dần";
					break;
				case 7:
					CHI = "Mão";
					break;
				case 8:
					CHI = "Thìn";
					break;
				case 9:
					CHI = "Tị";
					break;
				case 10:
					CHI = "Ngọ";
					break;
				case 11:
					CHI = "Mùi";
					break;
			}
			return $"{CAN} {CHI}";
		}

		public static LunarDate ConvertSolarToLunar(DateTime date, int timeZone)
		{
			long k, dayNumber, monthStart, a11, b11, lunarDay, lunarMonth, lunarYear, diff, leapMonthDiff;
			bool lunarLeap;

			dayNumber = GetJDFromDate(date.Day, date.Month, date.Year);
			k = INT((dayNumber - 2415021.076998695) / 29.530588853);
			monthStart = GetNewMoon(k + 1, timeZone);
			if (monthStart > dayNumber)
			{
				monthStart = GetNewMoon(k, timeZone);
			}
			// alert(dayNumber+" -> "+monthStart);
			a11 = GetLunarMonth11(date.Year, timeZone);
			b11 = a11;
			if (a11 >= monthStart)
			{
				lunarYear = date.Year;
				a11 = GetLunarMonth11(date.Year - 1, timeZone);
			}
			else
			{
				lunarYear = date.Year + 1;
				b11 = GetLunarMonth11(date.Year + 1, timeZone);
			}
			lunarDay = dayNumber - monthStart + 1;
			diff = INT((monthStart - a11) / 29);
			lunarLeap = false;
			lunarMonth = diff + 11;
			if (b11 - a11 > 365)
			{
				leapMonthDiff = GetLeapMonthOffset(a11, timeZone);
				if (diff >= leapMonthDiff)
				{
					lunarMonth = diff + 10;
					if (diff == leapMonthDiff)
					{
						lunarLeap = true;
					}
				}
			}
			if (lunarMonth > 12)
			{
				lunarMonth -= 12;
			}
			if (lunarMonth >= 11 && diff < 4)
			{
				lunarYear -= 1;
			}
			return new LunarDate((int)lunarDay, (int)lunarMonth, (int)lunarYear, lunarLeap, date);
		}

		/// <summary>
		/// Convert a lunar date to the corresponding solar date
		/// </summary>
		/// <param name="ld"></param>
		/// <returns></returns>
		public static DateTime ConvertLunarToSolar(LunarDate ld)
		{
			return LunarToSolar(ld, 7);
		}

		public static DateTime LunarToSolar(LunarDate ld, int timeZone)
		{
			long k, a11, b11, off, leapOff, leapMonth, monthStart;
			if (ld.Month < 11)
			{
				a11 = GetLunarMonth11(ld.Year - 1, timeZone);
				b11 = GetLunarMonth11(ld.Year, timeZone);
			}
			else
			{
				a11 = GetLunarMonth11(ld.Year, timeZone);
				b11 = GetLunarMonth11(ld.Year + 1, timeZone);
			}
			k = INT(0.5 + (a11 - 2415021.076998695) / 29.530588853);
			off = ld.Month - 11;
			if (off < 0)
			{
				off += 12;
			}
			if (b11 - a11 > 365)
			{
				leapOff = GetLeapMonthOffset(a11, timeZone);
				leapMonth = leapOff - 2;
				if (leapMonth < 0)
				{
					leapMonth += 12;
				}
				if (ld.IsLeapYear && ld.Month != leapMonth)
				{
					return DateTime.MinValue;
				}
				else if (ld.IsLeapYear || off >= leapOff)
				{
					off += 1;
				}
			}
			monthStart = GetNewMoon(k + off, timeZone);
			return GetJDToDate(monthStart + ld.Day - 1);
		}
		#endregion

	}
}