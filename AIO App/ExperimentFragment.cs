using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using Fragment = Android.Support.V4.App.Fragment;

namespace AIOApp
{
	public class ExperimentFragment : Fragment
	{
		private string[] menu = new string[] { "Compass" };
		private View view;
		private ListView listView;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

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
			ArrayAdapter<string> adapter = new ArrayAdapter<string>(view.Context, Android.Resource.Layout.SimpleListItem1, menu);
			listView.SetAdapter(adapter);
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
	}
}