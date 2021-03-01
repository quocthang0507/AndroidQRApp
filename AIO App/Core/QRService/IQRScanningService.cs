using Android.Graphics;

namespace AIOApp.QRService
{
	public interface IQRScanningService
	{
		void ScanAsync();
		void Scan(Bitmap image);
		Bitmap Encode(string text);
	}
}