// Decompiled with JetBrains decompiler
// Type: KinoConsole.ButtonDialog
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace KinoConsole
{
  public class ButtonDialog : UserControl
  {
    internal StackPanel LayoutRoot;
    internal ListBox buttonPicker;
    internal Button btnDlgCancel;
    private bool _contentLoaded;

    public ButtonDialog()
    {
      this.InitializeComponent();
      TiltEffect.TiltableItems.Add(typeof (StackPanel));
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/KinoConsole;component/ButtonDialog.xaml", UriKind.Relative));
      this.LayoutRoot = (StackPanel) ((FrameworkElement) this).FindName("LayoutRoot");
      this.buttonPicker = (ListBox) ((FrameworkElement) this).FindName("buttonPicker");
      this.btnDlgCancel = (Button) ((FrameworkElement) this).FindName("btnDlgCancel");
    }
  }
}
