using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;

namespace QR
{
	[Activity(Label = "ResultActivity", Theme = "@style/AppTheme")]
	public class ResultActivity : Activity
	{
		private ImageView imgQR;
		private const string EXTRA_QR = "QR CODE RESULT";

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.activity_qr_result);

			imgQR = FindViewById<ImageView>(Resource.Id.imgResult);

			Bitmap bitmap = (Bitmap)Intent.Extras.GetParcelable(EXTRA_QR);
			imgQR.SetImageBitmap(bitmap);
		}
	}
}