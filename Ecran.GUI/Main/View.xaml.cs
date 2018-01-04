using System.Windows;

namespace Ecran.GUI.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class View : Window
    {
        public View(ViewModel mainView)
        {
            InitializeComponent();
            DataContext = mainView;
        }
    }
}
