using System;

namespace Ecran.GUI.Main
{
    public class ViewModel
    {
        private Model model;

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

        public string Path
        {
            get
            {
                return model.Path;
            }
            set
            {
                model.Path = value;
            }
        }

        public ViewModel(Model mainModel)
        {
            model = mainModel;
            model.SetNativeResolution();
        }
    }
}
