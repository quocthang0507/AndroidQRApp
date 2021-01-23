using Android.App;
using ZXing.Client.Result;

namespace QR.Service
{
	public interface IQRScanningService
	{
		void ScanAsync();
		string Scan(Android.Graphics.Bitmap image);
		Android.Graphics.Bitmap Write(string text);
		ParsedResultType GetResultType(Result result);
		void ShowAlertWithURL(string title, string url);
		void ShowProperlyResult(string text, ParsedResultType type);
	}
}