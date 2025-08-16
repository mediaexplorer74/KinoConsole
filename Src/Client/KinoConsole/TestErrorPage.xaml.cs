using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using KinoConsole.Services;

namespace KinoConsole
{
    public sealed partial class TestErrorPage : Page
    {
        public TestErrorPage()
        {
            this.InitializeComponent();
        }

        private void TestUnhandledException_Click(object sender, RoutedEventArgs e)
        {
            // This will trigger our global exception handler
            throw new InvalidOperationException("This is a test unhandled exception from the UI thread.");
        }

        private async void TestAsyncException_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // This will trigger the unobserved task exception handler
                var task = Task.Run(() =>
                {
                    throw new InvalidOperationException("This is a test exception from a background task.");
                });

                // Let the task complete but don't await it
                await Task.Delay(100);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                await ErrorHandlingService.Instance.LogAsync("Error in TestAsyncException", LogLevel.Error, ex);
                await ErrorHandlingService.Instance.ShowErrorDialogAsync(
                    "Test Error", 
                    "An error occurred in the async test.", 
                    ex);
            }
        }

        private async void ViewLogFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string logContent = await ErrorHandlingService.Instance.GetLogContentAsync();
                LogContent.Text = logContent;
                
                // Optionally open the log file in default text editor
                var logFile = await KnownFolders.PicturesLibrary.GetFileAsync("KinoConsoleErrors.txt");
                if (logFile != null)
                {
                    await Launcher.LaunchFileAsync(logFile);
                }
            }
            catch (Exception ex)
            {
                LogContent.Text = $"Error accessing log file: {ex.Message}";
            }
        }
    }
}
