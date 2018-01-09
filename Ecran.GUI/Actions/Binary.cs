using Echoic.Binary;

namespace Ecran.GUI
{
    public class Binary
    {
        readonly string path;

        public string Path => path;

        public Binary(string path)
        {
            this.path = path;
        }

        public void Patch(Resolution resolution)
        {
            new ResolutionPatcher(new Blam(this.Path))
                .ApplyResolution(resolution)
                .ApplyNewHashing();
        }
    }
}
