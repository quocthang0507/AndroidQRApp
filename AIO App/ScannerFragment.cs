using AIOApp.QRService;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using Fragment = Android.Support.V4.App.Fragment;

namespace AIOApp
{
	public class ScannerFragment : Fragment
	{
		private View view;
		private Button btnQScan, btnSScan, btnScanImage;
		private ImageView imgScan;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		private void InitControl()
		{
			btnQScan = view.FindViewById<Button>(Resource.Id.btnQScan);
			btnSScan = view.FindViewById<Button>(Resource.Id.btnSScan);
			btnScanImage = view.FindViewById<Button>(Resource.Id.btnScanImage);
			imgScan = view.FindViewById<ImageView>(Resource.Id.imgScan);

			btnQScan.Click += btnQScan_Clicked;
			btnSScan.Click += btnSScan_Clicked;
			btnScanImage.Click += btnScanImage_Clicked;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			view = inflater.Inflate(Resource.Layout.activity_scanner, container, false);
			return view;

			// return base.OnCreateView(inflater, container, savedInstanceState);
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			InitControl();
		}

		private void btnQScan_Clicked(object sender, EventArgs e)
		{
			try
			{
				QRScanningService service = new QRScanningService(view.Context, false);
				service.ScanAsync();
			}
			catch (Exception ex)
			{
				IntentHelper.OpenAlert(view.Context, "Error", ex.Message);
			}
		}

		private void btnSScan_Clicked(object sender, EventArgs e)
		{
			try
			{
				QRScanningService scanner = new QRScanningService(view.Context, true);
				scanner.ScanAsync();
			}
			catch (Exception ex)
			{
				IntentHelper.OpenAlert(view.Context, "Error", ex.Message);
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
				QRScanningService scanner = new QRScanningService(view.Context);
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