using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace QR
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/qr_code")]
	public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener, View.IOnClickListener
	{
		TextView txtMessage;
		FloatingActionButton btnHome;
		BottomNavigationView bottomNav;
		Intent intent;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			txtMessage = FindViewById<TextView>(Resource.Id.message);
			btnHome = FindViewById<FloatingActionButton>(Resource.Id.btnHome);
			bottomNav = FindViewById<BottomNavigationView>(Resource.Id.bottomNav);

			btnHome.SetOnClickListener(this);
			bottomNav.SetOnNavigationItemSelectedListener(this);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		public bool OnNavigationItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Resource.Id.nav_scanner:
					intent = new Intent(this, typeof(ScannerActivity));
					StartActivity(intent);
					return true;
				case Resource.Id.nav_generator:
					intent = new Intent(this, typeof(GeneratorActivity));
					StartActivity(intent);
					return true;
			}
			return false;
		}

		public void OnClick(View v)
		{
			switch (v.Id)
			{
				case Resource.Id.btnHome:
					Finish();
					break;
				default:
					break;
			}
		}
	}
}

