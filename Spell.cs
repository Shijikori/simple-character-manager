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
public class Spell {
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public int Level {get; set; }
    public string Source { get; set; } = string.Empty;
    public List<char> Components { get; set; } = new List<char>();
    public CastTime CastingTime { get; set; }
    public Distance Range { get; set; } = new Distance { Units = 0 };
    public string Shape { get; set; } = string.Empty;
    public Distance Size { get; set; } = new Distance { Units = 0 };
    public Dictionary<string, string> Scaling { get; set; } = new Dictionary<string, string>();
    public List<string> Materials { get; set; } = new List<string>();

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

    public bool IsShaped {
        get {
            return Shape != string.Empty;
        }
    }
}

/// <summary>
/// Class <c>SpellSlot</c> represents a spell slot data.
/// </summary>
public class SpellSlot {
    public int Count { get; private set; }
    public int Total { get; private set; }

    /// <summary>
    /// Method <c>Use</c> uses a spell slot and reduces the Count accordingly
    /// </summary>
    /// <returns>
    /// A boolean <c>True</c> if a spell slot was expended. <c>False</c> will be returned otherwise.
    /// </returns>
    public bool Use() {
        if (Count < 0) {
            return false;
        }
        Count--;
        return true;
    }

    /// <summary>
    /// Method <c>Rest</c> resets the spell slots to full
    /// </summary>
    public void Rest() {
        Count = Total;
    }

    /// <summary>
    /// Method <c>IncreaseTotal</c> increases the total number of slots
    /// </summary>
    /// <param name="num">Integer being the number of spell slots to add to the total</param>
    public void IncreaseTotal(int num) {
        Total += num;
    }
}
