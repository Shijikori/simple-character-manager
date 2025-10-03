// See https://aka.ms/new-console-template for more information

using static System.Diagnostics.Debug;

/*
 * Distance object helps store data about any kind of measurement. It converts inputs in feet or meters to units. It can also be provided a unit measurement for parsing
 * ConversionFactors allow for adjusting the conversion factor if different conversions are used by the user. It defaults to community-agreed conversions for D&D.
 */
class Distance {
    public int Units = 0;
    public float ConversionFactorFT = 5.0F;
    public float ConversionFactorM = 1.5F;
    
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

    public void SetFT(float feetDist) {
        Units = (int)(feetDist / ConversionFactorFT);
    }

    public void SetM(float meterDist) {
        Units = (int)(meterDist / ConversionFactorM);
    }

    public float GetFT() {
        return (float)Units * ConversionFactorFT;
    }

    public float GetM() {
        return (float)Units * ConversionFactorM;
    }

    public string Serialize() {
        return $"{Units}u";
    }
}

class Shape {
    public string Type = "Sphere";
    public Distance Size = new Distance();
    
    public Shape(string type, string sizeStr) {
        Distance tSize = Distance.Parse(sizeStr);
        
        Size = tSize;
        Type = type;
    }

    public static Shape Parse(string str) {
        Assert(str.Length > 9); //Assertion extravaganza... '~'
        Assert(str.IndexOf("S") == 0);
        Assert(str.IndexOf("(") != -1 && str.IndexOf(")") != -1);
        Assert(str.IndexOf(",") != -1);

        //idk why it doesn't like "string params" but okay...
        string paramstr = str.Substring(str.IndexOf("(") + 1, str.IndexOf(")") - (str.IndexOf("(") + 1));
        string[] sparams = paramstr.Split(",");
        Assert(sparams.Length == 2);
        string ssize = "1u";
        string stype = "";
        foreach (string par in sparams) {
            if (par.StartsWith("t")) {
                stype = par.Substring(par.IndexOf(":")+1);
            }
            else if (par.StartsWith("s")) {
                ssize = par.Substring(par.IndexOf(":")+1);
            }
        }
        return new Shape(stype, ssize);
    }

    public string Serialize() {
        return $"S(t:{Type},s:{Size.Serialize()})";
    }
}

/*
 * Preserving this bit of code because I know it was annoying to get at and will be useful
public int ProficiencyBonus {
    get {
        return (int)Math.Ceiling((float)LVL / 4.0F) + 1;
    }
}
*/

// Name, Level, Type, CastingTime, Range, Self (bool), Components (1=V,2=S,4=M), Materials[], Duration, Description
class Spell {
    public int Level = 1;
    public int Components = 7;
    private bool _self = false;
    public string CastingTime = "Action";
    public string Name = "Fireball";
    public Distance Range = Distance.Parse("0u");
    
    public bool IsTouch {
        get {
            return !_self;
        }
    }

    public List<string> Materials = new List<string>();
    public string Duration = "Instant";
    public string Description = "A fireball, does 1d8 damage in a Line plus 1d8 per slot level above spell level.";
    public string ReferencePage = "p356";
}

// interface of items to allow for making different kinds of items accessible with the same interface
interface IItem {
    string Name { get; set; }
    string Description { get; set; }
    bool IsSpellBook { get; }
}

class SpellBook : IItem {
    private string _name = "";
    private string _description = "";

    public string Name {
        get {
            return _name;
        }
        set {
            _name = value;
        }
    }
    public string Description {
        get {
            return _description;
        }
        set {
            _description = value;
        }
    }
    public bool IsSpellBook {
        get {
            return true;
        }
    }

    public List<Spell> Spells = new List<Spell>();
}

class Item : IItem {
    private string _name = "";
    private string _description = "";

    public string Name {
        get {
            return _name;
        }
        set {
            _name = value;
        }
    }
    public string Value = "";
    public string Description {
        get {
            return _description;
        }
        set {
            _description = value;
        }
    }
    public bool IsEquipment = false;
    public bool IsEquiped = false;
    public bool IsSpellBook {
        get {
            return false;
        }
    }
    public string Bonus = ""; // would be "AC=5+DEX", "AC+5" or "MOD+1"
    public string Custom = ""; //specify any additional custom rules in that field such as additionnal damage
}

class CharacterClass {
    public string Name = "";
    public int Level = 1;
    public int PrimaryAbility = 0;
}

struct Abilities {
    public int STR;
    public int DEX;
    public int CON;
    public int INT;
    public int WIS;
    public int CHA;
}

class CharacterSheet {
    public string PlayerName = "";
    public string Name = "";
    public Abilities Abilities = new Abilities();
    public List<Item> Items = new List<Item>();

    public void AssignScores(int nSTR, int nDEX, int nCON, int nINT, int nWIS, int nCHA) {
        Abilities.STR = nSTR;
        Abilities.DEX = nDEX;
        Abilities.CON = nCON;
        Abilities.INT = nINT;
        Abilities.WIS = nWIS;
        Abilities.CHA = nCHA;
    }

}

class SCManager {
    private List<Spell> MasterSpellBook = new List<Spell>();
    private List<CharacterClass> ClassRegistry = new List<CharacterClass>();

    public int GetMod(int score) {
         return (int)Math.Floor( ((float)score - 10.0F) / 2.0F);
    }

    static void Main(string[] args) {
        Console.WriteLine("yellow huie");
    }
}
