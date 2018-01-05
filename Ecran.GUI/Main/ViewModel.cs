using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ecran.GUI.Main
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Model model;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Width {
            get {
                return model.Width;
            } set {
                if (value != model.Width)
                {
                    model.Width = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Height {
            get {
                return model.Height;
            } set {
                if (value != model.Height)
                {
                    model.Height = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Path {
            get {
                return model.Path;
            } set {
                if (value != model.Path)
                {
                    model.Path = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ViewModel(Model mainModel)
        {
            model = mainModel;
            model.SetNativeResolution();
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
