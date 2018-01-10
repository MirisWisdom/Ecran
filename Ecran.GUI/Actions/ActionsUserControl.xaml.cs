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

        ViewModelMediator viewModelMediator;

        public ViewModelMediator ViewModelMediator {
            get {
                return viewModelMediator;
            }
            set {
                viewModelMediator = value;
                DataContext = value.ActionsViewModel;
            }
        }

        public ActionsUserControl()
        {
            InitializeComponent();
            messageConsole = new ConsoleTextBox(ConsoleTextBox);
        }

        void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModelMediator.ActionsViewModel.SaveResolution(viewModelMediator.DisplayViewModel.Resolution);
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
                viewModelMediator.ActionsViewModel.Path = openFileDialog.FileName;
                Output(Properties.Resources.SelectedBlam + viewModelMediator.ActionsViewModel.Path);
            }
        }

        void Detect(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModelMediator.ActionsViewModel.DetectBlamsav();
                Output(Properties.Resources.DetectedBlam + viewModelMediator.ActionsViewModel.Path);
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
            messageConsole.Append(MessageFactory.GetMessage(message));
        }
    }
}
