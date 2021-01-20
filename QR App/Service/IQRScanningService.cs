using System.Threading.Tasks;

namespace QR.Service
{
	public interface IQRScanningService
	{
		Task<string> ScanAsync();
		void Write();
	}
}