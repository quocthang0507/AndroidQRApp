using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace AIOApp
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/logo")]
	public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
	{
		TextView txtMessage;
		BottomNavigationView bottomNav;
		Intent intent;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			CheckWritePermission();
			CheckReadPermission();

			InitControl();

			bottomNav.SetOnNavigationItemSelectedListener(this);
		}

		private void InitControl()
		{
			txtMessage = FindViewById<TextView>(Resource.Id.message);
			bottomNav = FindViewById<BottomNavigationView>(Resource.Id.bottomNav);
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
				case Resource.Id.nav_calendar:
					intent = new Intent(this, typeof(CalendarActivity));
					StartActivity(intent);
					return true;
			}
			return false;
		}

		private void CheckWritePermission()
		{
			if (CheckSelfPermission(Android.Manifest.Permission.WriteExternalStorage) == Permission.Denied)
			{
				RequestPermissions(new string[] { Manifest.Permission.WriteExternalStorage }, 1);
			}
		}

		private void CheckReadPermission()
		{
			if (CheckSelfPermission(Android.Manifest.Permission.ReadExternalStorage) == Permission.Denied)
			{
				RequestPermissions(new string[] { Manifest.Permission.ReadExternalStorage }, 1);
			}
		}
	}
}

