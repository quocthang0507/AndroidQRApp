using AIOApp.SensorLib;
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
		private ListView listView;

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

			List<string> menu = Sensors.Instance.GetSensors.Select(s => s.Name).ToList();
			ShowListView(menu);
		}

		public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
		{
			inflater.Inflate(Resource.Menu.option_menu, menu);
			// base.OnCreateOptionsMenu(menu, inflater);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			List<string> menu = Sensors.Instance.GetSensors.Select(s => s.Name).ToList();
			switch (item.ItemId)
			{
				case Resource.Id.menu_sort_az:
					menu = menu.OrderBy(x => x).ToList();
					break;
				case Resource.Id.menu_sort_za:
					menu = menu.OrderByDescending(x => x).ToList();
					break;
				case Resource.Id.menu_sort_default:
					break;
				default:
					return base.OnOptionsItemSelected(item);
			}
			ShowListView(menu);
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
			switch (position)
			{
				default:
					break;
			}
		}

		private void ShowListView(List<string> menu)
		{
			ArrayAdapter<string> adapter = new ArrayAdapter<string>(view.Context, Android.Resource.Layout.SimpleListItem1, menu);
			listView.Adapter = adapter;
		}
	}
}