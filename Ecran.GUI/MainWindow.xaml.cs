/**
 * Copyright (c) 2018-2019 yumiris, yuviria
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 */

using System;
using System.IO;
using System.Reflection;
using System.Windows;
using Microsoft.Win32;

namespace Ecran.GUI
{
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private readonly Main _main;

    public MainWindow()
    {
      InitializeComponent();
      _main = (Main) DataContext;

      var version = Assembly.GetExecutingAssembly().GetName().Version;
      VersionLabel.Content = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
      
      try
      {
        _main.Detect();
        Output(Properties.Resources.DetectedBlam + _main.Path);
      }
      catch (Exception ex)
      {
        Output(ex.Message);
      }
    }

    private void Save(object sender, RoutedEventArgs e)
    {
      try
      {
        _main.Apply();
        Output(Properties.Resources.SuccessfulPatch);
      }
      catch (Exception ex)
      {
        Output(ex.Message);
      }
    }

    private void Browse(object sender, RoutedEventArgs e)
    {
      var openFileDialog = new OpenFileDialog
      {
        Filter = Properties.Resources.FileFilter
      };

      if (openFileDialog.ShowDialog() != true) return;

      _main.Path = openFileDialog.FileName;

      Output(Properties.Resources.SelectedBlam + _main.Path);
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
      ConsoleTextBox.Text = message + "\n\n" + ConsoleTextBox.Text;
    }
  }
}