using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ecran.GUI
{
    public class MainViewModel : INotifyPropertyChanged
    {
        Main model;

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel(Main mainModel)
        {
            model = mainModel;
            model.Resolution = Resolutions[0];
        }

        public string Version {
            get {
                return $"{Properties.Resources.Version} // {Properties.Resources.Author.ToUpper()}";
            }
        }

        public Resolution Resolution {
            get {
                return model.Resolution;
            }
            set {
                if (value != model.Resolution)
                {
                    Width = value.Width;
                    Height = value.Height;
                    model.Resolution = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Width {
            get {
                return model.Resolution.Width;
            }
            set {
                if (value != model.Resolution.Width)
                {
                    model.Resolution = new Resolution(value, Height);
                    NotifyPropertyChanged();
                }
            }
        }

        public int Height {
            get {
                return model.Resolution.Height;
            }
            set {
                if (value != model.Resolution.Height)
                {
                    model.Resolution = new Resolution(Width, value);
                    NotifyPropertyChanged();
                }
            }
        }

        public List<Resolution> Resolutions {
            get {
                return new List<Resolution>
                {
                    new Resolution(800, 600),
                    new Resolution(1024, 600),
                    new Resolution(1024, 768),
                    new Resolution(1152, 864),
                    new Resolution(1280, 720),
                    new Resolution(1280, 768),
                    new Resolution(1280, 800),
                    new Resolution(1280, 102),
                    new Resolution(1360, 768),
                    new Resolution(1366, 768),
                    new Resolution(1440, 900),
                    new Resolution(1536, 864),
                    new Resolution(1600, 900),
                    new Resolution(1680, 1050),
                    new Resolution(1920, 1080),
                    new Resolution(1920, 1200),
                    new Resolution(2560, 1080),
                    new Resolution(2560, 1440),
                    new Resolution(3440, 1440),
                    new Resolution(3840, 2160),
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
