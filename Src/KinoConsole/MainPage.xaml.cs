// Decompiled with JetBrains decompiler
// Type: KinoConsole.MainPage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3AA39D0A-B391-4615-B21E-9EAE1E0B1581
// Assembly location: C:\Users\Admin\Desktop\re\KC\KinoConsole.dll

//using FlurryWP8SDK;
//using FlurryWP8SDK.Models;
//using GoogleAds;
//using Microsoft.Advertising.Mobile.UI;
//using Microsoft.Devices;
//using Microsoft.Phone.Controls;
//using Microsoft.Phone.Net.NetworkInformation;
//using Microsoft.Phone.Shell;
//using NativeLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Resources;
//using System.Windows.Threading;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using wp7coverflow;


namespace KinoConsole
{
    public sealed partial class MainPage : Page
    {
        private List<MainPage.AppInfo> appList = new List<MainPage.AppInfo>();
        private List<string> imageList = new List<string>();
        private bool ignoreTap = true;// Environment.DeviceType == 1;
        private DispatcherTimer searchTimer = new DispatcherTimer();
        private DispatcherTimer splashTimer = new DispatcherTimer();
        private DispatcherTimer testTimer = new DispatcherTimer();
        private bool splashDelay = true;
        private string mSSID;
        private bool mShowHelp;
        //private AdControl adControl;
        private IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        private int namePrefixNum;
        private double deltaX;
        private static readonly string InAppProductKey = "kinoconsole.pro";

        public CollectionFlow ImageList; //TEMP
        /*
        internal Grid LayoutRoot;
        internal TextBlock gamesLibrary;
        internal StackPanel collection;
        internal CollectionFlow ImageList;
        internal StackPanel searchInfo;
        internal TextBlock searchText0;
        internal TextBlock searchText1;
        internal TextBlock searchText2;
        internal ProgressBar searchBar;
        internal StackPanel appInfo;
        internal TextBlock appName;
        internal TextBlock appServer;
        internal TextBlock appDetail;
        internal AdView admob;
        private bool _contentLoaded;
        */

        public MainPage()
        {
            this.InitializeComponent();
            this.SetDefaultControls();
            this.searchTimer.Interval = TimeSpan.FromMilliseconds(10000.0);
            //this.searchTimer.Tick += new EventHandler(this.OnSearchTimerTick);
            this.splashTimer.Interval = TimeSpan.FromMilliseconds(3000.0);
            //this.splashTimer.Tick += new EventHandler(this.OnSplashTimerTick);
            this.splashTimer.Start();
            //if (!this.settings.Contains(nameof(mShowHelp)))
            //{
                this.mShowHelp = true;
                this.searchTimer.Interval = TimeSpan.FromMilliseconds(9000.0);
                this.searchTimer.Start();
            //}
            //if (!this.settings.Contains("ret0"))
            //{
            //    Api.LogEvent("ret0");
            //    this.settings["ret0"] = (object)DateTime.Now.ToBinary();
            //    this.settings.Save();
            //}
            //else
            //{
            //    DateTime dateTime = DateTime.FromBinary((long)this.settings["ret0"]);
            //    DateTime now = DateTime.Now;
            //    if (!this.settings.Contains("ret1") && now.CompareTo(dateTime.AddDays(1.0)) > 0)
            //    {
            //        Api.LogEvent("ret1");
            //        this.settings["ret1"] = (object)DateTime.Now.ToBinary();
            //        this.settings.Save();
            //    }
            //    if (this.settings.Contains("ret7") || DateTime.Now.CompareTo(dateTime.AddDays(7.0)) <= 0)
            //        return;
            //    Api.LogEvent("ret7");
            //    this.settings["ret7"] = (object)DateTime.Now.ToBinary();
            //    this.settings.Save();
            //}
        }

        protected /*virtual*/ void OnNavigatedTo(NavigationEventArgs e)
        {
            //((Page)this).OnNavigatedTo(e);
            this.UpdateProInfo();
            this.mSSID = this.GetSSIDName();
            CNativeLib nativeLib = (Application.Current as App).nativeLib;

            WindowsRuntimeMarshal.AddEventHandler<ListUpdatedHandler>(
                new Func<ListUpdatedHandler, EventRegistrationToken>(nativeLib.add_ListUpdated), 
                new Action<EventRegistrationToken>(nativeLib.remove_ListUpdated), 
                new ListUpdatedHandler(this.nativeLib_ListUpdated));

            if (this.mSSID == null)
            {
                this.UpdateUI();
            }
            else
            {
                (Application.Current as App).nativeLib.StartSearch();
                this.searchTimer.Start();
            }

            //((UIElement)this.ImageList).ManipulationStarted +=
            //    new EventHandler<ManipulationStartedEventArgs>(this.ImageList_ManipulationStarted);

            //((UIElement)this.ImageList).ManipulationDelta += 
            //    new EventHandler<ManipulationDeltaEventArgs>(this.ImageList_ManipulationDelta);

            //((UIElement)this.ImageList).Tap += new EventHandler<GestureEventArgs>(this.ImageList_Tap);
        }

