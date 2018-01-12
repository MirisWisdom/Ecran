using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ecran.GUI
{
    public class DisplayViewModel : INotifyPropertyChanged
    {
        Display display;

        public event PropertyChangedEventHandler PropertyChanged;

        public Resolution Resolution {
            get {
                return display.Resolution;
            }

            set {
                if (value != display.Resolution)
                {
                    Width = value.Width;
                    Height = value.Height;
                    display.Resolution = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Width {
            get {
                return display.Resolution.Width;
            }

            set {
                if (value != display.Resolution.Width)
                {
                    display.Resolution = new Resolution(value, Height);
                    NotifyPropertyChanged();
                }
            }
        }

        public int Height {
            get {
                return display.Resolution.Height;
            }

            set {
                if (value != display.Resolution.Height)
                {
                    display.Resolution = new Resolution(Width, value);
                    NotifyPropertyChanged();
                }
            }
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
            new Resolution(1280, 1024),
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

        public DisplayViewModel(Display display)
        {
            this.display = display;
        }

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
