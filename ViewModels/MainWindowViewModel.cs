namespace SCM.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "SCM - Simple Character Manager";

    public string? SelectedSheet { get; set; }

    public string[] Sheets { get; set; } = new string[3] {"Ivar", "Confidence", "Aurelia"};
}
