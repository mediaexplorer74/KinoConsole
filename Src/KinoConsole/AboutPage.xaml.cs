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
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
        }


        private void ApplicationBarIconButton_Click_Home(object sender, RoutedEventArgs e)
        {
            //((Page)this).NavigationService.Navigate(new Uri("/HelpPage.xaml", UriKind.Relative)); 
            Frame.Navigate(typeof(MainPage));
        }

        private void ApplicationBarIconButton_Click_Settings(object sender, RoutedEventArgs e)
        {
            //((Page)this).NavigationService.Navigate(new Uri("/AddServerPage.xaml", UriKind.Relative));
            Frame.Navigate(typeof(AddServerPage));
        }

        private void ApplicationBarIconButton_Click_Pro(object sender, RoutedEventArgs e)
        {
            //((Page)this).NavigationService.Navigate(new Uri("/ProPage.xaml", UriKind.Relative));
            Frame.Navigate(typeof(ProPage));
        }

        private void ApplicationBarIconButton_Click_About(object sender, RoutedEventArgs e)
        {
            //((Page)this).NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
            Frame.Navigate(typeof(AboutPage));
        }
    }

}