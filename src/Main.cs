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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Ecran.Properties;
using HXE;

namespace Ecran
{
  public sealed class Main : INotifyPropertyChanged
  {
    private int _width  = Screen.PrimaryScreen.Bounds.Width;
    private int _height = Screen.PrimaryScreen.Bounds.Height;

    private string _path;

    public int Width
    {
      get => _width;
      set
      {
        if (value == _width) return;
        _width = value;
        OnPropertyChanged();
      }
    }

    public int Height
    {
      get => _height;
      set
      {
        if (value == _height) return;
        _height = value;
        OnPropertyChanged();
      }
    }

    public string Path
    {
      get => _path;
      set
      {
        if (value == _path) return;
        _path = value;
        OnPropertyChanged();
      }
    }

    public void Detect()
    {
      Path = Profile.Detect().Path;
    }

    public void Apply()
    {
      var profile = (Profile) Path;

      profile.Load();
      profile.Video.Resolution.Width  = (ushort) Width;
      profile.Video.Resolution.Height = (ushort) Height;
      profile.Save();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}