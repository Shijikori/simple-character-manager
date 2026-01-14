
public class Feature {
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public string Source { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsAbility { get; set; }
    public Dictionary<string,string> Properties = new Dictionary<string,string>();
}

public class CharacterClass {
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public string PrimaryAbility { get; set; } = string.Empty;
    public List<Feature> Features = new List<Feature>();
}

public struct Abilities {
    public int STR;
    public int DEX;
    public int CON;
    public int INT;
    public int WIS;
    public int CHA;

    public int this[string abilityName] {
        get {
            switch (abilityName.ToUpper()) {
                case "STR": return STR;
                case "DEX": return DEX;
                case "CON": return CON;
                case "INT": return INT;
                case "WIS": return WIS;
                case "CHA": return CHA;
                default:
                    throw new ArgumentException("Invalid ability name: " + abilityName);
            }
        }
        set {
            switch (abilityName.ToUpper()) {
                case "STR": STR = value; break;
                case "DEX": DEX = value; break;
                case "CON": CON = value; break;
                case "INT": INT = value; break;
                case "WIS": WIS = value; break;
                case "CHA": CHA = value; break;
                default:
                    throw new ArgumentException("Invalid ability name: " + abilityName);
            }
        }
    }

    public int GetMod(int score) {
        return (int)Math.Floor( ((float)score - 10.0f) / 2.0f );
    }
}

public class CharacterSheet {
    public string PlayerName = "";
    public string Name = "";
    public CharacterClass CharClass = new CharacterClass();
    public Wallet CoinPurse = new Wallet();
    public Abilities AbilityScores = new Abilities();
    public List<Item> Items = new List<Item>();
    public List<Feature> Feats = new List<Feature>();
    public List<Feature> Traits = new List<Feature>();
    public List<Spell> Spells = new List<Spell>();
    public SpellSlot[] SpellSlots = new SpellSlot[9];

    public int ProficiencyBonus {
        get {
            return (int)Math.Ceiling((float)CharClass.Level / 4.0f) + 1;
        }
    }

    public float MaxCarryWeight {
        get {
            return (float)AbilityScores.GetMod(AbilityScores["STR"]) * 15.0f;
        }
    }

    public void AssignScores(int nSTR, int nDEX, int nCON, int nINT, int nWIS, int nCHA) {
        AbilityScores.STR = nSTR;
        AbilityScores.DEX = nDEX;
        AbilityScores.CON = nCON;
        AbilityScores.INT = nINT;
        AbilityScores.WIS = nWIS;
        AbilityScores.CHA = nCHA;
    }
}
