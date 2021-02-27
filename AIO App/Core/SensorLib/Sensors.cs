using Android.App;
using Android.Content;
using Android.Hardware;
using System.Collections.Generic;
using System.Linq;

namespace AIOApp.SensorLib
{
	public class Sensors
	{
		private SensorManager sensorManager;
		private static Sensors singleton;

		public static List<KeyValuePair<int, string>> VietnameseSensorType = new List<KeyValuePair<int, string>> {
			new KeyValuePair<int, string>(-1,"Tất cả"),
			new KeyValuePair<int, string>(1,"Gia tốc kế"),
			new KeyValuePair<int, string>(2,"Từ trường"),
			new KeyValuePair<int, string>(3,"Hướng"),
			new KeyValuePair<int, string>(4,"Con quay hồi chuyển"),
			new KeyValuePair<int, string>(5,"Ánh sáng"),
			new KeyValuePair<int, string>(6,"Áp suất"),
			new KeyValuePair<int, string>(7,"Nhiệt độ"),
			new KeyValuePair<int, string>(8,"Tiệm cận"),
			new KeyValuePair<int, string>(9,"Trọng lực"),
			new KeyValuePair<int, string>(10,"Gia tốc tuyến tính"),
			new KeyValuePair<int, string>(11,"Véctơ xoay"),
			new KeyValuePair<int, string>(12,"Độ ẩm tương đối"),
			new KeyValuePair<int, string>(13,"Nhiệt độ môi trường"),
			new KeyValuePair<int, string>(14,"Từ trường chưa hiệu chỉnh"),
			new KeyValuePair<int, string>(15,"Véctơ xoay trò chơi"),
			new KeyValuePair<int, string>(16,"Con quay hồi chuyển chưa hiệu chỉnh"),
			new KeyValuePair<int, string>(17,"Chuyển động đáng kể"),
			new KeyValuePair<int, string>(18,"Phát hiện bước đi"),
			new KeyValuePair<int, string>(19,"Đếm bước đi"),
			new KeyValuePair<int, string>(20,"Véctơ xoay địa từ"),
			new KeyValuePair<int, string>(21,"Nhịp tim"),
			new KeyValuePair<int, string>(28,"Cảm biến tư thế 6 bậc tự do"),
			new KeyValuePair<int, string>(29,"Phát hiện đứng yên"),
			new KeyValuePair<int, string>(30,"Phát hiện chuyển động"),
			new KeyValuePair<int, string>(31,"Nhịp tim"),
			new KeyValuePair<int, string>(34,"LowLatencyOffbodyDetect"),
			new KeyValuePair<int, string>(35,"Gia tốc kế chưa hiệu chỉnh"),
			new KeyValuePair<int, string>(65536,"DevicePrivateBase")
		};

		private Sensors()
		{
			sensorManager = (SensorManager)Application.Context.GetSystemService(Context.SensorService);
		}

		public static Sensors Instance
		{
			get
			{
				if (singleton == null)
				{
					singleton = new Sensors();
				}

				return singleton;
			}
		}

		public List<Sensor> GetSensors
		{
			get
			{
				return singleton.sensorManager.GetSensorList(SensorType.All).ToList();
			}
		}
	}
}