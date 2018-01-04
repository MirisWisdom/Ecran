using Ecran.GUI.Main;
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

            var model = new Model();
            var mainViewModel = new ViewModel(model);
            var view = new Main.View(mainViewModel);

            view.Show();
        }
    }
}