        protected /*virtual*/ void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //((Page)this).OnNavigatingFrom(e);
            //((UIElement)this.ImageList).Tap -= new EventHandler<GestureEventArgs>(this.ImageList_Tap);
            //((UIElement)this.ImageList).ManipulationStarted -= new EventHandler<ManipulationStartedEventArgs>(this.ImageList_ManipulationStarted);
            //((UIElement)this.ImageList).ManipulationDelta -= new EventHandler<ManipulationDeltaEventArgs>(this.ImageList_ManipulationDelta);
            this.searchTimer.Stop();
            this.testTimer.Stop();
            (Application.Current as App).nativeLib.StopSearch();
            WindowsRuntimeMarshal.RemoveEventHandler<ListUpdatedHandler>(
                new Action<EventRegistrationToken>((Application.Current as App).nativeLib.remove_ListUpdated), 
                new ListUpdatedHandler(this.nativeLib_ListUpdated));
        }

        private void OnSearchTimerTick(object sender, EventArgs args)
        {
            this.searchTimer.Stop();
            if (this.mShowHelp && this.appList.Count == 0)
            {
                this.mShowHelp = false;
                //this.settings["mShowHelp"] = (object)false;
                //this.settings.Save();
                //((Page)this).NavigationService.Navigate(new Uri("/HelpPage.xaml", UriKind.Relative));
                Frame.Navigate(typeof(HelpPage));
            }
            else
                this.UpdateUI();
        }

        private void OnTestTimerTick(object sender, EventArgs args)
        {
            if (this.appList.Count <= 0)
                return;
            int index = new Random().Next(0, this.appList.Count);
            if (this.appList[index].appName == null || this.appList[index].appName.Length < 1 || !this.appList[index].serverName.Equals("wkasi"))
                return;
            this.testTimer.Stop();
            RemotePage.serverUid = this.appList[index].serverUid;
            RemotePage.appPath = this.appList[index].appPath;
            RemotePage.appName = this.appList[index].appName;
            //((Page)this).NavigationService.Navigate(new Uri("/RemotePage.xaml", UriKind.Relative));
            Frame.Navigate(typeof(RemotePage));
        }

        private void FeedbackOverlay_VisibilityChanged(object sender, EventArgs e)
        {
        }

        private void UpdateUI()
        {
            ((UIElement)this.searchBar).Visibility = (Visibility)1;
            if (this.appList.Count > 0)
            {
                ((UIElement)this.searchInfo).Visibility = (Visibility)1;
                if (this.appList[0].appPath != null)
                {
                    this.appName.Text = this.appList[0].appName;
                    this.appServer.Text = "on " + this.appList[0].serverName;
                    ((UIElement)this.appServer).Visibility = (Visibility)0;
                    //if (this.settings.Contains("peli:" + this.appList[0].appName))
                    //{
                    this.appDetail.Text = "last played ";// + this.settings["peli:" + this.appList[0].appName];
                        ((UIElement)this.appDetail).Visibility = (Visibility)0;
                    //}
                    //else
                    //    ((UIElement)this.appDetail).Visibility = (Visibility)1;
                }
                else
                {
                    this.appName.Text = this.appList[0].serverName;
                    ((UIElement)this.appServer).Visibility = (Visibility)1;
                    this.appDetail.Text = this.appList[0].status != MainPage.AppInfo.Status.EPasswordMissing ? "no games configured" : "password required";
                }
              ((UIElement)this.appName).Visibility = (Visibility)0;
                ((UIElement)this.gamesLibrary).Visibility = (Visibility)0;
                ((UIElement)this.collection).Visibility = (Visibility)0;
                ((UIElement)this.appInfo).Visibility = (Visibility)0;
                Debug.WriteLine("[i] ServerFound");
            }
            else
            {
                ((UIElement)this.gamesLibrary).Visibility = (Visibility)1;
                ((UIElement)this.collection).Visibility = (Visibility)1;
                ((UIElement)this.appInfo).Visibility = (Visibility)1;
                ((UIElement)this.searchText0).Visibility = (Visibility)0;
                ((UIElement)this.searchInfo).Visibility = (Visibility)0;
                string ssidName = this.GetSSIDName();
                if (ssidName != null)
                {
                    this.searchText1.Text = "No computers found from your local network (" + ssidName + ").";
                    ((FrameworkElement)this.searchText1).HorizontalAlignment = (HorizontalAlignment)0;
                    this.searchText2.Text = "Please check that you have installed Kinoni Windows Server in your PC and that it is connected to the same network as your Windows Phone device.";
                    this.searchText2.Text += "\nAlso check that bluetooth is switched off. It can sometimes disturb WiFi connections.";
                    ((UIElement)this.searchText2).Visibility = (Visibility)0;
                    Debug.WriteLine("[i] NoServersFound");
                }
                else
                {
                    this.searchText1.Text = "It seems that your device is not connected to the network.\nPlease check your network settings.";
                    ((FrameworkElement)this.searchText1).HorizontalAlignment = (HorizontalAlignment)0;
                    ((UIElement)this.searchText2).Visibility = (Visibility)1;
                    Debug.WriteLine("[i] NoNetwork");
                }
            }
        }

