using AspireDemo.Components.Other.Canvas.Clock;
using AspireDemo.Components.Other.Canvas.Range;
using Marqdouj.CLRCommon;
using System.Drawing;

namespace AspireDemo.Components.Other.Canvas
{
    internal class PipeEvent()
    {
        public int Id { get; set; }

        public PipeEventGroup Group { get; set; }

        public double DFS { get; set; }

        #region Clock
        private ClockTime clockTime;
        private double clock;
        public double Clock { get => clock; set { clock = value; clockTime = new ClockTime(value); } }
        #endregion

        public string ClockText => clockTime.ClockText;

        public double Length { get; set; }

        public double Width { get; set; }

        public PipeRect Bottom { get; set; } = new PipeRect(0, 0, 0, 0, false);

        public PipeRect Top { get; set; } = new PipeRect(0, 0, 0, 0, false);

        public bool IsSelected { get; set; }

        public string Name => Group.ToString();

        public int ZIndex => IsSelected ? 100 : Group.ToZIndex();
    }

    internal static class PipeEventExtensions
    {
        public static async Task<List<PipeEvent>> GetPipeEvents(this MinMaxN<double> range)
        {
            //Simulate getting events from API service
            await Task.CompletedTask;

            var items = new List<PipeEvent>();

            items.SeedValves(range);
            items.SeedWelds(range);
            items.SeedDefects(range);

            var id = 0;

            foreach (var item in items)
            {
                id++;
                item.Id = id;
            }

            return [.. items.OrderBy(e => e.DFS)];
        }

        private static void SeedValves(this List<PipeEvent> items, MinMaxN<double> range)
        {
            //Add a Valve at start of pipeline
            items.Add(new PipeEvent { Group = PipeEventGroup.Valve, DFS = range.Min, Clock = 0, Length = 0, Width = 0 });

            //Add a Valve at end of pipeline
            items.Add(new PipeEvent { Group = PipeEventGroup.Valve, DFS = range.Max, Clock = 0, Length = 0, Width = 0 });
        }

        private static void SeedWelds(this List<PipeEvent> items, MinMaxN<double> range)
        {
            var weldOffset = 8; //meters
            var dfs = range.Min + weldOffset;

            while (dfs < range.Max)
            {
                items.Add(new PipeEvent { Group = PipeEventGroup.Weld, DFS = dfs, Clock = 0, Length = 0, Width = 0 });
                dfs += weldOffset;
            }
        }

        private static void SeedDefects(this List<PipeEvent> items, MinMaxN<double> range)
        {
            const double DEGREES_PER_CLOCK = 30.0;
            var defectOffset = 0.25;
            var weldOffset = 8; //meters
            var dfs = range.Min + defectOffset;
            var defectMax = range.Max - weldOffset;

            while (dfs < defectMax)
            {
                //Undefined
                items.Add(new PipeEvent { Group = PipeEventGroup.Undefined, DFS = dfs + 2.75, Clock = 7.45 * @DEGREES_PER_CLOCK, Length = 6.24, Width = 0.65 });
                items.Add(new PipeEvent { Group = PipeEventGroup.Undefined, DFS = dfs + 4.5, Clock = 4.37 * @DEGREES_PER_CLOCK, Length = 3.54, Width = 2.33 });

                //Metal Loss
                items.Add(new PipeEvent { Group = PipeEventGroup.MetalLoss, DFS = dfs, Clock = 5.30 * @DEGREES_PER_CLOCK, Length = 37.0, Width = 0.12 });
                items.Add(new PipeEvent { Group = PipeEventGroup.MetalLoss, DFS = dfs + 1.35, Clock = 7.43 * @DEGREES_PER_CLOCK, Length = 12.25, Width = 0.33 });

                //Geometry
                items.Add(new PipeEvent { Group = PipeEventGroup.Geometry, DFS = dfs + 4.5, Clock = 5.45 * @DEGREES_PER_CLOCK, Length = 13.24, Width = 0.45 });
                items.Add(new PipeEvent { Group = PipeEventGroup.Geometry, DFS = dfs + 1.5, Clock = 6.17 * @DEGREES_PER_CLOCK, Length = 6.44, Width = 1.33 });

                //General
                items.Add(new PipeEvent { Group = PipeEventGroup.General, DFS = dfs + 2.5, Clock = 8.45 * @DEGREES_PER_CLOCK, Length = 8.24, Width = 0.75 });
                items.Add(new PipeEvent { Group = PipeEventGroup.General, DFS = dfs + 3.75, Clock = 2.17 * @DEGREES_PER_CLOCK, Length = 3.44, Width = 1.38 });

                dfs += weldOffset;
            }
        }

