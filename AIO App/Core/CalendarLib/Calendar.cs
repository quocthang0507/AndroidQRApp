using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIOApp.CalendarLib
{
	public class Calendar
	{
		public DateTime SolarDate { get; set; }
		public LunarDate LunarDate { get; set; }
		public string PrintedSolarDay { get; set; }
		public string PrintedLunarDay { get; set; }
		public bool IsTet { get; set; }
		public bool NotInMonth { get; set; }

		public Calendar()
		{

		}

		public Calendar(DateTime solarDate, bool notInMonth = false)
		{
			SolarDate = solarDate;
			LunarDate = solarDate.ToLunarDate();
			IsTet = false;
			NotInMonth = notInMonth;

			PrintedSolarDay = solarDate.Day.ToString();
			// Nếu là ngày 1 ÂL thì thêm tháng âm lịch vào
			PrintedLunarDay = LunarDate.Day == 1 ? $"{LunarDate.Day}/{LunarDate.Month}" : LunarDate.Day.ToString();
			// Nếu là Tết nguyên đán
			IsTet = (LunarDate.Day == 1 || LunarDate.Day == 2 || LunarDate.Day == 3) && LunarDate.Month == 1;
		}

		public override string ToString()
		{
			return FindEvents();
		}

		private string FindEvents()
		{
			// Xác định khóa tìm kiếm
			string solarDate = $"{SolarDate.Day}/{SolarDate.Month}";
			string lunarDate = $"{LunarDate.Day}/{LunarDate.Month}";
			StringBuilder sb = new StringBuilder();

			// Tìm ngày lễ trong cả âm lịch và dương lịch
			List<KeyValuePair<string, string>> result = Events.SolarEvents.Where(e => e.Key == solarDate).ToList();
			result.AddRange(Events.LunarEvents.Where(e => e.Key == lunarDate));
			////// Vì tất niên rơi vào ngày cuối cùng của năm (29 hoặc 30 tháng Chạp) nên
			if (LunarDate.Month == 12 && LunarDate.Day == 30)
			{
				result.Add(new KeyValuePair<string, string>("30/12", "Lễ Tất Niên"));
			}
			else if (LunarDate.Month == 12 && LunarDate.Day == 29)
			{
				result.Add(new KeyValuePair<string, string>("29/12", "Lễ Tất Niên"));
			}

			// Duyệt tuần tự
			if (result.Count > 0)
			{
				sb.AppendLine("Sự kiện/ ngày lễ: ");
				foreach (KeyValuePair<string, string> @event in result)
				{
					sb.AppendLine($"• {@event.Value}");
				}
			}
			return sb.ToString();
		}
	}
}