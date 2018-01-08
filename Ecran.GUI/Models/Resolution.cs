using Echoic.Binary;

namespace Ecran.GUI
{
    public class Resolution
    {
        readonly int width;
        readonly int height;

        public int Width => width;

        public int Height => height;

        public string Description => $"{Width}x{Height}";

        public Resolution(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void ApplyToBinary(Binary binary)
        {
            new ResolutionPatcher(new Blam(binary.Path))
                .ApplyResolution(this)
                .ApplyNewHashing();
        }
    }
}
