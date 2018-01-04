namespace Ecran.GUI.Main
{
    public class Resolution
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public void SetNativeResolution()
        {
            Width = (int) System.Windows.SystemParameters.PrimaryScreenWidth;
            Height = (int) System.Windows.SystemParameters.PrimaryScreenHeight;
        }
    }
}
