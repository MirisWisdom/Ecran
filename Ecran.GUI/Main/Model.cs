namespace Ecran.GUI.Main
{
    public class Model
    {
        public Resolution Resolution { get; set; } = new Resolution();

        public string Path { get; set; }

        public void SetNativeResolution()
        {
            Resolution.Width = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            Resolution.Height = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
        }
    }
}
