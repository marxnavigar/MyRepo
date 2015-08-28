using System;

namespace SlidingTabLayOut
{
	public class SlidingTabScrollView
	{
		public interface TabColorizer
		{
			int GetIndicatorColor(int position);
			int GetDividerColor(int position);
		}
	}
}

