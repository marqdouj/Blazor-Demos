namespace AspireDemo.Components.Other.Canvas.Clock
{
    public struct ClockTime
    {
        #region Constructor

        public ClockTime(double degrees) : this()
        {
            SetValue(degrees);
        }

        public ClockTime(int hours, int minutes) : this()
        {
            SetValue(hours, minutes);
        }

        #endregion

        #region Private Fields

        private int _hours;
        private int _minutes;

        #endregion

        #region Private Methods

        private bool DoSetHours(int hours)
        {
            if (ClockTimeExtensions.SetHour(_hours, hours, out int hr))
            {
                _hours = hr;
                return true;
            }

            return false;
        }

        private bool DoSetMinutes(int minutes)
        {
            if (ClockTimeExtensions.SetMinutes(_minutes, minutes))
            {
                _minutes = minutes;
                return true;
            }

            return false;
        }

        #endregion

        #region Public Properties

        public int Hours
        {
            get { return _hours; }
            set { DoSetHours(value); }
        }

        public int Minutes
        {
            get { return _minutes; }
            set { DoSetMinutes(value); }
        }

        #endregion

        #region Public Methods

        public double ToDegrees()
        {
            return PipeClocks.ToDegrees(Hours, Minutes);
        }

        public double ToDecimalHours()
        {
            return PipeClocks.ToDecimalHours(Hours, Minutes);
        }

        private bool SetValue(double degrees)
        {
            var ts = TimeSpan.FromHours(PipeClocks.DegreesToHours(degrees));
            return SetValue(ts.Hours, ts.Minutes);
        }

        private bool SetValue(int hours, int minutes)
        {
            bool result = DoSetHours(hours);

            if (DoSetMinutes(minutes))
            {
                result = true;
            }

            return result;
        }

        public double DistanceFrom(ClockTime target, ClockDirection direction)
        {
            return PipeClocks.DistanceFrom(ToDecimalHours(), target.ToDecimalHours(), direction);
        }

        public string ClockText => PipeClocks.ToClockText(Hours, Minutes, false);
        public override string ToString() => ClockText;

        #endregion
    }
}
