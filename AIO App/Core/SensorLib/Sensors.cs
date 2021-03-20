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

		public static string ShowProperlyInfo(SensorEvent e)
		{
			string unit = string.Empty, info = string.Empty;
			switch (e.Sensor.Type)
			{
				case SensorType.Gravity:
				case SensorType.Accelerometer:
				case SensorType.LinearAcceleration:
					unit = "m/s²";
					info = $"Gx = {e.Values[0]}\nGy = {e.Values[1]}\nGz = {e.Values[2]}";
					break;
				case SensorType.AccelerometerUncalibrated:
					unit = "m/s²";
					info = $"X_uncalib = {e.Values[0]}\nY_uncalib = {e.Values[1]}\nZ_uncalib = {e.Values[2]}";
					info += $"\nX_bias = {e.Values[3]}\nY_bias = {e.Values[4]}\nZ_bias = {e.Values[5]}";
					break;
				case SensorType.RotationVector:
				case SensorType.GameRotationVector:
					info = $"x*sin(θ/2) = {e.Values[0]}\ny*sin(θ/2) = {e.Values[1]}\nz*sin(θ/2) = {e.Values[2]}";
					break;
				case SensorType.Gyroscope:
					unit = "rad/s";
					info = $"X = {e.Values[0]}\nY = {e.Values[1]}\nZ = {e.Values[2]}";
					break;
				case SensorType.GyroscopeUncalibrated:
					unit = "rad/s";
					info = "Angular speed (w/o drift compensation) around \n";
					info += $"X = {e.Values[0]}\nY = {e.Values[1]}\nZ= {e.Values[2]}";
					info += "\nEstimated drift around \n";
					info += $"X = {e.Values[3]}\nY = {e.Values[4]}\nZ = {e.Values[5]}";
					break;
				case SensorType.HeartBeat:
					unit = "Sự kiện xảy ra có giá trị bằng 1 nếu phát hiện đỉnh nhịp tim";
					info = e.Values[0].ToString();
					break;
				case SensorType.Light:
					unit = "lux";
					info = e.Values[0].ToString();
					break;
				case SensorType.LowLatencyOffbodyDetect:
					unit = "Giá trị bằng 1 nếu thiết bị nằm trên cơ thể";
					info = e.Values[0].ToString();
					break;
				case SensorType.MagneticField:
					unit = "μT";
					info = $"X = {e.Values[0]}\nY = {e.Values[1]}\nZ = {e.Values[2]}";
					break;
				case SensorType.MagneticFieldUncalibrated:
					unit = "μT";
					info = $"X_uncalib = {e.Values[0]}\nY_uncalib = {e.Values[1]}\nZ_uncalib = {e.Values[2]}";
					info += $"\nX = {e.Values[3]}\nY = {e.Values[4]}\nZ = {e.Values[5]}";
					break;
				case SensorType.MotionDetect:
					unit = "Giá trị nếu chuyển động ít nhất 5 giây = 1.0";
					info = e.Values[0].ToString();
					break;
				case SensorType.Pose6dof:
					info = "x*sin(θ/ 2) = " + e.Values[0];
					info += "\ny*sin(θ/ 2) = " + e.Values[1];
					info += "\nz*sin(θ/ 2) = " + e.Values[2];
					info += "\ncos(θ / 2) = " + e.Values[3];
					info += "\nTranslation along x axis from an arbitrary origin = " + e.Values[4];
					info += "\nTranslation along y axis from an arbitrary origin = " + e.Values[5];
					info += "\nTranslation along z axis from an arbitrary origin = " + e.Values[6];
					info += "\nDelta quaternion rotation x*sin(θ / 2) = " + e.Values[7];
					info += "\nDelta quaternion rotation y*sin(θ / 2) = " + e.Values[8];
					info += "\nDelta quaternion rotation z*sin(θ / 2) = " + e.Values[9];
					info += "\nDelta quaternion rotation cos(θ/ 2) = " + e.Values[10];
					info += "\nDelta translation along x axis = " + e.Values[11];
					info += "\nDelta translation along y axis = " + e.Values[12];
					info += "\nDelta translation along z axis = " + e.Values[13];
					info += "\nSequence number = " + e.Values[14];
					break;
				case SensorType.Pressure:
					unit = "hPa (millibar)";
					info = e.Values[0].ToString();
					break;
				case SensorType.Proximity:
					unit = "cm";
					info = e.Values[0].ToString();
					break;
				case SensorType.StationaryDetect:
					unit = "Giá trị nếu đứng yên ít nhất 5 giây = 1.0";
					info = e.Values[0].ToString();
					break;
				default:
					foreach (float value in e.Values)
					{
						info += $"{value}\n";
					}
					break;
			}
			return $"({unit})\n{info}";
		}
	}
}