using System.Windows.Controls;

namespace Ecran.GUI
{
    /// <summary>
    /// Interaction logic for DisplayUserControl.xaml
    /// </summary>
    public partial class DisplayUserControl : UserControl
    {
        ViewModelMediator viewModelMediator;

        public ViewModelMediator ViewModelMediator {
            get {
                return viewModelMediator;
            }
            set {
                viewModelMediator = value;
                DataContext = value.DisplayViewModel;
            }
        }

        public DisplayUserControl()
        {
            InitializeComponent();
        }
    }
}
