using System.Windows;

namespace Ecran.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var viewModel = new MainViewModel(new Main());

            var mainWindow = new MainWindow
            {
                ViewModel = viewModel,
                DataContext = viewModel
            };

            mainWindow.Show();
        }
    }
}
