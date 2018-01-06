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

            var model = new Main();
            var mainViewModel = new MainViewModel(model);
            var view = new MainWindow(mainViewModel);

            view.Show();
        }
    }
}
