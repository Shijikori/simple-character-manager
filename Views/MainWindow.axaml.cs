using Avalonia.Controls;
using Avalonia.Interactivity;

namespace SCM.Views;

public partial class MainWindow : Window {

    public MasterSpellbookWindow? SpellbookWindow { get; set; }

    public MainWindow() {
        InitializeComponent();
    }

    public void ButtonHandler(object sender, RoutedEventArgs args) {
        Button e = (Button)sender;
        switch (e.Name) {
            case "MasterSpellbook":
                OpenMasterSpellbook();
                break;
            default:
                break;
        }
    }

    private void OpenMasterSpellbook() {
        if (SpellbookWindow != null && SpellbookWindow.IsClosed != true) {
            SpellbookWindow.Activate();
            return;
        }
        SpellbookWindow = new MasterSpellbookWindow { DataContext = new SCM.ViewModels.MasterSpellbookViewModel() };
        SpellbookWindow.Show();
    }
}
