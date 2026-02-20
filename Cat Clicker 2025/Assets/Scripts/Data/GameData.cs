using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public BaseData baseData;
    public List<SpammableData> unitsData;
    public List<SpammableData> buildingsData;

    public GameData()
    {
        baseData = new BaseData(0, 0, 1);
        unitsData = new List<SpammableData>();
        buildingsData = new List<SpammableData>();
    }

    public override string ToString()
    {
        return $"Current Bits: {baseData.currentBits.ToString()}" +
            $"\nBPS: {baseData.bps.ToString()}";
    }
}

[System.Serializable]
public struct BaseData
{
    public double currentBits,
                  bps,
                  clickPower;

    public BaseData(double currentBits, double bps, double clickPower)
    {
        this.currentBits = currentBits;
        this.bps = bps;
        this.clickPower = clickPower;
    }
}