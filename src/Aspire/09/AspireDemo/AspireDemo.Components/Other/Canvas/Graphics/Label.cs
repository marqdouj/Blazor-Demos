namespace AspireDemo.Components.Other.Canvas.Graphics
{
    internal record Label(string Background, string Foreground)
    {
        public Label(ColorName Background, ColorName Foreground)
            : this(Background.ToString(), Foreground.ToString()) { }
    }
}
