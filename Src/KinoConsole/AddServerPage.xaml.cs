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

namespace KinoConsole
{
    public sealed partial class AddServerPage : Page
    {
        //internal Grid LayoutRoot;
        //internal StackPanel LayoutRoot2;
        //internal TextBox tbx;
        //internal Button btnOK;
        //internal Button btnCancel;
        //private bool _contentLoaded;

        public AddServerPage()
        {
            this.InitializeComponent();
        }

        private void AddServerPage_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO
            //((Control)this.tbx).Focus();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            //if (!(Application.Current as App).nativeLib.AddServer(this.tbx.Text))
            //{
            //MessageBox.Show("Invalid address");                
            //}

            //((Page)this).NavigationService.GoBack();
            Frame.Navigate(typeof(MainPage));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            //((Page)this).NavigationService.GoBack();
            Frame.Navigate(typeof(MainPage));
        }

        /*
        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/KinoConsole;component/AddServerPage.xaml", UriKind.Relative));
            this.LayoutRoot = (Grid)((FrameworkElement)this).FindName("LayoutRoot");
            this.LayoutRoot2 = (StackPanel)((FrameworkElement)this).FindName("LayoutRoot2");
            this.tbx = (TextBox)((FrameworkElement)this).FindName("tbx");
            this.btnOK = (Button)((FrameworkElement)this).FindName("btnOK");
            this.btnCancel = (Button)((FrameworkElement)this).FindName("btnCancel");
        }
        */
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
    public sealed partial class AddServerPage : Page
    {
        public AddServerPage()
        {
            this.InitializeComponent();
        }
    }
}
*/
