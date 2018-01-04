using System.Windows.Controls;

namespace Ecran.GUI.Main.Console
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class View : UserControl
    {
        private ViewModel viewModel = new ViewModel(new Model());

        public View()
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
