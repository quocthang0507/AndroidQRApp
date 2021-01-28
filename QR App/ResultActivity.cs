using Android.App;
using Android.OS;

namespace QR
{
	[Activity(Label = "ResultActivity", Theme = "@style/AppTheme")]
	public class ResultActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.activity_qr_result);

		}
	}
}