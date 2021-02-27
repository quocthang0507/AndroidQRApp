using AIOApp.SensorLib;
using Android.App;
using Android.Hardware;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;

namespace AIOApp.Core.SensorLib
{
	public class SensorAdapter : BaseAdapter<Sensor>
	{
		private readonly Sensor[] sensors;
		private readonly Activity context;
		private readonly int layout;

		public SensorAdapter(Activity context, Sensor[] sensors)
		{
			this.sensors = sensors;
			this.context = context;
			this.layout = Resource.Layout.list_sensor_layout;
		}

		public SensorAdapter(Activity context, IList<Sensor> sensors)
		{
			this.sensors = sensors.ToArray();
			this.context = context;
			this.layout = Resource.Layout.list_sensor_layout;
		}

		public override Sensor this[int position] => sensors[position];

		public override int Count => sensors.Length;

		public override long GetItemId(int position) => position;

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView;
			if (view == null)
			{
				view = context.LayoutInflater.Inflate(layout, null);
			}
			Sensor sensor = sensors[position];
			view.FindViewById<TextView>(Resource.Id.txtSensorName).Text = sensor.Name;
			view.FindViewById<TextView>(Resource.Id.txtSensorVendor).Text = "NSX: " + sensor.Vendor;
			KeyValuePair<int, string> result = Sensors.VietnameseSensorType.FirstOrDefault(x => x.Key == (int)sensor.Type);
			if (!result.Equals(default(KeyValuePair<int, string>)))
			{
				view.FindViewById<TextView>(Resource.Id.txtSensorType).Text = "Loại: " + result.Value;
			}
			else
			{
				view.FindViewById<TextView>(Resource.Id.txtSensorType).Text = "Loại: Không xác định";
			}
			return view;
		}

	}
}