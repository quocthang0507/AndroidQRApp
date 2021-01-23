using Android.App;
using Android.Content;
using Uri = Android.Net.Uri;

namespace QR.Service
{
	public class IntentHelper
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

		public static void OpenAlert(Context context, string title, string positiveTitle, string data, string intentType = Intent.ActionDefault)
		{
			AlertDialog dialog = null;
			dialog = new AlertDialog.Builder(context)
			.SetTitle(title)
			.SetMessage(data)
			.SetNegativeButton("Cancel", (sender, e) =>
			{
				dialog.Cancel();
			})
			.SetPositiveButton(positiveTitle, (sender, e) =>
			{
				Intent intent = new Intent((string)intentType);
				intent.SetData(Uri.Parse(data));
				context.StartActivity(intent);
			})
			.Create();
			dialog.Show();
		}
	}
}