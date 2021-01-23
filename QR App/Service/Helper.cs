using Android.App;
using Android.Content;
using Uri = Android.Net.Uri;

namespace QR.Service
{
	public class Helper
	{
		public static void OpenAlert(Context context, string title, string message)
		{
			AlertDialog dialog = null;
			dialog = new AlertDialog.Builder(context)
			.SetTitle(title)
			.SetMessage(message)
			.SetNeutralButton("OK", (sender, e) =>
			{
				dialog.Cancel();
			}).Create();
			dialog.Show();
		}

		public static void OpenUrlAlert(Context context, string title, string positiveTitle, string url)
		{
			AlertDialog dialog = null;
			dialog = new AlertDialog.Builder(context)
			.SetTitle(title)
			.SetMessage(url)
			.SetNegativeButton("Cancel", (sender, e) =>
			{
				dialog.Cancel();
			})
			.SetPositiveButton(positiveTitle, (sender, e) =>
			{
				Intent browserIntent = new Intent(Intent.ActionDefault);
				browserIntent.SetData(Uri.Parse(url));
				context.StartActivity(browserIntent);
			})
			.Create();
			dialog.Show();
		}

		public static void OpenGeoAlert(Context context, string title, string geolocation)
		{
			AlertDialog dialog = null;
			dialog = new AlertDialog.Builder(context)
			.SetTitle(title)
			.SetMessage(geolocation)
			.SetNegativeButton("Cancel", (sender, e) =>
			{
				dialog.Cancel();
			})
			.SetPositiveButton("Open Map", (sender, e) =>
			{
				Uri geo = Uri.Parse(geolocation);
				Intent mapIntent = new Intent(Intent.ActionView, geo);
				mapIntent.SetPackage("com.google.android.apps.maps");
				context.StartActivity(mapIntent);
			})
			.Create();
			dialog.Show();
		}
	}
}