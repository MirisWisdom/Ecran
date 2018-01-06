using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ecran.GUI.Main
{
    public class ViewModel : INotifyPropertyChanged
    {
        Model model;

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewModel(Model mainModel)
        {
            model = mainModel;
        }

        public string Version {
            get {
                return $"{Resource.Version} // {Resource.Author.ToUpper()}";
            }
        }

        public Resolution SelectedResolution {
            get {
                return model.Resolution;
            }
            set {
                if (value != model.Resolution)
                {
                    model.Resolution = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public List<Resolution> Resolutions {
            get {
                return new List<Resolution>
                {
                    new Resolution { Width = 800, Height =  600 },
                    new Resolution { Width = 1024, Height =  600 },
                    new Resolution { Width = 1024, Height =  768 },
                    new Resolution { Width = 1152, Height =  864 },
                    new Resolution { Width = 1280, Height =  720 },
                    new Resolution { Width = 1280, Height =  768 },
                    new Resolution { Width = 1280, Height =  800 },
                    new Resolution { Width = 1280, Height =  1024 },
                    new Resolution { Width = 1360, Height =  768 },
                    new Resolution { Width = 1366, Height =  768 },
                    new Resolution { Width = 1440, Height =  900 },
                    new Resolution { Width = 1536, Height =  864 },
                    new Resolution { Width = 1600, Height =  900 },
                    new Resolution { Width = 1680, Height =  1050 },
                    new Resolution { Width = 1920, Height =  1080 },
                    new Resolution { Width = 1920, Height =  1200 },
                    new Resolution { Width = 2560, Height =  1080 },
                    new Resolution { Width = 2560, Height =  1440 },
                    new Resolution { Width = 3440, Height =  1440 },
                    new Resolution { Width = 3840, Height =  2160 },
                };
            }
        }

        public string Path {
            get {
                return model.Path;
            }
            set {
                if (value != model.Path)
                {
                    model.Path = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
