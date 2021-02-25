namespace AIOApp.CalendarLib
{
	/// <summary>
	/// Tử vi: http://khoia0.com/PDF-Files/THAMKHAO.pdf
	/// </summary>
	public class ZiWei
	{
		/// <summary>
		/// Giờ hoàng đào tốt (Ecliptic hour) theo 12 chi của ngày
		/// </summary>
		/// <param name="zhiOfDay"></param>
		/// <returns></returns>
		public static string EclipticHour_Good(string zhiOfDay)
		{
			switch (zhiOfDay)
			{
				case "Dần":
				case "Thân":
					return "Tý, Sửu, Thìn, Tỵ, Mùi, Tuất";
				case "Mão":
				case "Dậu":
					return "Tý, Dần, Mão, Ngọ, Mùi, Dậu";
				case "Thìn":
				case "Tuất":
					return "Dần, Thìn, Tỵ, Thân, Dậu, Hợi";
				case "Tỵ":
				case "Tị":
				case "Hợi":
					return "Sửu, Thìn, Ngọ, Mùi, Tuất, Hợi";
				case "Tý":
				case "Tí":
				case "Ngọ":
					return "Tý, Sửu, Mão, Ngọ, Thân, Dậu";
				case "Sửu":
				case "Mùi":
					return "Dần, Mão, Tỵ, Thân, Tuất, Hợi";
				default:
					return null;
			}
		}

		/// <summary>
		/// Giờ hoàng đào xấu (Ecliptic hour) Thiên cẩu hạ thực theo 12 chi của ngày và tháng
		/// </summary>
		/// <param name="zhiOfDay"></param>
		/// <returns></returns>
		public static string EclipticHour_Bad(string zhiOfDay, int month)
		{
			if (month == 1 && (zhiOfDay.Equals("Tý") || zhiOfDay.Equals("Tí")))
				return "Hợi";
			else if (month == 2 && zhiOfDay.Equals("Sửu"))
				return "Tý";
			else if (month == 3 && zhiOfDay.Equals("Dần"))
				return "Sửu";
			else if (month == 4 && zhiOfDay.Equals("Mão"))
				return "Dần";
			else if (month == 5 && zhiOfDay.Equals("Thìn"))
				return "Mão";
			else if (month == 6 && (zhiOfDay.Equals("Tỵ") || zhiOfDay.Equals("Tị")))
				return "Thìn";
			else if (month == 7 && zhiOfDay.Equals("Ngọ"))
				return "Tỵ";
			else if (month == 8 && zhiOfDay.Equals("Mùi"))
				return "Ngọ";
			else if (month == 9 && zhiOfDay.Equals("Thân"))
				return "Mùi";
			else if (month == 10 && zhiOfDay.Equals("Dậu"))
				return "Thân";
			else if (month == 11 && zhiOfDay.Equals("Tuất"))
				return "Dậu";
			else if (month == 12 && zhiOfDay.Equals("Hợi"))
				return "Hợi";
			return null;
		}
	}
}