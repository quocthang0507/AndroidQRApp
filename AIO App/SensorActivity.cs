using AIOApp.SensorLib;
using Android.App;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using System;

namespace AIOApp
{
	[Activity(Label = "SensorActivity")]
	public class SensorActivity : Activity, ISensorEventListener
	{
		private TextView txtInfo, txtName;
		private SensorManager sensorManager;
		private ImageView imgVisualization;
		// For compass
		private float DegreeStart = 0f;

		/// <summary>
		/// Sets sensor to trace
		/// </summary>
		public static Sensor sensor { get; set; }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.activity_sensors);
			InitControl();
		}

		private void InitControl()
		{
			txtInfo = FindViewById<TextView>(Resource.Id.txtSensorInfo);
			txtName = FindViewById<TextView>(Resource.Id.txtSensorName);
			imgVisualization = FindViewById<ImageView>(Resource.Id.imgVisualization);
			sensorManager = (SensorManager)Application.Context.GetSystemService(SensorService);

			txtName.Text = sensor.Name;

			imgVisualization.Visibility = sensor.Type != SensorType.Orientation ? ViewStates.Invisible : Android.Views.ViewStates.Visible;
		}

		protected override void OnResume()
		{
			base.OnResume();
			sensorManager.RegisterListener(this, sensor, SensorDelay.Normal);
			DegreeStart = 0f;
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

			txtInfo.Text = Sensors.ShowProperlyInfo(e);

			// https://www.codespeedy.com/simple-compass-code-with-android-studio/
			if (sensor.Type == SensorType.Orientation)
			{
				// get angle around the z-axis rotated
				float degree = (float)Math.Round(e.Values[0]);

				// rotation animation - reverse turn degree degrees
				RotateAnimation animation = new RotateAnimation(
						DegreeStart,
						-degree,
						Dimension.RelativeToSelf, 0.5f,
						Dimension.RelativeToSelf, 0.5f)
				{
					// set the compass animation after the end of the reservation status
					FillAfter = true,
					// set how long the animation for the compass image will take place
					Duration = 210
				};
				// Start animation of compass image
				imgVisualization.StartAnimation(animation);
				DegreeStart = -degree;
			}
		}
	}
}