        private string GetSSIDName()
        {
            foreach (NetworkInterfaceInfo networkInterfaceInfo in new NetworkInterfaceList())
            {
                if ((networkInterfaceInfo.InterfaceType == 71 /*|| Environment.DeviceType == 1*/) 
                    && networkInterfaceInfo.InterfaceState == 1)
                    return networkInterfaceInfo.InterfaceName;
            }
            return (string)null;
        }

        private void OnSplashTimerTick(object sender, EventArgs args)
        {
            if (this.splashDelay)
            {
                this.splashDelay = false;
                this.splashTimer.Stop();
                this.splashTimer.Interval = TimeSpan.FromMilliseconds(20.0);
                this.splashTimer.Start();
            }
            else if ((Application.Current as App).splashPopup.Child.Opacity > 0.1)
            {
                (Application.Current as App).splashPopup.Child.Opacity -= 0.05;
            }
            else
            {
                this.splashTimer.Stop();
                (Application.Current as App).splashPopup.IsOpen = false;
                //SystemTray.IsVisible = true;
                //this.ApplicationBar.IsVisible = true;
                BottomAppBar.IsOpen = false;//true;
            }
        }

        private void nativeLib_ListUpdated()
        {
            //((DependencyObject)Deployment.Current).Dispatcher.BeginInvoke((Action)(() =>
            {
                string str = this.namePrefixNum++.ToString();
                this.appList = new List<MainPage.AppInfo>();
                this.imageList = new List<string>();
                IsolatedStorageFile.GetUserStoreForApplication();
                try
                {
                    using (IsolatedStorageFile storeForApplication 
                    = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        string path1 = ApplicationData.Current.LocalFolder.Path + "\\";
                        foreach (string fileName in storeForApplication.GetFileNames(Path.Combine(path1, 
                            "CoverFile*.png")))
                            storeForApplication.DeleteFile(Path.Combine(path1, fileName));
                    }
                }
                catch (Exception ex)
                {
                }
                for (int idx = 0; idx < 1000; ++idx)
                {
                    MainPage.AppInfo appInfo = new MainPage.AppInfo();
                    IBuffer listServerUid = (Application.Current as App).nativeLib.GetListServerUid(idx);
                    if (listServerUid != null)
                    {
                        appInfo.serverUid = listServerUid;
                        appInfo.serverName = (Application.Current as App).nativeLib.GetListServerName(idx);
                        appInfo.appPath = (Application.Current as App).nativeLib.GetListAppPath(idx);
                        appInfo.appName = (Application.Current as App).nativeLib.GetListAppName(idx);
                        appInfo.status = default;
                           // (MainPage.AppInfo.Status)(Application.Current as App).nativeLib.GetListStatus(idx);

                        IBuffer listAppIcon = (Application.Current as App).nativeLib.GetListAppIcon(idx);
                        if (listAppIcon != null)
                        {
                            string path = ApplicationData.Current.LocalFolder.Path + "\\CoverFile" 
                                + str + idx.ToString() + ".png";
                            try
                            {
                                using (IsolatedStorageFile storeForApplication 
                                    = IsolatedStorageFile.GetUserStoreForApplication())
                                {
                                    using (IsolatedStorageFileStream file = storeForApplication.CreateFile(path))
                                    {
                                        using (BinaryWriter binaryWriter = new BinaryWriter((Stream)file))
                                            binaryWriter.Write(listAppIcon.ToArray());
                                        file.Dispose();//.Close();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("[ex] ListUpdated bug: " + ex.Message);
                                return;
                            }
                            this.imageList.Add(path);
                        }
                        else if (appInfo.status == MainPage.AppInfo.Status.EPasswordMissing)
                            this.imageList.Add("Assets/serverlocked.png");
                        else
                            this.imageList.Add("Assets/server.png");
                        this.appList.Add(appInfo);
                    }
                    else
                        break;
                }
                int selectedItemIndex = this.ImageList.SelectedItemIndex;
                this.ImageList.ItemsSource = (IEnumerable)this.imageList;
                this.ImageList.SelectedItemIndex = selectedItemIndex < 0 
                || selectedItemIndex >= this.imageList.Count ? 0 : selectedItemIndex;
                this.UpdateUI();
            }//));
        }

        private void ImageList_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            this.deltaX = 0.0;
        }

        private void ImageList_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            this.deltaX += e.DeltaManipulation.Translation.X;
            if (this.deltaX > 100.0)
            {
                if (this.ImageList.SelectedItemIndex > 0)
                    --this.ImageList.SelectedItemIndex;
                this.deltaX = 0.0;
            }
            else if (this.deltaX < -100.0)
            {
                if (this.ImageList.SelectedItemIndex < this.appList.Count - 1)
                    ++this.ImageList.SelectedItemIndex;
                this.deltaX = 0.0;
            }
            if (this.appList[this.ImageList.SelectedItemIndex].appPath != null)
            {
                this.appName.Text = this.appList[this.ImageList.SelectedItemIndex].appName;
                this.appServer.Text = "on " + this.appList[this.ImageList.SelectedItemIndex].serverName;
                ((UIElement)this.appServer).Visibility = (Visibility)0;
                //if (this.settings.Contains("peli:" + this.appList[this.ImageList.SelectedItemIndex].appName))
                //{
                //    this.appDetail.Text = "last played " + this.settings["peli:" + this.appList[this.ImageList.SelectedItemIndex].appName];
                //    ((UIElement)this.appDetail).Visibility = (Visibility)0;
                //}
                //else
                {
                    ((UIElement)this.appDetail).Visibility = (Visibility)1;
                }
            }
            else
            {
                this.appName.Text = this.appList[this.ImageList.SelectedItemIndex].serverName;
                ((UIElement)this.appServer).Visibility = (Visibility)1;
                this.appDetail.Text = "game list not available";
            }
        }

