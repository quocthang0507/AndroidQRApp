﻿using Android.Content;
using Android.Graphics;
using System;
using ZXing;
using ZXing.Client.Result;
using ZXing.Common;
using ZXing.Mobile;
using ZXing.QrCode;
using Result = ZXing.Result;
using Settings = Android.Provider.Settings;

namespace QR.Service
{
	public class QRScanningService : IQRScanningService
	{
		private Context context;
		private bool difficulty = false;

		public QRScanningService(Context context)
		{
			this.context = context;
		}

		public QRScanningService(Context context, bool difficulty)
		{
			this.context = context;
			this.difficulty = difficulty;
		}

		public async void ScanAsync()
		{
			var options = new MobileBarcodeScanningOptions();
			if (difficulty)
			{
				options = new MobileBarcodeScanningOptions()
				{
					TryHarder = true,
					AutoRotate = true,
					TryInverted = true
				};
			}

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

		private BinaryBitmap ConvertBitmapToBytes(Bitmap bitmap)
		{
			int[] intArr = new int[bitmap.Width * bitmap.Height];
			bitmap.GetPixels(intArr, 0, bitmap.Width, 0, 0, bitmap.Width, bitmap.Height);
			var bytes = ConvertIntArrToByteArr(intArr);
			LuminanceSource source = new RGBLuminanceSource(bytes, bitmap.Width, bitmap.Height);
			BinaryBitmap binaryBitmap = new BinaryBitmap(new HybridBinarizer(source));
			return binaryBitmap;
		}

		private static byte[] ConvertIntArrToByteArr(int[] src)
		{
			byte[] result = new byte[src.Length * sizeof(int)];
			Buffer.BlockCopy(src, 0, result, 0, result.Length);
			return result;
		}

		private ParsedResultType GetResultType(Result result)
		{
			ParsedResult parser = ResultParser.parseResult(result);
			return parser.Type;
		}

		private void ShowProperlyResult(string content, ParsedResultType type)
		{
			switch (type)
			{
				case ParsedResultType.ADDRESSBOOK:
					break;
				case ParsedResultType.EMAIL_ADDRESS:
					IntentHelper.OpenAlert(context, "Would you like to open Email App?", "Open", content);
					break;
				case ParsedResultType.PRODUCT:
					break;
				case ParsedResultType.URI:
					IntentHelper.OpenAlert(context, "Would you like to open Browser?", "Open", content);
					break;
				case ParsedResultType.TEXT:
					IntentHelper.OpenAlert(context, "Text", content);
					break;
				case ParsedResultType.GEO:
					IntentHelper.OpenAlert(context, "Would you like to open Map?", "Open", content);
					break;
				case ParsedResultType.TEL:
					IntentHelper.OpenAlert(context, "Would you like to open Dialer?", "Open", content);
					break;
				case ParsedResultType.SMS:
					IntentHelper.OpenAlert(context, "Would you like to open Message App?", "Open", content);
					break;
				case ParsedResultType.CALENDAR:
					break;
				case ParsedResultType.WIFI:
					IntentHelper.OpenAlert(context, "Would you like to connect this wifi?", "Connect", content, Settings.ActionWirelessSettings);
					break;
				case ParsedResultType.ISBN:
					break;
				case ParsedResultType.VIN:
					break;
				default:
					IntentHelper.OpenAlert(context, "Result", content);
					break;
			}
		}
	}
}