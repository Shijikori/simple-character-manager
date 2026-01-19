using System.Linq;

namespace SCM.ViewModels;

public partial class SpellEditorViewModel : ViewModelBase {
    public string[] CastTimes { get; private set; } = Enum.GetNames<CastTime>();
    public string SelectedCastTime { get; set; } = nameof(CastTime.Action);
    public string Title { get; set; } = "Spell Editor | New Spell";
    public bool Verbal { get; set; } = false;
    public bool Somatic { get; set; } = false;
    public bool Material { get; set; } = false;
    public string Materials { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public string SpellName {get; set; } = string.Empty;
    public string Range { get; set; } = string.Empty;
    public string Shape { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
}
