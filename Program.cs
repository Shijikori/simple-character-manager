using static System.Diagnostics.Debug;

/// <summary>
/// Class <c>Distance</c> represents the data of any range or distance.
/// </summary>
public class Distance {
    /// <value>Property <c>Units</c> represents the value stored in units</value>
    public int Units = 0;
    /// <value>Property <c>ConversionFactorFT</c> represents the conversion factor used for imperial units</value>
    public float ConversionFactorFT = 5.0F;
    /// <value>Property <c>ConversionFactorM</c> represents the conversion factor used for metric units</value>
    public float ConversionFactorM = 1.5F;
    
    /// <summary>
    /// Method <c>Parse</c> parses a distance string
    /// </summary>
    /// <param name="distance">the distance string to parse</param>
    /// <param name="convFactorFT">the conversion factor for imperial units</param>
    /// <param name="convFactorM">the conversion factor for metric units</param>
    /// <returns>
    /// A new distance object from the parsed string
    /// </returns>
    public static Distance Parse(string distance, float convFactorFT=5.0F, float convFactorM=1.5F) {
        Distance tdist = new Distance();
        tdist.ConversionFactorFT = convFactorFT;
        tdist.ConversionFactorM = convFactorM;
        if (distance.IndexOf("ft") > 0) {
            tdist.SetFT((float)Int32.Parse(distance.Substring(0, distance.IndexOf("ft"))));
        }
        else if (distance.IndexOf("m") > 0) {
            tdist.SetM((float)Int32.Parse(distance.Substring(0, distance.IndexOf("m"))));
        }
        else if (distance.IndexOf("u") > 0) {
            tdist.Units = Int32.Parse(distance.Substring(0, distance.IndexOf("u")));
        }
        return tdist;
    }

    /// <summary>
    /// Method <c>SetFT</c> sets a new value with imperial units as input
    /// </summary>
    /// <param name="feetDist">new value in imperial units</param>
    public void SetFT(float feetDist) {
        Units = (int)(feetDist / ConversionFactorFT);
    }

    /// <summary>
    /// Method <c>SetM</c> sets a new value with metric units as input
    /// </summary>
    /// <param name="meterDist">new value in metric units</param>
    public void SetM(float meterDist) {
        Units = (int)(meterDist / ConversionFactorM);
    }

    /// <summary>
    /// Method <c>GetFT</c> gets the value in imperial unit
    /// </summary>
    /// <returns>
    /// A floating point number of imperial units
    /// </returns>
    public float GetFT() {
        return (float)Units * ConversionFactorFT;
    }

    /// <summary>
    /// Method <c>GetM</c> gets the value in metric unit
    /// </summary>
    /// <returns>
    /// A floating point number of metric units
    /// </returns>
    public float GetM() {
        return (float)Units * ConversionFactorM;
    }

    /// <summary>
    /// Method <c>Serialize</c> serialises the data in the form of a string
    /// </summary>
    /// <returns>
    /// A parse-able string representing the value of the object in units
    /// </returns>
    public string Serialize() {
        return $"{Units}u";
    }
}

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

class SCManager {
    private List<Spell> MasterSpellBook = new List<Spell>();
    private List<CharacterClass> ClassRegistry = new List<CharacterClass>();

    static void Main(string[] args) {
        CharacterClass tcc = new CharacterClass {
            Name = "rogue",
            Level = 1,
            PrimaryAbility = "DEX"
        };
        CharacterSheet tcs = new CharacterSheet {
            PlayerName = "test",
            Name = "Ivar",
            AbilityScores = new Abilities {
                STR = 10,
                DEX = 15,
                CON = 10,
                INT = 10,
                WIS = 10,
                CHA = 10
            },
            Items = new List<Item>{
                new Item {
                    Name = "Stick",
                    Category = ItemCategory.Generic,
                    Weight = 0.05f,
                    AttunementRequired = false
                }
            }
        };

        Item ti = new Item {
            Name = "Super Stick",
            Description = "A super strong magical stick!",
            Category = ItemCategory.Weapon,
            Value = new Wallet(0, 10, 0, 0, 0),
            Weight = 0.5f,
            AttunementRequired = false,
            Properties = new Dictionary<string, string> {
                {"DamageDice", "3d8"},
                {"DamageType", "Magical"},
                {"Attuned", "true"}
            },
            Attributes = new List<Dictionary<string, string>> {
                new Dictionary<string, string> {
                    {"AbilityName", "Mega Bonk"},
                    {"AbilityDescription", "Enlargees the stick for a single attack with boosted blunt damage"},
                    {"AbilityDice", "1d6"},
                    {"AbilityUses", "1pd"},
                    {"AbilityUsed", "0"}
                }
            }
        };

        Console.WriteLine(tcc.Name);
        Console.WriteLine(tcs.AbilityScores[tcc.PrimaryAbility]);
        Console.WriteLine(tcs.Items[0].Name);

        Console.WriteLine(ti.Name);
        Console.WriteLine(ti.Description);
        Console.WriteLine(ti.Category);
        Console.WriteLine(ti.Properties["DamageDice"]);
        Console.WriteLine(ti.Attributes[0]["AbilityName"]);
        Console.WriteLine(ti.Attributes[0]["AbilityDescription"]);
        Console.WriteLine(ti.Attributes[0]["AbilityDice"]);
    }
}
