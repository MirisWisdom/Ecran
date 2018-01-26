using Echoic.Binary;
using Ecran.GUI.Display;
using Ecran.GUI.Modules;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ecran.GUI.Actions
{
    public class ActionsViewModel : INotifyPropertyChanged
    {
        private readonly Actions _actions;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Path {
            get => _actions.Binary.Path;
            set {
                if (value != _actions.Binary.Path)
                {
                    _actions.Binary = new Binary(value);
                    NotifyPropertyChanged();
                }
            }
        }

        public ActionsViewModel(Actions actions)
        {
            _actions = actions;
        }

        public void SaveResolution(Resolution resolution)
        {
            new ResolutionPatcher(_actions.Binary)
                .ApplyResolution(resolution)
                .ApplyNewHashing();
        }

        public void DetectBlamsav()
        {
            Path = new BlamDetect().Find();
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
