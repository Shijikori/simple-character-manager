// See https://aka.ms/new-console-template for more information

class Distance {
    public int Value = 0;
    public float ConversionFactorFT = 5.0F;
    public float ConversionFactorM = 1.5F;
    
    public Distance() {
        Value = 0;
        ConversionFactorFT = 5.0F;
        ConversionFactorM = 1.5F;
    }

    public Distance(int value) {
        Value = value;
    }

    public static Distance Parse(string distance) {
        Distance tdist = new Distance();
        if (distance.IndexOf("ft") > 0) {
            tdist.SetFT((float)Int32.Parse(distance.Substring(0, distance.IndexOf("ft"))));
        }
        else if (distance.IndexOf("m") > 0) {
            tdist.SetM((float)Int32.Parse(distance.Substring(0, distance.IndexOf("m"))));
        }
        else if (distance.IndexOf("u") > 0) {
            tdist.Value = Int32.Parse(distance.Substring(0, distance.IndexOf("u")));
        }
        return tdist;
    }

    public void SetFT(float feetDist) {
        Value = (int)(feetDist / ConversionFactorFT);
    }

    public void SetM(float meterDist) {
        Value = (int)(meterDist / ConversionFactorM);
    }

    public float GetFT() {
        return (float)Value * ConversionFactorFT;
    }

    public float GetM() {
        return (float)Value * ConversionFactorM;
    }

    public string Serialize() {
        return $"{Value}u";
    }
}

class Shape {
    public string Type = "Sphere";
    public Distance Size = new Distance();
    
    public Shape(string type, string sizeStr) {
        Distance tSize = new Distance();
        if (sizeStr.IndexOf("ft") > 0) {
            tSize = Distance.Parse(sizeStr.Substring(0, sizeStr.IndexOf("ft")));
        }
        else if (sizeStr.IndexOf("m") > 0) {
            tSize.SetM((float)Int32.Parse(sizeStr.Substring(0, sizeStr.IndexOf("m"))));
        }
        Size = tSize;
        Type = type;
    }
}

class Character {
    public int STR = 0;
    public int DEX = 0;
    public int CON = 0;
    public int INT = 0;
    public int WIS = 0;
    public int CHA = 0;
    public Distance SPD = new Distance(0);
    public int LVL = 1;
    public float ConversionFactorFT = 5.0F;
    public float ConversionFactorM = 1.5F;
    public string Name = "being";
    public string ClassName = "fighter";

    public int ArmorClass {
        get {
            return 10 + GetMod(DEX);
        }
    }
    public int ProficiencyBonus {
        get {
            return (int)Math.Ceiling((float)LVL / 4.0F) + 1;
        }
    }

    public int GetMod(int ability) {
        return (int)Math.Floor( ((float)ability - 10.0F) / 2.0F);
    }
}


// Name, Level, Type, CastingTime, Range, Self (bool), Components (1=V,2=S,4=M), Materials[], Duration, Description
class Spell {
    public string Name = "Fireball";
    public int Level = 1;
    public string CastingTime = "Action";
    public Distance Range = new Distance(0);
    private bool _self = false;
    public bool Self {
        get {
            return _self;
        }
    }
    public int Components = 7;
    public List<string> Materials = new List<string>();
    public string Duration = "Instant";
    public string Description = "A fireball, does 1d8 damage in a Line plus 1d8 per slot level above spell level.";
}

class SCManager {
    static void Main(string[] args) {
        Console.WriteLine("Hello, World!");
        Distance td = Distance.Parse("30ft");
        Console.WriteLine($"{td.Value}u|{td.GetM()}m|{td.GetFT()}ft");
    }
}

