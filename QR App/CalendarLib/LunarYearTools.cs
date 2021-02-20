﻿using System;

namespace QR.CalendarLib
{
	public static class LunarYearTools
	{
		#region Các hàm tính toán chung
		const double PI = Math.PI;

		/* Discard the fractional part of a number, e.g., INT(3.2) = 3 */
		public static long INT(double d)
		{
			return (long)Math.Floor(d);
		}

		/* Compute the (integral) Julian day number of day dd/mm/yyyy, i.e., the number 
         * of days between 1/1/4713 BC (Julian calendar) and dd/mm/yyyy. 
         * Formula from http://www.tondering.dk/claus/calendar.html
         */
		public static long JDFromDate(int dd, int mm, int yy)
		{
			long a, y, m, jd;
			a = INT((14 - mm) / 12);
			y = (yy + 4800 - a);
			m = (mm + 12 * a - 3);
			jd = dd + INT((153 * m + 2) / 5) + 365 * y + INT(y / 4) - INT(y / 100) + INT(y / 400) - 32045;
			if (jd < 2299161)
			{
				jd = dd + INT((153 * m + 2) / 5) + 365 * y + INT(y / 4) - 32083;
			}
			return jd;
		}

		/* Convert a Julian day number to day/month/year. Parameter jd is an integer */
		public static DateTime JDToDate(long jd)
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

		/* Compute the time of the k-th new moon after the new moon of 1/1/1900 13:52 UCT 
         * (measured as the number of days since 1/1/4713 BC noon UCT, e.g., 2451545.125 is 1/1/2000 15:00 UTC).
         * Returns a floating number, e.g., 2415079.9758617813 for k=2 or 2414961.935157746 for k=-2
         * Algorithm from: "Astronomical Algorithms" by Jean Meeus, 1998
         */
		public static long NewMoon(long k)
		{
			double T, T2, T3, dr, Jd1, M, Mpr, F, C1, deltat, JdNew;
			T = k / 1236.85; // Time in Julian centuries from 1900 January 0.5
			T2 = T * T;
			T3 = T2 * T;
			dr = PI / 180;
			Jd1 = 2415020.75933 + 29.53058868 * k + 0.0001178 * T2 - 0.000000155 * T3;
			Jd1 += 0.00033 * Math.Sin((166.56 + 132.87 * T - 0.009173 * T2) * dr); // Mean new moon
			M = 359.2242 + 29.10535608 * k - 0.0000333 * T2 - 0.00000347 * T3; // Sun's mean anomaly
			Mpr = 306.0253 + 385.81691806 * k + 0.0107306 * T2 + 0.00001236 * T3; // Moon's mean anomaly
			F = 21.2964 + 390.67050646 * k - 0.0016528 * T2 - 0.00000239 * T3; // Moon's argument of latitude
			C1 = (0.1734 - 0.000393 * T) * Math.Sin(M * dr) + 0.0021 * Math.Sin(2 * dr * M);
			C1 = C1 - (0.4068 * Math.Sin(Mpr * dr)) + 0.0161 * Math.Sin(dr * 2 * Mpr);
			C1 -= 0.0004 * Math.Sin(dr * 3 * Mpr);
			C1 = C1 + (0.0104 * Math.Sin(dr * 2 * F)) - 0.0051 * Math.Sin(dr * (M + Mpr));
			C1 = C1 - (0.0074 * Math.Sin(dr * (M - Mpr))) + 0.0004 * Math.Sin(dr * (2 * F + M));
			C1 = C1 - (0.0004 * Math.Sin(dr * (2 * F - M))) - 0.0006 * Math.Sin(dr * (2 * F + Mpr));
			C1 = C1 + 0.0010 * Math.Sin(dr * (2 * F - Mpr)) + 0.0005 * Math.Sin(dr * (2 * Mpr + M));
			if (T < -11)
			{
				deltat = 0.001 + 0.000839 * T + 0.0002261 * T2 - 0.00000845 * T3 - 0.000000081 * T * T3;
			}
			else
			{
				deltat = -0.000278 + 0.000265 * T + 0.000262 * T2;
			};
			JdNew = Jd1 + C1 - deltat;
			return (long)Math.Round(JdNew);
		}

