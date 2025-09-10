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

class CharacterOld {
    private int[] _abilityScores = new int[6]{ 0, 0, 0, 0, 0, 0}; 
    public int LVL = 1;
    
    public int STR {
        get {
            return _abilityScores[0];
        }
        set {
            Assert(value >= 0);
            _abilityScores[0] = value;
        }
    }
    
    public int DEX {
        get {
            return _abilityScores[1];
        }
        set {
            Assert(value >= 0);
            _abilityScores[1] = value;
        }
    }
    
    public int CON {
        get {
            return _abilityScores[2];
        }
        set {
            Assert(value >= 0);
            _abilityScores[2] = value;
        }
    }
    
    public int INT {
        get {
            return _abilityScores[3];
        }
        set {
            Assert(value >= 0);
            _abilityScores[3] = value;
        }
    }
    
    public int WIS {
        get {
            return _abilityScores[4];
        }
        set {
            Assert(value >= 0);
            _abilityScores[4] = value;
        }
    }
    
    public int CHA {
        get {
            return _abilityScores[5];
        }
        set {
            Assert(value >= 0);
            _abilityScores[5] = value;
        }
    }

    public Distance SPD = Distance.Parse("0u");
    public string Name = "being";
    public string ClassName = "fighter";

    public int ArmorClass {
        get {
            return 10 + GetMod(this.DEX);
        }
    }
    
    public int ProficiencyBonus {
        get {
            return (int)Math.Ceiling((float)LVL / 4.0F) + 1;
        }
    }

    public float ConversionFactorM {
        get {
            return SPD.ConversionFactorM;
        }
        set {
            SPD.ConversionFactorM = value;
        }
    }

    public float ConversionFactorFT {
        get {
            return SPD.ConversionFactorFT;
        }
        set {
            SPD.ConversionFactorFT = value;
        }
    }

    public int GetMod(int ability) {
        return (int)Math.Floor( ((float)ability - 10.0F) / 2.0F);
    }
}

class Ability {
    public string Name = "";
    public string Description = "";
    public bool Usable = false;
    public int Uses = 0; // uses according to condition
    public string Condition = ""; // restore uses
    public int MaxUses = 0;

    public void Rest() {
        Uses = 0;
    }
}

class CharClass {
    public string Name = "commoner";
    public int Level = 1;
    public List<Ability> Abilities = new List<Ability>();
    // spell slots... i think it should be some kind of dictionary but i need to research that solution further
}

class Item {
    public string Name = "";
    public int Value = 0;
    public string Description = "";
    public bool IsEquipment = false;
    public bool IsEquiped = false;
    public string Bonus = ""; // would be "AC=5+DEX", "AC+5" or "MOD+1"
    public string Custom = ""; //specify any additional custom rules in that field such as additionnal damage
}

class Character {
    public string Name = "";
    public string Player = "";
    public int HitPoints = 8;
    public Distance Speed = Distance.Parse("6u");
    public List<CharClass> Classes = new List<CharClass>();
    public List<Item> Items = new List<Item>();
    public int Wealth = 0;
}


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

class SCManager {
    private List<Spell> MasterSpellBook = new List<Spell>();
    private List<CharClass> ClassRegistry = new List<CharClass>();
    static void Main(string[] args) {
        Console.WriteLine("yellow huie");
    }
}
