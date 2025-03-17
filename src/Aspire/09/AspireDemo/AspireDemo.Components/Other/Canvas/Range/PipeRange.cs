using Marqdouj.CLRCommon;
using System.Drawing;

namespace AspireDemo.Components.Other.Canvas.Range
{
    public class PipeRange(double min, double max, double value, RangeUnit unit) 
        : MinMaxN<double>(min, max, value)
    {
        public PipeRange(double dfs, double width, RangeUnit unit)
            :this(dfs - (width / 2.0), dfs + (width / 2.0), dfs, unit)
        {
            
        }

        public RangeUnit Unit { get; set; } = unit;

        public bool IntersectsWith(PipeRange target)
        {
            var rc1 = new RectangleF((float)Min, 0, (float)Width, 2);
            var rc2 = new RectangleF((float)target.Min, 1, (float)target.Width, 2);

            return rc1.IntersectsWith(rc2);
        }

        public bool IntersectsWith(double x1, double width)
        {
            return IntersectsWith(new RectangleF((float)x1, 0, (float)width, 0));
        }

        public bool IntersectsWith(RectangleF target)
        {
            var rc1 = new RectangleF((float)Min, 0, (float)Width, 2);
            var rc2 = new RectangleF((float)target.X, 1, (float)target.Width, 2);

            return rc1.IntersectsWith(rc2);
        }

        public PipeRange Scroll(RangeDirection direction, int rangeScroll)
        {
            var dfs = direction == RangeDirection.Upstream ?
                      Value - rangeScroll :
                      Value + rangeScroll;

            return new PipeRange(dfs, Width, Unit);
        }
    }
}
