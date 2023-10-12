using System;
using FancyScrollView;

namespace AdventureTraveler.Scroll
{
    public class ScrollUnitContext : FancyScrollRectContext
    {
        public int SelectedIndex = -1;
        public Action<int> OnCellClicked;
    }
}