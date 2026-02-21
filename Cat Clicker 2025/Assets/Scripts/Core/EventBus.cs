using System;
using System.Collections.Generic;

public static class EventBus
{
    public static event Action OnBigButtonClick;
    public static event Action<double> OnBitsAdded;
    public static event Action OnGameTick;
    public static event Action<double> OnSpamBPSChange;
    public static event Action<List<SpammableData>> OnSpammablePurchase;
    public static event Action<string> OnAchievement;

    public static void GoBigButtonClick()
    {
        OnBigButtonClick?.Invoke();
    }

    public static void GoBitsAdded(double amount)
    {
        OnBitsAdded?.Invoke(amount); 
    }
    
    public static void GoGameTick()
    {
        OnGameTick?.Invoke();
    }

    public static void GoSpammablePurchase(List<SpammableData> spammableData)
    {
        OnSpammablePurchase?.Invoke(spammableData);
    }

    public static void GoAchievement(string name)
    {
        OnAchievement?.Invoke(name);
    }

    public static void GoSpamBPSChange(double newBPS)
    {
        OnSpamBPSChange?.Invoke(newBPS);
    }
}
