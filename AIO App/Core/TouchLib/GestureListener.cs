using Android.Views;
using System;

namespace AIOApp.TouchLib
{
	/// <summary>
	/// https://stackoverflow.com/questions/11327095/implement-the-swipe-gesture-on-grid-view/35394472
	/// </summary>
	public class GestureListener : GestureDetector.SimpleOnGestureListener
	{
		public delegate void GoSpecificMonth();
		public GoSpecificMonth GoNextMonth, GoPreviousMonth;

		private static readonly int SWIPE_THRESHOLD = 100;
		private static readonly int SWIPE_VELOCITY_THRESHOLD = 100;

		public override bool OnDown(MotionEvent e)
		{
			return base.OnDown(e);
		}

		public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			float diffY = e2.GetY() - e1.GetY();
			float diffX = e2.GetX() - e1.GetX();
			if (Math.Abs(diffX) > Math.Abs(diffY))
			{
				if (Math.Abs(diffX) > SWIPE_THRESHOLD && Math.Abs(velocityX) > SWIPE_VELOCITY_THRESHOLD)
				{
					if (diffX > 0)
					{
						OnSwipeRight();
					}
					else
					{
						OnSwipeLeft();
					}
				}
			}
			else
			{
				if (Math.Abs(diffY) > SWIPE_THRESHOLD && Math.Abs(velocityY) > SWIPE_VELOCITY_THRESHOLD)
				{
					if (diffY > 0)
					{
						OnSwipeBottom();
					}
					else
					{
						OnSwipeTop();
					}
				}
			}
			return true;
		}

		private void OnSwipeLeft()
		{
			GoNextMonth.Invoke();
		}

		private void OnSwipeRight()
		{
			GoPreviousMonth.Invoke();
		}

		private void OnSwipeTop()
		{

		}

		private void OnSwipeBottom()
		{

		}
	}
}