using Echoic.Binary;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace Ecran.GUI.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class View : Window
    {
        readonly ConsoleTextBox console;

        readonly ViewModel viewModel;

        public View(ViewModel mainView)
        {
            InitializeComponent();
            viewModel = mainView;
            DataContext = viewModel;

            console = new ConsoleTextBox(ConsoleTextBox);

            viewModel.SelectedResolution = viewModel.Resolutions[0];
        }

        void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                new ResolutionPatcher(new Blam(viewModel.Path))
                    .ApplyResolution(viewModel.SelectedResolution)
                    .ApplyNewHashing();

                console.Show(Resource.SuccessfulPatch);
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
                Filter = Resource.FileFilter
            };

            if (openFileDialog.ShowDialog() == true)
            {
                viewModel.Path = openFileDialog.FileName;
            }
        }

        private void Detect(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.Path = new BlamDetect().Find();
                console.Show("Detected blam.sav:\n" + viewModel.Path);
            }
            catch (FileNotFoundException ex)
            {
                console.Show(ex.Message);
            }
        }

        private void About(object sender, RoutedEventArgs e)
        {
            console.Show(Resource.AboutString);
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            console.Show(Resource.HelpString);
        }
    }
}
