using System.IO;
using System.Threading.Tasks;
using ZXing;
using ZXing.Mobile;
using ZXing.QrCode;

namespace QR.Service
{
    public class QRScanningService : IQRScanningService
    {
        public async Task<string> ScanAsync()
        {
            var options = new MobileBarcodeScanningOptions();
            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please wait...",
                CancelButtonText = "Cancel"
            };
            var scanResult = await scanner.Scan(options);
            return scanResult.Text;
        }

        public string Scan(Android.Graphics.Bitmap image)
        {
            BarcodeReader reader = new BarcodeReader()
            {
                AutoRotate = true,
                TryInverted = true
            };
            var bytes = ConvertBitmapToBytes(image);
            Result result = reader.Decode(bytes);
            return result.Text.Trim();
        }

        public Android.Graphics.Bitmap Write(string text)
        {
            var options = new QrCodeEncodingOptions()
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 250,
                Height = 250
            };
            var writer = new BarcodeWriter();
            writer.Format = ZXing.BarcodeFormat.QR_CODE;
            writer.Options = options;
            var bitmap = writer.Write(text);
            return bitmap;
        }

        public byte[] ConvertBitmapToBytes(Android.Graphics.Bitmap bitmap)
        {
            byte[] bitmapData;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }
            return bitmapData;
        }
    }
}