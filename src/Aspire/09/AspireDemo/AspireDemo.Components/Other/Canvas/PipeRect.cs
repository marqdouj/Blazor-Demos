namespace AspireDemo.Components.Other.Canvas
{
    internal record PipeRect(double X1, double Y1, double X2, double Y2, bool Visible = true, double R1 = 0, double R2 = 0)
    {
        public bool IsEmpty => X1 == 0 && Y1 == 0 && X2 == 0 && Y2 == 0;
        public bool IsNotEmpty => !IsEmpty;
        public double Width => X2 - X1;
        public double Height => Y2 - Y1;
        public double CenterX => X1 + Width / 2.0;
        public double CenterY => Y1 + Height / 2.0;
    }
}
