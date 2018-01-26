namespace Ecran.GUI.Display
{
    public class Resolution
    {
        private readonly int _width;
        private readonly int _height;

        public int Width => _width;

        public int Height => _height;

        public string Description => $"{Width}x{Height}";

        public Resolution(int width, int height)
        {
            _width = width;
            _height = height;
        }
    }
}