        private void ImageList_Tap(object sender, GestureEventArgs e)
        {
            Point position = e.GetPosition((UIElement)this.LayoutRoot);
            if (this.appList.Count < 1)
                return;
            if (this.ignoreTap)
            {
                this.ignoreTap = false;
            }
            else
            {
                if (position.X < 160.0 || position.X > 320.0 || position.Y > 500.0)
                    return;
                int selectedItemIndex = this.ImageList.SelectedItemIndex;
                if (this.appList[this.ImageList.SelectedItemIndex].status == MainPage.AppInfo.Status.EPasswordMissing)
                {
                    PasswordPage.serverUid = this.appList[this.ImageList.SelectedItemIndex].serverUid;
                    //((Page)this).NavigationService.Navigate(new Uri("/PasswordPage.xaml", UriKind.Relative));
                    Frame.Navigate(typeof(PasswordPage));
                }
                else
                {
                    this.UpdateTile();
                    //this.settings["peli:" + this.appList[this.ImageList.SelectedItemIndex].appName] = (object)DateTime.Now.ToString();
                    List<IBuffer> ibufferList = new List<IBuffer>();
                    int num = 0;
                    for (int index1 = 0; index1 < this.appList.Count; ++index1)
                    {
                        bool flag = true;
                        for (int index2 = 0; index2 < ibufferList.Count; ++index2)
                        {
                            if (((IEnumerable<byte>)ibufferList[index2].ToArray()).SequenceEqual<byte>((IEnumerable<byte>)this.appList[index1].serverUid.ToArray()))
                                flag = false;
                        }
                        if (flag)
                            ibufferList.Add(this.appList[index1].serverUid);
                        if (this.appList[index1].appName != null && this.appList[index1].appName.Length > 0)
                            ++num;
                    }
                    Debug.WriteLine
                    ("[i] peli: ", 
                          new List<Parameter>()
                            {
                            new Parameter("nimi", this.appList[this.ImageList.SelectedItemIndex].appName),
                            new Parameter("svrCount", ibufferList.Count.ToString()),
                            new Parameter("appCount", num.ToString())
                            }
                    );
                    RemotePage.serverUid = this.appList[this.ImageList.SelectedItemIndex].serverUid;
                    RemotePage.appPath = this.appList[this.ImageList.SelectedItemIndex].appPath;
                    RemotePage.appName = this.appList[this.ImageList.SelectedItemIndex].appName;
                    //((Page)this).NavigationService.Navigate(new Uri("/RemotePage.xaml", UriKind.Relative));
                    Frame.Navigate(typeof(RemotePage));
                }
            }
        }


