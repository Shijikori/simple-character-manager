using Avalonia;
using System;

namespace SCM;

sealed class Manager {
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
                                                .UsePlatformDetect()
                                                .LogToTrace();
}
