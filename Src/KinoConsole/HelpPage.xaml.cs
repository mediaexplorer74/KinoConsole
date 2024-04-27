// Type: KinoConsole.HelpPage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

//using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Navigation;
//using System.Windows.Resources;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;

namespace KinoConsole
{
    public sealed partial class HelpPage : Page
    {
        //internal Grid LayoutRoot;
        //internal ScrollViewer ContentPanel;
        //internal WebBrowser webBrowser;
        //private bool _contentLoaded;

        public HelpPage()
        {
            this.InitializeComponent();
        }

        private void webBrowser_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            this.webBrowser.Navigate(new Uri("index.html", UriKind.Relative));
        }

        private void WebBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            this.webBrowser.Navigate(new Uri("http://kinoconsole.kinoni.com/wpsetup.html",
                UriKind.Absolute));
        }

        private void SaveFilesToIsoStore()
        {
            string[] strArray = new string[5]
            {
            "index.html",
            "config.png",
            "download.png",
            "pc_phone.png",
            "style.css"
            };
            IsolatedStorageFile.GetUserStoreForApplication();
            foreach (string fileName in strArray)
            {
                //StreamResourceInfo resourceStream = Application.GetResourceStream(
                //    new Uri("Assets/Help/" + fileName, UriKind.Relative));

                //using (BinaryReader binaryReader = new BinaryReader(resourceStream.Stream))
                //{
                //    byte[] data = binaryReader.ReadBytes((int)resourceStream.Stream.Length);
                //    this.SaveToIsoStore(fileName, data);
                //}
            }
        }

        private void SaveToIsoStore(string fileName, byte[] data)
        {
            string str = string.Empty;
            char[] charArray = "/".ToCharArray();
            string[] strArray = fileName.Split(charArray);
            IsolatedStorageFile storeForApplication 
                = IsolatedStorageFile.GetUserStoreForApplication();

            for (int index = 0; index < strArray.Length - 1; ++index)
            {
                str = Path.Combine(str, strArray[index]);
                storeForApplication.CreateDirectory(str);
            }
            if (storeForApplication.FileExists(fileName))
                storeForApplication.DeleteFile(fileName);
            using (BinaryWriter binaryWriter = new BinaryWriter(
                (Stream)storeForApplication.CreateFile(fileName)))
            {
                binaryWriter.Write(data);
                binaryWriter.Dispose();//.Close();
            }
        }

        /*
        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/KinoConsole;component/HelpPage.xaml", UriKind.Relative));
            this.LayoutRoot = (Grid)((FrameworkElement)this).FindName("LayoutRoot");
            this.ContentPanel = (ScrollViewer)((FrameworkElement)this).FindName("ContentPanel");
            this.webBrowser = (WebBrowser)((FrameworkElement)this).FindName("webBrowser");
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
    public sealed partial class HelpPage : Page
    {
        public HelpPage()
        {
            this.InitializeComponent();
        }
    }
}
*/
