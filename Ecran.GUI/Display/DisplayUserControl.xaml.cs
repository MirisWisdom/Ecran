using Ecran.GUI.Mediators;
using System.Windows.Controls;

namespace Ecran.GUI.Display
{
    /// <summary>
    /// Interaction logic for DisplayUserControl.xaml
    /// </summary>
    public partial class DisplayUserControl : UserControl
    {
        private ViewModelMediator _viewModelMediator;

        public ViewModelMediator ViewModelMediator {
            get => _viewModelMediator;
            set {
                _viewModelMediator = value;
                DataContext = value.DisplayViewModel;
            }
        }

        public DisplayUserControl()
        {
            InitializeComponent();
        }
    }
}
