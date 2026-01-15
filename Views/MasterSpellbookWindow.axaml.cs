using Avalonia.Controls;
using Avalonia.Interactivity;

namespace SCM.Views;

public partial class MasterSpellbookWindow : Window {

    public bool IsClosed { get; private set; } = false;

    public MasterSpellbookWindow() {
        InitializeComponent();
    }

    protected override void OnClosed(EventArgs e) {
        base.OnClosed(e);
        IsClosed = true;
    }
}
