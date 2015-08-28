using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Util;


namespace SlidingTabLayOut
{
	public class SlidingTabStrip : LinearLayout
	{
		private const int DEFAULT_BOTTOM_BORDER_THICKNESS_DIPS = 2;
		private const byte DEFAULT_BOTTOM_BORDER_COLOR_ALPHA = 0X026;
		private const int SELECTED_INDICATOR_THICKNESS_DIPS = 8;
		private int[] INDICATOR_COLORS = { 0x19A319, 0x0000FC };
		private int[] DIVIDER_COLORS = { 0xC5C5C5 };


		private const int DEFAULT_DIVIDER_THICKNESS_DIPS = 1;
		private const float DEFAULT_DIVIDER_HEIGHT = 0.5f;

		private int uBottomBorderThickness;
		private Paint uBottomBorderPaint;
		private int uDefaultBottomBorderColor;


		private int uSelectedIndicatorThickness;
		private Paint uSelectedIndicatorPaint;


		private Paint uDividerPaint;
		private float uDividerHeight;


		private int uSelectedPosition;
		private int uSelectionOffset;


		private SlidingTabScrollView.TabColorizer uCustomTabColorizer;
		private SimpleTabColorizer uDefaultTabColorizer;

		public SlidingTabStrip(Context context) : this(context, null){}	

		public SlidingTabStrip (Context context, IAttributeSet attrs) : base(context,attrs)
		{
			SetWillNotDraw (false);

			float density = Resources.DisplayMetrics.Density;

			TypedValue outValue = new TypedValue ();
			Context.Theme.ResolveAttribute (Android.Resource.Attribute.ColorForeground, outValue, true);
			int themeForeGround = outValue.Data;
			uDefaultBottomBorderColor = SetColorAlpha (themeForeGround, DEFAULT_BOTTOM_BORDER_COLOR_ALPHA);		

			uDefaultTabColorizer = new SimpleTabColorizer ();
			uDefaultTabColorizer.IndicatorColors = INDICATOR_COLORS;
			uDefaultTabColorizer.DividerColors = DIVIDER_COLORS;

			uBottomBorderThickness = (int)(DEFAULT_BOTTOM_BORDER_THICKNESS_DIPS * density);
			uBottomBorderPaint = new Paint ();
			uBottomBorderPaint.Color = GetColorFromInteger (0xC5C5C5);
		}
		public SlidingTabScrollView.TabColorizer CustomTabColorizer
		{
			set{ 
				uCustomTabColorizer = value;
				this.Invalidate ();
			}
		}

		public int [] SelectedIndicatorColors
		{
			set{ 
				uCustomTabColorizer = null;
				uDefaultTabColorizer.IndicatorColors = value;
				this.Invalidate ();
			}
		}

		public int [] DividerColors
		{
			set{ 
				uDefaultTabColorizer = null;
				uDefaultTabColorizer.DividerColors = value;
				this.Invalidate;
			}
		}
			
		private Color GetColorFromInteger(int color)
		{
			return Color.Rgb (Color.GetRedComponent (color), Color.GetGreenComponent (color), Color.GetBlueComponent (color));
		}

		private int SetColorAlpha(int color, byte alpha)
		{
			return Color.Argb(alpha, Color.GetRedComponent(color), Color.GetGreenComponent(color), Color.GetBlueComponent(color));
		}

		public void OnViewPagerPageChanged(int position, float positionOffset)
		{
			uSelectedPosition = position;
			uSelectionOffset = positionOffset;
			this.Invalidate ();
		}

		protected override void OnDraw(Canvas canvas)
		{
			int height = Height;
			int tabCount = ChildCount;
			int dividerHeightPx = (int)(Math.Min (Math.Max (0f, uDividerHeight), 1f) * height);
			SlidingTabScrollView.TabColorizer tabColorizer = uCustomTabColorizer != null ? uCustomTabColorizer : uDefaultTabColorizer;
			//Thick colored underline

			if (tabCount > 0) {
				View selectedTitle = GetChildAt (uSelectedPosition);
				int left = selectedTitle.Left;
				int right = selectedTitle.Right;
				int color = tabColorizer.GetIndicatorColor (uSelectedPosition);

				if (uSelectionOffset > 0f && uSelectedPosition < (tabCount - 1)) {
					int nextColor = tabColorizer.GetIndicatorColor(uSelectedPosition + 1));
					if(color != nextColor)
					{
						color = blendColor(nextColor, color, uSelectionOffset);

					}

					View nextTitle = GetChildAt(uSelectedPosition + 1);
					left = (int)(uSelectionOffset * nextTitle.Left + (1.0f - uSelectionOffset) * left);
					right = (int)(uSelectionOffset * nextTitle.Right + (1.0f - uSelectionOffset) * right);
				}

				uSelectedIndicatorPaint.Color = GetColorFromInteger(color);

				canvas.DrawRect(left, height - uSelectedIndicatorThickness, right, height, uSelectedIndicatorPaint);

				//Creating Vertical Divider on tabs
				int separatorTop = (
			}
		}
		private class SimpleTabColorizer : SlidingTabScrollView.TabColorizer
		{
			private int[] uIndicatorColors;
			private int[] uDividerColors;

			public int GetIndicatorColor(int position)
			{
				return uIndicatorColors [position % uIndicatorColors.Length];
			}

			public int GetDividerColors(int position)
			{
				return uDividerColors [position % uDividerColors.Length];
			} 

			public int[] IndicatorColors
			{
				set { uIndicatorColors = value; }
			}

			public int[] DividerColors
			{
				set { uDividerColors = value; }
			}
		}
	}
					}

