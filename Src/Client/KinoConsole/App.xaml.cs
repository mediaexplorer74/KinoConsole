// Type: KinoConsole.App
// Assembly: KinoConsole, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Media.Animation;
using Windows.Storage;
using Windows.ApplicationModel.Resources;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using KinoConsole.Services;
using Windows.UI.Popups;
using System.Threading;
using NativeLib;
using KinoConsole.Resources;

namespace KinoConsole
{
    public delegate void UnhandledExceptionDelegate(object sender, Exception ex);
    public partial class App : Application
    {
        public static event UnhandledExceptionDelegate UnhandledExceptionOccurred;
        private bool _isInitialized = false;
        public Popup splashPopup = new Popup();
        public CNativeLib nativeLib;
        // private string FlurryKey = "BTH69V8X5HNPW8BM7K9P"; // removed unused field
        private bool phoneApplicationInitialized;
  
        public static Frame RootFrame { get; private set; }

        public App()
        {
            // Initialize error handling first
            InitializeErrorHandling();
            
            this.InitializeComponent();
            //this.InitializePhoneApplication();
            //this.InitializeLanguage();
            SplashScreenControl splashScreenControl = new SplashScreenControl();
            //((FrameworkElement)splashScreenControl).Height = 400;//Application.Current.Host.Content.ActualHeight;
            
            this.splashPopup = new Popup();
            this.splashPopup.Child = (UIElement)splashScreenControl;
            this.splashPopup.IsOpen = true;

            try
            {
                // Plan A: try to create/load native lib...
                this.nativeLib = new CNativeLib();
            }
            catch (Exception ex)
            {
                // Plan B 
                Debug.WriteLine("[ex] CNativeLib class creation critical error:  " + ex.Message);
               
                ErrorHandlingService.Instance.LogAsync("Unhandled AppDomain exception", LogLevel.Critical, ex);
                ErrorHandlingService.Instance.ShowErrorDialogAsync(  "Critical Error",
                "An unexpected error occurred. The application may become unstable.",
                ex,
                true);

                // Plan B is Exit the application
                //App.Current.Exit();
            }           
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

        // *On App launching*
        //Api.StartSession(this.FlurryKey);
        CNativeLib.Start(/*IsolatedStorageSettings.ApplicationSettings.Contains("proVersion")*/true);
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

            //this.nativeLib.Start(true);
            CNativeLib.Start(/*IsolatedStorageSettings.ApplicationSettings.Contains("proVersion")*/true);
        }

        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            //Api.StartSession(this.FlurryKey);

            //this.nativeLib.Start(true);
            CNativeLib.Start(/*IsolatedStorageSettings.ApplicationSettings.Contains("proVersion")*/true);
        }

        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        { 
            CNativeLib.Stop();
        }

        private void Application_Closing(object sender, ClosingEventArgs e)
        { 
            CNativeLib.Stop(); 
        }

        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (!Debugger.IsAttached)
                return;
            Debugger.Break();
        }

        private async void InitializeErrorHandling()
        {
            if (_isInitialized) return;
            
            // Initialize the error handling service
            await ErrorHandlingService.Instance.InitializeAsync();

            // Handle unhandled exceptions
            Application.Current.UnhandledException += async (s, e) =>
            {
                if (e.Exception is Exception ex)
                {
                    await ErrorHandlingService.Instance.LogAsync("Unhandled AppDomain exception", LogLevel.Critical, ex);
                    await ErrorHandlingService.Instance.ShowErrorDialogAsync(
                        "Critical Error",
                        "An unexpected error occurred. The application may become unstable.",
                        ex,
                        true);
                }
            };

            // Handle unobserved task exceptions
            TaskScheduler.UnobservedTaskException += async (s, e) =>
            {
                e.SetObserved();
                await ErrorHandlingService.Instance.LogAsync("Unobserved task exception", LogLevel.Error, e.Exception);
                await ErrorHandlingService.Instance.ShowErrorDialogAsync(
                    "Task Error", 
                    "An error occurred in a background task.", 
                    e.Exception);
            };

            // Handle UI thread exceptions
            this.UnhandledException += async (s, e) =>
            {
                e.Handled = true; // Prevent app from crashing
                await ErrorHandlingService.Instance.LogAsync("Unhandled UI exception", LogLevel.Error, e.Exception);
                UnhandledExceptionOccurred?.Invoke(this, e.Exception);
                
                // Show error dialog
                await ErrorHandlingService.Instance.ShowErrorDialogAsync(
                    "Application Error", 
                    "An unexpected error occurred. Please try again.", 
                    e.Exception);
            };

            _isInitialized = true;
        }

        /*private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // This will be handled by our global handler above
            this.UnhandledException += async (s, e) =>
            {
                e.Handled = true; // Prevent app from crashing
                                  // ...
            };
        }*/

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

