using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ecran.GUI
{
    public class DisplayViewModel : INotifyPropertyChanged
    {
        Display display;

        readonly List<Resolution> resolutions;

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

        public List<Resolution> Resolutions {
            get {
                return resolutions;
            }
        }

        public DisplayViewModel(Display display)
        {
            this.display = display;
        }

        public DisplayViewModel(Display display, List<Resolution> resolutions) : this(display)
        {
            this.resolutions = resolutions;
        }

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
