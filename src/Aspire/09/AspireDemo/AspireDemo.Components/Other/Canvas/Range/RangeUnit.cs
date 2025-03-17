namespace AspireDemo.Components.Other.Canvas.Range
{
    public enum RangeUnit
    {
        Undefined,
        Millimeter,
        Centimeter,
        Meter,
        Kilometer,
        Inch,
        Foot,
        Yard,
        Mile,
    }

    internal static class LinearUnitExtensions
    {
        public static string ToSuffix(this RangeUnit unit)
        {
            return unit switch
            {
                RangeUnit.Undefined => "?",
                RangeUnit.Millimeter => "mm",
                RangeUnit.Centimeter => "cm",
                RangeUnit.Meter => "m",
                RangeUnit.Kilometer => "km",
                RangeUnit.Inch => "in",
                RangeUnit.Foot => "ft",
                RangeUnit.Yard => "yd",
                RangeUnit.Mile => "ml",
                _ => string.Empty,
            };
        }
    }
}
