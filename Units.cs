
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
