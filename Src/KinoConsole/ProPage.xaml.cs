// Decompiled with JetBrains decompiler
// Type: KinoConsole.ProPage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

//using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Navigation;
using Windows.ApplicationModel.Store;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;

namespace KinoConsole
{
    public sealed partial class ProPage : Page
    {
        private static readonly string InAppProductKey = "kinoconsole.pro";
        private ProductListing productListing;
        //internal Grid LayoutRoot;
        //internal StackPanel purchasePanel;
        //internal StackPanel thankYouPanel;
        //internal StackPanel errorPanel;
        //private bool _contentLoaded;

        public ProPage()
        {
            this.InitializeComponent();
        }

        protected /*virtual*/ void OnNavigatedTo(NavigationEventArgs e)
        {
           // ((Page)this).OnNavigatedTo(e); ProPage
            this.UpdatePage();
        }

        private async void UpdatePage()
        {
            ((UIElement)this.purchasePanel).Visibility = (Visibility)1;
            ((UIElement)this.thankYouPanel).Visibility = (Visibility)1;
            ((UIElement)this.errorPanel).Visibility = (Visibility)1;
            try
            {
                ListingInformation products = await CurrentApp.LoadListingInformationByProductIdsAsync((IEnumerable<string>)new string[1]
                {
          ProPage.InAppProductKey
                });
                if (products.ProductListings.TryGetValue(ProPage.InAppProductKey, out this.productListing))
                {
                    ProductLicense productLicense = (ProductLicense)null;
                    if (CurrentApp.LicenseInformation.ProductLicenses.TryGetValue(ProPage.InAppProductKey, out productLicense))
                    {
                        if (productLicense.IsActive)
                        {
                            ((UIElement)this.thankYouPanel).Visibility = (Visibility)0;
                            return;
                        }
                      ((UIElement)this.purchasePanel).Visibility = (Visibility)0;
                        return;
                    }
                  ((UIElement)this.purchasePanel).Visibility = (Visibility)0;
                    return;
                }
            }
            catch (Exception ex)
            {
            }
          ((UIElement)this.errorPanel).Visibility = (Visibility)0;
        }

        private async void purchaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.productListing == null)
                return;
            try
            {
                string str = await CurrentApp.RequestProductPurchaseAsync(this.productListing.ProductId, false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] RequestProductPurchase error: " + ex.Message);
            }

            ProductLicense productLicense = (ProductLicense)null;
            if (!CurrentApp.LicenseInformation.ProductLicenses.TryGetValue(ProPage.InAppProductKey, out productLicense) || !productLicense.IsActive)
                ;
        }

        /*
        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/KinoConsole;component/ProPage.xaml", UriKind.Relative));
            this.LayoutRoot = (Grid)((FrameworkElement)this).FindName("LayoutRoot");
            this.purchasePanel = (StackPanel)((FrameworkElement)this).FindName("purchasePanel");
            this.thankYouPanel = (StackPanel)((FrameworkElement)this).FindName("thankYouPanel");
            this.errorPanel = (StackPanel)((FrameworkElement)this).FindName("errorPanel");
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
    public sealed partial class ProPage : Page
    {
        public ProPage()
        {
            this.InitializeComponent();
        }
    }
}
*/
