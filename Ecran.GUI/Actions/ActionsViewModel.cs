using Echoic.Binary;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ecran.GUI
{
    public class ActionsViewModel : INotifyPropertyChanged
    {
        readonly Actions actions;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Path {
            get {
                return actions.Binary.Path;
            }
            set {
                if (value != actions.Binary.Path)
                {
                    actions.Binary = new Binary(value);
                    NotifyPropertyChanged();
                }
            }
        }

        public ActionsViewModel(Actions actions)
        {
            this.actions = actions;
        }

        public void SaveResolution(Resolution resolution)
        {
            new ResolutionPatcher(actions.Binary)
                .ApplyResolution(resolution)
                .ApplyNewHashing();
        }

        public void DetectBlamsav()
        {
            Path = new BlamDetect().Find();
        }

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
