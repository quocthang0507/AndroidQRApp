﻿using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Widget;

namespace AIOApp
{
	[Activity(Label = "SensorActivity")]
	public class SensorActivity : Activity, ISensorEventListener
	{
		private TextView txtValue;
		private SensorManager sensorManager;
		private Sensor sensor;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.activity_sensors);
			InitControl();
		}

		private void InitControl()
		{
			txtValue = FindViewById<TextView>(Resource.Id.txtSensorValue);
			sensorManager = (SensorManager)Application.Context.GetSystemService(Context.SensorService);
		}

		protected override void OnResume()
		{
			base.OnResume();
			sensorManager.RegisterListener(this, sensor, SensorDelay.Normal);
		}

		protected override void OnPause()
		{
			base.OnPause();
			sensorManager.UnregisterListener(this);
		}

		public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
		{
			// Do something here if sensor accuracy changes.
		}

		public void OnSensorChanged(SensorEvent e)
		{
			// The light sensor returns a single value.
			// Many sensors return 3 values, one for each axis.

		}
	}
}