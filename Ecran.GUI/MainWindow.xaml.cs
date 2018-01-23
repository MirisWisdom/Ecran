using System.Reflection;
using System.Windows;

namespace Ecran.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            VersionLabel.Content = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