        public static void UpdateForCanvas(
            this List<PipeEvent> items,
            Size canvasSize,
            ClockTime canvasClock,
            PipeRange canvasRange,
            double pixelsPerClock,
            double pixelsPerUnit)
        {
            var minPixelSize = 15.0;

            foreach (var pipeEvent in items)
            {
                var eventRange = new PipeRange(pipeEvent.DFS, pipeEvent.DFS + pipeEvent.Width, pipeEvent.DFS, canvasRange.Unit);
                
                //If this event does not fall within the range then hide 
                //rectangles and return;
                if (!eventRange.IntersectsWith(canvasRange))
                {
                    pipeEvent.Bottom = new PipeRect(0, 0, 0, 0, false);
                    pipeEvent.Top = new PipeRect(0, 0, 0, 0, false);
                    return;
                }

                var fullPipe = pipeEvent.Group.IsFullPipe();
                double defectHeight;
                double defectWidth;
                bool defTopVis = true;
                bool defBotVis = false;

                if (fullPipe)
                {
                    defectHeight = canvasSize.Height;
                    defectWidth = eventRange.Width * pixelsPerUnit;

                    if (defectWidth < minPixelSize)
                        defectWidth = minPixelSize;
                }
                else
                {
                    //Calculate the height of this defect
                    defectHeight = pipeEvent.Length / PipeClocks.DEGREES_PER_CLOCK * pixelsPerClock;
                    if (defectHeight < minPixelSize)
                        defectHeight = minPixelSize;

                    //Calculate the width of this defect
                    defectWidth = eventRange.Width * pixelsPerUnit;
                    if (defectWidth < minPixelSize)
                        defectWidth = minPixelSize;
                }

                //Calculate X1 position of this defect
                var cx1 = canvasRange.Min;
                var x1 = eventRange.Min;
                var xAbs = Math.Abs(cx1 - x1);
                if (x1 < cx1)
                {
                    x1 = -(xAbs * pixelsPerUnit);
                }
                else
                {
                    x1 = xAbs * pixelsPerUnit;
                }

                if (fullPipe)
                {
                    x1 -= defectWidth / 2.0;
                }

                var peClock = new ClockTime(pipeEvent.Clock);
                var yTop = 0.0;
                var yBot = 0.0;

                if (!fullPipe)
                {
                    var hrCW = canvasClock.DistanceFrom(peClock, ClockDirection.Clockwise);
                    var hrACW = canvasClock.DistanceFrom(peClock, ClockDirection.AntiClockwise);

                    var cenCanvas = canvasSize.Height / 2.0;

                    yTop = cenCanvas + hrCW * pixelsPerClock;
                    yBot = cenCanvas - hrACW * pixelsPerClock;

                    defBotVis = yBot != yTop;
                }
                else
                {
                    //Place all defects always at the top of the canvas if no valid clock
                    //yTop = 0;
                    //yBot = 0;

                    //no need to show bottom defect as the clocks would be the same
                    //defBotVis = false;
                }

                var rcCanvas = new RectangleF(0, 0, canvasSize.Width, canvasSize.Height);

                if (defTopVis)
                {
                    var rcTop = new RectangleF((float)x1, (float)yTop, (float)(x1 + defectWidth), (float)(yTop + defectHeight));
                    defTopVis = rcCanvas.IntersectsWith(rcTop);
                }

                if (defBotVis)
                {
                    var rcBot = new RectangleF((float)x1, (float)yBot, (float)(x1 + defectWidth), (float)(yBot + defectHeight));
                    defBotVis = rcCanvas.IntersectsWith(rcBot);
                }

                pipeEvent.Top = new PipeRect(x1, yTop, x1 + defectWidth, yTop + defectHeight, defTopVis);
                pipeEvent.Bottom = new PipeRect(x1, yBot, x1 + defectWidth, yBot + defectHeight, defBotVis);
            }
        }
    }
}
