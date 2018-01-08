using Joli.Commande;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace Ecran.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly IAppendMessage messageConsole;
        readonly MessageFactory messageFactory;

        public MainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            messageConsole = new ConsoleTextBox(ConsoleTextBox);
            messageFactory = new MessageFactory();
        }

        void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.SaveSettings();
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
                ViewModel.Path = openFileDialog.FileName;
                Output(Properties.Resources.SelectedBlam + ViewModel.Path);
            }
        }

        void Detect(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.DetectBlamsav();
                Output(Properties.Resources.DetectedBlam + ViewModel.Path);
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
