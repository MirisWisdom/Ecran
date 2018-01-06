using Echoic.Binary;
using Echoic.Checksum;
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
        readonly ViewModel viewModel;

        readonly int divideValue = (int)Math.Pow(2, 8);
        readonly int offsetValue = 0xA68;

        public View(ViewModel mainView)
        {
            InitializeComponent();
            viewModel = mainView;
            DataContext = viewModel;
        }

        void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                new Blam(viewModel.Path).Patch(new Func<byte[]>(() =>
                {
                    return new byte[]
                    {
                        (byte) (viewModel.SelectedResolution.Width % divideValue),
                        (byte) (viewModel.SelectedResolution.Width / divideValue),

                        (byte) (viewModel.SelectedResolution.Height % divideValue),
                        (byte) (viewModel.SelectedResolution.Height / divideValue),
                    };
                })(), offsetValue);

                new Blam(viewModel.Path).Patch(new Func<byte[]>(() =>
                {
                    var forge = new Forge(viewModel.Path).Calculate();
                    Array.Reverse(forge);
                    return forge;
                })(), Checksum.FileLength);

                MessageBox.Show("Successfully applied the new resolution!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void Browse(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

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
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Could not find the necessarily files!");
            }
        }
    }
}
