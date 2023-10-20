using FlurryWP8SDK;
using FlurryWP8SDK.Models;
using KinoConsole.Resources;
using NativeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Controls.Primitives;
//using System.Windows.Markup;
//using System.Windows.Navigation;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;

namespace KinoConsole
{
    public class App : Application
    {
        public Popup splashPopup = new Popup();
        public CNativeLib nativeLib;
        private string FlurryKey = "BTH69V8X5HNPW8BM7K9P";
        private bool phoneApplicationInitialized;
        private bool _contentLoaded;

        public static PhoneApplicationFrame RootFrame { get; private set; }

        public App()
        {
            this.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(this.Application_UnhandledException);
            this.InitializeComponent();
            this.InitializePhoneApplication();
            this.InitializeLanguage();
            SplashScreenControl splashScreenControl = new SplashScreenControl();
            ((FrameworkElement)splashScreenControl).Height = Application.Current.Host.Content.ActualHeight;
            this.splashPopup = new Popup();
            this.splashPopup.Child = (UIElement)splashScreenControl;
            this.splashPopup.IsOpen = true;
            this.nativeLib = new CNativeLib();
            CNativeLib nativeLib1 = this.nativeLib;
            WindowsRuntimeMarshal.AddEventHandler<FlurryEventHandler>(new Func<FlurryEventHandler, EventRegistrationToken>(nativeLib1.add_FlurryEvent), new Action<EventRegistrationToken>(nativeLib1.remove_FlurryEvent), new FlurryEventHandler(this.nativeLib_FlurryEvent));
            CNativeLib nativeLib2 = this.nativeLib;
            WindowsRuntimeMarshal.AddEventHandler<FlurryEventWithParamHandler>(new Func<FlurryEventWithParamHandler, EventRegistrationToken>(nativeLib2.add_FlurryEventWithParam), new Action<EventRegistrationToken>(nativeLib2.remove_FlurryEventWithParam), new FlurryEventWithParamHandler(this.nativeLib_FlurryEventWithParam));
            CNativeLib nativeLib3 = this.nativeLib;
            WindowsRuntimeMarshal.AddEventHandler<FlurryErrorHandler>(new Func<FlurryErrorHandler, EventRegistrationToken>(nativeLib3.add_FlurryError), new Action<EventRegistrationToken>(nativeLib3.remove_FlurryError), new FlurryErrorHandler(this.nativeLib_FlurryError));
            if (!Debugger.IsAttached)
                return;
            Application.Current.Host.Settings.EnableFrameRateCounter = false;
            PhoneApplicationService.Current.UserIdleDetectionMode = (IdleDetectionMode)1;
        }

        private void nativeLib_FlurryEvent(string eventName) => Api.LogEvent(eventName);

        private void nativeLib_FlurryEventWithParam(string eventName, string param, string value) => Api.LogEvent(eventName, new List<Parameter>()
    {
      new Parameter(param, value)
    });

        private void nativeLib_FlurryError(string text) => Api.LogError(text, new Exception());

        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            Api.StartSession(this.FlurryKey);
            this.nativeLib.Start(IsolatedStorageSettings.ApplicationSettings.Contains("proVersion"));
        }

        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            Api.StartSession(this.FlurryKey);
            this.nativeLib.Start(IsolatedStorageSettings.ApplicationSettings.Contains("proVersion"));
        }

        private void Application_Deactivated(object sender, DeactivatedEventArgs e) => this.nativeLib.Stop();

        private void Application_Closing(object sender, ClosingEventArgs e) => this.nativeLib.Stop();

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
            App.RootFrame = new PhoneApplicationFrame();
            ((Frame)App.RootFrame).Navigated += new NavigatedEventHandler(this.CompleteInitializePhoneApplication);
            ((Frame)App.RootFrame).NavigationFailed += new NavigationFailedEventHandler(this.RootFrame_NavigationFailed);
            ((Frame)App.RootFrame).Navigated += new NavigatedEventHandler(this.CheckForResetNavigation);
            this.phoneApplicationInitialized = true;
        }

        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            if (this.RootVisual != App.RootFrame)
                this.RootVisual = (UIElement)App.RootFrame;
            ((Frame)App.RootFrame).Navigated -= new NavigatedEventHandler(this.CompleteInitializePhoneApplication);
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            if (e.NavigationMode != 4)
                return;
            ((Frame)App.RootFrame).Navigated += new NavigatedEventHandler(this.ClearBackStackAfterReset);
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            ((Frame)App.RootFrame).Navigated -= new NavigatedEventHandler(this.ClearBackStackAfterReset);
            if (e.NavigationMode != null && e.NavigationMode != 3)
                return;
            do
                ;
            while (App.RootFrame.RemoveBackEntry() != null);
        }

        private void InitializeLanguage()
        {
            try
            {
                ((FrameworkElement)App.RootFrame).Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);
                ((FrameworkElement)App.RootFrame).FlowDirection = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
            }
            catch
            {
                if (Debugger.IsAttached)
                    Debugger.Break();
                throw;
            }
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/KinoConsole;component/App.xaml", UriKind.Relative));
        }
    }
}

/*
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

namespace KinoConsole
{
    /// <summary>
    /// Обеспечивает зависящее от конкретного приложения поведение, дополняющее класс Application по умолчанию.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Инициализирует одноэлементный объект приложения. Это первая выполняемая строка разрабатываемого
        /// кода, поэтому она является логическим эквивалентом main() или WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Вызывается при обычном запуске приложения пользователем. Будут использоваться другие точки входа,
        /// например, если приложение запускается для открытия конкретного файла.
        /// </summary>
        /// <param name="e">Сведения о запросе и обработке запуска.</param>
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
        }

        /// <summary>
        /// Вызывается в случае сбоя навигации на определенную страницу
        /// </summary>
        /// <param name="sender">Фрейм, для которого произошел сбой навигации</param>
        /// <param name="e">Сведения о сбое навигации</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Вызывается при приостановке выполнения приложения.  Состояние приложения сохраняется
        /// без учета информации о том, будет ли оно завершено или возобновлено с неизменным
        /// содержимым памяти.
        /// </summary>
        /// <param name="sender">Источник запроса приостановки.</param>
        /// <param name="e">Сведения о запросе приостановки.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Сохранить состояние приложения и остановить все фоновые операции
            deferral.Complete();
        }
    }
}
*/
