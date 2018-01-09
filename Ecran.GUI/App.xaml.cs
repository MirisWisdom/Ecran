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

            var vmMediator = new ViewModelMediator
            {
                DisplayViewModel = new DisplayViewModel(new Display
                {
                    Resolution = new Resolution(800, 800)
                }),
                ActionsViewModel = new ActionsViewModel(new Actions
                {
                    Binary = new Binary(string.Empty)
                })
            };

            var window = new MainWindow();

            window.DisplayUc.ViewModelMediator = vmMediator;
            window.ActionsUc.ViewModelMediator = vmMediator;

            window.Show();
        }
    }
}
