using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using System;
using System.IO;

namespace AIOApp
{
	[Activity(Label = "ResultActivity", Theme = "@style/AppTheme")]
	public class ResultActivity : Activity
	{
		private ImageView imgQR;
		private Button btnSave;
		private Bitmap bmpQR;
		private const string EXTRA_QR = "QR CODE RESULT";

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.activity_qr_result);

			InitControl();

			GetQR();
			btnSave.Click += BtnSave_Click;
		}

		private void InitControl()
		{
			imgQR = FindViewById<ImageView>(Resource.Id.imgResult);
			btnSave = FindViewById<Button>(Resource.Id.btnSave);
		}

		private void GetQR()
		{
			bmpQR = (Bitmap)Intent.Extras.GetParcelable(EXTRA_QR);
			imgQR.SetImageBitmap(bmpQR);
		}

		private void BtnSave_Click(object sender, System.EventArgs e)
		{
			Java.IO.File storagePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
			string datetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
			string path = System.IO.Path.Combine(storagePath.ToString(), $"QR_{datetime}.jpg");
			SaveImage(bmpQR, path);
		}

		private void SaveImage(Bitmap bitmap, string filename)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(filename, FileMode.Create);
				bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, fs);
			}
			catch (Exception e)
			{
				Toast.MakeText(this, e.Message, ToastLength.Short).Show();
				return;
			}
			finally
			{
				if (fs != null)
				{
					fs.Flush();
					fs.Close();
				}
			}
			Toast.MakeText(this, $"Save successfully to your phone at {filename}", ToastLength.Long).Show();
		}
	}
}