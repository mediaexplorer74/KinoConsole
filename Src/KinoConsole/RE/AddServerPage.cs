// Decompiled with JetBrains decompiler
// Type: KinoConsole.AddServerPage
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
  public class AddServerPage : PhoneApplicationPage
  {
    internal Grid LayoutRoot;
    internal StackPanel LayoutRoot2;
    internal TextBox tbx;
    internal Button btnOK;
    internal Button btnCancel;
    private bool _contentLoaded;

    public AddServerPage() => this.InitializeComponent();

    private void AddServerPage_Loaded(object sender, RoutedEventArgs e) => ((Control) this.tbx).Focus();

    private void btnOK_Click(object sender, RoutedEventArgs e)
    {
      if (!(Application.Current as App).nativeLib.AddServer(this.tbx.Text))
        MessageBox.Show("Invalid address");
      ((Page) this).NavigationService.GoBack();
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e) => ((Page) this).NavigationService.GoBack();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/KinoConsole;component/AddServerPage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.LayoutRoot2 = (StackPanel) ((FrameworkElement) this).FindName("LayoutRoot2");
      this.tbx = (TextBox) ((FrameworkElement) this).FindName("tbx");
      this.btnOK = (Button) ((FrameworkElement) this).FindName("btnOK");
      this.btnCancel = (Button) ((FrameworkElement) this).FindName("btnCancel");
    }
  }
}
