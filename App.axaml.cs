using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SCM.Views;
using SCM.ViewModels;

namespace SCM;

public partial class App : Application {
    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow {
                DataContext = new MainWindowViewModel(),
            };
        }
        base.OnFrameworkInitializationCompleted();
    }

    /*private void DisableAvaloniaDataAnnotationValidation() {
     *            var dataValidationPluginsToRemove = BindingPlugins.DataValidators.OfTyp<DataAnnotationsValidationPlugin>().ToArray();
     *
     *            foreach (var plugin in dataValidationPluginsToRemove) {
     *                BindingPlugins.DataValidators.Remove(plugin);
                    }
    }*/
}

