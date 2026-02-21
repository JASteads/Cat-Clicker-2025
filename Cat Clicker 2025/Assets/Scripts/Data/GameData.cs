using UnityEngine;

[System.Serializable]
public class GameData
{
    const int SPAMMABLE_COUNT = 6;

    [SerializeField] public BaseData baseData;
    [SerializeField] public SpammableSaveData[] spammablesData;
    [SerializeField] public bool[] upgradesPool;

    public GameData()
    {
        baseData = new BaseData(0, 0, 1);
        spammablesData = new SpammableSaveData[SPAMMABLE_COUNT];
        upgradesPool = new bool[1] { false }; // Just one for testing
    }

    public double GetTotalBPS()
    {
        double sum = 0;
        sum += baseData.spamBPS;

        return sum;
    }

    public override string ToString()
    {
        return $"Current Bits: {baseData.currentBits.ToString()}" +
            $"\nBPS: {baseData.spamBPS.ToString()}";
    }
}

[System.Serializable]
public struct BaseData
{
    public double currentBits,
                  spamBPS,
                  clickPower;

    public BaseData(double currentBits, double spamBPS, double clickPower)
    {
        this.currentBits = currentBits;
        this.spamBPS = spamBPS;
        this.clickPower = clickPower;
    }
}

[System.Serializable]
public struct SpammableSaveData
{
    public int owned;
    public double multiplier;

    public SpammableSaveData(int owned = 0, double multiplier = 1)
    {
        this.owned = owned;
        this.multiplier = multiplier;
    }
}