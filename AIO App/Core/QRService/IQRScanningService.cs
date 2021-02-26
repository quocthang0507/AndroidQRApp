using Android.Graphics;

namespace AIOApp.QRService
{
	public interface IQRScanningService
	{
		void ScanAsync();
		string Scan(Bitmap image);
		Bitmap Encode(string text);
	}
}