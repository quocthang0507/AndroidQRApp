using System.Threading.Tasks;

namespace QR.Service
{
    public interface IQRScanningService
    {
        Task<string> ScanAsync();
        string Scan(Android.Graphics.Bitmap image);
        Android.Graphics.Bitmap Write(string text);
    }
}