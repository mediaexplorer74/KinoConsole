using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KinoConsole.Services
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error,
        Critical
    }

    public class ErrorHandlingService
    {
        private static ErrorHandlingService _instance;
        private static readonly object _lock = new object();
        private const string LOG_FILE_NAME = "KinoConsoleErrors.txt";
        private const int MAX_LOG_ENTRIES = 1000;
        private readonly Queue<string> _logEntries = new Queue<string>();
        private bool _isInitialized = false;
        private StorageFile _logFile;

        public static ErrorHandlingService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ErrorHandlingService();
                        }
                    }
                }
                return _instance;
            }
        }

        private ErrorHandlingService() { }

        public async Task InitializeAsync()
        {
            if (_isInitialized) return;

            try
            {
                // Get the Pictures library
                StorageFolder picturesFolder = KnownFolders.PicturesLibrary;
                _logFile = await picturesFolder.CreateFileAsync(LOG_FILE_NAME, CreationCollisionOption.OpenIfExists);
                await LoadExistingLogsAsync();
                _isInitialized = true;
                
                await LogAsync("ErrorHandlingService initialized", LogLevel.Info);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to initialize ErrorHandlingService: {ex}");
                // Continue anyway, we'll try to log to debug output
            }
        }

        private async Task LoadExistingLogsAsync()
        {
            try
            {
                var logText = await FileIO.ReadTextAsync(_logFile);
                var lines = logText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                
                // Keep only the most recent entries
                int startIndex = Math.Max(0, lines.Length - MAX_LOG_ENTRIES);
                for (int i = startIndex; i < lines.Length; i++)
                {
                    _logEntries.Enqueue(lines[i]);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load existing logs: {ex}");
            }
        }

        public async Task LogAsync(string message, LogLevel level = LogLevel.Info, Exception ex = null)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string logLevelStr = level.ToString().ToUpper();
                string logMessage = $"[{timestamp}] [{logLevelStr}] {message}";
                
                if (ex != null)
                {
                    logMessage += $"{Environment.NewLine}Exception: {ex}{Environment.NewLine}Stack Trace: {ex.StackTrace}{Environment.NewLine}";
                }

                // Add to in-memory queue
                _logEntries.Enqueue(logMessage);
                
                // Keep only the most recent entries
                while (_logEntries.Count > MAX_LOG_ENTRIES)
                {
                    _logEntries.Dequeue();
                }

                // Write to debug output
                Debug.WriteLine(logMessage);

                // Write to file if initialized
                if (_isInitialized && _logFile != null)
                {
                    try
                    {
                        await FileIO.AppendTextAsync(_logFile, logMessage + Environment.NewLine);
                    }
                    catch (Exception fileEx)
                    {
                        Debug.WriteLine($"Failed to write to log file: {fileEx}");
                    }
                }
            }
            catch (Exception logEx)
            {
                Debug.WriteLine($"Critical error in LogAsync: {logEx}");
            }
        }

        public async Task ShowErrorDialogAsync(
            string title, 
            string message, 
            Exception ex = null, 
            bool isFatal = false)
        {
            try
            {
                // Log the error first
                await LogAsync($"Error Dialog: {message}", 
                    isFatal ? LogLevel.Critical : LogLevel.Error, 
                    ex);

                // Create dialog
                var dialog = new ContentDialog
                {
                    Title = title,
                    PrimaryButtonText = isFatal ? "Exit Application" : "OK",
                    DefaultButton = ContentDialogButton.Primary
                };

                // Create a scrollable text block for the error details
                var scrollViewer = new ScrollViewer
                {
                    VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                    MaxHeight = 300
                };

                var stackPanel = new StackPanel { Spacing = 10 };

                // Add the main message
                stackPanel.Children.Add(new TextBlock 
                { 
                    Text = message,
                    TextWrapping = TextWrapping.WrapWholeWords
                });

                // Add exception details if available
                if (ex != null)
                {
                    stackPanel.Children.Add(new TextBlock 
                    { 
                        Text = "Error Details:",
                        FontWeight = Windows.UI.Text.FontWeights.Bold,
                        Margin = new Thickness(0, 10, 0, 0)
                    });

                    stackPanel.Children.Add(new TextBlock 
                    { 
                        Text = $"Message: {ex.Message}",
                        TextWrapping = TextWrapping.WrapWholeWords
                    });

                    if (!string.IsNullOrEmpty(ex.StackTrace))
                    {
                        stackPanel.Children.Add(new TextBlock 
                        { 
                            Text = "Stack Trace:",
                            FontWeight = Windows.UI.Text.FontWeights.Bold,
                            Margin = new Thickness(0, 10, 0, 0)
                        });

                        stackPanel.Children.Add(new TextBlock 
                        { 
                            Text = ex.StackTrace,
                            FontFamily = new Windows.UI.Xaml.Media.FontFamily("Consolas"),
                            TextWrapping = TextWrapping.Wrap
                        });
                    }
                }

                // Add log file location
                if (_isInitialized && _logFile != null)
                {
                    stackPanel.Children.Add(new TextBlock 
                    { 
                        Text = $"\nLog file: {_logFile.Path}",
                        FontStyle = Windows.UI.Text.FontStyle.Italic,
                        Margin = new Thickness(0, 15, 0, 0)
                    });
                }

                scrollViewer.Content = stackPanel;
                dialog.Content = scrollViewer;

                // Show the dialog
                await dialog.ShowAsync();

                // If this is a fatal error, exit the application
                if (isFatal)
                {
                    Application.Current.Exit();
                }
            }
            catch (Exception dialogEx)
            {
                // If showing the dialog fails, at least write to debug output
                Debug.WriteLine($"Failed to show error dialog: {dialogEx}");
                Debug.WriteLine($"Original error: {message}");
                if (ex != null)
                {
                    Debug.WriteLine($"Exception: {ex}");
                }
            }
        }

        public string GetLogFilePath()
        {
            return _logFile?.Path ?? "Log file not available";
        }

        public async Task<string> GetLogContentAsync()
        {
            try
            {
                if (_logFile != null)
                {
                    return await FileIO.ReadTextAsync(_logFile);
                }
            }
            catch (Exception ex)
            {
                await LogAsync("Failed to read log file", LogLevel.Error, ex);
            }
            return "Log content not available";
        }
    }
}
