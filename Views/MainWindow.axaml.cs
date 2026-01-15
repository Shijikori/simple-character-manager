using Avalonia.Controls;
using Avalonia.Interactivity;

namespace SCM.Views;

public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();
    }

    public void ButtonHandler(object sender, RoutedEventArgs args) {
        Button e = (Button)sender;
        Console.WriteLine($"{e.Content} has been pressed!");
        if (e.Name == "LoadCS") {
            if (this.DataContext is SCM.ViewModels.MainWindowViewModel viewModel) {
                Console.WriteLine($"{viewModel.SelectedSheet} is being loaded");
            }
        }
    }
}
