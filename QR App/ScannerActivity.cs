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
		Button btnScan, btnScanImage;
		ImageView imgScan;
		int pickImageID = 1000;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_scanner);

			ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);

			btnScan = FindViewById<Button>(Resource.Id.btnScan);
			btnScanImage = FindViewById<Button>(Resource.Id.btnScanImage);
			imgScan = FindViewById<ImageView>(Resource.Id.imgScan);

			btnScan.Click += btnScan_Clicked;
			btnScanImage.Click += btnScanImage_Clicked;
		}

		private async void btnScan_Clicked(object sender, EventArgs e)
		{
			try
			{
				var scanner = new QRScanningService(this);
				scanner.ScanAsync();
			}
			catch (Exception ex)
			{
				ShowAlert("Error", ex.Message);
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
				var scanner = new QRScanningService(this);
				var result = scanner.Scan(bitmap);
				if (result != null)
				{
				}
			}
			catch (Exception)
			{
			}
		}

		private void ShowAlert(string title, string message)
		{
			AlertDialog dialog = null;
			dialog = new AlertDialog.Builder(this)
			.SetTitle(title)
			.SetMessage(message)
			.SetNeutralButton("OK", (sender, e) =>
			{
				dialog.Cancel();
			}).Create();
			dialog.Show();
		}
	}
}