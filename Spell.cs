/// <summary>
/// The possible cast times for a spell
/// </summary>
public enum CastTime {
    Action,
    BonusAction,
    Ritual,
    Reaction
}

/// <summary>
/// Class <c>Spell</c> represents a spell a character knows
/// </summary>
public abstract class Spell {
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public int Level { get; set; }
    public int Components {get; set; }
    public CastTime CastingTime { get; set; }
    public Distance Range { get; set; } = Distance.Parse("0u");

    public bool IsTouch {
        get {
            return Range.Units == 1;
        }
    }

    public bool IsSelf {
        get {
            return Range.Units == 0;
        }
    }

    public List<string> Materials { get; set; } = new List<string>();
}

/// <summary>
/// Class <c>ShapedSpell</c> represents spells which are of a certain shape
/// </summary>
public class ShapedSpell : Spell {
    public string Shape { get; set; } = string.Empty;
    public Distance Size { get; set; } = Distance.Parse("0u");
}
