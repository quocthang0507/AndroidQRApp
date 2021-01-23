using Android.App;
using Android.Content;
using Uri = Android.Net.Uri;

namespace QR.Util
{
	public class Helper
	{
		public static void ShowCommonAlert(Context context, string title, string message)
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

		public static void ShowUrlAlert(Context context, string title, string positiveTitle, string url)
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

	}
}