        private void ApplicationBarIconButton_Click_Settings(object sender, RoutedEventArgs e)
        {
            //((Page)this).NavigationService.Navigate(new Uri("/AddServerPage.xaml", UriKind.Relative));
            Frame.Navigate(typeof(AddServerPage));
        }

        private void ApplicationBarIconButton_Click_Help(object sender, RoutedEventArgs e)
        {
            //((Page)this).NavigationService.Navigate(new Uri("/HelpPage.xaml", UriKind.Relative)); 
            Frame.Navigate(typeof(HelpPage));
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

        private async void UpdateProInfo()
        {
            if (1==0)//(this.settings.Contains("proVersion"))
            {
                //if (this.ApplicationBar.Buttons.Count > 3)
                //    this.ApplicationBar.Buttons.Remove((object)(this.ApplicationBar.Buttons[2] 
                //        as ApplicationBarIconButton));

                //if (this.adControl != null)
                //    ((UIElement)this.adControl).Visibility = (Visibility)1;
                //((UIElement)this.admob).Visibility = (Visibility)1;
            }
            else
            {
                //if (this.adControl == null)
                //{
                //    this.adControl = new AdControl("1a7ddb59-0bc0-410f-b58b-e906a9fe34d7", "Image480_80", true);
                //    ((FrameworkElement)this.adControl).Width = 480.0;
                //    ((FrameworkElement)this.adControl).Height = 80.0;
                //    this.adControl.AdRefreshed += new EventHandler(this.adControl_AdRefreshed);
                //    this.adControl.ErrorOccurred += new EventHandler<Microsoft.Advertising.AdErrorEventArgs>(this.adControl_ErrorOccurred);
                //    ((PresentationFrameworkCollection<UIElement>)((Panel)this.appInfo).Children).Add((UIElement)this.adControl);
                //}
                try
                {
                    ListingInformation products = 
                        await CurrentApp.LoadListingInformationByProductIdsAsync(
                            (IEnumerable<string>)new string[1]
                    {
            MainPage.InAppProductKey
                    });
                    ProductListing productListing = (ProductListing)null;

                    if (products.ProductListings.TryGetValue(MainPage.InAppProductKey, out productListing))
                    {
                        ProductLicense productLicense = (ProductLicense)null;
                        if (!CurrentApp.LicenseInformation.ProductLicenses.TryGetValue(MainPage.InAppProductKey, 
                            out productLicense) || !productLicense.IsActive)
                            return;
                        
                        //this.settings["proVersion"] = (object)true;
                        //this.settings.Save();

                        //if (this.adControl != null)
                        //    ((UIElement)this.adControl).Visibility = (Visibility)1;
                        //((UIElement)this.admob).Visibility = (Visibility)1;
                    }
                    else
                        Debug.WriteLine("[ex] noProductInfo error: no product license key");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[ex] ProductList bug:" + ex.Message);
                }

                //if (this.ApplicationBar.Buttons.Count <= 3)
                //    return;
                //this.ApplicationBar.Buttons.Remove((object)(this.ApplicationBar.Buttons[2]
                //    as ApplicationBarIconButton));
            }
        }

        private void UpdateTile()
        {

            //ShellTile shellTile = ShellTile.ActiveTiles.First<ShellTile>();
            //if (shellTile == null)
            //    return;
            try
            {
                /*
                FlipTileData flipTileData = new FlipTileData();
                byte[] buffer;
                using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (this.ImageList.SelectedItemIndex > this.imageList.Count)
                        return;
                    string image = this.imageList[this.ImageList.SelectedItemIndex];
                    using (IsolatedStorageFileStream storageFileStream = storeForApplication.OpenFile(image, FileMode.Open, FileAccess.Read))
                    {
                        buffer = new byte[storageFileStream.Length];
                        storageFileStream.Read(buffer, 0, buffer.Length);
                        storageFileStream.Close();
                    }
                }
                MemoryStream memoryStream = new MemoryStream(buffer);
                BitmapImage bitmap = new BitmapImage();
                ((BitmapSource)bitmap).SetSource((Stream)memoryStream);
                this.SaveJpeg(bitmap, 159);
                this.SaveJpeg(bitmap, 336);
                flipTileData.SmallBackgroundImage = new Uri("isostore:/Shared/ShellContent/back159x159.jpg", UriKind.Absolute);
                ((StandardTileData)flipTileData).BackBackgroundImage = new Uri("isostore:/Shared/ShellContent/back336x336.jpg", UriKind.Absolute);
                shellTile.Update((ShellTileData)flipTileData);
                */
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] UpdateTile bug: " + nameof(UpdateTile) + " " +  ex.Message);
            }
        }

