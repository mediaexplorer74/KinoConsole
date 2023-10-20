// Decompiled with JetBrains decompiler
// Type: KinoConsole.BackDialog
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace KinoConsole
{
  public class BackDialog : UserControl
  {
    internal StackPanel LayoutRoot;
    internal Button btnKeyboard;
    internal Button btnEdit;
    internal Button btnDisconnect;
    private bool _contentLoaded;

    public BackDialog() => this.InitializeComponent();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/KinoConsole;component/BackDialog.xaml", UriKind.Relative));
      this.LayoutRoot = (StackPanel) ((FrameworkElement) this).FindName("LayoutRoot");
      this.btnKeyboard = (Button) ((FrameworkElement) this).FindName("btnKeyboard");
      this.btnEdit = (Button) ((FrameworkElement) this).FindName("btnEdit");
      this.btnDisconnect = (Button) ((FrameworkElement) this).FindName("btnDisconnect");
    }
  }
}
