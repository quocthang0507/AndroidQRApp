using Android.Graphics;

namespace QR.Service
{
	public interface IQRScanningService
	{
		void ScanAsync();
		string Scan(Bitmap image);
		Bitmap Write(string text);
	}
}