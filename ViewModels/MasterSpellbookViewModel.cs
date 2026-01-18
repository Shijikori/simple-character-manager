using System.Linq;

namespace SCM.ViewModels;

public partial class MasterSpellbookViewModel : ViewModelBase {
    public string[] CastTimes {get; private set; } = Enum.GetNames<CastTime>();
    public string SelectedCastTime { get; set; } = nameof(CastTime.Action);
}
