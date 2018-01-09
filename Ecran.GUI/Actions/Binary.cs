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
            new ResolutionPatcher(this)
                .ApplyResolution(resolution)
                .ApplyNewHashing();
        }
    }
}
