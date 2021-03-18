using AIOApp.Core.SensorLib;
using AIOApp.SensorLib;
using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;
using Fragment = Android.Support.V4.App.Fragment;

namespace AIOApp
{
	public class ExperimentFragment : Fragment
	{
		private View view;
		private List<Sensor> sensors;
		private ListView listView;
		public static Sensor selectedSensor;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			HasOptionsMenu = true;
			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			view = inflater.Inflate(Resource.Layout.activity_experiments, container, false);
			return view;

			// return base.OnCreateView(inflater, container, savedInstanceState);
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);

			InitControl();

			sensors = Sensors.Instance.GetSensors;
			ShowListView();
		}

		public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
		{
			inflater.Inflate(Resource.Menu.option_menu, menu);
			// base.OnCreateOptionsMenu(menu, inflater);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			sensors = Sensors.Instance.GetSensors;
			switch (item.ItemId)
			{
				case Resource.Id.menu_group_type:
					sensors = sensors.OrderBy(x => x.Type).ToList();
					break;
				case Resource.Id.menu_sort_az_name:
					sensors = sensors.OrderBy(x => x.Name).ToList();
					break;
				case Resource.Id.menu_sort_az_type:
					sensors = sensors.OrderBy(s => Sensors.VietnameseSensorType.FirstOrDefault(x => x.Key == (int)s.Type).Value).ToList();
					break;
				case Resource.Id.menu_sort_za_name:
					sensors = sensors.OrderByDescending(x => x.Name).ToList();
					break;
				case Resource.Id.menu_sort_za_type:
					sensors = sensors.OrderByDescending(s => Sensors.VietnameseSensorType.FirstOrDefault(x => x.Key == (int)s.Type).Value).ToList();
					break;
				case Resource.Id.menu_sort_default:
					break;
				default:
					return base.OnOptionsItemSelected(item);
			}
			ShowListView();
			return true;
		}

		private void InitControl()
		{
			listView = view.FindViewById<ListView>(Resource.Id.menuExperiment);

			listView.ItemClick += ListView_ItemClick;
		}

		private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			int position = e.Position;
			selectedSensor = sensors[position];
			SensorActivity.sensor = selectedSensor;
			Intent intent = new Intent(Activity, typeof(SensorActivity));
			StartActivity(intent);
		}

		private void ShowListView()
		{
			SensorAdapter adapter = new SensorAdapter((Activity)view.Context, sensors);
			listView.Adapter = adapter;
		}
	}
}