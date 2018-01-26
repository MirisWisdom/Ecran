using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ecran.GUI.Display
{
    public class DisplayViewModel : INotifyPropertyChanged
    {
        private Display _display;

        private readonly List<Resolution> _resolutions;

        public event PropertyChangedEventHandler PropertyChanged;

        public Resolution Resolution {
            get => _display.Resolution;

            set {
                if (value != _display.Resolution)
                {
                    Width = value.Width;
                    Height = value.Height;
                    _display.Resolution = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Width {
            get => _display.Resolution.Width;

            set {
                if (value != _display.Resolution.Width)
                {
                    _display.Resolution = new Resolution(value, Height);
                    NotifyPropertyChanged();
                }
            }
        }

        public int Height {
            get => _display.Resolution.Height;

            set {
                if (value != _display.Resolution.Height)
                {
                    _display.Resolution = new Resolution(Width, value);
                    NotifyPropertyChanged();
                }
            }
        }

        public List<Resolution> Resolutions => _resolutions;

        public DisplayViewModel(Display display)
        {
            _display = display;
        }

        public DisplayViewModel(Display display, List<Resolution> resolutions) : this(display)
        {
            _resolutions = resolutions;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
