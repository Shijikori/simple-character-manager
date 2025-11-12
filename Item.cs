/// <summary>
/// Struct <c>Wallet</c> represents a collection of coin(s)
/// </summary>
public struct Wallet {
    /// <value>Property <c>CP</c> stands for <c>Copper Pieces</c></value>
    public int CP { get; private set; }
    /// <value>Property <c>SP</c> stands for <c>Silver Piecess</c></value>
    public int SP { get; private set; }
    /// <value>Property <c>EP</c> stands for <c>Electrum Pieces</c></value>
    public int EP { get; private set; }
    /// <value>Property <c>GP</c> stands for <c>Gold Pieces</c></value>
    public int GP { get; private set; }
    /// <value>Property <c>PP</c> stands for <c>Platinum Pieces</c></value>
    public int PP { get; private set; }
    /// <value>Property <c>Weight</c> is the total weight of the wallet in pounds (lbs)</value>
    public float Weight {
        get {
            return (CP + SP + EP + GP + PP) * 0.02f;
        }
    }

    public Wallet() {
        CP = 0;
        SP = 0;
        EP = 0;
        GP = 0;
        PP = 0;
    }

    public Wallet(int pp, int gp, int ep, int sp, int cp) {
        CP = cp;
        SP = sp;
        EP = ep;
        GP = gp;
        PP = pp;
    }

    /// <summary>
    /// Method <c>Add</c> adds the contents of another Wallet Object into this Wallet
    /// </summary>
    /// <param name="val">the wallet containing the value change</param>
    public void Add(Wallet val) {
        this.PP += val.PP;
        this.GP += val.GP;
        this.EP += val.EP;
        this.SP += val.SP;
        this.CP += val.CP;
    }

    /// <summary>
    /// Method <c>Subtract</c> subtracts the contents of another Wallet Object from this Wallet
    /// </summary>
    /// <param name="val">the wallet containing the values subtracted</param>
    public void Subtract(Wallet val) {
        this.PP -= val.PP;
        this.GP -= val.GP;
        this.EP -= val.EP;
        this.SP -= val.SP;
        this.CP -= val.CP;
    }

    /// <summary>
    /// Method <c>Pay</c> subtracts another Wallet Object from this Wallet but prevents negatives
    /// </summary>
    /// <param name="cost">the wallet containing the payment value</param>
    /// <returns>
    /// A boolean <c>True</c> if the operation succeeded. The value will be <c>False</c> if it failed. The wallet of the parameter <c>cost</c> will not be applied to this object.
    /// </returns>
    public bool Pay(Wallet cost) {
        Wallet tWall = new Wallet();
        tWall.Add(this);
        tWall.Subtract(cost);
        if (tWall.PP >= 0 && tWall.GP >= 0 && tWall.EP >= 0 && tWall.SP >= 0 && tWall.CP >= 0) {
            this.Subtract(cost);
            return true;
        }
        else {
            return false;
        }
    }
}

/// <summary>
/// The item categories where Generic is for common items. Other catgories describe the kind of item.
/// </summary>
public enum ItemCategory {
    Generic,
    Magic,
    Consumable,
    Armour,
    Weapon
}

/// <summary>
/// Class <c>CustomAttribute</c> represents a custom attribute to be added to an Item object
/// <summary/>
public class CustomAttribute {
    public string Description { get; set; } = string.Empty;
    public string DiceNotion { get; set; } = string.Empty;
    public int? MaxCharges { get; set; }
    public int? ChargesRemaining { get; set; }
    public int? Duration { get; set; }
}

/// <summary>
/// Class <c>Item</c> is a generic item
/// </summary>
public abstract class Item {
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ItemCategory Category { get; set; }
    public Wallet Value { get; set; }
    public int Weight { get; set; }
    public bool AttunementRequired { get; set; }
    public List<CustomAttribute> CustomAttributes { get; set; } = new List<CustomAttribute>();
}

/// <summary>
/// Class <c>MeleeWeapon</c> represents a Melee item such as a sword
/// </summary>
public class MeleeWeapon : Item {
    public string DamageDice { get; set; } = string.Empty;
    public string DamageType { get; set; } = string.Empty;
    public string Properties { get; set; } = string.Empty;
    public Distance Reach { get; set; } = Distance.Parse("5u");
    public int? Bonus { get; set; }
}

/// <summary>
/// Class <c>RangedWeapon</c> represents a Ranged weapon such as a bow
/// </summary>
public class RangedWeapon : Item {
    public string DamageDice { get; set; } = string.Empty;
    public string DamageType { get; set; } = string.Empty;
    public string Properties { get; set; } = string.Empty;
    public Distance Range { get; set; } = Distance.Parse("5u");
    public Distance MaxRange { get; set; } = Distance.Parse("10u");
    public int? Bonus { get; set; }
}

/// <summary>
/// Class <c>MagicItem</c> represents an item which grants a certain magical effect described by the <c>Effect</c> field
/// </summary>
public class MagicItem : Item {
    public int? ChargesRemaining { get; set; }
    public int? MaxCharges { get; set; }
    public string Effect { get; set; } = string.Empty;
}
