namespace Ecran.GUI.Main
{
    public class Resolution
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public string Description {
            get {
                return $"{Width}x{Height}";
            }
        }
    }
}
