// Type: KinoConsole.RemotePage
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

//using FlurryWP8SDK;
//using GoogleAds;
//using Microsoft.Devices.Sensors;
//using Microsoft.Phone.Controls;
//using Microsoft.Phone.Info;
//using Microsoft.Phone.Media;
//using NativeLib;
using NativeLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Controls.Primitives;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using System.Windows.Threading;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Media.Core;
using Windows.Storage.Streams;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace KinoConsole
{
    public sealed partial class RemotePage : Page
    {
        private const int EVENT_ID_JOYSTICK_LEFT_X = 9;
        private const int EVENT_ID_JOYSTICK_LEFT_Y = 10;
        private const int EVENT_ID_JOYSTICK_RIGHT_X = 11;
        private const int EVENT_ID_JOYSTICK_RIGHT_Y = 12;
        private const int EVENT_ID_JOYSTICK_LEFT_TRIGGER = 13;
        private const int EVENT_ID_JOYSTICK_RIGHT_TRIGGER = 14;
        private const int KEYCODE_BUTTON_A = 57344;
        private const int KEYCODE_BUTTON_B = 57345;
        private const int KEYCODE_BUTTON_X = 57346;
        private const int KEYCODE_BUTTON_Y = 57347;
        private const int KEYCODE_BUTTON_LB = 57348;
        private const int KEYCODE_BUTTON_RB = 57349;
        private const int KEYCODE_BUTTON_START = 57350;
        private const int KEYCODE_BUTTON_BACK = 57351;
        private const int KEYCODE_DPAD_UP = 57352;
        private const int KEYCODE_DPAD_DOWN = 57353;
        private const int KEYCODE_DPAD_LEFT = 57354;
        private const int KEYCODE_DPAD_RIGHT = 57355;
        private const int ButtonA = 0;
        private const int ButtonB = 1;
        private const int ButtonX = 2;
        private const int ButtonY = 3;
        private const int ButtonLB = 4;
        private const int ButtonRB = 5;
        private const int ButtonStart = 6;
        private const int ButtonBack = 7;
        private const int ButtonLT = 8;
        private const int ButtonRT = 9;
        private const int ButtonJoystick = 10;
        private const int ButtonJoystick2 = 11;
        private const int ButtonDpad = 12;
        public static IBuffer serverUid;
        public static IBuffer appPath;
        public static string appName;
        public static WriteableBitmap snapshot;
        private Popup popup = new Popup();
        private Popup buttonPopup = new Popup();
        private VideoMediaStreamSource mediaStreamSource;

        private MediaStreamer mediaStreamer;
        
        private int screenWidth;
        private int screenHeight;
        private bool enableGyroscope;
        private double joystickSensitivity;

        //private InterstitialAd interstitialAd;
        //private AdRequest adRequest;
        
        private DispatcherTimer adTimer = new DispatcherTimer();
        private DispatcherTimer closeTimer = new DispatcherTimer();
        private DispatcherTimer editModeTimer = new DispatcherTimer();
        private DispatcherTimer holdTimer = new DispatcherTimer();
        private DispatcherTimer testTimer = new DispatcherTimer();
        private bool mAdAvailable;
        private Motion motion;
        private bool mEditMode;
        private RemotePage.SliderType sliderType;
        private Image sliderButton;
        private Image currentImage;
        private bool currentDpadUp;
        private bool currentDpadDown;
        private bool currentDpadLeft;
        private bool currentDpadRight;
        private IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        private RemotePage.TouchTbl[] touchTbl;
        private bool proVersion;
        private RemotePage.Button[] buttons = new RemotePage.Button[13];
        private static Uri streamUri = new Uri("ms-media-stream-id:MediaStreamer-4567");
        private bool touch0Pressed;
        private bool touch1Pressed;
        private int Orientation;

        //internal Grid LayoutRoot;
        //internal Grid ContentPanel;
        //internal MediaElement myMediaElement;
        //internal Rectangle EditModeRect;
        //internal StackPanel EditModeText;
        //internal Image buttonX;
        //internal Image buttonY;
        //internal Image buttonA;
        //internal Image buttonB;
        //internal Image buttonLB;
        //internal Image buttonRB;
        //internal Image buttonStart;
        //internal Image buttonBack;
        //internal Image buttonLT;
        //internal Image buttonRT;
        //internal Image buttonJoystickBack;
        //internal Image buttonJoystick;
        //internal Image buttonJoystick2Back;
        //internal Image buttonJoystick2;
        //internal Image buttonDpadBack;
        //internal Image buttonDpad;
        //internal StackPanel progressBar;
        //internal StackPanel slider;
        //internal TextBlock sliderText;
        //internal Slider sliderValue;
        //internal StackPanel PressToAdd;
        //internal StackPanel NoMoreButtons;
        //internal TextBox textBox1;
        //private bool _contentLoaded;

        public RemotePage()
        {
            this.InitializeComponent();
            //TiltEffect.TiltableItems.Add(typeof(StackPanel));
            /*
            if (Application.Current.Host.Content.ScaleFactor == 100)
            {
                this.screenWidth = 800;
                this.screenHeight = 480;
            }
            else if (Application.Current.Host.Content.ScaleFactor == 160)
            {
                this.screenWidth = 1280;
                this.screenHeight = 768;
            }
            else
            {
                this.screenWidth = 1280;
                this.screenHeight = 720;
            }
            */

            double dpi;
            //try
            //{
            //    dpi = (double)DeviceExtendedProperties.GetValue("RawDpiX");
            //}
            //catch (Exception ex)
            //{
                dpi = 160.0;
            //}
            //(Application.Current as App).nativeLib.SetScreenSize(this.screenWidth, this.screenHeight, (int)dpi);
            ((FrameworkElement)this.popup).Height = 800.0;
            ((FrameworkElement)this.popup).Width = 480.0;
            this.popup.VerticalOffset = 0.0;
            BackDialog backDialog = new BackDialog();
            this.popup.Child = (UIElement)backDialog;
            this.popup.IsOpen = false;
            ((UIElement)backDialog).RenderTransformOrigin = new Point(0.5, 0.5);
            ((UIElement)backDialog).RenderTransform = (Transform)new CompositeTransform()
            {
                Rotation = 90.0
            };

            //((ButtonBase)backDialog.btnKeyboard).Click += (RoutedEventHandler)((s, args) =>
            //{
            //    this.popup.IsOpen = false;
            //    ((Control)this.textBox1).Focus();
            //});

            //((ButtonBase)backDialog.btnEdit).Click += (RoutedEventHandler)((s, args) =>
            //{
            //    this.mEditMode = true;
            //    ((UIElement)this.EditModeText).Visibility = ((UIElement)this.EditModeRect).Visibility = (Visibility)0;
            //    ((UIElement)this.LayoutRoot).ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.LayoutRoot_ManipulationStarted);
            //    ((UIElement)this.LayoutRoot).ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.LayoutRoot_ManipulationDelta);
            //    ((UIElement)this.LayoutRoot).ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.LayoutRoot_ManipulationCompleted);
            //    ((UIElement)this.myMediaElement).Hold += new EventHandler<GestureEventArgs>(this.myMediaElement_Hold);
            //    ((UIElement)this.myMediaElement).Tap += new EventHandler<GestureEventArgs>(this.myMediaElement_Tap);
            //    ((UIElement)this.PressToAdd).Visibility = (Visibility)0;
            //    this.editModeTimer.Interval = TimeSpan.FromSeconds(4.0);
            //    this.editModeTimer.Tick += (EventHandler)((s2, args2) => ((UIElement)this.PressToAdd).Visibility = (Visibility)1);
            //    this.editModeTimer.Start();
            //    this.popup.IsOpen = false;
            //});

            //((ButtonBase)backDialog.btnDisconnect).Click += (RoutedEventHandler)((s, args) =>
            //{
            //    this.popup.IsOpen = false;
            //    if (this.mAdAvailable)
            //    {
            //        this.settings["lastAd"] = (object)DateTime.Now.ToBinary();
            //        this.settings.Save();
            //        this.interstitialAd.ShowAd();
            //    }
            //    else
            //{
            //        ((Page)this).NavigationService.GoBack();
            //Frame.Navigate.GoBack();
            //}
            //});

            this.myMediaElement.MediaOpened += new RoutedEventHandler(this.myMediaElement_MediaOpened);
            this.myMediaElement.MediaEnded += new RoutedEventHandler(this.myMediaElement_MediaEnded);
            this.myMediaElement.CurrentStateChanged += new RoutedEventHandler(this.myMediaElement_CurrentStateChanged);
            //this.myMediaElement.MediaFailed += new EventHandler<ExceptionRoutedEventArgs>(this.myMediaElement_MediaFailed);
            CNativeLib nativeLib1 = (Application.Current as App).nativeLib;

            //WindowsRuntimeMarshal.AddEventHandler<ConnectedHandler>(
            //    new Func<ConnectedHandler, EventRegistrationToken>(nativeLib1.add_Connected), 
            //    new Action<EventRegistrationToken>(nativeLib1.remove_Connected), 
            //    new ConnectedHandler(this.nativeLib_Connected));

            CNativeLib nativeLib2 = (Application.Current as App).nativeLib;
            //WindowsRuntimeMarshal.AddEventHandler<ErrorHandler>(
            //    new Func<ErrorHandler, EventRegistrationToken>(nativeLib2.add_Error), 
            //    new Action<EventRegistrationToken>(nativeLib2.remove_Error), new ErrorHandler(this.nativeLib_Error));
            this.touchTbl = new RemotePage.TouchTbl[2];
            this.touchTbl[0] = new RemotePage.TouchTbl();
            this.touchTbl[1] = new RemotePage.TouchTbl();
            this.textBox1.Text = "";
            //((UIElement)this.textBox1).KeyDown += new KeyEventHandler(this.textBox1_KeyDown);
            //((UIElement)this.textBox1).KeyUp += new KeyEventHandler(this.textBox1_KeyUp);
            this.textBox1.TextChanged += new TextChangedEventHandler(this.textBox1_TextChanged);

            //this.interstitialAd = new InterstitialAd("ca-app-pub-1676199826219622/5550273195");
            //this.interstitialAd.ReceivedAd += new EventHandler<AdEventArgs>(this.OnAdReceived);
            //this.interstitialAd.FailedToReceiveAd += new EventHandler<AdErrorEventArgs>(this.OnFailedToReceiveAd);
            //this.interstitialAd.DismissingOverlay += new EventHandler<AdEventArgs>(this.OnDismissingOverlay);
            //this.adRequest = new AdRequest();
        }

        
        protected /*virtual*/ void OnBackKeyPress(CancelEventArgs e)
        {
            //base.OnBackKeyPress(e);
            e.Cancel = true;
            if (this.mEditMode)
            {
                if (((UIElement)this.slider).Visibility == null)
                {
                    ((UIElement)this.slider).Visibility = (Visibility)1;
                    e.Cancel = true;
                }
                this.mEditMode = false;
                ((UIElement)this.EditModeText).Visibility = ((UIElement)this.EditModeRect).Visibility = (Visibility)1;
                //((UIElement)this.LayoutRoot).ManipulationStarted -= this.LayoutRoot_ManipulationStarted;//new EventHandler<ManipulationStartedEventArgs>(this.LayoutRoot_ManipulationStarted);
                //((UIElement)this.LayoutRoot).ManipulationDelta -= this.LayoutRoot_ManipulationDelta;//new EventHandler<ManipulationDeltaEventArgs>(this.LayoutRoot_ManipulationDelta);
                //((UIElement)this.LayoutRoot).ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(this.LayoutRoot_ManipulationCompleted);
                //((UIElement)this.myMediaElement).Hold -= new EventHandler<GestureEventArgs>(this.myMediaElement_Hold);
                //((UIElement)this.myMediaElement).Tap -= new EventHandler<GestureEventArgs>(this.myMediaElement_Tap);
            }
            else
                this.popup.IsOpen = !this.popup.IsOpen;
        }

        private void LayoutRoot_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnTestTimerTick(object sender, EventArgs args)
        {
            this.testTimer.Stop();
            this.popup.IsOpen = false;
            
            //TODO
            //((Page)this).NavigationService.GoBack();
            Frame.Navigate(typeof(MainPage));
        }

        private void nativeLib_Connected()
        {
            //((DependencyObject)Deployment.Current).Dispatcher.BeginInvoke(
            //    (Action)(() => (Application.Current as App).nativeLib.SetMouseMode(true)));
        }

        private void nativeLib_Error(int error)
        {
            /*  ((DependencyObject)Deployment.Current).Dispatcher.BeginInvoke((Action)(() =>
            {
              if (error == 1)
              {
                  MessageBox.Show("Connection failed");
                  Debug.WriteLine("Connection failed");
              }

              //((Page)this).NavigationService.GoBack();
            }));*/
            Frame.Navigate(typeof(MainPage));
        }

        private void myMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        { 
            this.myMediaElement.Stop(); 
        }

        private void myMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            this.myMediaElement.Play();
            ((UIElement)this.progressBar).Visibility = (Visibility)1;
        }

        private void myMediaElement_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            //
        }

        private void myMediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            //
        }

        private void myMediaElement_Tap(object sender, GestureEventArgs e)
        {
            ((UIElement)this.slider).Visibility = (Visibility)1;
        }

        private void myMediaElement_Hold(object sender, GestureEventArgs e)
        {
            List<VirtualButton> virtualButtonList = new List<VirtualButton>();
            foreach (RemotePage.Button button in this.buttons)
            {
                Image name = (Image)((FrameworkElement)this).FindName(button.name);
                if (name != null && ((UIElement)name).Visibility != null)
                    virtualButtonList.Add(new VirtualButton(button.name));
            }
            if (virtualButtonList.Count < 1)
            {
                ((UIElement)this.NoMoreButtons).Visibility = (Visibility)0;
                this.editModeTimer.Interval = TimeSpan.FromSeconds(3.0);
                //this.editModeTimer.Tick += (EventHandler)(
                //    (s, args) => ((UIElement)this.NoMoreButtons).Visibility = (Visibility)1);
                this.editModeTimer.Start();
            }
            else
            {
                ((FrameworkElement)this.buttonPopup).Height = 800.0;
                ((FrameworkElement)this.buttonPopup).Width = 480.0;
                this.buttonPopup.VerticalOffset = 0.0;
                ButtonDialog control = new ButtonDialog();
                this.buttonPopup.Child = (UIElement)control;
                this.buttonPopup.IsOpen = true;
                ((UIElement)control).RenderTransformOrigin = new Point(0.5, 0.5);
                ((UIElement)control).RenderTransform = (Transform)new CompositeTransform()
                {
                    Rotation = 90.0
                };
                //((ButtonBase)control.btnDlgCancel).Click += (RoutedEventHandler)((s, args) => this.buttonPopup.IsOpen = false);
                //((ItemsControl)control.buttonPicker).ItemsSource = (IEnumerable)virtualButtonList;
                
                /*((Selector)control.buttonPicker).SelectionChanged += (SelectionChangedEventHandler)((s, args) =>
                {
                    VirtualButton selectedItem = ((Selector)control.buttonPicker).SelectedItem as VirtualButton;
                    Image name = (Image)((FrameworkElement)this).FindName(selectedItem.name);
                    if (name != null)
                    {
                        Thickness margin = ((FrameworkElement)name).Margin;
                        margin.Top = e.GetPosition((UIElement)this.ContentPanel).Y - ((FrameworkElement)name).Height / 2.0;
                        if (margin.Top < 0.0)
                            margin.Top = 0.0;
                        margin.Left = e.GetPosition((UIElement)this.ContentPanel).X - ((FrameworkElement)name).Width / 2.0;
                        if (margin.Left < 0.0)
                            margin.Left = 0.0;
                        ((FrameworkElement)name).Margin = margin;
                        ((UIElement)name).Visibility = (Visibility)0;
                        ((UIElement)this.buttonJoystickBack).Visibility = ((UIElement)this.buttonJoystick).Visibility;
                        ((FrameworkElement)this.buttonJoystickBack).Margin = ((FrameworkElement)this.buttonJoystick).Margin;
                        ((UIElement)this.buttonJoystick2Back).Visibility = ((UIElement)this.buttonJoystick2).Visibility;
                        ((FrameworkElement)this.buttonJoystick2Back).Margin = ((FrameworkElement)this.buttonJoystick2).Margin;
                        ((FrameworkElement)this.buttonDpadBack).Margin = ((FrameworkElement)this.buttonDpad).Margin;
                        ((UIElement)this.buttonDpadBack).Visibility = ((UIElement)this.buttonDpad).Visibility;
                        for (int index = 0; index < ((IEnumerable<RemotePage.Button>)this.buttons).Count<RemotePage.Button>(); ++index)
                        {
                            if (this.buttons[index].name.Equals(selectedItem.name))
                            {
                                this.buttons[index].rect = new Rect(new Point(((FrameworkElement)name).Margin.Left, ((FrameworkElement)name).Margin.Top), new Size(((FrameworkElement)name).Width, ((FrameworkElement)name).Height));
                                this.buttons[index].visible = true;
                                break;
                            }
                        }
                    }
                    this.buttonPopup.IsOpen = false;
                });
                */
            }
        }

        protected /*virtual*/ void OnNavigatedTo(NavigationEventArgs e)
        {
            //((Page)this).OnNavigatedTo(e);
            this.touchTbl[0].id = -1;
            this.touchTbl[1].id = -1;
            ((UIElement)this.progressBar).Visibility = (Visibility)0;
            this.mediaStreamer = MediaStreamerFactory.CreateMediaStreamer(4567);
            this.mediaStreamSource = new VideoMediaStreamSource(this.screenWidth, this.screenHeight);
            //this.mediaStreamer.SetSource((MediaStreamSource)this.mediaStreamSource);
            this.myMediaElement.Source = RemotePage.streamUri;
            this.LoadGameControls();

            //if (this.settings.Contains("proVersion"))
                this.proVersion = true;
            
            if (!(Application.Current as App).nativeLib.Connect(RemotePage.serverUid, RemotePage.appPath,
                this.enableGyroscope))
            {
                //((Page)this).NavigationService.GoBack();
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                this.mAdAvailable = false;

                if (!this.proVersion)
                {
                    DateTime now = DateTime.Now;
                    //if (!this.settings.Contains("lastAd") || now.CompareTo(DateTime.FromBinary((long)this.settings["lastAd"]).AddHours(2.0)) > 0)
                    //{
                    //    this.adTimer.Interval = TimeSpan.FromSeconds(30.0);
                    //    this.adTimer.Tick += new EventHandler(this.OnAdTimerTick);
                    //    this.adTimer.Start();
                    //}
                }

                //Touch.FrameReported += new TouchFrameEventHandler(this.Touch_FrameReported);
                CNativeLib nativeLib = (Application.Current as App).nativeLib;
                //WindowsRuntimeMarshal.AddEventHandler<GameControllerStateHandler>(
                //    new Func<GameControllerStateHandler, EventRegistrationToken>(
                //        nativeLib.add_GameControllerState), 
                //    new Action<EventRegistrationToken>(nativeLib.remove_GameControllerState), 
                //    new GameControllerStateHandler(this.nativeLib_GameControllerState));
                this.StartRotation();
            }
        }

        private void nativeLib_GameControllerState(bool connected)
        {
            //((DependencyObject)Deployment.Current).Dispatcher.BeginInvoke(
            //    (Action)(() => this.LoadGameControls()));
        }

        protected /*virtual*/ void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //((Page)this).OnNavigatingFrom(e);
            this.StopRotation();

            //WindowsRuntimeMarshal.RemoveEventHandler<GameControllerStateHandler>(
            //    new Action<EventRegistrationToken>(
            //        (Application.Current as App).nativeLib.nativeLib.remove_GameControllerState),
            //    new GameControllerStateHandler(this.nativeLib_GameControllerState));
            //nativeLib.GameControllerState -= new GameControllerStateHandler(this.nativeLib_GameControllerState);

            //Touch.FrameReported -= new TouchFrameEventHandler(this.Touch_FrameReported);
            this.adTimer.Stop();
            this.closeTimer.Stop();
            this.testTimer.Stop();
            (Application.Current as App).nativeLib.Disconnect();
            this.myMediaElement.Stop();
            if (this.mediaStreamSource != null)
            {
                this.mediaStreamSource.Shutdown();
                this.mediaStreamSource.Dispose();
                this.mediaStreamSource = (VideoMediaStreamSource)null;
            }
            if (this.mediaStreamer != null)
            {
                this.mediaStreamer.Dispose();
                this.mediaStreamer = (MediaStreamer)null;
            }
            this.SaveGameControls();
        }

        private void SaveGameControls()
        {
            try
            {
                IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
                IsolatedStorageFileStream storageFileStream1 = (IsolatedStorageFileStream)null;
                XDocument xdocument;
                try
                {
                    storageFileStream1 = new IsolatedStorageFileStream("appconfig.xml", FileMode.Open, 
                        storeForApplication);
                    xdocument = XDocument.Load((Stream)storageFileStream1);
                }
                catch (Exception ex)
                {
                    xdocument = new XDocument();
                    xdocument.Add((object)new XElement((XName)"kinoconsole"));
                }
                storageFileStream1?.Dispose();//.Close();
                XElement xelement1 = xdocument.Element((XName)"kinoconsole");
                XElement content1 = new XElement((XName)"app");
                content1.Add((object)new XElement((XName)"name", (object)RemotePage.appName));
                XElement content2 = new XElement((XName)"buttonList");
                foreach (RemotePage.Button button in this.buttons)
                {
                    Image name = (Image)((FrameworkElement)this).FindName(button.name);
                    if (name != null)
                    {
                        XElement content3 = new XElement((XName)"button");
                        content3.SetAttributeValue((XName)"name", (object)button.name);
                        content3.SetAttributeValue((XName)"visible", (object)(((UIElement)name).Visibility == 0));
                        content3.SetAttributeValue((XName)"top", (object)((FrameworkElement)name).Margin.Top);
                        content3.SetAttributeValue((XName)"left", (object)((FrameworkElement)name).Margin.Left);
                        content3.SetAttributeValue((XName)"width", (object)((FrameworkElement)name).Width);
                        content3.SetAttributeValue((XName)"height", (object)((FrameworkElement)name).Height);
                        content3.SetAttributeValue((XName)"opacity", (object)((UIElement)name).Opacity);
                        if (button.name == "buttonJoystick")
                        {
                            content3.SetAttributeValue((XName)"enableGyroscope", (object)this.enableGyroscope);
                            content3.SetAttributeValue((XName)"joystickSensitivity", (object)this.joystickSensitivity);
                        }
                        content2.Add((object)content3);
                    }
                }
                content1.Add((object)content2);
                bool flag = false;
                foreach (XElement element in xelement1.Elements((XName)"app"))
                {
                    XElement xelement2 = element.Element((XName)"name");
                    if (xelement2 != null && xelement2.Value == RemotePage.appName)
                    {
                        element.ReplaceWith((object)content1);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    xelement1.Add((object)content1);

                IsolatedStorageFileStream storageFileStream2 = new IsolatedStorageFileStream(
                    "appconfig.xml", FileMode.Create, storeForApplication);

                xdocument.Save((Stream)storageFileStream2);
                storageFileStream2.Dispose();//.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadGameControls()
        {
            this.buttons[0].name = "buttonA";
            this.buttons[0].keyCode = 57344;
            this.buttons[1].name = "buttonB";
            this.buttons[1].keyCode = 57345;
            this.buttons[2].name = "buttonX";
            this.buttons[2].keyCode = 57346;
            this.buttons[3].name = "buttonY";
            this.buttons[3].keyCode = 57347;
            this.buttons[4].name = "buttonLB";
            this.buttons[4].keyCode = 57348;
            this.buttons[5].name = "buttonRB";
            this.buttons[5].keyCode = 57349;
            this.buttons[6].name = "buttonStart";
            this.buttons[6].keyCode = 57350;
            this.buttons[7].name = "buttonBack";
            this.buttons[7].keyCode = 57351;
            this.buttons[8].name = "buttonLT";
            this.buttons[8].joystickEvent = 13;
            this.buttons[9].name = "buttonRT";
            this.buttons[9].joystickEvent = 14;
            this.buttons[10].name = "buttonJoystick";
            this.buttons[10].joystickEvent = 9;
            this.buttons[11].name = "buttonJoystick2";
            this.buttons[11].joystickEvent = 11;
            this.buttons[12].name = "buttonDpad";
            this.buttons[12].joystickEvent = 57352;
            this.currentDpadUp = this.currentDpadDown = this.currentDpadLeft = this.currentDpadRight = false;
            PlaneProjection projection = (PlaneProjection)((UIElement)this.buttonDpad).Projection;
            projection.RotationX = 0.0;
            projection.RotationY = 0.0;
            ((UIElement)this.buttonDpad).Projection = (Projection)projection;
            for (int index = 0; index < ((IEnumerable<RemotePage.Button>)this.buttons).Count<RemotePage.Button>(); ++index)
            {
                Image name = (Image)((FrameworkElement)this).FindName(this.buttons[index].name);
                if (name != null)
                    ((UIElement)name).Visibility = (Visibility)1;
            }
            this.enableGyroscope = false;
            this.joystickSensitivity = 1.0;
            try
            {
                IsolatedStorageFileStream storageFileStream = new IsolatedStorageFileStream("appconfig.xml", FileMode.Open, IsolatedStorageFile.GetUserStoreForApplication());
                foreach (XElement element in XDocument.Load((Stream)storageFileStream).Element((XName)"kinoconsole").Elements((XName)"app"))
                {
                    XElement xelement1 = element.Element((XName)"name");
                    if (xelement1 != null && RemotePage.appName.Equals(xelement1.Value))
                    {
                        XElement xelement2 = element.Element((XName)"buttonList");
                        if (xelement2 != null)
                        {
                            using (IEnumerator<XElement> enumerator = xelement2.Elements((XName)"button").GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    XElement current = enumerator.Current;
                                    Image name = (Image)((FrameworkElement)this).FindName(current.Attribute((XName)"name").Value);
                                    if (name != null)
                                    {
                                        if (current.Attribute((XName)"visible") != null)
                                            ((UIElement)name).Visibility = (bool)current.Attribute((XName)"visible") ? (Visibility)0 : (Visibility)1;
                                        else
                                            ((UIElement)name).Visibility = (Visibility)1;
                                        Thickness margin = ((FrameworkElement)name).Margin;
                                        if (current.Attribute((XName)"top") != null)
                                            margin.Top = (double)current.Attribute((XName)"top");
                                        if (current.Attribute((XName)"left") != null)
                                            margin.Left = (double)current.Attribute((XName)"left");
                                        ((FrameworkElement)name).Margin = margin;
                                        if (current.Attribute((XName)"width") != null)
                                            ((FrameworkElement)name).Width = (double)current.Attribute((XName)"width");
                                        if (current.Attribute((XName)"height") != null)
                                            ((FrameworkElement)name).Height = (double)current.Attribute((XName)"height");
                                        if (current.Attribute((XName)"opacity") != null)
                                            ((UIElement)name).Opacity = (double)current.Attribute((XName)"opacity");
                                        if (current.Attribute((XName)"enableGyroscope") != null)
                                            this.enableGyroscope = (bool)current.Attribute((XName)"enableGyroscope");
                                        if (current.Attribute((XName)"joystickSensitivity") != null)
                                            this.joystickSensitivity = (double)current.Attribute((XName)"joystickSensitivity");
                                    }
                                }
                                break;
                            }
                        }
                        else
                            break;
                    }
                }
                storageFileStream.Dispose();//.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex]" + ex.Message);
            }

            if ((Application.Current as App).nativeLib.GetGameControllerState())
            {
                foreach (RemotePage.Button button in this.buttons)
                {
                    Image name = (Image)((FrameworkElement)this).FindName(button.name);
                    if (name != null)
                        ((UIElement)name).Visibility = (Visibility)1;
                }
              ((UIElement)this.buttonJoystickBack).Visibility = (Visibility)1;
                ((UIElement)this.buttonJoystick2Back).Visibility = (Visibility)1;
                ((UIElement)this.buttonDpadBack).Visibility = (Visibility)1;
                this.enableGyroscope = false;
            }
            else
            {
                ((FrameworkElement)this.buttonJoystickBack).Margin = ((FrameworkElement)this.buttonJoystick).Margin;
                ((FrameworkElement)this.buttonJoystickBack).Width = ((FrameworkElement)this.buttonJoystick).Width;
                ((FrameworkElement)this.buttonJoystickBack).Height = ((FrameworkElement)this.buttonJoystick).Height;
                ((UIElement)this.buttonJoystickBack).Opacity = ((UIElement)this.buttonJoystick).Opacity / 3.0;
                ((UIElement)this.buttonJoystickBack).Visibility = ((UIElement)this.buttonJoystick).Visibility;
                ((FrameworkElement)this.buttonJoystick2Back).Margin = ((FrameworkElement)this.buttonJoystick2).Margin;
                ((FrameworkElement)this.buttonJoystick2Back).Width = ((FrameworkElement)this.buttonJoystick2).Width;
                ((FrameworkElement)this.buttonJoystick2Back).Height = ((FrameworkElement)this.buttonJoystick2).Height;
                ((UIElement)this.buttonJoystick2Back).Opacity = ((UIElement)this.buttonJoystick2).Opacity / 3.0;
                ((UIElement)this.buttonJoystick2Back).Visibility = ((UIElement)this.buttonJoystick2).Visibility;
                ((FrameworkElement)this.buttonDpadBack).Margin = ((FrameworkElement)this.buttonDpad).Margin;
                ((UIElement)this.buttonDpadBack).Opacity = ((UIElement)this.buttonDpad).Opacity / 3.0;
                ((UIElement)this.buttonDpadBack).Visibility = ((UIElement)this.buttonDpad).Visibility;
                for (int index = 0; index < ((IEnumerable<RemotePage.Button>)this.buttons).Count<RemotePage.Button>(); ++index)
                {
                    Image name = (Image)((FrameworkElement)this).FindName(this.buttons[index].name);
                    if (name != null)
                    {
                        if (((UIElement)name).Visibility == null)
                        {
                            this.buttons[index].rect = new Rect(new Point(((FrameworkElement)name).Margin.Left, ((FrameworkElement)name).Margin.Top), new Size(((FrameworkElement)name).Width, ((FrameworkElement)name).Height));
                            this.buttons[index].visible = true;
                        }
                        this.buttons[index].touchId = -1;
                    }
                }
            }
        }

        private void StartRotation()
        {
            if (!Motion.IsSupported)
                return;
            if (this.motion == null)
            {
                this.motion = new Motion();
                ((SensorBase<MotionReading>)this.motion).TimeBetweenUpdates = TimeSpan.FromMilliseconds(20.0);
                ((SensorBase<MotionReading>)this.motion).CurrentValueChanged += new EventHandler<SensorReadingEventArgs<MotionReading>>(this.motion_CurrentValueChanged);
            }
            try
            {
                ((SensorBase<MotionReading>)this.motion).Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] motionStart error: "+ ex.Message);
            }
        }

        private void StopRotation()
        {
            if (this.motion == null)
                return;
            try
            {
                ((SensorBase<MotionReading>)this.motion).Stop();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] motionStop error: " + ex.Message);
            }
        }

        private void motion_CurrentValueChanged(object sender, SensorReadingEventArgs<MotionReading> e)
        {
            //((DependencyObject)this).Dispatcher.BeginInvoke((Action)(()
            //    => this.CurrentValueChanged(e.SensorReading)));
        }

        private void CurrentValueChanged(MotionReading e)
        {
            if (!((SensorBase<MotionReading>)this.motion).IsDataValid)
                return;
            AttitudeReading attitude1 = ((MotionReading)e).Attitude;
            float x = (float)((double)((AttitudeReading)attitude1).Roll * 180.0 / 3.1400001049041748);
            AttitudeReading attitude2 = ((MotionReading)e).Attitude;
            float y = (float)((double)((AttitudeReading)attitude2).Pitch * 180.0 / 3.1400001049041748);
            AttitudeReading attitude3 = ((MotionReading)e).Attitude;
            float z = (float)((double)((AttitudeReading)attitude3).Yaw * 180.0 / 3.1400001049041748);
            if (this.Orientation == 34)
            {
                x = -x;
                y = -y;
                z = -z;
            }
          (Application.Current as App).nativeLib.SetRotation(x, y, z);
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.textBox1.Text.Length <= 0)
                return;
            char c = this.textBox1.Text.ElementAt<char>(0);
            this.textBox1.Text = "";
            (Application.Current as App).nativeLib.KeyboardEvent(true, (int)c);
            (Application.Current as App).nativeLib.KeyboardEvent(false, (int)c);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            ModifierKeys modifiers = Keyboard.Modifiers;
            Key key = e.Key;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            ModifierKeys modifiers = Keyboard.Modifiers;
            Key key1 = e.Key;
            if (e.Key == (Key)1)
            {
                (Application.Current as App).nativeLib.KeyboardEvent(true, 8);
                (Application.Current as App).nativeLib.KeyboardEvent(false, 8);
            }
            else if (e.Key == (Key)3)
            {
                (Application.Current as App).nativeLib.KeyboardEvent(true, 13);
                (Application.Current as App).nativeLib.KeyboardEvent(false, 13);
                (Application.Current as App).nativeLib.KeyboardEvent(true, 10);
                (Application.Current as App).nativeLib.KeyboardEvent(false, 10);
            }
            else
            {
                Key key2 = e.Key;
            }
        }

        private void UpdateTouchTbl(int id, double x, double y, int down)
        {
            int index;
            if (this.touchTbl[0].id == id)
                index = 0;
            else if (this.touchTbl[1].id == id)
                index = 1;
            else if (this.touchTbl[0].id == -1)
            {
                index = 0;
            }
            else
            {
                if (this.touchTbl[1].id != -1)
                    return;
                index = 1;
            }
            this.touchTbl[index].x = (int)(x * (double)this.screenWidth / ((FrameworkElement)this.ContentPanel).ActualWidth);
            this.touchTbl[index].y = (int)(y * (double)this.screenHeight / ((FrameworkElement)this.ContentPanel).ActualHeight);
            if (this.touchTbl[index].x > this.screenWidth)
                this.touchTbl[index].x = this.screenWidth;
            if (this.touchTbl[index].y > this.screenHeight)
                this.touchTbl[index].y = this.screenHeight;
            this.touchTbl[index].down = down;
            this.touchTbl[index].id = id;
        }

        private void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            if (this.mEditMode || this.popup.IsOpen)
                return;
            TouchPointCollection touchPoints = e.GetTouchPoints((UIElement)this.ContentPanel);
            for (int index1 = 0; index1 < ((IEnumerable<TouchPoint>)touchPoints).Count<TouchPoint>(); ++index1)
            {
                bool flag = false;
                TouchPoint touchPoint = default;// ((PresentationFrameworkCollection<TouchPoint>)touchPoints)[index1];
                if (touchPoint.Action == 1)
                {
                    for (int index2 = 0; index2 < ((IEnumerable<RemotePage.Button>)this.buttons).Count<RemotePage.Button>(); ++index2)
                    {
                        if (this.buttons[index2].visible && this.buttons[index2].rect.Contains(touchPoint.Position))
                        {
                            this.buttons[index2].touchId = touchPoint.TouchDevice.Id;
                            flag = true;
                            if (this.buttons[index2].keyCode != 0)
                                (Application.Current as App).nativeLib.KeyboardEvent(true, this.buttons[index2].keyCode);
                            else if (this.buttons[index2].joystickEvent == 9)
                                this.JoystickTouch(touchPoint.Position);
                            else if (this.buttons[index2].joystickEvent == 11)
                                this.JoystickTouch2(touchPoint.Position);
                            else if (this.buttons[index2].joystickEvent == 57352)
                                this.DpadTouch(touchPoint.Position);
                            else if (this.buttons[index2].joystickEvent != 0)
                                (Application.Current as App).nativeLib.JoystickEvent(this.buttons[index2].joystickEvent, 1f);
                        }
                    }
                }
                else if (touchPoint.Action == 2)
                {
                    for (int index3 = 0; index3 < ((IEnumerable<RemotePage.Button>)this.buttons).Count<RemotePage.Button>(); ++index3)
                    {
                        if (this.buttons[index3].touchId == touchPoint.TouchDevice.Id)
                        {
                            flag = true;
                            if (this.buttons[index3].joystickEvent == 9)
                                this.JoystickTouch(touchPoint.Position);
                            if (this.buttons[index3].joystickEvent == 11)
                                this.JoystickTouch2(touchPoint.Position);
                            if (this.buttons[index3].joystickEvent == 57352)
                                this.DpadTouch(touchPoint.Position);
                        }
                    }
                }
                else if (touchPoint.Action == 3)
                {
                    for (int index4 = 0; index4 < ((IEnumerable<RemotePage.Button>)this.buttons).Count<RemotePage.Button>(); ++index4)
                    {
                        if (this.buttons[index4].touchId == touchPoint.TouchDevice.Id)
                        {
                            this.buttons[index4].touchId = -1;
                            flag = true;
                            if (this.buttons[index4].keyCode != 0)
                                (Application.Current as App).nativeLib.KeyboardEvent(false, this.buttons[index4].keyCode);
                            else if (this.buttons[index4].joystickEvent == 9)
                            {
                                (Application.Current as App).nativeLib.JoystickEvent(9, 0.0f);
                                (Application.Current as App).nativeLib.JoystickEvent(10, 0.0f);
                                ((FrameworkElement)this.buttonJoystick).Margin = ((FrameworkElement)this.buttonJoystickBack).Margin;
                            }
                            else if (this.buttons[index4].joystickEvent == 11)
                            {
                                (Application.Current as App).nativeLib.JoystickEvent(11, 0.0f);
                                (Application.Current as App).nativeLib.JoystickEvent(12, 0.0f);
                                ((FrameworkElement)this.buttonJoystick2).Margin = ((FrameworkElement)this.buttonJoystick2Back).Margin;
                            }
                            else if (this.buttons[index4].joystickEvent == 57352)
                            {
                                if (this.currentDpadUp)
                                    (Application.Current as App).nativeLib.KeyboardEvent(false, 57352);
                                if (this.currentDpadDown)
                                    (Application.Current as App).nativeLib.KeyboardEvent(false, 57353);
                                if (this.currentDpadLeft)
                                    (Application.Current as App).nativeLib.KeyboardEvent(false, 57354);
                                if (this.currentDpadRight)
                                    (Application.Current as App).nativeLib.KeyboardEvent(false, 57355);
                                this.currentDpadUp = this.currentDpadDown = this.currentDpadLeft = this.currentDpadRight = false;
                                PlaneProjection projection = (PlaneProjection)((UIElement)this.buttonDpad).Projection;
                                projection.RotationX = 0.0;
                                projection.RotationY = 0.0;
                                ((UIElement)this.buttonDpad).Projection = (Projection)projection;
                            }
                            else if (this.buttons[index4].joystickEvent != 0)
                                (Application.Current as App).nativeLib.JoystickEvent(this.buttons[index4].joystickEvent, 0.0f);
                        }
                    }
                }
                if (!flag)
                    this.UpdateTouchTbl(touchPoint.TouchDevice.Id, touchPoint.Position.X, touchPoint.Position.Y, touchPoint.Action == 1 || touchPoint.Action == 2 ? 1 : 0);
            }
            if (this.touchTbl[0].down == 1 || this.touchTbl[0].down == 0 && this.touch0Pressed)
                (Application.Current as App).nativeLib.PointerEvent(0, this.touchTbl[0].down == 1, this.touchTbl[0].x, this.touchTbl[0].y);
            this.touch0Pressed = this.touchTbl[0].down == 1;
            if (this.touchTbl[1].down == 1 || this.touchTbl[1].down == 0 && this.touch1Pressed)
                (Application.Current as App).nativeLib.PointerEvent(1, this.touchTbl[1].down == 1, this.touchTbl[1].x, this.touchTbl[1].y);
            this.touch1Pressed = this.touchTbl[1].down == 1;
        }

        private void JoystickTouch(Point point)
        {
            double num1 = point.X - (((FrameworkElement)this.buttonJoystickBack).Margin.Left + ((FrameworkElement)this.buttonJoystickBack).Width / 2.0);
            if (num1 < -((FrameworkElement)this.buttonJoystickBack).Width / 2.0)
                num1 = -((FrameworkElement)this.buttonJoystickBack).Width / 2.0;
            else if (num1 > ((FrameworkElement)this.buttonJoystickBack).Width / 2.0)
                num1 = ((FrameworkElement)this.buttonJoystickBack).Width / 2.0;
            double num2 = point.Y - (((FrameworkElement)this.buttonJoystickBack).Margin.Top + ((FrameworkElement)this.buttonJoystickBack).Height / 2.0);
            if (num2 < -((FrameworkElement)this.buttonJoystickBack).Width / 2.0)
                num2 = -((FrameworkElement)this.buttonJoystickBack).Width / 2.0;
            else if (num2 > ((FrameworkElement)this.buttonJoystickBack).Width / 2.0)
                num2 = ((FrameworkElement)this.buttonJoystickBack).Width / 2.0;
            float data = (float)(num1 / (((FrameworkElement)this.buttonJoystickBack).Width / 2.0) * this.joystickSensitivity);
            if ((double)data < -1.0)
                data = -1f;
            if ((double)data > 1.0)
                data = 1f;
            float num3 = (float)(num2 / (((FrameworkElement)this.buttonJoystickBack).Height / 2.0) * this.joystickSensitivity);
            if ((double)num3 < -1.0)
                num3 = -1f;
            if ((double)num3 > 1.0)
                num3 = 1f;
            (Application.Current as App).nativeLib.JoystickEvent(9, data);
            (Application.Current as App).nativeLib.JoystickEvent(10, -num3);
            Thickness margin = ((FrameworkElement)this.buttonJoystick).Margin;
            margin.Top = ((FrameworkElement)this.buttonJoystickBack).Margin.Top + ((FrameworkElement)this.buttonJoystickBack).Width / 2.0 + num2 - ((FrameworkElement)this.buttonJoystick).Width / 2.0;
            margin.Left = ((FrameworkElement)this.buttonJoystickBack).Margin.Left + ((FrameworkElement)this.buttonJoystickBack).Width / 2.0 + num1 - ((FrameworkElement)this.buttonJoystick).Width / 2.0;
            ((FrameworkElement)this.buttonJoystick).Margin = margin;
        }

        private void JoystickTouch2(Point point)
        {
            double num1 = point.X - (((FrameworkElement)this.buttonJoystick2Back).Margin.Left + ((FrameworkElement)this.buttonJoystick2Back).Width / 2.0);
            if (num1 < -((FrameworkElement)this.buttonJoystick2Back).Width / 2.0)
                num1 = -((FrameworkElement)this.buttonJoystick2Back).Width / 2.0;
            else if (num1 > ((FrameworkElement)this.buttonJoystick2Back).Width / 2.0)
                num1 = ((FrameworkElement)this.buttonJoystick2Back).Width / 2.0;
            double num2 = point.Y - (((FrameworkElement)this.buttonJoystick2Back).Margin.Top + ((FrameworkElement)this.buttonJoystick2Back).Height / 2.0);
            if (num2 < -((FrameworkElement)this.buttonJoystick2Back).Width / 2.0)
                num2 = -((FrameworkElement)this.buttonJoystick2Back).Width / 2.0;
            else if (num2 > ((FrameworkElement)this.buttonJoystick2Back).Width / 2.0)
                num2 = ((FrameworkElement)this.buttonJoystick2Back).Width / 2.0;
            float data = (float)(num1 / (((FrameworkElement)this.buttonJoystick2Back).Width / 2.0) * this.joystickSensitivity);
            if ((double)data < -1.0)
                data = -1f;
            if ((double)data > 1.0)
                data = 1f;
            float num3 = (float)(num2 / (((FrameworkElement)this.buttonJoystick2Back).Height / 2.0) * this.joystickSensitivity);
            if ((double)num3 < -1.0)
                num3 = -1f;
            if ((double)num3 > 1.0)
                num3 = 1f;
            (Application.Current as App).nativeLib.JoystickEvent(11, data);
            (Application.Current as App).nativeLib.JoystickEvent(12, -num3);
            Thickness margin = ((FrameworkElement)this.buttonJoystick2Back).Margin;
            margin.Top += num2;
            margin.Left += num1;
            ((FrameworkElement)this.buttonJoystick2).Margin = margin;
        }

        private void DpadTouch(Point point)
        {
            double num1 = point.X - (((FrameworkElement)this.buttonDpad).Margin.Left + ((FrameworkElement)this.buttonDpad).Width / 2.0);
            double num2 = point.Y - (((FrameworkElement)this.buttonDpad).Margin.Top + ((FrameworkElement)this.buttonDpad).Height / 2.0);
            double num3 = ((FrameworkElement)this.buttonDpad).Width / 6.0;
            bool pressed1 = false;
            bool pressed2 = false;
            bool pressed3 = false;
            bool pressed4 = false;
            double num4 = 0.0;
            double num5 = 0.0;
            if (num1 > num3)
            {
                pressed4 = true;
                num5 = -30.0;
            }
            if (num1 < -num3)
            {
                pressed3 = true;
                num5 = 30.0;
            }
            if (num2 > num3)
            {
                pressed2 = true;
                num4 = 30.0;
            }
            if (num2 < -num3)
            {
                pressed1 = true;
                num4 = -30.0;
            }
            if (pressed1 != this.currentDpadUp)
                (Application.Current as App).nativeLib.KeyboardEvent(pressed1, 57352);
            if (pressed2 != this.currentDpadDown)
                (Application.Current as App).nativeLib.KeyboardEvent(pressed2, 57353);
            if (pressed3 != this.currentDpadLeft)
                (Application.Current as App).nativeLib.KeyboardEvent(pressed3, 57354);
            if (pressed4 != this.currentDpadRight)
                (Application.Current as App).nativeLib.KeyboardEvent(pressed4, 57355);
            this.currentDpadUp = pressed1;
            this.currentDpadDown = pressed2;
            this.currentDpadLeft = pressed3;
            this.currentDpadRight = pressed4;
            PlaneProjection projection = (PlaneProjection)((UIElement)this.buttonDpad).Projection;
            projection.RotationX = num4;
            projection.RotationY = num5;
            ((UIElement)this.buttonDpad).Projection = (Projection)projection;
        }

            

        private void ButtonVisibilityClick(object sender, RoutedEventArgs e)
        {
            Image name = default;//(Image)((FrameworkElement)this).FindName(((FrameworkElement)(sender as MenuItem)).Tag.ToString());
            if (name == null)
                return;
            ((UIElement)name).Visibility = (Visibility)1;
            ((UIElement)this.buttonJoystickBack).Visibility = ((UIElement)this.buttonJoystick).Visibility;
            ((UIElement)this.buttonJoystick2Back).Visibility = ((UIElement)this.buttonJoystick2).Visibility;
            ((UIElement)this.buttonDpadBack).Visibility = ((UIElement)this.buttonDpad).Visibility;
        }

        private void ButtonOpacityClick(object sender, RoutedEventArgs e)
        {
            Image name = default;//(Image)((FrameworkElement)this).FindName(((FrameworkElement)(sender as MenuItem)).Tag.ToString());
            if (name == null)
                return;
            this.sliderType = RemotePage.SliderType.Opacity;
            this.sliderButton = name;
            this.sliderText.Text = "adjust opacity";
            ((RangeBase)this.sliderValue).Value = ((UIElement)name).Opacity;
            ((RangeBase)this.sliderValue).Minimum = 0.3;
            ((RangeBase)this.sliderValue).Maximum = 1.0;
            ((UIElement)this.slider).Visibility = (Visibility)0;
        }

        private void ButtonSizeClick(object sender, RoutedEventArgs e)
        {
            Image name = default;//(Image)((FrameworkElement)this).FindName(((FrameworkElement)(sender as MenuItem)).Tag.ToString());
            if (name == null)
                return;
            this.sliderType = RemotePage.SliderType.Size;
            this.sliderButton = name;
            this.sliderText.Text = "adjust size";
            ((RangeBase)this.sliderValue).Value = ((FrameworkElement)name).Width;
            ((RangeBase)this.sliderValue).Minimum = 10.0;
            ((RangeBase)this.sliderValue).Maximum = 400.0;
            ((UIElement)this.slider).Visibility = (Visibility)0;
        }


        private void joystickSensivity_Click(object sender, RoutedEventArgs e)
        {
            this.sliderType = RemotePage.SliderType.Sensitivity;
            this.sliderButton = this.buttonJoystick;
            this.sliderText.Text = "adjust sensitivity";
            ((RangeBase)this.sliderValue).Value = this.joystickSensitivity;
            ((RangeBase)this.sliderValue).Minimum = 0.5;
            ((RangeBase)this.sliderValue).Maximum = 2.0;
            ((UIElement)this.slider).Visibility = (Visibility)0;
        }

        private void joystickGyroscope_Loaded(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            if (this.enableGyroscope)
                menuItem.Header = (object)"disable gyroscope";
            else
                menuItem.Header = (object)"enable gyroscope";
        }

        private void joystickGyroscope_Click(object sender, RoutedEventArgs e)
        { 
            this.enableGyroscope = !this.enableGyroscope; 
        }

        //private void LayoutRoot_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        //{
        //    if (((RoutedEventArgs)e).OriginalSource == this.buttonA || ((RoutedEventArgs)e).OriginalSource == this.buttonB || ((RoutedEventArgs)e).OriginalSource == this.buttonX || ((RoutedEventArgs)e).OriginalSource == this.buttonY || ((RoutedEventArgs)e).OriginalSource == this.buttonLB || ((RoutedEventArgs)e).OriginalSource == this.buttonRB || ((RoutedEventArgs)e).OriginalSource == this.buttonStart || ((RoutedEventArgs)e).OriginalSource == this.buttonBack || ((RoutedEventArgs)e).OriginalSource == this.buttonLT || ((RoutedEventArgs)e).OriginalSource == this.buttonRT || ((RoutedEventArgs)e).OriginalSource == this.buttonJoystick || ((RoutedEventArgs)e).OriginalSource == this.buttonJoystick2 || ((RoutedEventArgs)e).OriginalSource == this.buttonDpad)
        //        this.currentImage = (Image)((RoutedEventArgs)e).OriginalSource;
        //    else
        //        this.currentImage = (Image)null;
        //}

        private void LayoutRoot_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (this.currentImage == null)
                return;
            Thickness margin = ((FrameworkElement)this.currentImage).Margin;
            //margin.Top += e.DeltaManipulation.Translation.Y;
            if (margin.Top < 0.0)
                margin.Top = 0.0;
            //margin.Left += e.DeltaManipulation.Translation.X;
            ((FrameworkElement)this.currentImage).Margin = margin;
            ((FrameworkElement)this.buttonJoystickBack).Margin = ((FrameworkElement)this.buttonJoystick).Margin;
            ((FrameworkElement)this.buttonJoystick2Back).Margin = ((FrameworkElement)this.buttonJoystick2).Margin;
            ((FrameworkElement)this.buttonDpadBack).Margin = ((FrameworkElement)this.buttonDpad).Margin;
        }

        private void LayoutRoot_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            if (this.currentImage == null)
                return;
            for (int index = 0; index < ((IEnumerable<RemotePage.Button>)this.buttons).Count<RemotePage.Button>(); ++index)
            {
                if (this.buttons[index].name.Equals(((FrameworkElement)this.currentImage).Name))
                {
                    this.buttons[index].rect = new Rect(new Point(((FrameworkElement)this.currentImage).Margin.Left, ((FrameworkElement)this.currentImage).Margin.Top), new Size(((FrameworkElement)this.currentImage).Width, ((FrameworkElement)this.currentImage).Height));
                    this.buttons[index].visible = true;
                    break;
                }
            }
        }

        /*
        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/KinoConsole;component/RemotePage.xaml", UriKind.Relative));
            this.LayoutRoot = (Grid)((FrameworkElement)this).FindName("LayoutRoot");
            this.ContentPanel = (Grid)((FrameworkElement)this).FindName("ContentPanel");
            this.myMediaElement = (MediaElement)((FrameworkElement)this).FindName("myMediaElement");
            this.EditModeRect = (Rectangle)((FrameworkElement)this).FindName("EditModeRect");
            this.EditModeText = (StackPanel)((FrameworkElement)this).FindName("EditModeText");
            this.buttonX = (Image)((FrameworkElement)this).FindName("buttonX");
            this.buttonY = (Image)((FrameworkElement)this).FindName("buttonY");
            this.buttonA = (Image)((FrameworkElement)this).FindName("buttonA");
            this.buttonB = (Image)((FrameworkElement)this).FindName("buttonB");
            this.buttonLB = (Image)((FrameworkElement)this).FindName("buttonLB");
            this.buttonRB = (Image)((FrameworkElement)this).FindName("buttonRB");
            this.buttonStart = (Image)((FrameworkElement)this).FindName("buttonStart");
            this.buttonBack = (Image)((FrameworkElement)this).FindName("buttonBack");
            this.buttonLT = (Image)((FrameworkElement)this).FindName("buttonLT");
            this.buttonRT = (Image)((FrameworkElement)this).FindName("buttonRT");
            this.buttonJoystickBack = (Image)((FrameworkElement)this).FindName("buttonJoystickBack");
            this.buttonJoystick = (Image)((FrameworkElement)this).FindName("buttonJoystick");
            this.buttonJoystick2Back = (Image)((FrameworkElement)this).FindName("buttonJoystick2Back");
            this.buttonJoystick2 = (Image)((FrameworkElement)this).FindName("buttonJoystick2");
            this.buttonDpadBack = (Image)((FrameworkElement)this).FindName("buttonDpadBack");
            this.buttonDpad = (Image)((FrameworkElement)this).FindName("buttonDpad");
            this.progressBar = (StackPanel)((FrameworkElement)this).FindName("progressBar");
            this.slider = (StackPanel)((FrameworkElement)this).FindName("slider");
            this.sliderText = (TextBlock)((FrameworkElement)this).FindName("sliderText");
            this.sliderValue = (Slider)((FrameworkElement)this).FindName("sliderValue");
            this.PressToAdd = (StackPanel)((FrameworkElement)this).FindName("PressToAdd");
            this.NoMoreButtons = (StackPanel)((FrameworkElement)this).FindName("NoMoreButtons");
            this.textBox1 = (TextBox)((FrameworkElement)this).FindName("textBox1");
        }
        */

        private enum SliderType
        {
            None,
            Size,
            Opacity,
            Sensitivity,
        }

        private class TouchTbl
        {
            public int id;
            public int x;
            public int y;
            public int down;

            public TouchTbl()
            {
                this.id = -1;
                this.x = 0;
                this.y = 0;
                this.down = 0;
            }
        }

        private struct Button
        {
            public string name;
            public Rect rect;
            public int touchId;
            public bool visible;
            public int keyCode;
            public int joystickEvent;
        }

        private void button_Hold(object sender, DragEventArgs e)
        {
            if (this.mEditMode)
                return;
            e.Handled = true;
        }

        private void sliderValue_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            double newValue = e.NewValue;
            if (this.sliderButton == null || this.sliderType == RemotePage.SliderType.None)
                return;
            if (this.sliderType == RemotePage.SliderType.Opacity)
            {
                ((UIElement)this.sliderButton).Opacity = newValue;
                ((UIElement)this.buttonJoystickBack).Opacity = ((UIElement)this.buttonJoystick).Opacity / 3.0;
                ((UIElement)this.buttonJoystick2Back).Opacity = ((UIElement)this.buttonJoystick2).Opacity / 3.0;
                ((UIElement)this.buttonDpadBack).Opacity = ((UIElement)this.buttonDpad).Opacity / 3.0;
            }
            else if (this.sliderType == RemotePage.SliderType.Size)
            {
                ((FrameworkElement)this.sliderButton).Height = ((FrameworkElement)this.sliderButton).Width = newValue;
                ((FrameworkElement)this.buttonJoystickBack).Width = ((FrameworkElement)this.buttonJoystickBack).Height = ((FrameworkElement)this.buttonJoystick).Width;
                ((FrameworkElement)this.buttonJoystick2Back).Width = ((FrameworkElement)this.buttonJoystick2Back).Height = ((FrameworkElement)this.buttonJoystick2).Width;
                ((FrameworkElement)this.buttonDpadBack).Width = ((FrameworkElement)this.buttonDpadBack).Height = ((FrameworkElement)this.buttonDpad).Width;
            }
            else
            {
                if (this.sliderType != RemotePage.SliderType.Sensitivity)
                    return;
                this.joystickSensitivity = newValue;
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace KinoConsole
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class RemotePage : Page
    {
        public RemotePage()
        {
            this.InitializeComponent();
        }
    }
}
*/
