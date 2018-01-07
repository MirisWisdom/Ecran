namespace Ecran.GUI
{
    public class Resolution
    {
        readonly int width;
        readonly int height;

        public int Width {
            get {
                return width;
            }
        }

        public int Height {
            get {
                return height;
            }
        }

        public string Description {
            get {
                return $"{Width}x{Height}";
            }
        }

        public Resolution(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
}
