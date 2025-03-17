namespace AspireDemo.Components.Other.Canvas.Clock
{
    internal static class PipeClocks
    {
        #region Public Constants

        public const double DEGREES_PER_CLOCK = 30.0;

        #endregion

        #region DegreesToHours

        public static double DegreesToHours(double degrees)
        {
            return DegreesToHours(degrees, out _);
        }

        public static double DegreesToHours(double degrees, out int revolutions)
        {
            degrees = NormalizeDegrees(degrees, out revolutions);

            var clock = degrees / DEGREES_PER_CLOCK;

            return clock;
        }

        #endregion

        public static double DistanceFrom(double sourceHours, double targetHours, ClockDirection direction)
        {
            if (sourceHours >= 12.0) { sourceHours -= 12.0; }
            if (targetHours >= 12.0) { targetHours -= 12.0; }

            if (sourceHours == targetHours) { return 0; }

            var hrDiff = Math.Abs(sourceHours - targetHours);

            switch (direction)
            {
                case ClockDirection.Clockwise:
                    if (sourceHours > targetHours)
                    {
                        return 12.0 - hrDiff;
                    }
                    else if (sourceHours < targetHours)
                    {
                        return hrDiff;
                    }
                    break;
                case ClockDirection.AntiClockwise:
                    if (sourceHours > targetHours)
                    {
                        return hrDiff;
                    }
                    else if (sourceHours < targetHours)
                    {
                        return 12.0 - hrDiff;
                    }
                    break;
                default:
                    break;
            }

            throw new Exception(string.Format("DistanceFrom failed: {0},{1}", sourceHours, targetHours));
        }

        #region NormalizeDegrees

        public static double NormalizeDegrees(double degrees)
        {
            return NormalizeDegrees(degrees, out _);
        }

        public static double NormalizeDegrees(double degrees, out int revolutions)
        {
            revolutions = 0;

            while (degrees > 360.0)
            {
                degrees -= 360.0;
                revolutions++;
            }

            while (degrees < -360.0)
            {
                degrees += 360.0;
                revolutions++;
            }

            if (degrees < 0)
            {
                degrees = 360.0 + degrees;
            }

            return degrees;
        }

        #endregion

        #region NormalizeHours

        public static double NormalizeHours(double hours)
        {
            return NormalizeHours(hours, out _);
        }

        public static double NormalizeHours(double hours, out int revolutions)
        {
            revolutions = 0;

            while (hours > 12.0)
            {
                hours -= 12.0;
                revolutions++;
            }

            while (hours < -12.0)
            {
                hours += 12.0;
                revolutions++;
            }

            if (hours < 0)
            {
                hours = 12.0 + hours;
            }

            return hours;
        }

        #endregion

        public static string ToClockText(int hour, int minutes, bool degrees)
        {
            var hr = hour;
            if (hr == 0 && minutes == 0)
            {
                hr = 12;
            }
            var s = hr.ToString("00") + ":" + minutes.ToString("00");

            if (degrees)
            {
                var deg = ToDegrees(hour, minutes);

                return string.Format("{0}{1}({2}°)", s, Environment.NewLine, deg);
            }

            return s;
        }

        public static DateTime ToDateTime(int hour, int minutes)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minutes, 0);
        }

        public static double ToDecimalHours(int hours, int minutes)
        {
            var m = TimeSpan.FromMinutes(minutes);
            var d = hours + m.TotalHours;

            return d;
        }

        #region ToDegrees

        public static double ToDegrees(int hours, int minutes)
        {
            return ToDegrees(hours, minutes, out _);
        }

        public static double ToDegrees(int hours, int minutes, out int revolutions)
        {
            var hrs = ToDecimalHours(hours, minutes);
            return DoToDegrees(hrs, out revolutions);
        }

        private static double DoToDegrees(double decimalHours, out int revolutions)
        {
            var hrs = NormalizeHours(decimalHours, out revolutions);

            double deg = hrs * DEGREES_PER_CLOCK;

            return deg;
        }

        #endregion
    }
}
