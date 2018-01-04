namespace Ecran.GUI
{
    public class MainViewModel
    {
        private Main main;

        public int Width
        {
            get
            {
                return main.Width;
            }
            set
            {
                main.Width = value;
            }
        }

        public int Height
        {
            get
            {
                return main.Height;
            }
            set
            {
                main.Height = value;
            }
        }

        public MainViewModel(Main mainModel)
        {
            main = mainModel;
        }
    }
}
