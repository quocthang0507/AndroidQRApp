using System.Threading.Tasks;
using ZXing.Mobile;
using ZXing.QrCode;

namespace QR.Service
{
    public class QRScanningService : IQRScanningService
    {
        public async Task<string> ScanAsync()
        {
            var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();
            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please wait...",
                CancelButtonText = "Cancel"
            };
            var scanResult = await scanner.Scan(optionsCustom);
            return scanResult.Text;
        }

        public void Write()
        {
            var optionsCustom = new QrCodeEncodingOptions()
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 250,
                Height = 250
            };
            var writer = new BarcodeWriter();
            writer.Format = ZXing.BarcodeFormat.QR_CODE;
            writer.Options = optionsCustom;
        }
    }
}