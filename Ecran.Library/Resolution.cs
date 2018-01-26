namespace Ecran.Library
{
    public class Resolution
    {
        public int Width { get; }

        public int Height { get; }

        public string Description => $"{Width}x{Height}";

        public Resolution(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
