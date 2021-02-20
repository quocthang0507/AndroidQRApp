using Android.Graphics;

namespace AIOApp.Service
{
	public interface IQRScanningService
	{
		void ScanAsync();
		string Scan(Bitmap image);
		Bitmap Encode(string text);
	}
}