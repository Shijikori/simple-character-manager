namespace SCM.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "SCM - Simple Character Manager";

    public string? SelectedSheet { get; set; }

    public string[] Sheets { get; set; } = new string[] {"Ivar", "Confidence", "Aurelia", "Twig", "Erika", "Charles", "Gregory", "Steve", "Ruby"};
}
