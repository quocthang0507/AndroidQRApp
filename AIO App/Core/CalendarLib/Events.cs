using System.Collections.Generic;

namespace AIOApp.CalendarLib
{
	/// <summary>
	/// https://wikigioitre.com/cac-ngay-le-trong-nam-theo-duong-lich-va-am-lich.html
	/// </summary>
	public class Events
	{
		public static List<KeyValuePair<string, string>> LunarEvents = new List<KeyValuePair<string, string>>()
		{
			new KeyValuePair<string, string>("1/1", "Tết Nguyên Đán (Mùng 1)"),
			new KeyValuePair<string, string>("2/1", "Tết Nguyên Đán (Mùng 2)"),
			new KeyValuePair<string, string>("3/1", "Tết Nguyên Đán (Mùng 3)"),
			new KeyValuePair<string, string>("15/1", "Tết Nguyên Tiêu (Lễ Thượng Nguyên)"),
			new KeyValuePair<string, string>("3/3", "Tết Hàn Thực"),
			new KeyValuePair<string, string>("10/3", "Giỗ Tổ Hùng Vương"),
			new KeyValuePair<string, string>("15/4", "Lễ Phật Đản"),
			new KeyValuePair<string, string>("5/5", "Tết Đoan Ngọ"),
			new KeyValuePair<string, string>("15/7", "Lễ Vu Lan"),
			new KeyValuePair<string, string>("15/8", "Tết Trung Thu"),
			new KeyValuePair<string, string>("9/9", "Tết Trùng Cửu"),
			new KeyValuePair<string, string>("10/10", "Tết Thường Tân (Tết Cơm mới)"),
			new KeyValuePair<string, string>("15/10", "Tết Hạ Nguyên"),
			new KeyValuePair<string, string>("23/12", "Tiễn Táo Quân về trời")
		};

