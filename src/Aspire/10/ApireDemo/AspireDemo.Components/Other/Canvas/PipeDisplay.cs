using AspireDemo.Components.Other.Canvas.Graphics;
using AspireDemo.Components.Other.Canvas.Range;

namespace AspireDemo.Components.Other.Canvas
{
    internal class PipeDisplay()
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public PipeState State { get; set; } = new();

        public Line ClockLine { get; set; } = new(ColorName.Red) { LineDash = [5, 3] };
        public string ClockText => $"{State.Clock.ClockText} ({State.Channels.Value})";
        public List<PipeEvent> Events { get; set; } = [];
        public Label Labels { get; set; } = new(ColorName.LightCyan, ColorName.Red);
        public Line RangeLine { get; set; } = new(ColorName.Blue) { LineDash = [5, 3] };
        public Line SelectedBorder { get; set; } = new(ColorName.Red) { LineDash = [1, 2], LineWidth = 5 };

        private int Precision { get; set; } = 2;

        private string FormatS(double value) => value.ToString($"F{Precision}");

        #region Range Strings

        public PipeRange Range => State.Range;

        public string X1 { get { return FormatS(Range.Min); } }

        public string X2 { get { return FormatS(Range.Max); } }

        public string XCenter { get { return FormatS(Range.Value); } }

        public string XCenterUnit { get { return string.Format("{0}{1}", XCenter, XUnit); } }

        public string XUnit { get { return State.Range.Unit.ToSuffix(); } }

        #endregion
    }
}
