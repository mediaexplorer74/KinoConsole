// Decompiled with JetBrains decompiler
// Type: KinoConsole.PasswordPage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Windows.Storage.Streams;

namespace KinoConsole
{
  public class PasswordPage : PhoneApplicationPage
  {
    public static IBuffer serverUid;
    internal Grid LayoutRoot;
    internal StackPanel TitlePanel;
    internal TextBlock PageTitle;
    internal PasswordBox passwordBox;
    internal Grid ContentPanel;
    private bool _contentLoaded;

    public PasswordPage()
    {
      this.InitializeComponent();
      ((FrameworkElement) this).Loaded += new RoutedEventHandler(this.PasswordPage_Loaded);
    }

    private void PasswordPage_Loaded(object sender, RoutedEventArgs e) => ((Control) this.passwordBox).Focus();

    private void OK_Clicked(object sender, RoutedEventArgs e)
    {
      (Application.Current as App).nativeLib.SetPassword(PasswordPage.serverUid, this.passwordBox.Password);
      ((Page) this).NavigationService.GoBack();
    }

    private void Cancel_Clicked(object sender, RoutedEventArgs e) => ((Page) this).NavigationService.GoBack();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/KinoConsole;component/PasswordPage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.TitlePanel = (StackPanel) ((FrameworkElement) this).FindName("TitlePanel");
      this.PageTitle = (TextBlock) ((FrameworkElement) this).FindName("PageTitle");
      this.passwordBox = (PasswordBox) ((FrameworkElement) this).FindName("passwordBox");
      this.ContentPanel = (Grid) ((FrameworkElement) this).FindName("ContentPanel");
    }
  }
}
