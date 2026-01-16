using Avalonia.Controls;
using Avalonia.Interactivity;

namespace SCM.Views;

public partial class ClassRegistryWindow : Window {

    public bool IsClosed { get; private set; } = false;

    public ClassRegistryWindow() {
        InitializeComponent();
    }

    protected override void OnClosed(EventArgs e) {
        base.OnClosed(e);
        IsClosed = true;
    }
}
