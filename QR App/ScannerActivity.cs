using Android.App;
using Android.Content;
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
        TextView txtResult;
        ImageView imgScan;
        int pickImageID = 1000;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_scanner);

            ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);

            btnScan = FindViewById<Button>(Resource.Id.btnScan);
            btnScanImage = FindViewById<Button>(Resource.Id.btnScanImage);
            txtResult = FindViewById<TextView>(Resource.Id.txtResult);
            imgScan = FindViewById<ImageView>(Resource.Id.imgScan);

            btnScan.Click += btnScan_Clicked;
            btnScanImage.Click += btnScanImage_Clicked;
        }

        private async void btnScan_Clicked(object sender, EventArgs e)
        {
            try
            {
                var scanner = new QRScanningService();
                var result = await scanner.ScanAsync();
                if (result != null)
                {
                    txtResult.Text = result;
                }
            }
            catch
            {
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

                var bitmap = imgScan.GetDrawingCache(true);
                ScanImage(bitmap);
            }
        }

        private void ScanImage(Android.Graphics.Bitmap bitmap)
        {
            try
            {
                var scanner = new QRScanningService();
                var result = scanner.Scan(bitmap);
                if (result != null)
                {
                    txtResult.Text = result;
                }
            }
            catch
            {
            }
        }
    }
}