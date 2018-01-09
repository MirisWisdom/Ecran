using Joli.Commande;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Ecran.GUI
{
    /// <summary>
    /// Interaction logic for ActionsUserControl.xaml
    /// </summary>
    public partial class ActionsUserControl : UserControl
    {
        readonly IAppendMessage messageConsole;
        readonly MessageFactory messageFactory;

        MainViewModel viewModel;

        public MainViewModel ViewModel {
            get {
                return viewModel;
            }
            set {
                viewModel = value;
                DataContext = value;
            }
        }

        public ActionsUserControl()
        {
            InitializeComponent();
            messageConsole = new ConsoleTextBox(ConsoleTextBox);
            messageFactory = new MessageFactory();
        }

        void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.SaveSettings();
                Output(Properties.Resources.SuccessfulPatch);
            }
            catch (Exception ex)
            {
                Output(ex.Message);
            }
        }

        void Browse(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = Properties.Resources.FileFilter
            };

            if (openFileDialog.ShowDialog() == true)
            {
                viewModel.Path = openFileDialog.FileName;
                Output(Properties.Resources.SelectedBlam + viewModel.Path);
            }
        }

        void Detect(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.DetectBlamsav();
                Output(Properties.Resources.DetectedBlam + viewModel.Path);
            }
            catch (FileNotFoundException ex)
            {
                Output(ex.Message);
            }
        }

        void About(object sender, RoutedEventArgs e)
        {
            Output(Properties.Resources.AboutString);
        }

        void Help(object sender, RoutedEventArgs e)
        {
            Output(Properties.Resources.HelpString);
        }

        void Output(string message)
        {
            messageConsole.Append(messageFactory.GetMessage(message));
        }
    }
}
