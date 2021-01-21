using Android.App;
using Android.OS;

namespace QR
{
    [Activity(Label = "QR Scanner")]
    public class ScannerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_scanner);
        }
    }
}