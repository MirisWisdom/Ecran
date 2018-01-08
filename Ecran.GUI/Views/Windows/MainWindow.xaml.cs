using Echoic.Binary;
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
        readonly ConsoleTextBox console;

        public MainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            console = new ConsoleTextBox(ConsoleTextBox);
        }

        void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                new ResolutionPatcher(new Blam(ViewModel.Path))
                    .ApplyResolution(ViewModel.Resolution)
                    .ApplyNewHashing();

                console.Show(Properties.Resources.SuccessfulPatch);
            }
            catch (Exception ex)
            {
                console.Show(ex.Message);
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
            }
        }

        private void Detect(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.Path = new BlamDetect().Find();
                console.Show("Detected blam.sav:\n" + ViewModel.Path);
            }
            catch (FileNotFoundException ex)
            {
                console.Show(ex.Message);
            }
        }

        private void About(object sender, RoutedEventArgs e)
        {
            console.Show(Properties.Resources.AboutString);
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            console.Show(Properties.Resources.HelpString);
        }
    }
}