        private void SaveJpeg(BitmapImage bitmap, int width)
        {
            using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string str = "/Shared/ShellContent/back" + (object)width + "x" + (object)width + ".jpg";
                if (storeForApplication.FileExists(str))
                    storeForApplication.DeleteFile(str);
                using (IsolatedStorageFileStream file = storeForApplication.CreateFile(str))
                {
                    WriteableBitmap writeableBitmap1 = new WriteableBitmap(bitmap.PixelWidth, bitmap.PixelHeight);//((BitmapSource)bitmap);
                    Image image = new Image();
                    ((FrameworkElement)image).Height = (double)((BitmapSource)bitmap).PixelWidth;
                    ((FrameworkElement)image).Width = (double)((BitmapSource)bitmap).PixelHeight;
                    image.Source = (ImageSource)bitmap;
                    Color color = new Color();
                    color.A = byte.MaxValue;
                    color.R = (byte)38;
                    color.G = (byte)114;
                    color.B = (byte)236;
                    WriteableBitmap writeableBitmap2 = writeableBitmap1;
                    Canvas canvas1 = new Canvas();
                    ((Panel)canvas1).Background = (Brush)new SolidColorBrush(color);
                    ((FrameworkElement)canvas1).Width = (double)((BitmapSource)bitmap).PixelHeight;
                    ((FrameworkElement)canvas1).Height = (double)((BitmapSource)bitmap).PixelHeight;
                    Canvas canvas2 = canvas1;
                    //writeableBitmap2.Render((UIElement)canvas2, (Transform)null);
                    //writeableBitmap1.Render((UIElement)image, (Transform)null);
                    writeableBitmap1.Invalidate();
                    //Extensions.SaveJpeg(writeableBitmap1, (Stream)file, width, width, 0, 85);
                    file.Dispose();//.Close();
                }
            }
        }

        private void SetDefaultControls()
        {
            try
            {
                IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
                if (storeForApplication.FileExists("appconfig.xml"))
                    return;
                StreamResourceInfo resourceStream = default;
                //Application.GetResourceStream(new Uri("Assets/appconfig.xml", UriKind.Relative));

                /*using (BinaryReader binaryReader = new BinaryReader(resourceStream.Stream))
                {
                    byte[] buffer = binaryReader.ReadBytes((int)resourceStream.Stream.Length);
                    using (BinaryWriter binaryWriter = 
                        new BinaryWriter((Stream)storeForApplication.CreateFile("appconfig.xml")))
                    {
                        binaryWriter.Write(buffer);
                        binaryWriter.Dispose();//.Close();
                    }
                }*/
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] " + nameof(SetDefaultControls) + ex.Message);
            }
        }

        /*private void adControl_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            ((UIElement)this.adControl).Visibility = (Visibility)1;
            ((UIElement)this.admob).Visibility = (Visibility)0;
        }

        private void adControl_AdRefreshed(object sender, EventArgs e)
        {
        }

        private void AdmobReceived(object sender, AdEventArgs e)
        {
        }

        private void AdmobFailed(object sender, GoogleAds.AdErrorEventArgs errorCode)
        {
        }
        */

     

        private class AppInfo
        {
            public IBuffer serverUid;
            public string serverName;
            public IBuffer appPath;
            public string appName;
            public MainPage.AppInfo.Status status;

            public enum Status
            {
                EUnknown,
                EOk,
                EPasswordMissing,
                ENoAppsConfigured,
            }
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
...
*/
