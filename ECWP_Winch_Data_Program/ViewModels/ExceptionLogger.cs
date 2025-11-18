using System;
using System.IO;
using System.Threading.Tasks;

namespace ViewModels
{
    public static class ExceptionLogger
    {
        public static void RegisterUnhandledExceptionLogging()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                LogExceptionToFile(args.ExceptionObject as Exception, "AppDomain Unhandled Exception");
            };

            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                LogExceptionToFile(args.Exception, "Unobserved Task Exception");
                args.SetObserved(); // Mark the exception as observed to prevent application termination
            };
        }

        private static void LogExceptionToFile(Exception ex, string context)
        {
            // Define the log file path (e.g., in the user's desktop or application data folder)
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ECWPDataCrashLog.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true)) // Append to the file
                {
                    writer.WriteLine($"[{DateTime.Now}] {context}");
                    writer.WriteLine($"Message: {ex.Message}");
                    writer.WriteLine($"StackTrace: {ex.StackTrace}");
                    if (ex.InnerException != null)
                    {
                        writer.WriteLine($"Inner Exception Message: {ex.InnerException.Message}");
                        writer.WriteLine($"Inner Exception StackTrace: {ex.InnerException.StackTrace}");
                    }
                    writer.WriteLine(new string('-', 50)); // Separator
                }
            }
            catch (Exception logEx)
            {
                // Handle potential errors during logging itself (e.g., file access issues)
                Console.WriteLine($"Error writing to log file: {logEx.Message}");
            }
        }
    }
}