		public static List<KeyValuePair<string, string>> SolarEvents = new List<KeyValuePair<string, string>>()
		{
			new KeyValuePair<string, string>("1/1", "Tết Dương lịch"),
			new KeyValuePair<string, string>("9/1", "Ngày Học sinh - Sinh viên Việt Nam"),
			new KeyValuePair<string, string>("3/2", "Ngày thành lập Đảng Cộng sản Việt Nam"),
			new KeyValuePair<string, string>("14/2", "Lễ tình nhân (Valentine)"),
			new KeyValuePair<string, string>("27/2", "Ngày thầy thuốc Việt Nam"),
			new KeyValuePair<string, string>("8/3", "Ngày Quốc tế Phụ nữ"),
			new KeyValuePair<string, string>("20/3", "Ngày Quốc tế Hạnh phúc"),
			new KeyValuePair<string, string>("22/3", "Ngày Nước sạch Thế giới"),
			new KeyValuePair<string, string>("23/3", "Ngày Khí tượng Thế giới"),
			new KeyValuePair<string, string>("26/3", "Ngày thành lập Đoàn TNCS Hồ Chí Minh"),
			new KeyValuePair<string, string>("27/3", "Ngày Thể thao Việt Nam"),
			new KeyValuePair<string, string>("28/3", "Ngày thành lập lực lượng Dân quân tự vệ"),
			new KeyValuePair<string, string>("1/4", "Ngày Cá tháng Tư"),
			new KeyValuePair<string, string>("2/4", "Ngày Thế giới Nhận thức Tự kỷ"),
			new KeyValuePair<string, string>("7/4", "Ngày Sức khỏe Thế giới"),
			new KeyValuePair<string, string>("22/4", "Ngày Trái đất"),
			new KeyValuePair<string, string>("22/4", "Ngày Pháp Luật Thế giới"),
			new KeyValuePair<string, string>("23/4", "Ngày Sách Việt Nam"),
			new KeyValuePair<string, string>("30/4", "Ngày giải phóng miền Nam"),
			new KeyValuePair<string, string>("1/5", "Ngày Quốc tế Lao động"),
			new KeyValuePair<string, string>("7/5", "Ngày chiến thắng Điện Biên Phủ"),
			new KeyValuePair<string, string>("8/5", "Ngày Chữ Thập Đỏ Quốc tế"),
			new KeyValuePair<string, string>("13/5", "Ngày của mẹ"),
			new KeyValuePair<string, string>("15/5", "Ngày thành lập Đội Thiếu niên Tiền phong Hồ Chí Minh"),
			new KeyValuePair<string, string>("15/5", "Ngày quốc tế Gia đình"),
			new KeyValuePair<string, string>("19/5", "Ngày sinh chủ tịch Hồ Chí Minh"),
			new KeyValuePair<string, string>("31/5", "Ngày Thế giới không thuốc lá"),
			new KeyValuePair<string, string>("1/6", "Ngày Quốc tế thiếu nhi"),
			new KeyValuePair<string, string>("5/6", "Ngày Bác Hồ ra đi tìm đường cứu nước  và Ngày môi trường thế giới"),
			new KeyValuePair<string, string>("14/6", "Ngày Hiến Máu Thế giới"),
			new KeyValuePair<string, string>("17/6", "Ngày của cha"),
			new KeyValuePair<string, string>("21/6", "Ngày báo chí Việt Nam"),
			new KeyValuePair<string, string>("28/6", "Ngày gia đình Việt Nam"),
			new KeyValuePair<string, string>("11/7", "Ngày dân số thế giới"),
			new KeyValuePair<string, string>("27/7", "Ngày Thương binh liệt sĩ"),
			new KeyValuePair<string, string>("28/7", "Ngày thành lập công đoàn Việt Nam"),
			new KeyValuePair<string, string>("19/8", "Ngày tổng khởi nghĩa, Ngày Cách mạng tháng Tám thành công"),
			new KeyValuePair<string, string>("2/9", "Ngày Quốc Khánh"),
			new KeyValuePair<string, string>("7/9", "Ngày thành lập Đài Truyền hình Việt Nam"),
			new KeyValuePair<string, string>("10/9", "Ngày thành lập Mặt trận Tổ quốc Việt Nam"),
			new KeyValuePair<string, string>("1/10", "Ngày quốc tế người cao tuổi"),
			new KeyValuePair<string, string>("5/10", "Ngày Nhà giáo thế giới"),
			new KeyValuePair<string, string>("10/10", "Ngày giải phóng thủ đô"),
			new KeyValuePair<string, string>("11/10", "Ngày Quốc tế Trẻ em gái"),
			new KeyValuePair<string, string>("13/10", "Ngày doanh nhân Việt Nam"),
			new KeyValuePair<string, string>("14/10", "Ngày thành lập Hội Nông dân Việt Nam"),
			new KeyValuePair<string, string>("16/10", "Ngày Lương thực thế giới"),
			new KeyValuePair<string, string>("20/10", "Ngày Phụ nữ Việt Nam"),
			new KeyValuePair<string, string>("24/10", "Ngày Liên Hiệp Quốc"),
			new KeyValuePair<string, string>("31/10", "Ngày Halloween"),
			new KeyValuePair<string, string>("9/11", "Ngày pháp luật Việt Nam"),
			new KeyValuePair<string, string>("19/11", "Ngày Quốc tế Nam giới"),
			new KeyValuePair<string, string>("20/11", "Ngày Nhà giáo Việt Nam"),
			new KeyValuePair<string, string>("23/11", "Ngày thành lập Hội chữ thập đỏ Việt Nam"),
			new KeyValuePair<string, string>("1/12", "Ngày thế giới phòng chống AIDS"),
			new KeyValuePair<string, string>("19/12", "Ngày toàn quốc kháng chiến"),
			new KeyValuePair<string, string>("22/12", "Ngày thành lập quân đội nhân dân Việt Nam"),
			new KeyValuePair<string, string>("24/12", "Ngày lễ Giáng sinh"),
		};
	}
}