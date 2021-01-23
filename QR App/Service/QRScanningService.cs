using Android.App;
using Android.Content;
using Android.Graphics;
using System;
using ZXing;
using ZXing.Client.Result;
using ZXing.Common;
using ZXing.Mobile;
using ZXing.QrCode;
using Result = ZXing.Result;

namespace QR.Service
{
	public class QRScanningService : IQRScanningService
	{
		private Context context;

		public QRScanningService(Context context)
		{
			this.context = context;
		}

		public async void ScanAsync()
		{
			var options = new MobileBarcodeScanningOptions()
			{
				TryHarder = true,
				AutoRotate = true,
				TryInverted = true
			};
			var scanner = new MobileBarcodeScanner()
			{
				TopText = "Hold the camera up to the barcode\nAbout 6 inches away",
				BottomText = "Wait for the barcode to automatically scan!",
				CancelButtonText = "Cancel",
				CameraUnsupportedMessage = "The device's camera is not supported"
			};
			var scanResult = await scanner.Scan(options);
			ShowProperlyResult(scanResult.Text, GetResultType(scanResult));
		}

		public string Scan(Bitmap image)
		{
			Reader reader = new MultiFormatReader();
			var bytes = ConvertBitmapToBytes(image);
			Result result = reader.decode(bytes);
			return result.Text.Trim();
		}

		public Bitmap Write(string text)
		{
			var options = new QrCodeEncodingOptions()
			{
				DisableECI = true,
				CharacterSet = "UTF-8",
				Width = 250,
				Height = 250
			};
			var writer = new BarcodeWriter();
			writer.Format = BarcodeFormat.QR_CODE;
			writer.Options = options;
			var bitmap = writer.Write(text);
			return bitmap;
		}

		public BinaryBitmap ConvertBitmapToBytes(Bitmap bitmap)
		{
			int[] intArr = new int[bitmap.Width * bitmap.Height];
			bitmap.GetPixels(intArr, 0, bitmap.Width, 0, 0, bitmap.Width, bitmap.Height);
			var bytes = ConvertIntArrToByteArr(intArr);
			LuminanceSource source = new RGBLuminanceSource(bytes, bitmap.Width, bitmap.Height);
			BinaryBitmap binaryBitmap = new BinaryBitmap(new HybridBinarizer(source));
			return binaryBitmap;
		}

		public static byte[] ConvertIntArrToByteArr(int[] src)
		{
			byte[] result = new byte[src.Length * sizeof(int)];
			Buffer.BlockCopy(src, 0, result, 0, result.Length);
			return result;
		}

		public ParsedResultType GetResultType(Result result)
		{
			ParsedResult parser = ResultParser.parseResult(result);
			return parser.Type;
		}

		private void ShowAlertWithURL(string title, string url)
		{
			AlertDialog dialog = null;
			dialog = new AlertDialog.Builder(context)
			.SetTitle(title)
			.SetMessage(url)
			.SetNegativeButton("Cancel", (sender, e) =>
			{
				dialog.Cancel();
			})
			.SetPositiveButton("Open", (sender, e) =>
			{
				Intent browserIntent = new Intent(Intent.ActionDefault);
				browserIntent.SetData(Android.Net.Uri.Parse(url));
				context.StartActivity(browserIntent);
			})
			.Create();
			dialog.Show();
		}


		public void ShowProperlyResult(string text, ParsedResultType type)
		{
			switch (type)
			{
				case ParsedResultType.ADDRESSBOOK:
					break;
				case ParsedResultType.EMAIL_ADDRESS:
					break;
				case ParsedResultType.PRODUCT:
					break;
				case ParsedResultType.URI:
					ShowAlertWithURL("Would you like to open this URL", text);
					break;
				case ParsedResultType.TEXT:
					break;
				case ParsedResultType.GEO:
					break;
				case ParsedResultType.TEL:
					break;
				case ParsedResultType.SMS:
					break;
				case ParsedResultType.CALENDAR:
					break;
				case ParsedResultType.WIFI:
					break;
				case ParsedResultType.ISBN:
					break;
				case ParsedResultType.VIN:
					break;
				default:
					break;
			}
		}
	}
}