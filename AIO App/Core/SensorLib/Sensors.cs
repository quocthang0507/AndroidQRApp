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