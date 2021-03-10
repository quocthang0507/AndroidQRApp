using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;
using ZXing.Mobile;
using Fragment = Android.Support.V4.App.Fragment;

namespace AIOApp
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/logo")]
	public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
	{
		private const string tagScanner = "SCANNER";
		private const string tagGenerator = "GENERATOR";
		private const string tagCalendar = "CALENDAR";
		private const string tagExperiment = "EXPERIMENTS";
		private BottomNavigationView bottomNav;
		private bool doubleBack = false;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			CheckWritePermission();
			CheckReadPermission();

			Platform.Init(Application);
			MobileBarcodeScanner.Initialize(Application);


			InitControl();
			bottomNav.SetOnNavigationItemSelectedListener(this);

			LoadFragment(new CalendarFragment());
		}

		public override void OnBackPressed()
		{
			if (doubleBack)
			{
				base.OnBackPressed();
				return;
			}

			doubleBack = true;
			Toast.MakeText(this, "Nhấn BACK một lần nữa để thoát", ToastLength.Short).Show();

			new Handler(Looper.MainLooper).PostDelayed(() => { doubleBack = false; }, 2000);
		}

		private void InitControl()
		{
			bottomNav = FindViewById<BottomNavigationView>(Resource.Id.bottomNav);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		public bool OnNavigationItemSelected(IMenuItem item)
		{
			Fragment fragment;
			switch (item.ItemId)
			{
				case Resource.Id.nav_scanner:
					fragment = new ScannerFragment();
					break;
				case Resource.Id.nav_generator:
					fragment = new GeneratorFragment();
					break;
				case Resource.Id.nav_calendar:
					fragment = new CalendarFragment();
					break;
				case Resource.Id.nav_experiments:
					fragment = new ExperimentFragment();
					break;
				default:
					return false;
			}
			LoadFragment(fragment);
			return true;
		}

		private bool LoadFragment(Fragment fragment, string tag = null)
		{
			if (fragment != null)
			{
				_ = SupportFragmentManager.BeginTransaction()
				.Replace(Resource.Id.container, fragment)
				// .AddToBackStack(null)
				.Commit();
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

