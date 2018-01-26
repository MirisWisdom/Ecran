using Ecran.GUI.Actions;
using Ecran.GUI.Display;
using Ecran.GUI.Mediators;
using System.Collections.Generic;
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

            var availableResolutions = new List<Resolution>
            {
                new Resolution(800, 600),
                new Resolution(1024, 600),
                new Resolution(1024, 768),
                new Resolution(1152, 864),
                new Resolution(1280, 720),
                new Resolution(1280, 768),
                new Resolution(1280, 800),
                new Resolution(1280, 1024),
                new Resolution(1360, 768),
                new Resolution(1366, 768),
                new Resolution(1440, 900),
                new Resolution(1536, 864),
                new Resolution(1600, 900),
                new Resolution(1680, 1050),
                new Resolution(1920, 1080),
                new Resolution(1920, 1200),
                new Resolution(2560, 1080),
                new Resolution(2560, 1440),
                new Resolution(3440, 1440),
                new Resolution(3840, 2160),
            };

            var initialDisplay = new Display.Display
            {
                Resolution = availableResolutions[0]
            };

            var initialActions = new Actions.Actions
            {
                Binary = new Binary(string.Empty)
            };

            var vmMediator = new ViewModelMediator
            {
                DisplayViewModel = new DisplayViewModel(initialDisplay, availableResolutions),
                ActionsViewModel = new ActionsViewModel(initialActions)
            };

            var window = new MainWindow();

            window.DisplayUc.ViewModelMediator = vmMediator;
            window.ActionsUc.ViewModelMediator = vmMediator;

            window.Show();
        }
    }
}
