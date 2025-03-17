using AspireDemo.Components.Other.Canvas.Range;

namespace AspireDemo.Components.Other.Canvas
{
    internal class PipeSettings
    {
        public const int HEIGHT_DEFAULT = 280;
        public const int WIDTH_DEFAULT = 850;
        public const int RANGE_WIDTH_DEFAULT = 20;
        public const RangeUnit RANGE_UNIT_DEFAULT = RangeUnit.Meter;

        public int Height { get; set; } = HEIGHT_DEFAULT;
        public int Width { get; set; } = WIDTH_DEFAULT;
        public int RangeScroll { get; set; } = 5;
        public int RangeWidth { get; set; } = RANGE_WIDTH_DEFAULT;
        public RangeUnit RangeUnit { get; set; } = RangeUnit.Meter;
    }
}
