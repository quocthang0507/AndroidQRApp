using AIOApp.Service;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;

namespace AIOApp
{
	[Activity(Label = "QR Scanner", Theme = "@style/AppTheme")]
	public class ScannerActivity : Activity
	{
		private Button btnQScan, btnSScan, btnScanImage;
		private ImageView imgScan;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_scanner);

			ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);

			InitControl();
		}

		private void InitControl()
		{
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
			PickAndScanPhoto();
		}

		private async void PickAndScanPhoto()
		{
			await CrossMedia.Current.Initialize();
			MediaFile file = await CrossMedia.Current.PickPhotoAsync();
			if (file != null)
			{
				string path = file.Path;

				Bitmap bitmap = BitmapFactory.DecodeFile(path);
				imgScan.SetImageBitmap(bitmap);

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