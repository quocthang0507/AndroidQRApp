using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using QR.Service;
using System;

namespace QR
{
	[Activity(Label = "QR Scanner", Theme = "@style/AppTheme")]
	public class ScannerActivity : Activity
	{
		Button btnQScan, btnSScan, btnScanImage;
		ImageView imgScan;
		int pickImageID = 1000;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_scanner);

			ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);

			btnQScan = FindViewById<Button>(Resource.Id.btnQScan);
			btnSScan = FindViewById<Button>(Resource.Id.btnSScan);
			btnScanImage = FindViewById<Button>(Resource.Id.btnScanImage);
			imgScan = FindViewById<ImageView>(Resource.Id.imgScan);

			btnQScan.Click += btnQScan_Clicked;
			btnSScan.Click += btnSScan_Clicked;
			btnScanImage.Click += btnScanImage_Clicked;
		}

		private void btnQScan_Clicked(object sender, EventArgs e)
		{
			try
			{
				QRScanningService service = new QRScanningService(this, false);
				service.ScanAsync();
			}
			catch (Exception ex)
			{
				IntentHelper.OpenAlert(this, "Error", ex.Message);
			}
		}

		private void btnSScan_Clicked(object sender, EventArgs e)
		{
			try
			{
				QRScanningService scanner = new QRScanningService(this, true);
				scanner.ScanAsync();
			}
			catch (Exception ex)
			{
				IntentHelper.OpenAlert(this, "Error", ex.Message);
			}
		}

		private void btnScanImage_Clicked(object sender, EventArgs e)
		{
			Intent gallery = new Intent();
			gallery.SetType("image/*");
			gallery.SetAction(Intent.ActionGetContent);
			StartActivityForResult(Intent.CreateChooser(gallery, "Select QR Code image from your gallery"), pickImageID);
		}

		protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
		{
			if ((requestCode == pickImageID) && resultCode == Result.Ok && data != null)
			{
				Android.Net.Uri uri = data.Data;
				imgScan.SetImageURI(uri);

				string path = uri.Path;
				Bitmap bitmap = BitmapFactory.DecodeFile(path);

				ScanImage(bitmap);
			}
		}

		private void ScanImage(Bitmap bitmap)
		{
			try
			{
				QRScanningService scanner = new QRScanningService(this);
				string result = scanner.Scan(bitmap);
				if (result != null)
				{
				}
			}
			catch (Exception)
			{
			}
		}
	}
}