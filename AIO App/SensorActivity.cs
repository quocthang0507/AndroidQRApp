using Android.App;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using System.Text;

namespace AIOApp
{
	[Activity(Label = "SensorActivity")]
	public class SensorActivity : Activity, ISensorEventListener
	{
		private TextView txtValue, txtName;
		private SensorManager sensorManager;

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
			txtValue = FindViewById<TextView>(Resource.Id.txtSensorValue);
			txtName = FindViewById<TextView>(Resource.Id.txtSensor);
			sensorManager = (SensorManager)Application.Context.GetSystemService(SensorService);

			txtName.Text = sensor.Name;
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
			StringBuilder sb = new StringBuilder();
			foreach (var value in e.Values)
			{
				sb.AppendLine(value.ToString());
			}
			txtValue.Text = sb.ToString();
		}
	}
}