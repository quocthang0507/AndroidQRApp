using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIOApp.SensorLib
{
	public class Sensors
	{
		public SensorManager sensorManager;

		private static Sensors singleton;

		public Sensors()
		{
			sensorManager = (SensorManager)Application.Context.GetSystemService(Context.SensorService);
		}

		public static Sensors Instance
		{
			get
			{
				if (singleton == null)
					singleton = new Sensors();
				return singleton;
			}
		}
	}
}