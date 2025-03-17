using AspireDemo.Components.Other.Canvas.Clock;
using AspireDemo.Components.Other.Canvas.Range;
using Marqdouj.CLRCommon;

namespace AspireDemo.Components.Other.Canvas
{
    public class PipeState
    {
        public static readonly double CLOCK_DEFAULT = 180.0; //6 O'Clock (bottom of pipe)

        public ClockTime Clock { get; set; } = new(CLOCK_DEFAULT);
        public MinMaxN<int> Channels { get; set; } = new(1, 12, 12);
        public PipeRange Range { get; set; } 
            = new (0, PipeSettings.RANGE_WIDTH_DEFAULT, PipeSettings.RANGE_UNIT_DEFAULT);

        internal void UpdateFromSettings(PipeSettings settings)
        {
            Range = new(0, settings.RangeWidth, settings.RangeUnit);
        }
    }
}
