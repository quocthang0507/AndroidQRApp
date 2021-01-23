using System;

namespace QR.Util
{
	public class Helper
	{
		public static bool CheckValidURL(string url)
		{
			Uri uriResult;
			bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
				&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
			return result;
		}
	}
}