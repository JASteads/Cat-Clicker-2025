using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    const int SPAMMABLE_COUNT = 6;
    const int SPECIALIST_COUNT = 1;

    [SerializeField] public BaseData baseData;
    [SerializeField] public SpammableSaveData[] spammablesData;
    [SerializeField] public SpecialistSaveData[] specialistData;
    [SerializeField] public HashSet<string> upgradesPool;

    public GameData()
    {
        baseData = new BaseData();
        spammablesData = new SpammableSaveData[SPAMMABLE_COUNT];
        specialistData = new SpecialistSaveData[SPECIALIST_COUNT];
        upgradesPool = new HashSet<string>();
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

[Serializable]
public struct BaseData
{
    public double currentBits,
                  spamBPS,
                  clickPower,
                  clickPowerMulti,
                  bpsToClickPowerMulti,
                  bpsToClickPowerPercent;

    public BaseData(double currentBits = 0, double spamBPS = 0,
        double clickPower = 1, double clickPowerMulti = 1,
        double bpsToClickPowerMulti = 0, double bpsToClickPowerPercent = 0)
    {
        this.currentBits = currentBits;
        this.spamBPS = spamBPS;
        this.clickPower = clickPower;
        this.clickPowerMulti = clickPowerMulti;
        this.bpsToClickPowerMulti = bpsToClickPowerMulti;
        this.bpsToClickPowerPercent = bpsToClickPowerPercent;
    }
}

[Serializable]
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

[Serializable]
public struct SpecialistSaveData
{
    public int level;
    public double bpsMulti;
    public bool isOwned;

    public SpecialistSaveData(
        int level = 0, double bpsMulti = 1, bool isOwned = false)
    {
        this.level = level;
        this.bpsMulti = bpsMulti;
        this.isOwned = isOwned;
    }
}