using Echoic.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ecran.GUI
{
    public class MainViewModel : INotifyPropertyChanged
    {
        ModelsMediator modelsMediator;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Version => $"{Properties.Resources.Version} // {Properties.Resources.Author.ToUpper()}";

        public string Path {
            get => modelsMediator.Binary.Path;
            set {
                if (value != modelsMediator.Binary.Path)
                {
                    modelsMediator.Binary = new Binary(value);
                    NotifyPropertyChanged();
                }
            }
        }

        public Resolution Resolution {
            get => modelsMediator.Resolution;
            set {
                if (value != modelsMediator.Resolution)
                {
                    Width = value.Width;
                    Height = value.Height;
                    modelsMediator.Resolution = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Width {
            get => modelsMediator.Resolution.Width;
            set {
                if (value != modelsMediator.Resolution.Width)
                {
                    modelsMediator.Resolution = new Resolution(value, Height);
                    NotifyPropertyChanged();
                }
            }
        }

        public int Height {
            get => modelsMediator.Resolution.Height;
            set {
                if (value != modelsMediator.Resolution.Height)
                {
                    modelsMediator.Resolution = new Resolution(Width, value);
                    NotifyPropertyChanged();
                }
            }
        }

        public void SaveSettings()
        {
            modelsMediator.Binary.Patch(modelsMediator.Resolution);
        }

        public void DetectBlamsav()
        {
            Path = new BlamDetect().Find();
        }

        public List<Resolution> Resolutions => new List<Resolution>
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

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel(ModelsMediator modelsMediator)
        {
            this.modelsMediator = modelsMediator;
        }
    }
}