		/* Compute the longitude of the sun at any time. 
         * Parameter: floating number jdn, the number of days since 1/1/4713 BC noon
         * Algorithm from: "Astronomical Algorithms" by Jean Meeus, 1998
         */
		public static double SunLongitude(double jdn)
		{
			double T, T2, dr, M, L0, DL, L;
			T = (jdn - 2451545.0) / 36525; // Time in Julian centuries from 2000-01-01 12:00:00 GMT
			T2 = T * T;
			dr = PI / 180; // degree to radian
			M = 357.52910 + 35999.05030 * T - 0.0001559 * T2 - 0.00000048 * T * T2; // mean anomaly, degree
			L0 = 280.46645 + 36000.76983 * T + 0.0003032 * T2; // mean longitude, degree
			DL = (1.914600 - 0.004817 * T - 0.000014 * T2) * Math.Sin(dr * M);
			DL = DL + (0.019993 - 0.000101 * T) * Math.Sin(dr * 2 * M) + 0.000290 * Math.Sin(dr * 3 * M);
			L = L0 + DL; // true longitude, degree
			L *= dr;
			L -= PI * 2 * (INT(L / (PI * 2))); // Normalize to (0, 2*PI)
			return L;
		}

		/* Compute sun position at midnight of the day with the given Julian day number. 
         * The time zone if the time difference between local time and UTC: 7.0 for UTC+7:00.
         * The function returns a number between 0 and 11. 
         * From the day after March equinox and the 1st major term after March equinox, 0 is returned. 
         * After that, return 1, 2, 3 ... 
         */
		public static long GetSunLongitude(long dayNumber, int timeZone)
		{
			return INT(SunLongitude(dayNumber - 0.5 - timeZone / 24) / PI * 6);
		}

		/* Compute the day of the k-th new moon in the given time zone.
         * The time zone if the time difference between local time and UTC: 7.0 for UTC+7:00
         */
		public static long GetNewMoonDay(long k, int timeZone)
		{
			return INT(NewMoon(k) + 0.5 + timeZone / 24);
		}

		/* Find the day that starts the luner month 11 of the given year for the given time zone */
		public static long GetLunarMonth11(int yy, int timeZone)
		{
			long k, off, nm, sunLong;
			//off = jdFromDate(31, 12, yy) - 2415021.076998695;
			off = JDFromDate(31, 12, yy) - 2415021;
			k = INT(off / 29.530588853);
			nm = GetNewMoonDay(k, timeZone);
			sunLong = GetSunLongitude(nm, timeZone); // sun longitude at local midnight
			if (sunLong >= 9)
			{
				nm = GetNewMoonDay(k - 1, timeZone);
			}
			return nm;
		}

		/* Find the index of the leap month after the month starting on the day a11. */
		public static long GetLeapMonthOffset(long a11, int timeZone)
		{
			long k, last, arc, i;
			k = INT((a11 - 2415021.076998695) / 29.530588853 + 0.5);
			i = 1; // We start with the month following lunar month 11
			arc = GetSunLongitude(GetNewMoonDay(k + i, timeZone), timeZone);
			do
			{
				last = arc;
				i++;
				arc = GetSunLongitude(GetNewMoonDay(k + i, timeZone), timeZone);
			} while (arc != last && i < 14);
			return i - 1;
		}
		#endregion

		#region Các hàm chuyển đổi
		/* Convert solar date dd/mm/yyyy to the corresponding lunar date */
		public static LunarDate SolarToLunar(DateTime date)
		{
			return SolarToLunar(date, 7);
		}

		public static LunarDate SolarToLunar(DateTime date, int timeZone)
		{
			long k, dayNumber, monthStart, a11, b11, lunarDay, lunarMonth, lunarYear, diff, leapMonthDiff;
			bool lunarLeap;

			dayNumber = JDFromDate(date.Day, date.Month, date.Year);
			k = INT((dayNumber - 2415021.076998695) / 29.530588853);
			monthStart = GetNewMoonDay(k + 1, timeZone);
			if (monthStart > dayNumber)
			{
				monthStart = GetNewMoonDay(k, timeZone);
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
			return new LunarDate((int)lunarDay, (int)lunarMonth, (int)lunarYear, lunarLeap);
		}

		/* Convert a lunar date to the corresponding solar date */
		public static DateTime LunarToSolar(LunarDate ld)
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
			monthStart = GetNewMoonDay(k + off, timeZone);
			return JDToDate(monthStart + ld.Day - 1);
		}
		#endregion

	}
}