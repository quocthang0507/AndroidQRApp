using Android.Support.V4.View;
using Android.Views;
using static Android.Views.View;

namespace AIOApp.TouchLib
{
	/// <summary>
	/// https://stackoverflow.com/questions/25223661/how-to-use-setontouchlistener-in-c-sharp-xamarin/25223860
	/// </summary>
	public class TouchListener : Java.Lang.Object, IOnTouchListener
	{
		private GestureDetectorCompat detector;

		public TouchListener(GestureDetectorCompat detector)
		{
			this.detector = detector;
		}

		public bool OnTouch(View v, MotionEvent e)
		{
			detector.OnTouchEvent(e);
			return false;
		}
	}
}