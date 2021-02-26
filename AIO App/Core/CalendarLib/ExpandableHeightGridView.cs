using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;

namespace AIOApp.Core.CalendarLib
{
	/// <summary>
	/// https://stackoverflow.com/questions/8481844/gridview-height-gets-cut
	/// </summary>
	public class ExpandableHeightGridView : GridView
	{
		private bool expanded = false;

		public ExpandableHeightGridView(Context context) : base(context)
		{
		}

		public ExpandableHeightGridView(Context context, IAttributeSet attrs) : base(context, attrs)
		{
		}

		public ExpandableHeightGridView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
		}

		public ExpandableHeightGridView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
		{
		}

		protected ExpandableHeightGridView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public bool IsExpanded
		{
			get { return expanded; }
			set { expanded = value; }
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			if (IsExpanded)
			{
				int expandSpec = MeasureSpec.MakeMeasureSpec(MeasuredSizeMask, MeasureSpecMode.AtMost);
				base.OnMeasure(widthMeasureSpec, expandSpec);
				ViewGroup.LayoutParams @params = LayoutParameters;
				@params.Height = this.MeasuredHeight;
			}
			else
			{
				base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
			}
		}
	}
}