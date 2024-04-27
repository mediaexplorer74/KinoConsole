// Type: KinoConsole.App
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using KinoConsole.Resources;
using NativeLib;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Controls.Primitives;
//using System.Windows.Markup;
//using System.Windows.Navigation;


namespace KinoConsole
{
    public partial class App : Application
    {
        public Popup splashPopup = new Popup();
        public CNativeLib nativeLib;
        private string FlurryKey = "BTH69V8X5HNPW8BM7K9P";
        private bool phoneApplicationInitialized;
  
        public static Frame RootFrame { get; private set; }

        public App()
        {
            //this.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(
            //    this.Application_UnhandledException);
            this.InitializeComponent();
            //this.InitializePhoneApplication();
            //this.InitializeLanguage();
            SplashScreenControl splashScreenControl = new SplashScreenControl();
            //((FrameworkElement)splashScreenControl).Height = 400;//Application.Current.Host.Content.ActualHeight;
            
            this.splashPopup = new Popup();
            this.splashPopup.Child = (UIElement)splashScreenControl;
            this.splashPopup.IsOpen = true;
            this.nativeLib = new CNativeLib();
            
            CNativeLib nativeLib1 = this.nativeLib;
            //WindowsRuntimeMarshal.AddEventHandler<FlurryEventHandler>(
            //new Func<FlurryEventHandler, EventRegistrationToken>(nativeLib1.add_FlurryEvent),
            //new Action<EventRegistrationToken>(nativeLib1.remove_FlurryEvent),
            //new FlurryEventHandler(this.nativeLib_FlurryEvent));
            CNativeLib nativeLib2 = this.nativeLib;
            //WindowsRuntimeMarshal.AddEventHandler<FlurryEventWithParamHandler>(
            //new Func<FlurryEventWithParamHandler, EventRegistrationToken>(nativeLib2.add_FlurryEventWithParam),
            //new Action<EventRegistrationToken>(nativeLib2.remove_FlurryEventWithParam),
            //new FlurryEventWithParamHandler(this.nativeLib_FlurryEventWithParam));
            CNativeLib nativeLib3 = this.nativeLib;
            //WindowsRuntimeMarshal.AddEventHandler<FlurryErrorHandler>(
            //new Func<FlurryErrorHandler, EventRegistrationToken>(nativeLib3.add_FlurryError),
            //new Action<EventRegistrationToken>(nativeLib3.remove_FlurryError), new FlurryErrorHandler(this.nativeLib_FlurryError));
          
            //if (!Debugger.IsAttached)
            //    return;

            //Application.Current.Host.Settings.EnableFrameRateCounter = false;
            //PhoneApplicationService.Current.UserIdleDetectionMode = (IdleDetectionMode)1;
       }


      
      protected override void OnLaunched(LaunchActivatedEventArgs e)
      {
        Frame rootFrame = Window.Current.Content as Frame;

        // Не повторяйте инициализацию приложения, если в окне уже имеется содержимое,
        // только обеспечьте активность окна
        if (rootFrame == null)
        {
        // Создание фрейма, который станет контекстом навигации, и переход к первой странице
        rootFrame = new Frame();

        rootFrame.NavigationFailed += OnNavigationFailed;

        if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
        {
            //TODO: Загрузить состояние из ранее приостановленного приложения
        }

        // Размещение фрейма в текущем окне
        Window.Current.Content = rootFrame;
        }

        if (e.PrelaunchActivated == false)
        {
        if (rootFrame.Content == null)
        {
            // Если стек навигации не восстанавливается для перехода к первой странице,
            // настройка новой страницы путем передачи необходимой информации в качестве параметра
            // навигации
            rootFrame.Navigate(typeof(MainPage), e.Arguments);
        }
        // Обеспечение активности текущего окна
        Window.Current.Activate();
        }
    }//OnLaunched

        
    void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
    {
        throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
    }

        
    private void OnSuspending(object sender, SuspendingEventArgs e)
    {
        var deferral = e.SuspendingOperation.GetDeferral();
            
        // ...

        deferral.Complete();
    }
    

    private void nativeLib_FlurryEvent(string eventName)
        {
            Debug.WriteLine("[i] " + eventName); 
        }

        private void nativeLib_FlurryEventWithParam(string eventName, string param, string value)
        {
            Debug.WriteLine
            (
              "[i] " + eventName, new List<Parameter>()
                {
                  new Parameter(param, value)
                }
             );
        }

        private void nativeLib_FlurryError(string text) 
        { 
          Debug.WriteLine("[ex] text: " + text);
        }

        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            //Api.StartSession(this.FlurryKey);
            this.nativeLib.Start(/*IsolatedStorageSettings.ApplicationSettings.Contains("proVersion")*/default);
        }

        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            //Api.StartSession(this.FlurryKey);
            this.nativeLib.Start(/*IsolatedStorageSettings.ApplicationSettings.Contains("proVersion")*/default);
        }

        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        { 
            this.nativeLib.Stop();
        }

        private void Application_Closing(object sender, ClosingEventArgs e)
        { 
            this.nativeLib.Stop(); 
        }

        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (!Debugger.IsAttached)
                return;
            Debugger.Break();
        }

        private void Application_UnhandledException(
          object sender,
          ApplicationUnhandledExceptionEventArgs e)
        {
            if (!Debugger.IsAttached)
                return;
            Debugger.Break();
        }

        private void InitializePhoneApplication()
        {
            if (this.phoneApplicationInitialized)
                return;
            App.RootFrame = new Frame();

            ((Frame)App.RootFrame).Navigated += new NavigatedEventHandler(this.CompleteInitializePhoneApplication);
            ((Frame)App.RootFrame).NavigationFailed += new NavigationFailedEventHandler(this.RootFrame_NavigationFailed);
            ((Frame)App.RootFrame).Navigated += new NavigatedEventHandler(this.CheckForResetNavigation);
            this.phoneApplicationInitialized = true;
        }

        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            //if (this.RootVisual != App.RootFrame)
            //    this.RootVisual = (UIElement)App.RootFrame;
            ((Frame)App.RootFrame).Navigated -= new NavigatedEventHandler(this.CompleteInitializePhoneApplication);
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            //if (e.NavigationMode != 4)
            //    return;
            ((Frame)App.RootFrame).Navigated += new NavigatedEventHandler(this.ClearBackStackAfterReset);
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            ((Frame)App.RootFrame).Navigated -= new NavigatedEventHandler(this.ClearBackStackAfterReset);
            if (e.NavigationMode != null/* && e.NavigationMode != 3*/)
                return;
            //do
            //{; }
            //while (App.RootFrame.RemoveBackEntry() != null);
        }

        private void InitializeLanguage()
        {
            try
            {
                ((FrameworkElement)App.RootFrame).Language = default;//XmlLanguage.GetLanguage(AppResources.ResourceLanguage);
                ((FrameworkElement)App.RootFrame).FlowDirection = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
            }
            catch
            {
                if (Debugger.IsAttached)
                    Debugger.Break();
                throw;
            }
        }

     
    }
}

