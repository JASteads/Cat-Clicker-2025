using System.Collections.Generic;
using UnityEngine;

public class CurrencySystem
{
    public double spammableBPS;
    public List<SpammableData> spammables;

    public CurrencySystem()
    {
        EventBus.OnGameTick += Tick;
        EventBus.OnSpammablePurchase += OnPurchasedSpammable;
        EventBus.OnSpamBPSChange += RecalculateBPS;
    }

    public void AddBits(double amount)
    {
        GameDataManager.gameData.baseData.currentBits += amount;
    }

    public void Tick()
    {
        // Add spammable BPS
        double spamBPSRate = 
            GameDataManager.gameData.baseData.spamBPS * Time.deltaTime;
        AddBits(spamBPSRate);
        EventBus.GoBitsAdded(spamBPSRate);
    }

    public void OnPurchasedSpammable(SpammableData s)
    {
        RecalculateBPS();
    }

    public void StopSystem()
    {
        EventBus.OnGameTick -= Tick;
        EventBus.OnSpammablePurchase -= OnPurchasedSpammable;
        EventBus.OnSpamBPSChange -= RecalculateBPS;
    }

    void RecalculateBPS()
    {
        Debug.Log("Recalculating BPS ...");
        RecalculateSpamBPS();
    }

    void RecalculateSpamBPS()
    {
        double spamBPS = 0;
        SpammableSaveData[] data = GameDataManager.gameData.spammablesData;

        for (int i = 0; i < spammables.Count; i++)
        {
            spamBPS += spammables[i].GetRawBPS(data[i].owned) * data[i].multiplier;
        }

        Debug.Log($"New Raw BPS: {spamBPS}");

        // Apply basic specialist logic
        SpecialistSaveData[] specialists = GameDataManager.gameData.specialistData;
        if (specialists[0].isOwned)
        {
            spamBPS *= GameDataManager.gameData.specialistData[0].bpsMulti;
        }

        Debug.Log($"BPS w/ Specialist Multiplier: {spamBPS}");

        GameDataManager.gameData.baseData.spamBPS = spamBPS;
    }
}
