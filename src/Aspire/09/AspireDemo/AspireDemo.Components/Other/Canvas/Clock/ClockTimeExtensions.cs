namespace AspireDemo.Components.Other.Canvas.Clock
{
    internal static class ClockTimeExtensions
    {
        public enum CoerceHoursTwelveAndZeroOptions
        {
            LeaveAsOriginal,
            TwelveIsZero,
            ZeroIsTwelve,
        }

        public static int CoerceHour(int hour, bool throwEx, string paramName, CoerceHoursTwelveAndZeroOptions convertTZ)
        {
            int hr = hour;

            if (hr > 12 && hr <= 24)
            {
                hr -= 12;
            }

            if (hr > 12 || hr < 0)
            {
                if (throwEx)
                {
                    throw new ArgumentOutOfRangeException(paramName);
                }
            }

            if (hr == 0)
            {
                if (convertTZ == CoerceHoursTwelveAndZeroOptions.ZeroIsTwelve)
                {
                    hr = 12;
                }
            }
            else if (hr == 12)
            {
                if (convertTZ == CoerceHoursTwelveAndZeroOptions.TwelveIsZero)
                {
                    hr = 0;
                }
            }

            return hr;
        }

        public static bool SetHour(int oldValue, int newValue, out int coercedValue)
        {
            return SetHour(oldValue, newValue, CoerceHoursTwelveAndZeroOptions.LeaveAsOriginal, out coercedValue);
        }

        public static bool SetHour(int oldValue, int newValue, CoerceHoursTwelveAndZeroOptions convertTZ, out int coercedValue)
        {
            coercedValue = newValue;

            if (newValue != oldValue)
            {
                coercedValue = CoerceHour(newValue, true, "Hour", convertTZ);

                return true;
            }

            return false;
        }

        public static bool SetMinutes(int oldValue, int newValue)
        {
            if (newValue != oldValue)
            {
                if (newValue > 59 || newValue < 0)
                {
                    throw new ArgumentOutOfRangeException("Minutes");
                }

                return true;
            }

            return false;
        }
    }
}
