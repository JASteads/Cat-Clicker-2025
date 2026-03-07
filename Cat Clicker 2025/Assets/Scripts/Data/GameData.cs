using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    const int SPAMMABLE_COUNT = 6;
    const int SPECIALIST_COUNT = 1;
    const int ACHIEVEMENT_CATEGORIES = 4;

    [SerializeField] public BaseData baseData;
    [SerializeField] public SpammableSaveData[] spammablesData;
    [SerializeField] public SpecialistSaveData[] specialistData;
    [SerializeField] public FeverSaveData feverData;
    [SerializeField] public int[] skillpoints;
    [SerializeField] public List<string> upgradesPool;
    [SerializeField] public List<string> achivementsPool;

    public GameData()
    {
        baseData = new BaseData
        {
            currentBits = 0,
            totalBits = 0,
            spamBPS = 0,
            clickPower = 1,
            clickPowerMulti = 1,
            bpsToClickPowerMulti = 0
        };

        spammablesData = new SpammableSaveData[SPAMMABLE_COUNT];
        for (int i = 0; i < SPAMMABLE_COUNT; i++)
        {
            spammablesData[i] = new SpammableSaveData
            {
                owned = 0,
                multiplier = 1
            };
        }

        specialistData = new SpecialistSaveData[SPECIALIST_COUNT];
        for (int i = 0; i < SPECIALIST_COUNT; i++)
        {
            specialistData[i] = new SpecialistSaveData
            {
                level = 0,
                bpsMulti = 1,
                isOwned = false
            };
        }

        feverData = new FeverSaveData
        {
            maxCharge = 100,
            drainAmount = 75,
            holdTime = 3
        };

        skillpoints = new int[ACHIEVEMENT_CATEGORIES];
        upgradesPool = new List<string>();
        achivementsPool = new List<string>();
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
            $"\nBPS: {baseData.spamBPS.ToString()}" +
            $"\nClick Power: {baseData.clickPower.ToString()}";
    }
}

[Serializable]
public struct BaseData
{
    public double currentBits,
                  totalBits,
                  spamBPS,
                  clickPower,
                  clickPowerMulti,
                  bpsToClickPowerMulti;
}

[Serializable]
public struct SpammableSaveData
{
    public int owned;
    public double multiplier;
}

[Serializable]
public struct SpecialistSaveData
{
    public int level;
    public double bpsMulti;
    public bool isOwned;

}

[Serializable]
public struct AchievementSkillpoints
{
    public int innovationPoints;
    public int infinityPoints;
    public int asterismPoints;
    public int anomalyPoints;
}