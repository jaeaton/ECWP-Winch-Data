using Avalonia;
using ViewModels;
using System;

namespace ECWP_Winch_Data_Program.Desktop;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    //original Void Main
    //public static void Main(string[] args) => BuildAvaloniaApp()
    //    .StartWithClassicDesktopLifetime(args);

    //Updated Void Main to include Exception Logger
    public static void Main(string[] args)
    {
        ExceptionLogger.RegisterUnhandledExceptionLogging(); // Register the logger

        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();



}
