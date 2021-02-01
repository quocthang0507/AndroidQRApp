using Android.Graphics;

namespace QR.Service
{
	public interface IQRScanningService
	{
		void ScanAsync();
		string Scan(Bitmap image);
		Bitmap Encode(string text);
	}
}