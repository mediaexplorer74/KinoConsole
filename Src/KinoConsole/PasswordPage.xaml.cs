// Decompiled with JetBrains decompiler
// Type: KinoConsole.PasswordPage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

//using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows;
//using System.Windows.Controls;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace KinoConsole
{
    public sealed partial class PasswordPage : Page
    {
        public static IBuffer serverUid;
        //internal Grid LayoutRoot;
        //internal StackPanel TitlePanel;
        //internal TextBlock PageTitle;
        //internal PasswordBox passwordBox;
        //internal Grid ContentPanel;
        //private bool _contentLoaded;

        public PasswordPage()
        {
            this.InitializeComponent();
            ((FrameworkElement)this).Loaded += new RoutedEventHandler(this.PasswordPage_Loaded);
        }

        private void PasswordPage_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO
            //((Control)this.passwordBox).Focus();
        }

        private void OK_Clicked(object sender, RoutedEventArgs e)
        {
            //TODO
            //(Application.Current as App).nativeLib.SetPassword(
            //    PasswordPage.serverUid, this.passwordBox.Password);

            //((Page)this).NavigationService.GoBack();
            Frame.Navigate(typeof(MainPage));
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            //TODO
            // ((Page)this).NavigationService.GoBack();
            Frame.Navigate(typeof(MainPage));
        }
               
    }
}

/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace KinoConsole
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class PasswordPage : Page
    {
        public PasswordPage()
        {
            this.InitializeComponent();
        }
    }
}
*/
