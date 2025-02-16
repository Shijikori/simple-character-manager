// See https://aka.ms/new-console-template for more information

class Distance {
    public int Value = 0;
    public float ConversionFactorFT = 5.0F;
    public float ConversionFactorM = 1.5F;
    
    public Distance(int val) {
        Value = val;
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
}

class Stat {
    public string Name = "Strength";
    public string SName = "STR";
    public int Value = 8;
    public int Modifier {
        get {
            int mod = 10;
            int[] steps = {30, 28, 26, 24, 22, 20, 18, 16, 14, 12, 10, 8, 6, 4};
            if (Value == 1) return -5;
            for (int i = 0; i < 14; i++) {
                if (!(Value < steps[i])) {
                    break;
                }
                mod = mod - 1;
            }
            return mod;
        }
    }

    public Stat(string name, string sname, int val) {
        Name = name;
        SName = sname;
        Value = val;
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
}


class Spell {
    public int DiceNum = 1;
    public int DiceSize = 4;
    public int AltDiceSize = 0;
    public int PHBReferencePage = 1;
    public int SpellLevel = 0;
    public string Description = "a spell";
    public string DamageType = "[any,of,list]";
    public string SpellType = "Evocation";

    public bool IsCantrip() {
        return SpellLevel == 0;
    }
}

class SCManager {
    static void Main(string[] args) {
        Console.WriteLine("Hello, World!");
        Stat test = new Stat("Charisma", "CHA", 15);
        Console.WriteLine(test.Modifier);
        Distance td = new Distance(0);
        td.SetFT(30);
        Console.WriteLine($"{td.Value}u|{td.GetM()}m|{td.GetFT()}ft");
    }
}

