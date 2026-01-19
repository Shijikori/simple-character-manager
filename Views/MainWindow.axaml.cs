using Avalonia.Controls;
using Avalonia.Interactivity;

namespace SCM.Views;

public partial class MainWindow : Window {

    public SpellEditorWindow? SpellbookWindow { get; set; }

    public ClassRegistryWindow? RegistryWindow { get; set; }

    public MainWindow() {
        InitializeComponent();
    }

    public void ButtonHandler(object sender, RoutedEventArgs args) {
        Button e = (Button)sender;
        switch (e.Name) {
            case "MasterSpellbook":
                OpenMasterSpellbook();
                break;
            case "ClassRegistry":
                OpenClassRegistry();
                break;
            default:
                break;
        }
    }

    private void OpenClassRegistry() {
        if (RegistryWindow != null && RegistryWindow.IsClosed != true) {
            RegistryWindow.Activate();
            return;
        }
        RegistryWindow = new ClassRegistryWindow { DataContext = new SCM.ViewModels.ClassRegistryViewModel() };
        RegistryWindow.Show();
    }

    private void OpenMasterSpellbook() {
        if (SpellbookWindow != null && SpellbookWindow.IsClosed != true) {
            SpellbookWindow.Activate();
            return;
        }
        //SpellbookWindow = new MasterSpellbookWindow { DataContext = new SCM.ViewModels.MasterSpellbook.ViewModel() };
        SpellbookWindow = new SpellEditorWindow { DataContext = new SCM.ViewModels.SpellEditorViewModel() };
        SpellbookWindow.Show();
    }

    protected override void OnClosed(EventArgs e) {
        if (SpellbookWindow != null && SpellbookWindow.IsClosed == false) SpellbookWindow.Close();
        if (RegistryWindow != null && RegistryWindow.IsClosed == false) RegistryWindow.Close();
        base.OnClosed(e);
    }
}
