// Decompiled with JetBrains decompiler
// Type: KinoConsole.SplashScreenControl
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace KinoConsole
{
  public class SplashScreenControl : UserControl
  {
    internal Grid LayoutRoot;
    internal ProgressBar progressBar1;
    private bool _contentLoaded;

    public SplashScreenControl() => this.InitializeComponent();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/KinoConsole;component/SplashScreenControl.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.progressBar1 = (ProgressBar) ((FrameworkElement) this).FindName("progressBar1");
    }
  }
}
