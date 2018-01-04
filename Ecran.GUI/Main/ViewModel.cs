namespace Ecran.GUI.Main
{
    public class ViewModel
    {
        private Resolution model;

        public int Width
        {
            get
            {
                return model.Width;
            }
            set
            {
                model.Width = value;
            }
        }

        public int Height
        {
            get
            {
                return model.Height;
            }
            set
            {
                model.Height = value;
            }
        }

        public ViewModel(Resolution mainModel)
        {
            model = mainModel;
            model.SetNativeResolution();
        }
    }
}
