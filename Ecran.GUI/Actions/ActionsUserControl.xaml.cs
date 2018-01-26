using Ecran.GUI.Mediators;
using Joli.Commande;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Ecran.GUI.Actions
{
    /// <summary>
    /// Interaction logic for ActionsUserControl.xaml
    /// </summary>
    public partial class ActionsUserControl : UserControl
    {
        private readonly ConsoleTextBox _messageConsole;

        private ViewModelMediator _viewModelMediator;

        public ViewModelMediator ViewModelMediator {
            get => _viewModelMediator;
            set {
                _viewModelMediator = value;
                DataContext = value.ActionsViewModel;
            }
        }

        public ActionsUserControl()
        {
            InitializeComponent();
            _messageConsole = new ConsoleTextBox(ConsoleTextBox);
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModelMediator.ActionsViewModel.SaveResolution(_viewModelMediator.DisplayViewModel.Resolution);
                Output(Properties.Resources.SuccessfulPatch);
            }
            catch (FileNotFoundException ex)
            {
                var saveErrorMessages = new IMessage[]
                {
                    MessageFactory.GetMessage(Properties.Resources.SaveError),
                    MessageFactory.GetMessage(ex.Message, MessageType.PrefixDate),
                };

                _messageConsole.Append(saveErrorMessages, new StarSeparator(length: 39));
            }
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = Properties.Resources.FileFilter
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _viewModelMediator.ActionsViewModel.Path = openFileDialog.FileName;
                Output(Properties.Resources.SelectedBlam + _viewModelMediator.ActionsViewModel.Path);
            }
        }

        private void Detect(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModelMediator.ActionsViewModel.DetectBlamsav();
                Output(Properties.Resources.DetectedBlam + _viewModelMediator.ActionsViewModel.Path);
            }
            catch (FileNotFoundException ex)
            {
                var detectionErrorMessages = new IMessage[]
                {
                    MessageFactory.GetMessage(Properties.Resources.FileNotFoundError),
                    MessageFactory.GetMessage(ex.Message, MessageType.PrefixDate),
                };

                _messageConsole.Append(detectionErrorMessages, new StarSeparator(length: 39));
            }
        }

        private void About(object sender, RoutedEventArgs e)
        {
            Output(Properties.Resources.AboutString);
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            Output(Properties.Resources.HelpString);
        }

        private void Output(string message)
        {
            _messageConsole.Append(MessageFactory.GetMessage(message));
        }
    }
}
