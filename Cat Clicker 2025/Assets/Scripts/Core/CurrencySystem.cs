using System.Collections.Generic;
using UnityEngine;

public class CurrencySystem
{
    public double spammableBPS;

    public CurrencySystem()
    {
        EventBus.OnGameTick += Tick;
        EventBus.OnSpammablePurchase += OnPurchasedSpammable;
    }

    public void AddBits(double amount)
    {
        GameDataManager.gameData.baseData.currentBits += amount;
    }

    public void ChangeBPS(double newBPS)
    {
        GameDataManager.gameData.baseData.spamBPS = newBPS;
    }

    public void Tick()
    {
        // Add spammable BPS
        double spamBPSRate = 
            GameDataManager.gameData.baseData.spamBPS * Time.deltaTime;
        AddBits(spamBPSRate);
        EventBus.GoBitsAdded(spamBPSRate);
    }

    public void OnPurchasedSpammable(List<SpammableData> spammables)
    {
        RecalculateSpamBPS(spammables);
    }

    public void StopSystem()
    {
        EventBus.OnGameTick -= Tick;
        EventBus.OnSpammablePurchase -= OnPurchasedSpammable;
    }

    void RecalculateSpamBPS(List<SpammableData> spammables)
    {
        double spamBPS = 0;
        SpammableSaveData[] data = GameDataManager.gameData.spammablesData;

        for (int i = 0; i < spammables.Count; i++)
        {
            spamBPS += spammables[i].GetRawBPS(data[i].owned) * data[i].multiplier;
        }

        GameDataManager.gameData.baseData.spamBPS = spamBPS;
        EventBus.GoSpamBPSChange(spamBPS);
    }
}
